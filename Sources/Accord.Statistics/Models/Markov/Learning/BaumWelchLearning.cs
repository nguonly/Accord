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

namespace Accord.Statistics.Models.Markov.Learning
{
    using System;
    using Accord.Math;

    /// <summary>
    ///   Baum-Welch learning algorithm for discrete-density Hidden Markov Models.
    /// </summary>
    /// 
    public class BaumWelchLearning : BaseBaumWelchLearning, IUnsupervisedLearning
    {

        private HiddenMarkovModel model;

        private int[][] discreteObservations;


        /// <summary>
        ///   Creates a new instance of the Baum-Welch learning algorithm.
        /// </summary>
        /// 
        public BaumWelchLearning(HiddenMarkovModel model)
            : base(model)
        {
            this.model = model;
        }


        /// <summary>
        ///   Runs the Baum-Welch learning algorithm for hidden Markov models.
        /// </summary>
        /// 
        /// <param name="observations">An array of observation sequences to be used to train the model.</param>
        /// 
        /// <returns>
        ///   The average log-likelihood for the observations after the model has been trained.
        /// </returns>
        /// 
        /// <remarks>
        ///   Learning problem. Given some training observation sequences O = {o1, o2, ..., oK}
        ///   and general structure of HMM (numbers of hidden and visible states), determine
        ///   HMM parameters M = (A, B, pi) that best fit training data.
        /// </remarks>
        /// 
        public double Run(params int[][] observations)
        {
            this.discreteObservations = observations;

            return base.Run(discreteObservations);
        }


        /// <summary>
        ///   Runs the Baum-Welch learning algorithm for hidden Markov models.
        /// </summary>
        /// 
        /// <param name="observations">The sequences of univariate or multivariate observations used to train the model.
        ///   Can be either of type double[] (for the univariate case) or double[][] for the
        ///   multivariate case.</param>
        ///   
        /// <returns>
        ///   The average log-likelihood for the observations after the model has been trained.
        /// </returns>
        /// 
        /// <remarks>
        ///   Learning problem. Given some training observation sequences O = {o1, o2, ..., oK}
        ///   and general structure of HMM (numbers of hidden and visible states), determine
        ///   HMM parameters M = (A, B, pi) that best fit training data.
        /// </remarks>
        /// 
        double IUnsupervisedLearning.Run(Array[] observations)
        {
            return Run(observations as int[][]);
        }

        /// <summary>
        ///   Computes the forward and backward probabilities matrices
        ///   for a given observation referenced by its index in the
        ///   input training data.
        /// </summary>
        /// 
        /// <param name="index">The index of the observation in the input training data.</param>
        /// <param name="lnFwd">Returns the computed forward probabilities matrix.</param>
        /// <param name="lnBwd">Returns the computed backward probabilities matrix.</param>
        /// 
        protected override void ComputeForwardBackward(int index, double[,] lnFwd, double[,] lnBwd)
        {
            int states = model.States;
            int T = discreteObservations[index].Length;

            System.Diagnostics.Debug.Assert(lnBwd.GetLength(0) >= T);
            System.Diagnostics.Debug.Assert(lnBwd.GetLength(1) == states);
            System.Diagnostics.Debug.Assert(lnFwd.GetLength(0) >= T);
            System.Diagnostics.Debug.Assert(lnFwd.GetLength(1) == states);

            ForwardBackwardAlgorithm.LogForward(model, discreteObservations[index], lnFwd);
            ForwardBackwardAlgorithm.LogBackward(model, discreteObservations[index], lnBwd);
        }

        /// <summary>
        ///   Updates the emission probability matrix.
        /// </summary>
        /// 
        /// <remarks>
        ///   Implementations of this method should use the observations
        ///   in the training data and the Gamma probability matrix to
        ///   update the probability distributions of symbol emissions.
        /// </remarks>
        /// 
        protected override void UpdateEmissions()
        {
            var B = model.Emissions;
            int states = model.States;
            int symbols = model.Symbols;

            for (int i = 0; i < states; i++)
            {
                for (int j = 0; j < symbols; j++)
                {
                    double lnnum = Double.NegativeInfinity;
                    double lnden = Double.NegativeInfinity;

                    for (int k = 0; k < discreteObservations.Length; k++)
                    {
                        int T = discreteObservations[k].Length;
                        var gammak = LogGamma[k];

                        for (int t = 0; t < T; t++)
                        {
                            if (discreteObservations[k][t] == j)
                                lnnum = Special.LogSum(lnnum, gammak[t, i]);
                            lnden = Special.LogSum(lnden, gammak[t, i]);
                        }
                    }

                    // TODO: avoid locking a parameter in zero.
                    B[i, j] = lnnum - lnden;
                }
            }
        }

        /// <summary>
        ///   Computes the ksi matrix of probabilities for a given observation
        ///   referenced by its index in the input training data.
        /// </summary>
        /// 
        /// <param name="index">The index of the observation in the input training data.</param>
        /// <param name="lnFwd">The matrix of forward probabilities for the observation.</param>
        /// <param name="lnBwd">The matrix of backward probabilities for the observation.</param>
        /// 
        protected override void ComputeKsi(int index, double[,] lnFwd, double[,] lnBwd)
        {
            int states = model.States;
            var logA = model.Transitions;
            var logB = model.Emissions;

            var sequence = discreteObservations[index];
            int T = sequence.Length;
            var logKsi = LogKsi[index];


            for (int t = 0; t < T - 1; t++)
            {
                double lnsum = Double.NegativeInfinity;
                var x = sequence[t + 1];

                for (int i = 0; i < states; i++)
                {
                    for (int j = 0; j < states; j++)
                    {
                        logKsi[t][i, j] = lnFwd[t, i] + logA[i, j] + logB[j, x] + lnBwd[t + 1, j];
                        lnsum = Special.LogSum(lnsum, logKsi[t][i, j]);
                    }
                }

                for (int i = 0; i < states; i++)
                    for (int j = 0; j < states; j++)
                        logKsi[t][i, j] = logKsi[t][i, j] - lnsum;
            }
        }
    }
}
