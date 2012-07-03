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
    ///   Nakagami distribution.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   The Nakagami distribution has been used in the modelling of wireless
    ///   signal attenuation while traversing multiple paths. </para>
    /// <para>    
    ///   References:
    ///   <list type="bullet">
    ///     <item><description><a href="http://en.wikipedia.org/wiki/Nakagami_distribution">
    ///       Wikipedia, The Free Encyclopedia. Nakagami distribution. Available on:
    ///       http://en.wikipedia.org/wiki/Nakagami_distribution </a></description></item>
    ///     <item><description>
    ///       Laurenson, Dave (1994). "Nakagami Distribution". Indoor Radio Channel Propagation
    ///       Modelling by Ray Tracing Techniques. </description></item>
    ///     <item><description>  
    ///       R. Kolar, R. Jirik, J. Jan (2004) "Estimator Comparison of the Nakagami-m Parameter
    ///       and Its Application in Echocardiography", Radioengineering, 13 (1), 8–12 </description></item>
    ///   </list></para>
    /// </remarks>
    /// 
    [Serializable]
    public class NakagamiDistribution : UnivariateContinuousDistribution
    {
        // distribution parameters
        private double mu;
        private double omega;

        // derived values
        private double? mean;
        private double? variance;

        private double constant;
        private double nratio;
        private double twoMu1;


        /// <summary>
        ///   Initializes a new instance of the <see cref="NakagamiDistribution"/> class.
        /// </summary>
        /// 
        /// <param name="shape">The shape parameter μ.</param>
        /// <param name="spread">The spread parameter ω.</param>
        /// 
        public NakagamiDistribution(double shape, double spread)
        {
            if (shape < 0.5) throw new ArgumentOutOfRangeException("shape",
                "Shape parameter (mu) should be greater than or equal to 0.5.");
            if (spread <= 0) throw new ArgumentOutOfRangeException("spread",
                "Spread parameter (omega) should be greater than 0.");

            this.mu = shape;
            this.omega = spread;

            init(shape, spread);
        }

        private void init(double shape, double spread)
        {
            double twoMuMu = 2.0 * Math.Pow(shape, shape);
            double gammaMu = Gamma.Function(shape);
            double spreadMu = Math.Pow(spread, shape);
            nratio = -shape / spread;
            twoMu1 = 2.0 * shape - 1.0;

            constant = twoMuMu / (gammaMu * spreadMu);

            mean = null;
            variance = null;
        }

        /// <summary>
        ///   Gets the distribution's shape parameter mu.
        /// </summary>
        /// 
        /// <value>The shape parameter mu.</value>
        /// 
        public double Shape
        {
            get { return mu; }
        }

        /// <summary>
        ///   Gets the distribution's spread parameter omega.
        /// </summary>
        /// 
        /// <value>The spread parameter omega.</value>
        /// 
        public double Spread
        {
            get { return omega; }
        }

        /// <summary>
        ///   Gets the mean for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's mean value.</value>
        /// 
        public override double Mean
        {
            get
            {
                if (mean == null)
                    mean = (Gamma.Function(mu + 0.5) / Gamma.Function(mu)) * Math.Sqrt(omega / mu);
                return mean.Value;
            }
        }

        /// <summary>
        ///   Gets the variance for this distribution.
        /// </summary>
        /// 
        /// <value>The distribution's variance.</value>
        /// 
        public override double Variance
        {
            get
            {
                if (variance == null)
                {
                    double a = Gamma.Function(mu + 0.5) / Gamma.Function(mu);
                    variance = omega * (1.0 - (1.0 / mu) * (a * a));
                }
                return variance.Value;
            }
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
            return Gamma.LowerIncomplete(mu, (mu / omega) * (x * x));
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
            return constant * Math.Pow(x, twoMu1) * Math.Exp(nratio * x * x);
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
            return Math.Log(constant) + twoMu1 * Math.Log(x) + nratio * x * x;
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
            if (options != null)
                throw new ArgumentException("This method does not accept fitting options.");

            // R. Kolar, R. Jirik, J. Jan (2004) "Estimator Comparison of the
            // Nakagami-m Parameter and Its Application in Echocardiography", 
            // Radioengineering, 13 (1), 8–12

            double[] x2 = Matrix.ElementwisePower(observations, 2);

            double mean, var;
            if (weights == null)
            {
                mean = Statistics.Tools.Mean(x2);
                var = Statistics.Tools.Variance(x2);
            }
            else
            {
                mean = Statistics.Tools.WeightedMean(x2, weights);
                var = Statistics.Tools.WeightedVariance(x2, weights);
            }

            double shape = (mean * mean) / var;
            double spread = mean;

            init(shape, spread);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            return new NakagamiDistribution(mu, omega);
        }
    }
}
