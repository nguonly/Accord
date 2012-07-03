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
    ///   F (Fisher-Snedecor) distribution.
    /// </summary>
    /// 
    [Serializable]
    public class FDistribution : UnivariateContinuousDistribution
    {

        // Distribution parameters
        private int d1;
        private int d2;

        // Derived values
        private double b;

        private double? mean;
        private double? variance;


        /// <summary>
        ///   Constructs a F-distribution with
        ///   the given degrees of freedom.
        /// </summary>
        /// 
        /// <param name="degrees1">The first degree of freedom.</param>
        /// <param name="degrees2">The second degree of freedom.</param>
        /// 
        public FDistribution(int degrees1, int degrees2)
        {
            if (degrees1 <= 0) throw new ArgumentOutOfRangeException("degrees1", "Degrees of freedom must be positive.");
            if (degrees2 <= 0) throw new ArgumentOutOfRangeException("degrees1", "Degrees of freedom must be positive.");

            this.d1 = degrees1;
            this.d2 = degrees2;

            this.b = Beta.Function(degrees1 * 0.5, degrees2 * 0.5);
        }

        /// <summary>
        ///   Gets the first degree of freedom.
        /// </summary>
        /// 
        public int DegreesOfFreedom1
        {
            get { return d1; }
        }

        /// <summary>
        ///   Gets the second degree of freedom.
        /// </summary>
        /// 
        public int DegreesOfFreedom2
        {
            get { return d2; }
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
                    if (d2 <= 2)
                    {
                        mean = Double.NaN;
                    }
                    else
                    {
                        mean = d2 / (d2 - 2.0);
                    }
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
                    if (d2 <= 4)
                    {
                        variance = Double.NaN;
                    }
                    else
                    {
                        variance = (2.0 * d2 * d2 * (d1 + d2 - 2)) /
                            (d1 * (d2 - 2) * (d2 - 2) * (d2 - 4));
                    }
                }

                return variance.Value;
            }
        }

        /// <summary>
        ///   Gets the entropy for this distribution.
        /// </summary>
        /// 
        public override double Entropy
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        ///   Gets the cumulative distribution function (cdf) for
        ///   the F-distribution evaluated at point <c>x</c>.
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
            double u = (d1 * x) / (d1 * x + d2);
            return Beta.Incomplete(d1 * 0.5, d2 * 0.5, u);
        }

        /// <summary>
        ///   Gets the complementary cumulative distribution
        ///   function evaluated at point <c>x</c>.
        /// </summary>
        /// 
        public override double ComplementaryDistributionFunction(double x)
        {
            double u = d1 / (d1 * x + d2);
            return Beta.Incomplete(d2 * 0.5, d1 * 0.5, u);
        }

        /// <summary>
        ///   Gets the inverse of the cumulative distribution function (icdf) for
        ///   this distribution evaluated at probability <c>p</c>. This function
        ///   is also known as the Quantile function.
        /// </summary>
        /// 
        /// <remarks>
        ///   The Inverse Cumulative Distribution Function (ICDF) specifies, for
        ///   a given probability, the value which the random variable will be at,
        ///   or below, with that probability.
        /// </remarks>
        /// 
        public override double InverseDistributionFunction(double p)
        {
            // Cephes Math Library Release 2.8:  June, 2000
            // Copyright 1984, 1987, 1995, 2000 by Stephen L. Moshier
            // Adapted under the LGPL with permission of original author.

            if (p <= 0.0 || p > 1.0)
                throw new ArgumentOutOfRangeException("p", "Input must be between zero and one.");


            double d1 = this.d1;
            double d2 = this.d2;

            double x;

            double w = Beta.Incomplete(0.5 * d2, 0.5 * d1, 0.5);

            if (w > p || p < 0.001)
            {
                w = Beta.IncompleteInverse(0.5 * d1, 0.5 * d2, p);
                x = d2 * w / (d1 * (1.0 - w));
            }
            else
            {
                w = Beta.IncompleteInverse(0.5 * d2, 0.5 * d1, 1.0 - p);
                x = (d2 - d2 * w) / (d1 * w);
            }

            return x;
        }

        /// <summary>
        ///   Gets the probability density function (pdf) for
        ///   the F-distribution evaluated at point <c>x</c>.
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
            double u = Math.Pow(d1 * x, d1) * Math.Pow(d2, d2) /
                Math.Pow(d1 * x + d2, d1 + d2);
            return Math.Sqrt(u) / (x * b);
        }

        /// <summary>
        /// Gets the log-probability density function (pdf) for
        /// this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// <param name="x">A single point in the distribution range.</param>
        /// <returns>
        /// The logarithm of the probability of <c>x</c>
        /// occurring in the current distribution.
        /// </returns>
        /// <remarks>
        /// The Probability Density Function (PDF) describes the
        /// probability that a given value <c>x</c> will occur.
        /// </remarks>
        public override double LogProbabilityDensityFunction(double x)
        {
            double lnu = d1 * Math.Log(d1 * x) + d2 * Math.Log(d2) -
                (d1 + d2) * Math.Log(d1 * x + d2);
            return 0.5 * lnu - Math.Log(x * b);
        }


        /// <summary>
        ///   Not available.
        /// </summary>
        /// 
        public override void Fit(double[] observations, double[] weights, IFittingOptions options)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///   Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///   A new object that is a copy of this instance.
        /// </returns>
        /// 
        public override object Clone()
        {
            return new FDistribution(d1, d2);
        }
    }
}
