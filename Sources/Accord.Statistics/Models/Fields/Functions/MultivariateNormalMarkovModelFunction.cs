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
    public sealed class MultivariateNormalMarkovModelFunction : BasePotentialFunction<double[]>, IPotentialFunction<double[]>
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
        /// <param name="model">A normal density hidden Markov.</param>
        /// 
        public MultivariateNormalMarkovModelFunction(
            HiddenMarkovModel<MultivariateNormalDistribution> model)
        {
            this.Dimensions = model.Dimension;

            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<double[]>>();


            var stateParams = new List<double>();
            var stateFeatures = new List<IFeature<double[]>>();

            var edgeParams = new List<double>();
            var edgeFeatures = new List<IFeature<double[]>>();



            // Create features for initial state probabilities
            for (int i = 0; i < model.States; i++)
            {
                edgeParams.Add(model.Probabilities[i]);
                edgeFeatures.Add(new InitialFeature<double[]>(this, 0, i));
            }

            // Create features for state transition probabilities
            for (int i = 0; i < model.States; i++)
            {
                for (int j = 0; j < model.States; j++)
                {
                    edgeParams.Add(model.Transitions[i, j]);
                    edgeFeatures.Add(new TransitionFeature<double[]>(this, 0, i, j));
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
                    stateFeatures.Add(new OccupancyFeature<double[]>(this, 0, i));

                    // 1st Moment (x)
                    stateParams.Add(mean / var);
                    stateFeatures.Add(new MultivariateFirstMomentFeature(this, 0, i, d));

                    // 2nd Moment (x²)
                    stateParams.Add(-1.0 / (2.0 * var));
                    stateFeatures.Add(new MultivariateSecondMomentFeature(this, 0, i, d));
                }
            }

            int startEdgeIndex = 0;
            int startStateIndex = edgeParams.Count;

            // First features and params are always belonging to edges
            Factors = new[] { new MultivariateNormalMarkovModelFactor(this, model.States, 0, Dimensions,
                    0, 0,  // 1. classes
                    startEdgeIndex, edgeParams.Count,    // 1. edges
                    startStateIndex, stateParams.Count)  // 2. states
                };

            // 1. edges
            factorFeatures.AddRange(edgeFeatures);
            factorParams.AddRange(edgeParams);

            // 2. states
            factorFeatures.AddRange(stateFeatures);
            factorParams.AddRange(stateParams);


            this.Weights = factorParams.ToArray();
            this.Features = factorFeatures.ToArray();
        }

            #region ICloneable Members

        private MultivariateNormalMarkovModelFunction() { }

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
            var clone = new MultivariateNormalMarkovModelFunction();

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
