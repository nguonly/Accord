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
    using System.Linq;
    using Accord.Math;

    /// <summary>
    ///   Base class for implementations of the Baum-Welch learning algorithm.
    ///   This class cannot be instantiated.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   This class uses a template method pattern so specialized classes
    ///   can be written for each kind of hidden Markov model emission density
    ///   (either discrete or continuous). The methods <see cref="UpdateEmissions"/>,
    ///   <see cref="ComputeForwardBackward"/> and <see cref="ComputeKsi"/> should
    ///   be overriden by inheriting classes to specify how those probabilities
    ///   should be computed for the density being modeled.</para>
    ///   
    /// <seealso cref="BaumWelchLearning"/>
    /// <seealso cref="BaumWelchLearning{T}"/>
    /// </remarks>
    /// 
    public abstract class BaseBaumWelchLearning
    {

        private IHiddenMarkovModel model;
        private int maxIterations = 100;
        private double tolerance = 0.00;


        /// <summary>
        ///   Gets the Ksi matrix of log probabilities created during
        ///   the last iteration of the Baum-Welch learning algorithm.
        /// </summary>
        /// 
        public double[][][,] LogKsi { get; protected set; }

        /// <summary>
        ///   Gets the Gamma matrix of log probabilities created during
        ///   the last iteration of the Baum-Welch learning algorithm.
        /// </summary>
        /// 
        public double[][,] LogGamma { get; protected set; }




        /// <summary>
        ///   Initializes a new instance of the <see cref="BaseBaumWelchLearning"/> class.
        /// </summary>
        /// 
        protected BaseBaumWelchLearning(IHiddenMarkovModel model)
        {
            this.model = model;
        }


        /// <summary>
        ///   Gets or sets the maximum change in the average log-likelihood
        ///   after an iteration of the algorithm used to detect convergence.
        /// </summary>
        /// 
        /// <remarks>
        ///   This is the likelihood convergence limit L between two iterations of the algorithm. The
        ///   algorithm will stop when the change in the likelihood for two consecutive iterations
        ///   has not changed by more than L percent of the likelihood. If left as zero, the
        ///   algorithm will ignore this parameter and iterates over a number of fixed iterations
        ///   specified by the previous parameter.
        /// </remarks>
        /// 
        public double Tolerance
        {
            get { return tolerance; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "Tolerance should be positive.");

                tolerance = value;
            }
        }

        /// <summary>
        ///   Gets or sets the maximum number of iterations
        ///   performed by the learning algorithm.
        /// </summary>
        /// 
        /// <remarks>
        ///   This is the maximum number of iterations to be performed by the learning algorithm. If
        ///   specified as zero, the algorithm will learn until convergence of the model average
        ///   likelihood respecting the desired limit.
        /// </remarks>
        /// 
        public int Iterations
        {
            get { return maxIterations; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "The maximum number of iterations should be positive.");

                maxIterations = value;
            }
        }


        /// <summary>
        ///   Checks if a model has converged given the likelihoods between two iterations
        ///   of the Baum-Welch algorithm and a criteria for convergence.
        /// </summary>
        /// 
        protected bool HasConverged(double oldLogLikelihood, double newLogLikelihood, int currentIteration)
        {
            // Update and verify stop criteria
            if (tolerance > 0)
            {
                // Stopping criteria is likelihood convergence
                double delta = Math.Abs(oldLogLikelihood - newLogLikelihood);
                if (delta <= tolerance)
                    return true;

                if (maxIterations > 0)
                {
                    // Maximum iterations should also be respected
                    if (currentIteration >= maxIterations)
                        return true;
                }
            }
            else
            {
                // Stopping criteria is number of iterations
                if (currentIteration == maxIterations)
                    return true;
            }

            // Check if we have reached an invalid or perfectly separable answer
            if (Double.IsNaN(newLogLikelihood) || Double.IsInfinity(newLogLikelihood))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        ///   Runs the Baum-Welch learning algorithm for hidden Markov models.
        /// </summary>
        /// <remarks>
        ///   Learning problem. Given some training observation sequences O = {o1, o2, ..., oK}
        ///   and general structure of HMM (numbers of hidden and visible states), determine
        ///   HMM parameters M = (A, B, pi) that best fit training data. 
        /// </remarks>
        /// <param name="observations">
        ///   The sequences of univariate or multivariate observations used to train the model.
        ///   Can be either of type double[] (for the univariate case) or double[][] for the
        ///   multivariate case.
        /// </param>
        /// <returns>
        ///   The average log-likelihood for the observations after the model has been trained.
        /// </returns>
        /// 
        protected double Run(Array[] observations)
        {
            if (observations == null)
                throw new ArgumentNullException("observations");

            if (observations.Length == 0)
                throw new ArgumentException("Observations vector must contain at least one observation", "observations");

            // Baum-Welch algorithm.

            // The Baum–Welch algorithm is a particular case of a generalized expectation-maximization
            // (GEM) algorithm. It can compute maximum likelihood estimates and posterior mode estimates
            // for the parameters (transition and emission probabilities) of an HMM, when given only
            // emissions as training data.

            // The algorithm has two steps:
            //  - Calculating the forward probability and the backward probability for each HMM state;
            //  - On the basis of this, determining the frequency of the transition-emission pair values
            //    and dividing it by the probability of the entire string. This amounts to calculating
            //    the expected count of the particular transition-emission pair. Each time a particular
            //    transition is found, the value of the quotient of the transition divided by the probability
            //    of the entire string goes up, and this value can then be made the new value of the transition.


            // Grab model information
            int states = model.States;
            var logA = model.Transitions;
            var logP = model.Probabilities;


            // Initialize the algorithm
            int N = observations.Length;
            double logN = Math.Log(N);
            LogKsi = new double[N][][,];
            LogGamma = new double[N][,];


            for (int i = 0; i < observations.Length; i++)
            {
                int T = observations[i].Length;

                LogKsi[i] = new double[T][,];
                LogGamma[i] = new double[T, states];

                for (int t = 0; t < LogKsi[i].Length; t++)
                    LogKsi[i][t] = new double[states, states];
            }

            int iteration = 1;
            bool stop = false;

            int TMax = observations.Max(x => x.Length);
            double[,] lnFwd = new double[TMax, states];
            double[,] lnBwd = new double[TMax, states];

            // Initialize the model log-likelihoods
            double newLogLikelihood = Double.NegativeInfinity;
            double oldLogLikelihood = Double.NegativeInfinity;


            do // Until convergence or max iterations is reached
            {
                // For each sequence in the observations input
                for (int i = 0; i < observations.Length; i++)
                {
                    int T = observations[i].Length;
                    double[,] logGamma = LogGamma[i];


                    // 1st step - Calculating the forward probability and the
                    //            backward probability for each HMM state.
                    ComputeForwardBackward(i, lnFwd, lnBwd);


                    // 2nd step - Determining the frequency of the transition-emission pair values
                    //            and dividing it by the probability of the entire string.

                    // Calculate gamma values for next computations
                    for (int t = 0; t < T; t++)
                    {
                        double lnsum = Double.NegativeInfinity;
                        for (int k = 0; k < states; k++)
                        {
                            logGamma[t, k] = lnFwd[t, k] + lnBwd[t, k];
                            lnsum = Special.LogSum(lnsum, logGamma[t, k]);
                        }

                        // Normalize if different from zero
                        if (lnsum != Double.NegativeInfinity)
                            for (int k = 0; k < states; k++)
                                logGamma[t, k] = logGamma[t, k] - lnsum;
                    }

                    // Calculate ksi values for next computations
                    ComputeKsi(i, lnFwd, lnBwd);

                    // Compute log-likelihood for the given sequence
                    newLogLikelihood = Double.NegativeInfinity;
                    for (int j = 0; j < states; j++)
                        newLogLikelihood = Special.LogSum(newLogLikelihood, lnFwd[T - 1, j]);
                }


                // Average the likelihood for all sequences
                newLogLikelihood /= observations.Length;


                // Check if the model has converged or if we should stop
                if (!HasConverged(oldLogLikelihood, newLogLikelihood, iteration))
                {
                    // We haven't converged yet

                    // 3. Continue with parameter re-estimation
                    iteration++;
                    oldLogLikelihood = newLogLikelihood;
                    newLogLikelihood = Double.NegativeInfinity;

                    // 3.1 Re-estimation of initial state probabilities 
                    for (int i = 0; i < logP.Length; i++)
                    {
                        double lnsum = Double.NegativeInfinity;
                        for (int k = 0; k < LogGamma.Length; k++)
                            lnsum = Special.LogSum(lnsum, LogGamma[k][0, i]);
                        logP[i] = lnsum - logN;
                    }

                    // 3.2 Re-estimation of transition probabilities 
                    for (int i = 0; i < states; i++)
                    {
                        for (int j = 0; j < states; j++)
                        {
                            double lnnum = Double.NegativeInfinity;
                            double lnden = Double.NegativeInfinity;

                            for (int k = 0; k < LogGamma.Length; k++)
                            {
                                int T = observations[k].Length;

                                for (int t = 0; t < T - 1; t++)
                                {
                                    lnnum = Special.LogSum(lnnum, LogKsi[k][t][i, j]);
                                    lnden = Special.LogSum(lnden, LogGamma[k][t, i]);
                                }
                            }

                            logA[i, j] = (lnnum == lnden) ? 0 : lnnum - lnden;
                        }
                    }

                    // 3.3 Re-estimation of emission probabilities
                    UpdateEmissions(); // discrete and continuous
                }
                else
                {
                    stop = true; // The model has converged.
                }

            } while (!stop);


            // Returns the model average log-likelihood
            return newLogLikelihood;
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
        protected abstract void ComputeForwardBackward(int index, double[,] lnFwd, double[,] lnBwd);

        /// <summary>
        ///   Computes the ksi matrix of probabilities for a given observation
        ///   referenced by its index in the input training data.
        /// </summary>
        /// 
        /// <param name="index">The index of the observation in the input training data.</param>
        /// <param name="lnFwd">The matrix of forward probabilities for the observation.</param>
        /// <param name="lnBwd">The matrix of backward probabilities for the observation.</param>
        /// 
        protected abstract void ComputeKsi(int index, double[,] lnFwd, double[,] lnBwd);

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
        protected abstract void UpdateEmissions();

    }

}
