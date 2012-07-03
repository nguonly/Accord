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
    using Accord.Math.Optimization;
    using Accord.Statistics.Distributions.Fitting;
    using AForge;

    /// <summary>
    ///   Cauchy-Lorentz distribution.
    /// </summary>
    /// 
    [Serializable]
    public class CauchyDistribution : UnivariateContinuousDistribution,
        IFittableDistribution<double, CauchyOptions>
    {

        // Distribution parameters
        private double location;
        private double scale;

        // Derived measures
        private double lnconstant;
        private double constant;

        private bool immutable;


        /// <summary>
        ///   Constructs a Cauchy-Lorentz distribution
        ///   with location parameter 0 and scale 1.
        /// </summary>
        /// 
        public CauchyDistribution() : this(0, 1) { }


        /// <summary>
        ///   Constructs a Cauchy-Lorentz distribution
        ///   with given location and scale parameters.
        /// </summary>
        /// 
        /// <param name="location">The location parameter x0.</param>
        /// <param name="scale">The scale parameter gamma.</param>
        /// 
        public CauchyDistribution(double location, double scale)
        {
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("scale", "Scale must be greater than zero.");

            init(location, scale);
        }

        private void init(double location, double scale)
        {
            this.location = location;
            this.scale = scale;

            this.constant = 1.0 / (Math.PI * scale);
            this.lnconstant = -Math.Log(Math.PI * scale);
        }

        /// <summary>
        ///   Gets the distribution's 
        ///   location parameter x0.
        /// </summary>
        /// 
        public double Location
        {
            get { return location; }
        }

        /// <summary>
        ///   Gets the distribution's
        ///   scale parameter gamma.
        /// </summary>
        /// 
        public double Scale
        {
            get { return scale; }
        }

        /// <summary>
        ///   Gets the median for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's median value.</value>
        /// 
        public override double Median
        {
            get { return location; }
        }

        /// <summary>
        ///   Gets the mode for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's mode value.</value>
        /// 
        public override double Mode
        {
            get { return location; }
        }


        /// <summary>
        ///   Cauchy's mean is undefined.
        /// </summary>
        /// 
        /// <value>Undefined.</value>
        /// 
        public override double Mean
        {
            get { return Double.NaN; }
        }

        /// <summary>
        ///   Cauchy's variance is undefined.
        /// </summary>
        /// 
        /// <value>Undefined.</value>
        /// 
        public override double Variance
        {
            get { return Double.NaN; }
        }

        /// <summary>
        ///   Gets the entropy for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's entropy.</value>
        /// 
        public override double Entropy
        {
            get { return Math.Log(scale) + Math.Log(4.0 * Math.PI); }
        }

        /// <summary>
        ///   Gets the cumulative distribution function (cdf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        /// <remarks>
        ///   The Cumulative Distribution Function (CDF) describes the cumulative
        ///   probability that a given value or any value smaller than it will occur.
        /// </remarks>
        /// 
        public override double DistributionFunction(double x)
        {
            return 1.0 / Math.PI * Math.Atan2(x - location, scale) + 0.5;
        }

        /// <summary>
        ///   Gets the probability density function (pdf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The probability of <c>x</c> occurring
        ///   in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Density Function (PDF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        public override double ProbabilityDensityFunction(double x)
        {
            double z = (x - location) / scale;
            return constant / (1.0 + z * z);
        }

        /// <summary>
        ///   Gets the log-probability density function (pdf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The logarithm of the probability of <c>x</c>
        ///   occurring in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The Probability Density Function (PDF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        public override double LogProbabilityDensityFunction(double x)
        {
            double z = (x - location) / scale;
            return lnconstant - Math.Log(1.0 + z * z);
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
        public override void Fit(double[] observations, double[] weights, IFittingOptions options)
        {
            CauchyOptions cauchyOptions = options as CauchyOptions;
            if (options != null && cauchyOptions == null)
                throw new ArgumentException("The specified options' type is invalid.", "options");

            Fit(observations, weights, cauchyOptions);
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
        public void Fit(double[] observations, double[] weights, CauchyOptions options)
        {
            if (immutable)
                throw new InvalidOperationException("This object can not be modified.");

            if (weights != null)
                throw new ArgumentException("This distribution does not support weighted samples.");

            bool useMLE = true;
            bool estimateT = true;
            bool estimateS = true;

            if (options != null)
            {
                useMLE = options.MaximumLikelihood;
                estimateT = options.EstimateLocation;
                estimateS = options.EstimateScale;
            }


            double t = location;
            double s = scale;

            int n = observations.Length;


            DoubleRange range;
            double median = Accord.Statistics.Tools.Quartiles(observations, out range, alreadySorted: false);

            if (estimateT)
                t = median;

            if (estimateS)
                s = range.Length;


            if (useMLE)
            {
                // Minimize the log-likelihood through numerical optimization
                BroydenFletcherGoldfarbShanno lbfgs = new BroydenFletcherGoldfarbShanno(2);


                // Define the negative log-likelihood function,
                // which is the objective we want to minimize:
                lbfgs.Function = (parameters) =>
                {
                    // Assume location is the first
                    // parameter, shape is the second
                    if (estimateT) t = parameters[0];
                    if (estimateS) s = parameters[1];

                    if (s < 0) s = -s;

                    double sum = 0;
                    for (int i = 0; i < observations.Length; i++)
                    {
                        double y = (observations[i] - t);
                        sum += Math.Log(s * s + y * y);
                    }

                    return -(n * Math.Log(s) - sum - n * Math.Log(Math.PI));
                };


                lbfgs.Gradient = (parameters) =>
                {
                    // Assume location is the first
                    // parameter, shape is the second
                    if (estimateT) t = parameters[0];
                    if (estimateS) s = parameters[1];

                    double sum1 = 0, sum2 = 0;
                    for (int i = 0; i < observations.Length; i++)
                    {
                        double y = (observations[i] - t);
                        sum1 += y / (s * s + y * y);
                        sum2 += s / (s * s + y * y);
                    }

                    double dt = -2.0 * sum1;
                    double ds = +2.0 * sum2 - n / s;

                    double[] g = new double[2];
                    g[0] = estimateT ? dt : 0;
                    g[1] = estimateS ? ds : 0;

                    return g;
                };


                // Initialize using the sample median as starting
                // value for location, and half interquartile range
                // for shape.

                double[] values = { t, s };

                // Minimize
                double error = lbfgs.Minimize(values);

                // Check solution
                t = lbfgs.Solution[0];
                s = lbfgs.Solution[1];
            }


            init(t, s); // Become the new distribution
        }

        /// <summary>
        ///   Gets the Standard Cauchy Distribution,
        ///   with zero location and unitary shape.
        /// </summary>
        /// 
        public static CauchyDistribution Standard { get { return standard; } }

        private static readonly CauchyDistribution standard = new CauchyDistribution() { immutable = true };


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
            return new CauchyDistribution(location, scale);
        }
    }
}
