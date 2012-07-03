// Accord Statistics Library
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

namespace Accord.Statistics.Models.Fields.Functions
{
    using System;
    using System.Collections.Generic;
    using Accord.Statistics.Distributions.Multivariate;
    using Accord.Statistics.Distributions.Univariate;
    using Accord.Statistics.Models.Fields.Features;
    using Accord.Statistics.Models.Markov;

    /// <summary>
    ///   Potential function modeling Hidden Markov Models.
    /// </summary>
    /// 
    [Serializable]
    public sealed class MultivariateNormalMarkovClassifierFunction
        : BasePotentialFunction<double[]>, IPotentialFunction<double[]>
    {

        /// <summary>
        ///   Gets the total number of dimensions for 
        ///   this multivariate potential function.
        /// </summary>
        /// 
        public int Dimensions { get; private set; }

        /// <summary>
        ///   Constructs a new potential function modeling Hidden Markov Models.
        /// </summary>
        /// 
        /// <param name="classifier">A hidden Markov sequence classifier.</param>
        /// <param name="includeClassFeatures">True to include class features (priors), false otherwise.</param>
        /// 
        public MultivariateNormalMarkovClassifierFunction(
            HiddenMarkovClassifier<MultivariateNormalDistribution> classifier, bool includeClassFeatures = true)
        {
            this.Outputs = classifier.Classes;
            this.Dimensions = classifier.Models[0].Dimension;

            int factorIndex = 0;
            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<double[]>>();

            this.Factors = new FactorPotential<double[]>[Outputs];


            // Create features for initial class probabilities
            for (int c = 0; c < classifier.Classes; c++)
            {
                var stateParams = new List<double>();
                var stateFeatures = new List<IFeature<double[]>>();

                var edgeParams = new List<double>();
                var edgeFeatures = new List<IFeature<double[]>>();

                var classParams = new List<double>();
                var classFeatures = new List<IFeature<double[]>>();

                var model = classifier[c];

                if (includeClassFeatures)
                {
                    // Create features for class labels
                    classParams.Add(Math.Log(classifier.Priors[c]));
                    classFeatures.Add(new OutputFeature<double[]>(this, c, c));
                }

                // Create features for initial state probabilities
                for (int i = 0; i < model.States; i++)
                {
                    edgeParams.Add(model.Probabilities[i]);
                    edgeFeatures.Add(new InitialFeature<double[]>(this, c, i));
                }

                // Create features for state transition probabilities
                for (int i = 0; i < model.States; i++)
                {
                    for (int j = 0; j < model.States; j++)
                    {
                        edgeParams.Add(model.Transitions[i, j]);
                        edgeFeatures.Add(new TransitionFeature<double[]>(this, c, i, j));
                    }
                }

                // Create features emission probabilities
                for (int i = 0; i < model.States; i++)
                {
                    for (int d = 0; d < model.Dimension; d++)
                    {
                        double mean = model.Emissions[i].Mean[d];
                        double var = model.Emissions[i].Variance[d];

                        // Occupancy
                        stateParams.Add(-0.5 * (Math.Log(2.0 * Math.PI * var) + (mean * mean) / var));
                        stateFeatures.Add(new OccupancyFeature<double[]>(this, c, i));

                        // 1st Moment (x)
                        stateParams.Add(mean / var);
                        stateFeatures.Add(new MultivariateFirstMomentFeature(this, c, i, d));

                        // 2nd Moment (x²)
                        stateParams.Add(-1.0 / (2.0 * var));
                        stateFeatures.Add(new MultivariateSecondMomentFeature(this, c, i, d));
                    }
                }

                int startClassIndex = factorIndex;
                int startEdgeIndex = factorIndex + classParams.Count;
                int startStateIndex = factorIndex + classParams.Count + edgeParams.Count;

                // First features and params are always belonging to classes
                Factors[c] = new MultivariateNormalMarkovModelFactor(this, model.States, c, Dimensions,
                    startClassIndex, classParams.Count,  // 1. classes
                    startEdgeIndex, edgeParams.Count,    // 2. edges
                    startStateIndex, stateParams.Count); // 3. states

                // 1. classes
                factorFeatures.AddRange(classFeatures);
                factorParams.AddRange(classParams);

                // 2. edges
                factorFeatures.AddRange(edgeFeatures);
                factorParams.AddRange(edgeParams);

                // 3. states
                factorFeatures.AddRange(stateFeatures);
                factorParams.AddRange(stateParams);

                factorIndex += classParams.Count + stateParams.Count + edgeParams.Count;
            }

            System.Diagnostics.Debug.Assert(factorIndex == factorParams.Count);
            System.Diagnostics.Debug.Assert(factorIndex == factorFeatures.Count);

            this.Weights = factorParams.ToArray();
            this.Features = factorFeatures.ToArray();
        }

        /// <summary>
        ///   Constructs a new potential function modeling Hidden Markov Models.
        /// </summary>
        /// 
        /// <param name="classifier">A hidden Markov sequence classifier.</param>
        /// 
        public MultivariateNormalMarkovClassifierFunction(
            HiddenMarkovClassifier<Independent<NormalDistribution>> classifier)
        {
            this.Outputs = classifier.Classes;

            int factorIndex = 0;
            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<double[]>>();

            this.Factors = new FactorPotential<double[]>[Outputs];

            // Create features for initial class probabilities
            for (int c = 0; c < classifier.Classes; c++)
            {
                var stateParams = new List<double>();
                var stateFeatures = new List<IFeature<double[]>>();

                var edgeParams = new List<double>();
                var edgeFeatures = new List<IFeature<double[]>>();

                var classParams = new List<double>();
                var classFeatures = new List<IFeature<double[]>>();

                var model = classifier[c];

                // Create features for class labels
                classParams.Add(Math.Log(classifier.Priors[c]));
                classFeatures.Add(new OutputFeature<double[]>(this, c, c));

                // Create features for initial state probabilities
                for (int i = 0; i < model.States; i++)
                {
                    edgeParams.Add(model.Probabilities[i]);
                    edgeFeatures.Add(new InitialFeature<double[]>(this, c, i));
                }

                // Create features for initial state probabilities
                for (int i = 0; i < model.States; i++)
                {
                    stateParams.Add(model.Probabilities[i]);
                    stateFeatures.Add(new InitialFeature<double[]>(this, c, i));
                }

                // Create features for state transition probabilities
                for (int i = 0; i < model.States; i++)
                {
                    for (int j = 0; j < model.States; j++)
                    {
                        edgeParams.Add(model.Transitions[i, j]);
                        edgeFeatures.Add(new TransitionFeature<double[]>(this, c, i, j));
                    }
                }

                // Create features emission probabilities
                for (int i = 0; i < model.States; i++)
                {
                    for (int d = 0; d < model.Emissions[i].Mean.Length; d++)
                    {
                        double mean = model.Emissions[i].Mean[d];
                        double var = model.Emissions[i].Variance[d];

                        // Occupancy
                        stateParams.Add(-0.5 * (Math.Log(2.0 * Math.PI * var) + (mean * mean) / var));
                        stateFeatures.Add(new OccupancyFeature<double[]>(this, c, i));

                        // 1st Moment (x)
                        stateParams.Add(mean / var);
                        stateFeatures.Add(new MultivariateFirstMomentFeature(this, c, i, d));

                        // 2nd Moment (x²)
                        stateParams.Add(-1.0 / (2.0 * var));
                        stateFeatures.Add(new MultivariateSecondMomentFeature(this, c, i, d));
                    }
                }

                int startClassIndex = factorIndex;
                int startEdgeIndex = factorIndex + classParams.Count;
                int startStateIndex = factorIndex + classParams.Count + edgeParams.Count;

                // First features and params are always belonging to classes
                Factors[c] = new MultivariateNormalMarkovModelFactor(this, model.States, c, Dimensions,
                    startClassIndex, classParams.Count,  // 1. classes
                    startEdgeIndex, edgeParams.Count,    // 2. edges
                    startStateIndex, stateParams.Count); // 3. states

                // 1. classes
                factorFeatures.AddRange(classFeatures);
                factorParams.AddRange(classParams);

                // 2. edges
                factorFeatures.AddRange(edgeFeatures);
                factorParams.AddRange(edgeParams);

                // 3. states
                factorFeatures.AddRange(stateFeatures);
                factorParams.AddRange(stateParams);

                factorIndex += classParams.Count + stateParams.Count + edgeParams.Count;
            }

            System.Diagnostics.Debug.Assert(factorIndex == factorParams.Count);
            System.Diagnostics.Debug.Assert(factorIndex == factorFeatures.Count);

            this.Weights = factorParams.ToArray();
            this.Features = factorFeatures.ToArray();
        }

        #region ICloneable Members

        private MultivariateNormalMarkovClassifierFunction() { }

        /// <summary>
        ///   Creates a new object that is a copy of the current instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A new object that is a copy of this instance.
        /// </returns>
        /// 
        public object Clone()
        {
            var clone = new MultivariateNormalMarkovClassifierFunction();

            clone.Factors = new FactorPotential<double[]>[Factors.Length];
            for (int i = 0; i < Factors.Length; i++)
                clone.Factors[i] = Factors[i].Clone(newOwner: clone);

            clone.Features = new IFeature<double[]>[Features.Length];
            for (int i = 0; i < Features.Length; i++)
                clone.Features[i] = Features[i].Clone(newOwner: clone);

            clone.Outputs = Outputs;
            clone.Weights = (double[])Weights.Clone();

            clone.Dimensions = Dimensions;

            return clone;
        }

        #endregion
    }
}
