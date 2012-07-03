// Accord Machine Learning Library
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009-2012
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.MachineLearning.DecisionTrees.Learning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Accord.Math;
    using System.Threading.Tasks;
    using AForge;
    using Parallel = System.Threading.Tasks.Parallel;

    /// <summary>
    ///   C4.5 Learning algorithm for <see cref="DecisionTree">Decision Trees</see>.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description>
    ///       Quinlan, J. R. C4.5: Programs for Machine Learning. Morgan
    ///       Kaufmann Publishers, 1993.</description></item>
    ///     <item><description>
    ///       Quinlan, J. R. C4.5: Programs for Machine Learning. Morgan
    ///       Kaufmann Publishers, 1993.</description></item>
    ///     <item><description>
    ///       Quinlan, J. R. Improved use of continuous attributes in c4.5. Journal
    ///       of Artificial Intelligence Research, 4:77-90, 1996.</description></item>
    ///     <item><description>
    ///       Mitchell, T. M. Machine Learning. McGraw-Hill, 1997. pp. 55-58. </description></item>
    ///     <item><description><a href="http://en.wikipedia.org/wiki/ID3_algorithm">
    ///       Wikipedia, the free enclyclopedia. ID3 algorithm. Available on 
    ///       http://en.wikipedia.org/wiki/ID3_algorithm </a></description></item>
    ///   </list>
    /// </para>   
    /// </remarks>
    ///
    /// <see cref="ID3Learning"/>
    ///
    [Serializable]
    public class C45Learning 
    {


        private DecisionTree tree;

        private double[][] thresholds;
        private IntRange[] inputRanges;
        private int outputClasses;

        private bool[] attributes;


        /// <summary>
        ///   Creates a new C4.5 learning algorithm.
        /// </summary>
        /// 
        /// <param name="tree">The decision tree to be generated.</param>
        /// 
        public C45Learning(DecisionTree tree)
        {
            this.tree = tree;
            this.attributes = new bool[tree.InputCount];
            this.inputRanges = new IntRange[tree.InputCount];
            this.outputClasses = tree.OutputClasses;

            for (int i = 0; i < inputRanges.Length; i++)
                inputRanges[i] = tree.Attributes[i].Range.ToIntRange(false);

        }

        /// <summary>
        ///   Runs the learning algorithm, creating a decision
        ///   tree modeling the given inputs and outputs.
        /// </summary>
        /// 
        /// <param name="inputs">The inputs.</param>
        /// <param name="outputs">The corresponding outputs.</param>
        /// 
        /// <returns>The error of the generated tree.</returns>
        /// 
        public double Run(double[][] inputs, int[] outputs)
        {
            for (int i = 0; i < attributes.Length; i++)
                attributes[i] = false;

            thresholds = new double[tree.Attributes.Count][];

            List<double> candidates = new List<double>(inputs.Length);

            // 0. Create candidate split thresholds for each attribute
            for (int i = 0; i < tree.Attributes.Count; i++)
            {
                if (tree.Attributes[i].Nature == DecisionAttributeKind.Continuous)
                {
                    double[] v = inputs.GetColumn(i);
                    int[] o = (int[])outputs.Clone();

                    Array.Sort(v, o);

                    for (int j = 0; j < v.Length - 1; j++)
                    {
                        // Add as candidate thresholds only adjacent values v[i] and v[i+1]
                        // belonging to different classes, following the results by Fayyad
                        // and Irani (1992). See footnote on Quinlan (1996).

                        if (o[j] != o[j + 1])
                            candidates.Add((v[j] + v[j + 1]) / 2.0);
                    }


                    thresholds[i] = candidates.ToArray();
                    candidates.Clear();
                }
            }


            // 1. Create a root node for the tree
            tree.Root = new DecisionNode(tree);

            split(tree.Root, inputs, outputs);

            return ComputeError(inputs, outputs);
        }

        /// <summary>
        ///   Computes the prediction error for the tree
        ///   over a given set of input and outputs.
        /// </summary>
        /// 
        /// <param name="inputs">The input points.</param>
        /// <param name="outputs">The corresponding output labels.</param>
        /// 
        /// <returns>The percentual error of the prediction.</returns>
        /// 
        public double ComputeError(double[][] inputs, int[] outputs)
        {
            int miss = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (tree.Compute(inputs[i]) != outputs[i])
                    miss++;
            }

            return (double)miss / inputs.Length;
        }

        private void split(DecisionNode root, double[][] input, int[] output)
        {

            // 2. If all examples are for the same class, return the single-node
            //    tree with the output label corresponding to this common class.
            double entropy = Statistics.Tools.Entropy(output, outputClasses);

            if (entropy == 0)
            {
                if (output.Length > 0)
                    root.Output = output[0];
                return;
            }

            // 3. If number of predicting attributes is empty, then return the single-node
            //    tree with the output label corresponding to the most common value of
            //    the target attributes in the examples.
            int predictors = attributes.Count(x => x == false);

            if (predictors == 0)
            {
                root.Output = Statistics.Tools.Mode(output);
                return;
            }


            // 4. Otherwise, try to select the attribute which
            //    best explains the data sample subset.

            double[] scores = new double[predictors];
            double[] entropies = new double[predictors];
            double[] thresholds = new double[predictors];
            int[][][] partitions = new int[predictors][][];

            // Retrieve candidate attribute indices
            int[] candidates = new int[predictors];
            for (int i = 0, k = 0; i < attributes.Length; i++)
                if (!attributes[i]) candidates[k++] = i;


            // For each attribute in the data set
            Parallel.For(0, scores.Length, i =>
            {
                scores[i] = computeGainRatio(input, output, candidates[i],
                    entropy, out partitions[i], out thresholds[i]);
            });

            // Select the attribute with maximum gain ratio
            int maxGainIndex; scores.Max(out maxGainIndex);
            var maxGainPartition = partitions[maxGainIndex];
            var maxGainEntropy = entropies[maxGainIndex];
            var maxGainAttribute = candidates[maxGainIndex];
            var maxGainRange = inputRanges[maxGainAttribute];
            var maxGainThreshold = thresholds[maxGainIndex];

            // Mark this attribute as already used
            attributes[maxGainAttribute] = true;

            double[][] inputSubset;
            int[] outputSubset;

            // Now, create next nodes and pass those partitions as their responsabilities.
            if (tree.Attributes[maxGainAttribute].Nature == DecisionAttributeKind.Discrete)
            {
                DecisionNode[] children = new DecisionNode[maxGainPartition.Length];

                for (int i = 0; i < children.Length; i++)
                {
                    children[i] = new DecisionNode(tree)
                    {
                        Parent = root,
                        Value = i + maxGainRange.Min,
                        Comparison = ComparisonKind.Equal,
                    };

                    inputSubset = input.Submatrix(maxGainPartition[i]);
                    outputSubset = output.Submatrix(maxGainPartition[i]);

                    split(children[i], inputSubset, outputSubset); // recursion
                }

                root.Branches = new DecisionBranchNodeCollection(maxGainAttribute, children);
            }
            else
            {
                DecisionNode[] children = 
                {
                    new DecisionNode(tree) 
                    {
                        Parent = root, Value = maxGainThreshold,
                        Comparison = ComparisonKind.LessThanOrEqual 
                    },

                    new DecisionNode(tree)
                    {
                        Parent = root, Value = maxGainThreshold,
                        Comparison = ComparisonKind.GreaterThan
                    }
                };

                inputSubset = input.Submatrix(maxGainPartition[0]);
                outputSubset = output.Submatrix(maxGainPartition[0]);
                split(children[0], inputSubset, outputSubset);

                inputSubset = input.Submatrix(maxGainPartition[1]);
                outputSubset = output.Submatrix(maxGainPartition[1]);
                split(children[1], inputSubset, outputSubset);

                root.Branches = new DecisionBranchNodeCollection(maxGainAttribute, children);
            }

            attributes[maxGainAttribute] = false;
        }


        private double computeGainRatio(double[][] input, int[] output, int attributeIndex,
            double entropy, out int[][] partitions, out double threshold)
        {
            double infoGain = computeInfoGain(input, output, attributeIndex, entropy, out partitions, out threshold);
            double splitInfo = Measures.SplitInformation(output.Length, partitions);

            return infoGain == 0 ? 0 : infoGain / splitInfo;
        }

        private double computeInfoGain(double[][] input, int[] output, int attributeIndex,
            double entropy, out int[][] partitions, out double threshold)
        {
            threshold = 0;

            if (tree.Attributes[attributeIndex].Nature == DecisionAttributeKind.Discrete)
                return entropy - computeInfo(input, output, attributeIndex, out partitions);

            return entropy - computeInfo(input, output, attributeIndex, out partitions, out threshold);
        }

        private double computeInfo(double[][] input, int[] output,
            int attributeIndex, out int[][] partitions)
        {
            // Compute the information gain obtained by using
            // this current attribute as the next decision node.
            double info = 0;

            IntRange valueRange = inputRanges[attributeIndex];
            partitions = new int[valueRange.Length + 1][];

            // For each possible value of the attribute
            for (int i = 0; i < partitions.Length; i++)
            {
                int value = valueRange.Min + i;

                // Partition the remaining data set
                // according to the attribute values
                partitions[i] = input.Find(x => x[attributeIndex] == value);

                // For each of the instances under responsability
                // of this node, check which have the same value
                int[] outputSubset = output.Submatrix(partitions[i]);

                // Check the entropy gain originating from this partitioning
                double e = Statistics.Tools.Entropy(outputSubset, outputClasses);

                info += ((double)outputSubset.Length / output.Length) * e;
            }

            return info;
        }

        private double computeInfo(double[][] input, int[] output,
            int attributeIndex, out int[][] partitions, out double threshold)
        {
            // Compute the information gain obtained by using
            // this current attribute as the next decision node.
            double[] t = thresholds[attributeIndex];

            double bestGain = Double.NegativeInfinity;
            double bestThreshold = t[0];
            partitions = null;

            // For each possible splitting point of the attribute
            for (int i = 0; i < t.Length; i++)
            {
                // Partition the remaining data set
                // according to the threshold value
                double value = t[i];

                int[] idx1 = input.Find(x => x[attributeIndex] <= value);
                int[] idx2 = input.Find(x => x[attributeIndex] > value);

                int[] output1 = output.Submatrix(idx1);
                int[] output2 = output.Submatrix(idx2);

                double p1 = (double)idx1.Length / output.Length;
                double p2 = (double)idx2.Length / output.Length;

                double splitGain =
                    -p1 * Statistics.Tools.Entropy(output1, outputClasses) +
                    -p2 * Statistics.Tools.Entropy(output2, outputClasses);

                if (splitGain > bestGain)
                {
                    bestThreshold = value;
                    bestGain = splitGain;

                    if (idx1.Length > 0 && idx2.Length > 0)
                        partitions = new int[][] { idx1, idx2 };
                    else if (idx1.Length > 0)
                        partitions = new int[][] { idx1 };
                    else if (idx2.Length > 0)
                        partitions = new int[][] { idx2 };
                    else
                        partitions = new int[][] { };
                }
            }

            threshold = bestThreshold;
            return bestGain;
        }

    }
}
