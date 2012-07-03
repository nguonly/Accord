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
    using Accord.Statistics.Models.Fields.Features;
    using Accord.Statistics.Models.Markov;

    /// <summary>
    ///   Potential function modeling Hidden Markov Models.
    /// </summary>
    /// 
    [Serializable]
    public sealed class DiscreteMarkovModelFunction : BasePotentialFunction<int>, IPotentialFunction<int>
    {

        /// <summary>
        ///   Gets the number of symbols assumed by this function.
        /// </summary>
        /// 
        public int Symbols { get; private set; }

        /// <summary>
        ///   Constructs a new potential function modeling Hidden Markov Models.
        /// </summary>
        /// 
        /// <param name="states">The number of states.</param>
        /// <param name="symbols">The number of symbols.</param>
        /// 
        public DiscreteMarkovModelFunction(int states, int symbols)
        {
            this.Symbols = symbols;

            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<int>>();

            var stateParams = new List<double>();
            var stateFeatures = new List<IFeature<int>>();

            var edgeParams = new List<double>();
            var edgeFeatures = new List<IFeature<int>>();


            // Create features for initial state probabilities
            for (int i = 0; i < states; i++)
            {
                edgeParams.Add(0);
                edgeFeatures.Add(new InitialFeature<int>(this, 0, i));
            }

            // Create features for state transition probabilities
            for (int i = 0; i < states; i++)
            {
                for (int j = 0; j < states; j++)
                {
                    edgeParams.Add(0);
                    edgeFeatures.Add(new TransitionFeature<int>(this, 0, i, j));
                }
            }

            // Create features for symbol emission probabilities
            for (int i = 0; i < states; i++)
            {
                for (int k = 0; k < symbols; k++)
                {
                    stateParams.Add(0);
                    stateFeatures.Add(new EmissionFeature(this, 0, i, k));
                }
            }

            // First features and params are always belonging to edges
            this.Factors = new[] { new DiscreteMarkovModelFactor(this, states, 0, symbols,
                0, edgeParams.Count,                     // first edges
                edgeParams.Count, stateParams.Count) };  // then states

            // First edges
            factorFeatures.AddRange(edgeFeatures);
            factorParams.AddRange(edgeParams);

            // First states
            factorFeatures.AddRange(stateFeatures);
            factorParams.AddRange(stateParams);

            this.Features = factorFeatures.ToArray();
            this.Weights = factorParams.ToArray();
        }

        /// <summary>
        ///   Constructs a new potential function modeling Hidden Markov Models.
        /// </summary>
        /// 
        /// <param name="model">The hidden Markov model.</param>
        /// 
        public DiscreteMarkovModelFunction(HiddenMarkovModel model)
        {
            int states = model.States;
            this.Symbols = model.Symbols;

            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<int>>();

            var stateParams = new List<double>();
            var stateFeatures = new List<IFeature<int>>();

            var edgeParams = new List<double>();
            var edgeFeatures = new List<IFeature<int>>();


            // Create features for initial state probabilities
            for (int i = 0; i < states; i++)
            {
                edgeParams.Add(model.Probabilities[i]);
                edgeFeatures.Add(new InitialFeature<int>(this, 0, i));
            }

            // Create features for state transition probabilities
            for (int i = 0; i < states; i++)
            {
                for (int j = 0; j < states; j++)
                {
                    edgeParams.Add(model.Transitions[i, j]);
                    edgeFeatures.Add(new TransitionFeature<int>(this, 0, i, j));
                }
            }

            // Create features for symbol emission probabilities
            for (int i = 0; i < states; i++)
            {
                for (int k = 0; k < Symbols; k++)
                {
                    stateParams.Add(model.Emissions[i, k]);
                    stateFeatures.Add(new EmissionFeature(this, 0, i, k));
                }
            }


            // First features and params are always belonging to edges
            this.Factors = new[] { new DiscreteMarkovModelFactor(this, states, 0, Symbols,
                0, edgeParams.Count,                     // first edges
                edgeParams.Count, stateParams.Count) };  // then states

            // First edges
            factorFeatures.AddRange(edgeFeatures);
            factorParams.AddRange(edgeParams);

            // First states
            factorFeatures.AddRange(stateFeatures);
            factorParams.AddRange(stateParams);

            this.Features = factorFeatures.ToArray();
            this.Weights = factorParams.ToArray();
        }



        #region ICloneable Members

        private DiscreteMarkovModelFunction() { }

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
            var clone = new DiscreteMarkovModelFunction();

            clone.Factors = new FactorPotential<int>[Factors.Length];
            for (int i = 0; i < Factors.Length; i++)
                clone.Factors[i] = Factors[i].Clone(newOwner: clone);

            clone.Features = new IFeature<int>[Features.Length];
            for (int i = 0; i < Features.Length; i++)
                clone.Features[i] = Features[i].Clone(newOwner: clone);

            clone.Outputs = Outputs;
            clone.Weights = (double[])Weights.Clone();

            clone.Symbols = Symbols;

            return clone;
        }

        #endregion

    }
}
