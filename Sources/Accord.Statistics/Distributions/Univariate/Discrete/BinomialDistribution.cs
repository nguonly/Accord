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

namespace Accord.Statistics.Distributions.Univariate
{
    using System;
    using Accord.Math;
    using Accord.Statistics.Distributions.Fitting;

    /// <summary>
    ///   Binomial probability distribution.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///  The binomial distribution is the discrete probability distribution of the number of
    ///  successes in a sequence of <c>>n</c> independent yes/no experiments, each of which 
    ///  yields success with probability <c>p</c>. Such a success/failure experiment is also
    ///  called a Bernoulli experiment or Bernoulli trial; when <c>n = 1</c>, the binomial 
    ///  distribution is a <see cref="BernoulliDistribution">Bernoulli distribution</see>.</para>
    ///   
    /// <para>    
    ///   References:
    ///   <list type="bullet">
    ///     <item><description><a href="http://en.wikipedia.org/wiki/Binomial_distribution">
    ///       Wikipedia, The Free Encyclopedia. Binomial distribution. Available on:
    ///       http://en.wikipedia.org/wiki/Binomial_distribution </a></description></item>
    ///     <item><description>
    ///       C. Bishop. “Pattern Recognition and Machine Learning”. Springer. 2006.</description></item>
    ///   </list></para>
    /// </remarks>
    /// 
    [Serializable]
    public class BinomialDistribution : UnivariateDiscreteDistribution,
        IFittableDistribution<double, IFittingOptions>
    {

        // Distribution parameters
        private int numberOfTrials; // number of trials
        private double probability; // success probability in each trial


        /// <summary>
        ///   Gets the number of trials <c>n</c> for the distribution.
        /// </summary>
        /// 
        public int NumberOfTrials
        {
            get { return numberOfTrials; }
        }

        /// <summary>
        ///   Gets the success probability <c>p</c> for the distribution.
        /// </summary>
        public double ProbabilityOfSuccess
        {
            get { return probability; }
        }

        /// <summary>
        ///   Constructs a new <see cref="BinomialDistribution">binomial distribution</see>.
        /// </summary>
        /// 
        /// <param name="trials">The number of trials <c>n</c>.</param>
        /// 
        public BinomialDistribution(int trials)
            : this(trials, 0) { }

        /// <summary>
        ///   Constructs a new <see cref="BinomialDistribution">binomial distribution</see>.
        /// </summary>
        /// 
        /// <param name="trials">The number of trials <c>n</c>.</param>
        /// <param name="probability">The success probability <c>p</c> in each trial.</param>
        /// 
        public BinomialDistribution(int trials, double probability)
        {
            if (trials <= 0)
                throw new ArgumentOutOfRangeException("trials", "The number of trials should be greater than zero.");

            if (probability < 0 || probability > 1)
                throw new ArgumentOutOfRangeException("probability", "A probability must be between 0 and 1.");

            this.numberOfTrials = trials;
            this.probability = probability;
        }

        /// <summary>
        ///   Gets the mean for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's mean value.</value>
        ///  
        public override double Mean
        {
            get { return numberOfTrials * probability; }
        }

        /// <summary>
        ///   Gets the variance for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's variance.</value>
        /// 
        public override double Variance
        {
            get { return numberOfTrials * probability * (1 - probability); }
        }

        /// <summary>
        ///   Gets the entropy for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's entropy.</value>
        /// 
        public override double Entropy
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        ///   Gets the cumulative distribution function (cdf) for
        ///   this distribution evaluated at point <c>k</c>.
        /// </summary>
        /// 
        /// <param name="k">A single point in the distribution range.</param>
        /// 
        /// <remarks>
        ///   The Cumulative Distribution Function (CDF) describes the cumulative
        ///   probability that a given value or any value smaller than it will occur.
        /// </remarks>
        /// 
        public override double DistributionFunction(int k)
        {
            return Beta.Incomplete(numberOfTrials - k, k + 1, 1 - probability);
        }

        /// <summary>
        ///   Gets the probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The probability of <c>k</c> occurring
        ///   in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        public override double ProbabilityMassFunction(int k)
        {
            if (k < 0 || k > numberOfTrials) return 0;
            return Special.Binomial(numberOfTrials, k) * Math.Pow(probability, k) * Math.Pow(1 - probability, numberOfTrials - k);
        }

        /// <summary>
        ///   Gets the log-probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The logarithm of the probability of <c>x</c>
        ///   occurring in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>k</c> will occur.
        /// </remarks>
        /// 
        public override double LogProbabilityMassFunction(int k)
        {
            if (k < 0 || k > numberOfTrials) return Double.NegativeInfinity;
            return Special.LogBinomial(numberOfTrials, k) + k * Math.Log(probability) + (numberOfTrials - k) * Math.Log(1 - probability);
        }

        /// <summary>
        ///   Fits the underlying distribution to a given set of observations.
        /// </summary>
        /// 
        /// <param name="observations">The array of observations to fit the model against. The array
        ///   elements can be either of type double (for univariate data) or
        ///   type double[] (for multivariate data).</param>
        /// <param name="weights">The weight vector containing the weight for each of the samples.</param>
        /// <param name="options">Optional arguments which may be used during fitting, such
        ///   as regularization constants and additional parameters.</param>
        /// 
        /// <remarks>
        ///   Although both double[] and double[][] arrays are supported,
        ///   providing a double[] for a multivariate distribution or a
        ///   double[][] for a univariate distribution may have a negative
        ///   impact in performance.
        /// </remarks>
        /// 
        public override void Fit(double[] observations, double[] weights, Fitting.IFittingOptions options)
        {
            if (weights != null)
                throw new NotSupportedException("Weighted estimation is not supported.");

            if (options != null)
                throw new ArgumentException("No options may be specified.");

            // The maximum likelihood estimator for p is the
            // number of successes over the number of trials

            int successes = 0;
            for (int i = 0; i < observations.Length; i++)
                if (observations[i] == 1) successes++;
            this.probability = successes / (double)numberOfTrials;
        }

        /// <summary>
        ///   Creates a new object that is a copy of the current instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A new object that is a copy of this instance.
        /// </returns>
        /// 
        public override object Clone()
        {
            return new BinomialDistribution(numberOfTrials, probability);
        }
    }

}
