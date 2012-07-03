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
    using Accord.Statistics.Models.Markov;
    using Accord.Statistics.Models.Fields.Features;

    /// <summary>
    ///   Potential function modeling Hidden Markov Models.
    /// </summary>
    /// 
    [Serializable]
    public sealed class DiscreteMarkovClassifierFunction : BasePotentialFunction<int>,
        IPotentialFunction<int>, ICloneable
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
        /// <param name="outputClasses">The number of output classes.</param>
        /// 
        public DiscreteMarkovClassifierFunction(int states, int symbols, int outputClasses)
        {
            this.Outputs = outputClasses;
            this.Symbols = symbols;

            int factorIndex = 0;
            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<int>>();

            this.Factors = new FactorPotential<int>[Outputs];

            // Create features for initial class probabilities
            for (int c = 0; c < outputClasses; c++)
            {
                var stateParams = new List<double>();
                var stateFeatures = new List<IFeature<int>>();

                var edgeParams = new List<double>();
                var edgeFeatures = new List<IFeature<int>>();

                var classParams = new List<double>();
                var classFeatures = new List<IFeature<int>>();


                // Create features for class labels
                classParams.Add(Math.Log(1.0 / outputClasses));
                classFeatures.Add(new OutputFeature<int>(this, c, c));

                // Create features for initial state probabilities
                for (int i = 0; i < states; i++)
                {
                    edgeParams.Add((i == 0) ? Math.Log(1.0) : Math.Log(0.0));
                    edgeFeatures.Add(new InitialFeature<int>(this, c, i));
                }

                // Create features for state transition probabilities
                for (int i = 0; i < states; i++)
                {
                    for (int j = 0; j < states; j++)
                    {
                        edgeParams.Add(Math.Log(1.0 / states));
                        edgeFeatures.Add(new TransitionFeature<int>(this, c, i, j));
                    }
                }

                // Create features for symbol emission probabilities
                for (int i = 0; i < states; i++)
                {
                    for (int k = 0; k < symbols; k++)
                    {
                        stateParams.Add(Math.Log(1.0 / symbols));
                        stateFeatures.Add(new EmissionFeature(this, c, i, k));
                    }
                }

                int startClassIndex = factorIndex;
                int startEdgeIndex = factorIndex + classParams.Count;
                int startStateIndex = factorIndex + classParams.Count + edgeParams.Count;

                // First features and params are always belonging to classes
                Factors[c] = new DiscreteMarkovModelFactor(this, states, c, symbols,
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
        /// <param name="classifier">The classifier model.</param>
        /// <param name="includeClassFeatures">True to include class features (priors), false otherwise.</param>
        /// 
        public DiscreteMarkovClassifierFunction(HiddenMarkovClassifier classifier, bool includeClassFeatures = true)
        {
            this.Symbols = classifier.Symbols;
            this.Outputs = classifier.Classes;

            int factorIndex = 0;
            var factorParams = new List<double>();
            var factorFeatures = new List<IFeature<int>>();

            this.Factors = new FactorPotential<int>[Outputs];

            // Create features for initial class probabilities
            for (int c = 0; c < classifier.Classes; c++)
            {
                var stateParams = new List<double>();
                var stateFeatures = new List<IFeature<int>>();

                var edgeParams = new List<double>();
                var edgeFeatures = new List<IFeature<int>>();

                var classParams = new List<double>();
                var classFeatures = new List<IFeature<int>>();

                var model = classifier[c];

                if (includeClassFeatures)
                {
                    // Create features for class labels
                    classParams.Add(Math.Log(classifier.Priors[c]));
                    classFeatures.Add(new OutputFeature<int>(this, c, c));
                }

                // Create features for initial state probabilities
                for (int i = 0; i < model.States; i++)
                {
                    edgeParams.Add(model.Probabilities[i]);
                    edgeFeatures.Add(new InitialFeature<int>(this, c, i));
                }

                // Create features for state transition probabilities
                for (int i = 0; i < model.States; i++)
                {
                    for (int j = 0; j < model.States; j++)
                    {
                        edgeParams.Add(model.Transitions[i, j]);
                        edgeFeatures.Add(new TransitionFeature<int>(this, c, i, j));
                    }
                }

                // Create features for symbol emission probabilities
                for (int i = 0; i < model.States; i++)
                {
                    for (int k = 0; k < model.Symbols; k++)
                    {
                        stateParams.Add(model.Emissions[i, k]);
                        stateFeatures.Add(new EmissionFeature(this, c, i, k));
                    }
                }

                int startClassIndex = factorIndex;
                int startEdgeIndex = factorIndex + classParams.Count;
                int startStateIndex = factorIndex + classParams.Count + edgeParams.Count;

                // First features and params are always belonging to classes
                Factors[c] = new DiscreteMarkovModelFactor(this, model.States, c, Symbols,
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

        private DiscreteMarkovClassifierFunction() { }

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
            var clone = new DiscreteMarkovClassifierFunction();

            clone.Factors = new FactorPotential<int>[Factors.Length];
            for (int i = 0; i < Factors.Length; i++)
                clone.Factors[i] = Factors[i].Clone(newOwner: clone);

            clone.Features = new IFeature<int>[Features.Length];
            for (int i = 0; i < Features.Length; i++)
                clone.Features[i] = Features[i].Clone(newOwner: clone);

            clone.Outputs = Outputs;
            clone.Symbols = Symbols;
            
            clone.Weights = (double[])Weights.Clone();

            return clone;
        }

        #endregion
    }
}
