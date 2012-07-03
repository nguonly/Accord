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

namespace Accord.MachineLearning.DecisionTrees
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    ///   Decision tree.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   Represent a decision tree which can be compiled to
    ///   code at run-time. For sample usage and example of
    ///   learning, please see the <see cref="Learning.ID3Learning">
    ///   ID3 learning algorithm for decision trees</see>.</para>
    /// </remarks>
    ///
    /// <seealso cref="Learning.ID3Learning"/>
    /// <seealso cref="Learning.C45Learning"/>
    ///
    [Serializable]
    public class DecisionTree
    {
        /// <summary>
        ///   Gets or sets the root node for this tree.
        /// </summary>
        /// 
        public DecisionNode Root { get; set; }

        /// <summary>
        ///   Gets the collection of attributes processed by this tree.
        /// </summary>
        /// 
        public DecisionAttributeCollection Attributes { get; private set; }

        /// <summary>
        ///   Gets the number of distinct output
        ///   classes classified by this tree.
        /// </summary>
        /// 
        public int OutputClasses { get; private set; }

        /// <summary>
        ///   Gets the number of input attributes
        ///   expected by this tree.
        /// </summary>
        public int InputCount { get; private set; }

        /// <summary>
        ///   Creates a new <see cref="DecisionTree"/> to process
        ///   the given <paramref name="attributes"/> and the given
        ///   number of possible <paramref name="outputClasses"/>.
        /// </summary>
        /// 
        /// <param name="attributes">An array specifying the attributes to be processed by this tree.</param>
        /// <param name="outputClasses">The number of possible output classes for the given atributes.</param>
        /// 
        public DecisionTree(DecisionVariable[] attributes, int outputClasses)
        {
            if (outputClasses <= 0)
                throw new ArgumentOutOfRangeException("outputClasses");
            if (attributes == null)
                throw new ArgumentNullException("attributes");

            this.Attributes = new DecisionAttributeCollection(attributes);
            this.InputCount = attributes.Length;
            this.OutputClasses = outputClasses;
        }


        /// <summary>
        ///   Computes the decision for a given input.
        /// </summary>
        /// 
        /// <param name="input">The input data.</param>
        /// 
        /// <returns>A predicted class for the given input.</returns>
        /// 
        public int Compute(double[] input)
        {
            if (Root == null)
                throw new InvalidOperationException();

            DecisionNode current = Root;

            // Start reasoning
            while (current != null)
            {
                // Check if this is a leaf
                if (current.IsLeaf)
                {
                    // This is a leaf node. The decision
                    // proccess thus should stop here.

                    return current.Output.Value;
                }

                // This node is not a leaf. Continue the
                // decisioning proccess following the childs

                // Get the next attribute to guide reasoning
                int attribute = current.Branches.AttributeIndex;

                // Check which child is responsible for dealing
                // which the particular value of the attribute
                DecisionNode nextNode = null;

                foreach (DecisionNode branch in current.Branches)
                {
                    if (branch.Compute(input[attribute]))
                    {
                        // This is the child node responsible for dealing
                        // which this particular attribute value. Choose it
                        // to continue reasoning.

                        nextNode = branch; break;
                    }
                }

                current = nextNode;
            }

            // Normal execution should not reach here.
            throw new InvalidOperationException("The tree is degenerated.");
        }


        /// <summary>
        ///   Creates an <see cref="Expression">Expression Tree</see> representation
        ///   of this decision tree, which can in turn be compiled into code.
        /// </summary>
        /// 
        /// <returns>A tree in the form of an expression tree.</returns>
        /// 
        public Expression<Func<double[], int>> ToExpression()
        {
            DecisionTreeExpressionCreator compiler = new DecisionTreeExpressionCreator(this);
            return compiler.Create();
        }
    }


}
