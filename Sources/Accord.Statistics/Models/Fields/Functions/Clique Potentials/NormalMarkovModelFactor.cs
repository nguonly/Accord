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
    ///   Normal-density Markov Factor Potential (Clique Potential) function.
    /// </summary>
    /// 
    [Serializable]
    public class NormalMarkovModelFactor : FactorPotential<double>
    {

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
        /// <param name="classIndex">The index of the first class label feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="classCount">The number of class label features in this factor.</param>
        /// <param name="edgeIndex">The index of the first edge feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="edgeCount">The number of edge features in this factor.</param>
        /// <param name="stateIndex">The index of the first state feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="stateCount">The number of state features in this factor.</param> 
        ///  
        public NormalMarkovModelFactor(IPotentialFunction<double> owner, int states, int factorIndex,
            int classIndex, int classCount,
            int edgeIndex, int edgeCount,
            int stateIndex, int stateCount)
            : base(owner, states, factorIndex, edgeIndex, edgeCount, stateIndex, stateCount)
        {
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
        /// <param name="edgeIndex">The index of the first edge feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="edgeCount">The number of edge features in this factor.</param>
        /// <param name="stateIndex">The index of the first state feature in the <paramref name="owner"/>'s parameter vector.</param>
        /// <param name="stateCount">The number of state features in this factor.</param>
        /// 
        public NormalMarkovModelFactor(IPotentialFunction<double> owner, int states, int factorIndex,
            int edgeIndex, int edgeCount, int stateIndex, int stateCount)
            : base(owner, states, factorIndex, edgeIndex, edgeCount, stateIndex, stateCount) { }

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
        public override double Compute(int previousState, int currentState, double[] observations, int index, int outputClass = 0)
        {
            if (outputClass != this.Index)
                return Double.NegativeInfinity;

            double[] parameters = Owner.Weights;
            IFeature<double>[] features = Owner.Features;

            double sum = 0;


            if (ClassParameterCount != 0)
            {
                int cindex = ClassParameterIndex;
                double w = parameters[cindex];
                if (Double.IsNaN(w) || Double.IsNegativeInfinity(w))
                    return w = parameters[cindex] = Double.NegativeInfinity;
                sum += w;
            }


            int bindex = StateParameterIndex + currentState * 3;

            // For each of the three possible features:
            // Occupancy, first moment and second moment
            for (int i = 0; i < 3; i++)
            {
                double w = parameters[bindex];

                if (Double.IsNaN(w))
                    w = parameters[bindex] = Double.NegativeInfinity;

                if (w != 0)
                {
                    double f = features[bindex].Compute(previousState, currentState, observations, index, outputClass);

                    if (f != 0)
                    {
                        sum += w * f;

                        if (Double.IsNegativeInfinity(sum))
                            return Double.NegativeInfinity;
                    }
                }

                bindex++;
            }

            if (previousState == -1)
            {
                int pindex = EdgeParameterIndex + currentState;
                double pi = parameters[pindex];
                if (Double.IsNaN(pi))
                    return pi = parameters[pindex] = Double.NegativeInfinity;
                sum += pi;
            }
            else
            {
                int aindex = EdgeParameterIndex + States + previousState * States + currentState;
                double a = parameters[aindex];
                if (Double.IsNaN(a))
                    return a = parameters[aindex] = Double.NegativeInfinity;
                sum += a;
            }

            return sum;
        }

    }
}
