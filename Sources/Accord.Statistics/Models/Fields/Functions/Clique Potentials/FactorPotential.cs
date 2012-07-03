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

    /// <summary>
    ///   Factor Potential (Clique Potential) function.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the observations being modeled.</typeparam>
    /// 
    [Serializable]
    public class FactorPotential<T> : IEnumerable<IFeature<T>>
    {
        /// <summary>
        ///   Gets the number of model states
        ///   assumed by this function.
        /// </summary>
        /// 
        public int States { get; private set; }

        /// <summary>
        ///   Gets the index of this factor in the 
        ///   <see cref="Owner"/> potential function.
        /// </summary>
        /// 
        public int Index { get; private set; }

        /// <summary>
        ///   Gets the index of the first edge feature function
        ///   belonging to this factor in the potential function.
        /// </summary>
        /// 
        public int EdgeParameterIndex { get; protected set; }

        /// <summary>
        ///   Gets the number of edge features in the factor potential.
        /// </summary>
        /// 
        public int EdgeParameterCount { get; protected set; }

        /// <summary>
        ///   Gets the index of the first state feature function
        ///   belonging to this factor in the potential function.
        /// </summary>
        /// 
        public int StateParameterIndex { get; protected set; }

        /// <summary>
        ///   Gets the number of state features in the factor potential.
        /// </summary>
        /// 
        public int StateParameterCount { get; protected set; }

        /// <summary>
        ///   Gets the <see cref="IPotentialFunction{T}"/> 
        ///   to which this factor potential belongs.
        /// </summary>
        /// 
        public IPotentialFunction<T> Owner { get; private set; }

        /// <summary>
        ///   Gets the index of the first parameter relative
        ///   to this factor potential in the parameter vector.
        /// </summary>
        /// 
        public int ParameterIndex { get; protected set; }

        /// <summary>
        ///   Gets the number of parameters (and features)
        ///   contained in this factor potential.
        /// </summary>
        /// 
        public int ParameterCount { get; protected set; }

        /// <summary>
        ///   Gets the index of the last edge parameter for
        ///   this factor potential in the parameter vector.
        /// </summary>
        /// 
        protected int EdgeParameterEnd { get; private set; }

        /// <summary>
        ///   Gets the index of the last state parameter for
        ///   this factor potential in the parameter vector.
        /// </summary>
        /// 
        protected int StateParameterEnd { get; private set; }

        /// <summary>
        ///   Gets the index of the last edge parameter for
        ///   this factor potential in the parameter vector.
        /// </summary>
        /// 
        protected int ParameterEnd { get; private set; }

        /// <summary>
        ///   Creates a new factor (clique) potential function.
        /// </summary>
        /// 
        /// <param name="owner">The owner <see cref="IPotentialFunction{T}"/>.</param>
        /// <param name="states">The number of states in this clique potential.</param>
        /// <param name="factorIndex">The index of this factor potential in the <paramref name="owner"/>.</param>
        /// <param name="edgeIndex">The index of the first edge feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="edgeCount">The number of edge features in this factor.</param>
        /// <param name="stateIndex">The index of the first state feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="stateCount">The number of state features in this factor.</param>
        /// 
        public FactorPotential(IPotentialFunction<T> owner, int states, int factorIndex,
            int edgeIndex, int edgeCount,
            int stateIndex, int stateCount)
            : this(owner, states, factorIndex)
        {
            EdgeParameterIndex = edgeIndex;
            EdgeParameterCount = edgeCount;
            EdgeParameterEnd = edgeIndex + edgeCount;

            StateParameterIndex = stateIndex;
            StateParameterCount = stateCount;
            StateParameterEnd = stateIndex + stateCount;

            ParameterIndex = Math.Min(edgeIndex, stateIndex);
            ParameterCount = edgeCount + stateCount;
            ParameterEnd = ParameterIndex + ParameterCount;
        }

        /// <summary>
        ///   Creates a new factor (clique) potential function.
        /// </summary>
        /// 
        /// <param name="owner">The owner <see cref="IPotentialFunction{T}"/>.</param>
        /// <param name="states">The number of states in this clique potential.</param>
        /// <param name="factorIndex">The index of this factor potential in the <paramref name="owner"/>.</param>
        /// 
        protected FactorPotential(IPotentialFunction<T> owner, int states, int factorIndex)
        {
            Owner = owner;
            States = states;
            Index = factorIndex;
        }

        /// <summary>
        ///   Computes the factor potential function for the given parameters.
        /// </summary>
        /// 
        /// <param name="states">A state sequence.</param>
        /// <param name="observations">A sequence of observations.</param>
        /// <param name="output">The output class label for the sequence.</param>
        /// <returns>The value of the factor potential function evaluated for the given parameters.</returns>
        /// 
        public double Compute(int[] states, T[] observations, int output = 0)
        {
            double p = Compute(-1, states[0], observations, 0, output);
            for (int t = 1; t < observations.Length; t++)
                p += Compute(states[t - 1], states[t], observations, t, output);

            return p;
        }

        /// <summary>
        ///   Computes the factor potential function for the given parameters.
        /// </summary>
        /// 
        /// <param name="states">A state sequence.</param>
        /// <param name="observations">A sequence of observations.</param>
        /// <param name="output">The output class label for the sequence.</param>
        /// <returns>The value of the factor potential function evaluated for the given parameters.</returns>
        /// 
        public virtual double[] GetFeatureVector(int[] states, T[] observations, int output = 0)
        {
            double[] featureVector = new double[ParameterCount];

            int start = ParameterIndex;
            int end = ParameterEnd;

            for (int i = 0, k = start; i < featureVector.Length; i++, k++)
                featureVector[i] = Owner.Features[k].Compute(states, observations, output);

            return featureVector;
        }

        /// <summary>
        ///   Computes the factor potential function for the given parameters.
        /// </summary>
        /// 
        /// <param name="previousState">The previous state in a given sequence of states.</param>
        /// <param name="currentState">The current state in a given sequence of states.</param>
        /// <param name="observations">The observation vector.</param>
        /// <param name="index">The index of the observation in the current state of the sequence.</param>
        /// <param name="outputClass">The output class label for the sequence.</param>
        /// <returns>The value of the factor potential function evaluated for the given parameters.</returns>
        /// 
        public virtual double Compute(int previousState, int currentState, T[] observations, int index,
            int outputClass = 0)
        {
            int start = ParameterIndex;
            int end = ParameterEnd;

            double sum = 0;
            for (int k = start; k < end; k++)
            {
                double weight = Owner.Weights[k];

                if (Double.IsNaN(weight))
                    Owner.Weights[k] = weight = 0;

                if (weight != 0)
                {
                    double value = Owner.Features[k].Compute(previousState, currentState, observations, index, outputClass);

                    if (value != 0) sum += weight * value;
                }

                if (Double.IsNaN(sum))
                    return 0;

                if (Double.IsInfinity(sum))
                    return sum;
            }

            return sum;
        }


        /// <summary>
        ///   Returns an enumerator that iterates through all features in this factor potential function.
        /// </summary>
        /// <returns>
        ///   An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IFeature<T>> GetEnumerator()
        {
            int start = ParameterIndex;
            int end = ParameterEnd;

            for (int k = start; k < end; k++)
            {
                yield return Owner.Features[k];
            }

            yield break;
        }

        /// <summary>
        ///   Returns an enumerator that iterates through all features in this factor potential function.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///   Creates a new object that is a copy of the current instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A new object that is a copy of this instance.
        /// </returns>
        /// 
        public FactorPotential<T> Clone(IPotentialFunction<T> newOwner)
        {
            var clone = (FactorPotential<T>)this.MemberwiseClone();
            clone.Owner = newOwner;
            return clone;
        }

    }
}
