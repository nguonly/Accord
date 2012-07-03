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
    ///   Multivariate Normal Markov Model Factor Potential (Clique Potential) function.
    /// </summary>
    /// 
    [Serializable]
    public class MultivariateNormalMarkovModelFactor : FactorPotential<double[]>
    {

        private int dimensions;

        /// <summary>
        ///   Gets the index of the first class feature function
        ///   belonging to this factor in the potential function.
        /// </summary>
        /// 
        public int ClassParameterIndex { get; protected set; }

        /// <summary>
        ///   Gets the number of class features in the factor potential.
        /// </summary>
        /// 
        public int ClassParameterCount { get; protected set; }

        /// <summary>
        ///   Creates a new factor (clique) potential function.
        /// </summary>
        /// 
        /// <param name="owner">The owner <see cref="IPotentialFunction{T}"/>.</param>
        /// <param name="states">The number of states in this clique potential.</param>
        /// <param name="factorIndex">The index of this factor potential in the <paramref name="owner"/>.</param>
        /// <param name="dimensions">The number of dimensions for the multivariate observations.</param>
        /// <param name="classIndex">The index of the first class label feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="classCount">The number of class label features in this factor.</param>
        /// <param name="edgeIndex">The index of the first edge feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="edgeCount">The number of edge features in this factor.</param>
        /// <param name="stateIndex">The index of the first state feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="stateCount">The number of state features in this factor.</param>
        /// 
        public MultivariateNormalMarkovModelFactor(IPotentialFunction<double[]> owner, int states, int factorIndex, int dimensions,
            int classIndex, int classCount,
            int edgeIndex, int edgeCount,
            int stateIndex, int stateCount)
            : base(owner, states, factorIndex, edgeIndex, edgeCount, stateIndex, stateCount)
        {
            this.dimensions = dimensions;

            ClassParameterIndex = classIndex;
            ClassParameterCount = classCount;

            ParameterIndex = Math.Min(Math.Min(edgeIndex, stateIndex), classIndex);
            ParameterCount = edgeCount + stateCount + classCount;
        }



        /// <summary>
        ///   Creates a new factor (clique) potential function.
        /// </summary>
        /// 
        /// <param name="owner">The owner <see cref="IPotentialFunction{T}"/>.</param>
        /// <param name="states">The number of states in this clique potential.</param>
        /// <param name="factorIndex">The index of this factor potential in the <paramref name="owner"/>.</param>
        /// <param name="dimensions">The number of dimensions for the multivariate observations.</param>
        /// <param name="edgeIndex">The index of the first edge feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="edgeCount">The number of edge features in this factor.</param>
        /// <param name="stateIndex">The index of the first state feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="stateCount">The number of state features in this factor.</param>
        /// 
        public MultivariateNormalMarkovModelFactor(IPotentialFunction<double[]> owner, int states, int factorIndex, int dimensions,
            int edgeIndex, int edgeCount,
            int stateIndex, int stateCount)
            : base(owner, states, factorIndex, edgeIndex, edgeCount, stateIndex, stateCount)
        {
            this.dimensions = dimensions;
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
        public override double Compute(int previousState, int currentState, double[][] observations, int index, int outputClass = 0)
        {
            if (outputClass != this.Index)
                return Double.NegativeInfinity;

            double[] parameters = Owner.Weights;
            IFeature<double[]>[] features = Owner.Features;


            double sum = 0;


            if (ClassParameterCount != 0)
            {
                if (previousState == -1)
                {
                    int cindex = ClassParameterIndex;
                    double w = parameters[cindex];
                    if (Double.IsNaN(w) || Double.IsNegativeInfinity(w))
                        return parameters[cindex] = Double.NegativeInfinity;
                    sum += w;
                }
            }

            int bindex = StateParameterIndex + currentState * dimensions * 3;
            double[] observation = observations[index];

            // For each dimension of the observation
            for (int j = 0; j < observation.Length; j++)
            {
                // State occupancy feature
                double b = parameters[bindex];
                if (Double.IsNaN(b) || Double.IsNegativeInfinity(b))
                    return parameters[bindex] = Double.NegativeInfinity;
                sum += b;

                double u = observation[j];

                int m1index = ++bindex;
                int m2index = ++bindex;

                if (u != 0)
                {
                    // First moment feature
                    double m1 = parameters[m1index];
                    if (Double.IsNaN(m1) || Double.IsNegativeInfinity(m1))
                        m1 = parameters[m1index] = Double.NegativeInfinity;
                    sum += m1 * u;

                    // Second moment feature
                    double m2 = parameters[m2index];
                    if (Double.IsNaN(m2) || Double.IsNegativeInfinity(m2))
                        m2 = parameters[m2index] = Double.NegativeInfinity;
                    sum += m2 * u * u;
                }

                bindex++;
            }

            if (previousState == -1)
            {
                int pindex = EdgeParameterIndex + currentState;
                double pi = parameters[pindex];
                if (Double.IsNaN(pi) || Double.IsNegativeInfinity(pi))
                    return parameters[pindex] = Double.NegativeInfinity;
                sum += pi;
            }
            else
            {
                int aindex = EdgeParameterIndex + States + previousState * States + currentState;
                double a = parameters[aindex];
                if (Double.IsNaN(a) || Double.IsNegativeInfinity(a))
                    return parameters[aindex] = Double.NegativeInfinity;
                sum += a;
            }

            return sum;
        }

    }
}
