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
    using Accord.Statistics.Distributions.Fitting;

    /// <summary>
    ///   Univariate general discrete distribution, also referred as the
    ///   Categorical distribution.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///   An univariate categorical distribution is a statistical distribution
    ///   whose variables can take on only discrete values. Each discrete value
    ///   defined within the interval of the distribution has an associated 
    ///   probability value indicating its frequency of occurrence.</para>
    ///  <para>
    ///   The discrete uniform distribution is a special case of a generic
    ///   discrete distribution whose probability values are constant.</para>
    /// </remarks>
    /// 
    [Serializable]
    public class GeneralDiscreteDistribution : UnivariateDiscreteDistribution,
        IFittableDistribution<double, GeneralDiscreteOptions>
    {

        // distribution parameters
        private int start;
        private double[] probabilities;

        // distribution measures
        private double? mean;
        private double? variance;
        private double? entropy;
        private int? mode;


        /// <summary>
        ///   Constructs a new generic discrete distribution.
        /// </summary>
        /// 
        /// <param name="start">
        ///   The integer value where the distribution starts, also
        ///   known as the offset value. Default value is 0.</param>
        /// <param name="probabilities">
        ///   The frequency of occurrence for each integer value in the
        ///   distribution. The distribution is assumed to begin in the
        ///   interval defined by start up to size of this vector.</param>
        ///   
        public GeneralDiscreteDistribution(int start, params double[] probabilities)
        {
            if (probabilities == null) throw new ArgumentNullException("probabilities");

            initialize(start, probabilities);
        }

        /// <summary>
        ///   Constructs a new uniform discrete distribution.
        /// </summary>
        /// 
        /// <param name="start">
        ///   The integer value where the distribution starts, also
        ///   known as the offset value. Default value is 0.</param>
        /// <param name="symbols">
        ///   The number of discrete values within the distribution.
        ///   The distribution is assumed to belong to the interval
        ///   [start, start + symbols].</param>
        ///   
        public GeneralDiscreteDistribution(int start, int symbols)
        {
            initialize(start, symbols);
        }

        /// <summary>
        ///   Constructs a new generic discrete distribution.
        /// </summary>
        /// 
        /// <param name="probabilities">
        ///   The frequency of occurrence for each integer value in the
        ///   distribution. The distribution is assumed to begin in the
        ///   interval defined by start up to size of this vector.</param>
        ///   
        public GeneralDiscreteDistribution(params double[] probabilities)
            : this(0, probabilities)
        {
        }

        /// <summary>
        ///   Constructs a new uniform discrete distribution.
        /// </summary>
        /// 
        /// <param name="symbols">
        ///   The number of discrete values within the distribution.
        ///   The distribution is assumed to belong to the interval
        ///   [start, start + symbols].</param>
        ///   
        public GeneralDiscreteDistribution(int symbols)
            : this(0, symbols)
        {
        }

        /// <summary>
        ///   Constructs a new uniform discrete distribution.
        /// </summary>
        /// 
        /// <param name="a">
        ///   The integer value where the distribution starts, also
        ///   known as <c>a</c>. Default value is 0.</param>
        /// <param name="b">
        ///   The integer value where the distribution ends, also 
        ///   known as <c>b</c>.</param>
        ///   
        public static GeneralDiscreteDistribution Uniform(int a, int b)
        {
            if (a > b)
                throw new ArgumentOutOfRangeException("b",
                    "The starting number a must be lower than b.");

            return new GeneralDiscreteDistribution(a, b - a + 1);
        }

        /// <summary>
        ///   Gets the integer value where the
        ///   discrete distribution starts.
        /// </summary>
        /// 
        public int Minimum
        {
            get { return start; }
        }

        /// <summary>
        ///   Gets the integer value where the
        ///   discrete distribution ends.
        /// </summary>
        /// 
        public int Maximum
        {
            get { return start + probabilities.Length; }
        }

        /// <summary>
        ///   Gets the number of symbols in the distribution.
        /// </summary>
        /// 
        public int Length
        {
            get { return probabilities.Length; }
        }

        /// <summary>
        ///   Gets the probabilities associated
        ///   with each discrete variable value.
        /// </summary>
        /// 
        public double[] Frequencies
        {
            get { return probabilities; }
        }

        /// <summary>
        ///   Gets the mean for this distribution.
        /// </summary>
        /// 
        public override double Mean
        {
            get
            {
                if (!mean.HasValue)
                {
                    mean = start;
                    for (int i = 0; i < probabilities.Length; i++)
                        mean += i * probabilities[i];
                }
                return mean.Value;
            }
        }

        /// <summary>
        ///   Gets the variance for this distribution.
        /// </summary>
        /// 
        public override double Variance
        {
            get
            {
                if (!variance.HasValue)
                {
                    variance = 0.0;
                    for (int i = 0; i < probabilities.Length; i++)
                    {
                        double d = i + start - mean.Value;
                        variance += probabilities[i] * (d * d);
                    }
                }
                return variance.Value;
            }
        }

        /// <summary>
        ///   Gets the mode for this distribution.
        /// </summary>
        /// 
        public override double Mode
        {
            get
            {
                if (!mode.HasValue)
                {
                    double max = 0;
                    int imax = 0;
                    for (int i = 0; i < probabilities.Length; i++)
                    {
                        if (probabilities[i] >= max)
                        {
                            max = probabilities[i];
                            imax = i;
                        }
                    }

                    mode = imax;
                }

                return mode.Value;
            }
        }


        /// <summary>
        ///   Gets the entropy for this distribution.
        /// </summary>
        /// 
        public override double Entropy
        {
            get
            {
                if (!entropy.HasValue)
                {
                    entropy = 0.0;
                    for (int i = 0; i < probabilities.Length; i++)
                        entropy -= probabilities[i] * System.Math.Log(probabilities[i]);
                }
                return entropy.Value;
            }
        }

        /// <summary>
        ///   Gets the probability density function (pdf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">
        ///   A single point in the distribution range. For a 
        ///   univariate distribution, this should be a single
        ///   double value. For a multivariate distribution,
        ///   this should be a double array.</param>
        ///   
        /// <remarks>
        ///   The Probability Density Function (PDF) describes the
        ///   probability that a given value <c>k</c> will occur.
        /// </remarks>
        /// 
        /// <returns>
        ///   The probability of <c>k</c> occurring
        ///   in the current distribution.</returns>
        ///   
        public override double DistributionFunction(int k)
        {
            int value = k - start;
            if (value < 0) return 0;
            if (value >= probabilities.Length) return 1.0;

            double sum = 0.0;
            for (int i = 0; i <= value; i++)
                sum += probabilities[i];

            return sum;
        }


        /// <summary>
        ///   Gets the probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">
        ///   A single point in the distribution range.</param>
        ///   
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        /// <returns>
        ///   The probability of <c>x</c> occurring
        ///   in the current distribution.</returns>
        ///   
        public override double ProbabilityMassFunction(int k)
        {
            int value = k - start;

            if (value < 0 || value >= probabilities.Length)
                return 0;

            return probabilities[value];
        }

        /// <summary>
        ///   Gets the log-probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="k">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The logarithm of the probability of <c>k</c>
        ///   occurring in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        public override double LogProbabilityMassFunction(int k)
        {
            int value = k - start;

            if (value < 0 || value >= probabilities.Length)
                return double.NegativeInfinity;

            return Math.Log(probabilities[value]);
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
        public override void Fit(double[] observations, double[] weights, IFittingOptions options)
        {
            GeneralDiscreteOptions discreteOptions = options as GeneralDiscreteOptions;
            if (options != null && discreteOptions == null)
                throw new ArgumentException("The specified options' type is invalid.", "options");

            Fit(observations, weights, discreteOptions);
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
        public void Fit(double[] observations, double[] weights, GeneralDiscreteOptions options)
        {
            double[] p = new double[probabilities.Length];

            if (weights == null)
            {
                for (int i = 0; i < observations.Length; i++)
                    p[(int)observations[i]]++;

                for (int i = 0; i < p.Length; i++)
                    p[i] /= observations.Length;
            }
            else
            {
                if (observations.Length != weights.Length)
                    throw new ArgumentException("The weight vector should have the same size as the observations", "weights");

                for (int i = 0; i < observations.Length; i++)
                {
                    int symbol = (int)observations[i];
                    p[symbol] += weights[i];
                }
            }

            if (options != null)
            {
                double sum = 0;
                for (int i = 0; i < p.Length; i++)
                {
                    if (p[i] == 0) p[i] = options.Minimum;
                    sum += p[i];
                }

                for (int i = 0; i < p.Length; i++)
                    p[i] /= sum;
            }

            initialize(0, p);
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
            GeneralDiscreteDistribution c = new GeneralDiscreteDistribution();

            c.probabilities = (double[])probabilities.Clone();
            c.start = start;
            c.mean = mean;
            c.entropy = entropy;
            c.variance = variance;

            return c;
        }


        private GeneralDiscreteDistribution()
        {
        }

        private void initialize(int s, double[] prob)
        {
            double sum = 0;
            for (int i = 0; i < prob.Length; i++)
                sum += prob[i];

            if (sum != 0 && sum != 1)
            {
                // assert that probabilities sum up to 1.
                for (int i = 0; i < prob.Length; i++)
                    prob[i] /= sum;
            }

            this.start = s;
            this.probabilities = prob;

            this.mean = null;
            this.variance = null;
            this.entropy = null;
        }

        private void initialize(int s, int symbols)
        {
            this.start = s;
            this.probabilities = new double[symbols];

            // Initialize with uniform distribution
            for (int i = 0; i < symbols; i++)
                probabilities[i] = 1.0 / symbols;

            this.mean = null;
            this.variance = null;
            this.entropy = null;
        }
    }
}
