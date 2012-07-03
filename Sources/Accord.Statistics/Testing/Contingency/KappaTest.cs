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

namespace Accord.Statistics.Testing
{
    using System;
    using Accord.Statistics.Analysis;
    using Accord.Statistics.Distributions.Univariate;
    using AForge;

    /// <summary>
    ///   Kappa Test for agreement in contingency tables.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    ///   The Kappa test tries to assert whether the Kappa measure of a
    ///   a contingency table, is significantly different from another 
    ///   hypothetized value. </para>
    ///   
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description>
    ///       Ientilucci, Emmett (2006). "On Using and Computing the Kappa Statistic".
    ///       Available on: http://www.cis.rit.edu/~ejipci/Reports/On_Using_and_Computing_the_Kappa_Statistic.pdf </description></item>
    ///    </list></para>
    /// </remarks>
    ///
    [Serializable]
    public class KappaTest : HypothesisTest, IHypothesisTest<NormalDistribution>
    {

        private double stdError;
        private double variance;
        private double k;
        private DoubleRange confidence;

        /// <summary>
        ///   Kappa coefficient being tested.
        /// </summary>
        /// 
        public double Kappa
        {
            get { return k; }
        }

        /// <summary>
        ///   Variance of the Kappa
        ///   coefficient for the test.
        /// </summary>
        /// 
        public double Variance
        {
            get { return variance; }
        }

        /// <summary>
        ///   Standard error of the Kappa
        ///   coefficient for the test.
        /// </summary>
        /// 
        public double StandardError
        {
            get { return stdError; }
        }

        /// <summary>
        ///   Confidence Interval (CI) 
        ///   for the test statistic.
        /// </summary>
        /// 
        public DoubleRange Confidence
        {
            get { return confidence; }
        }

        /// <summary>
        ///   Gets the distribution associated
        ///   with the test statistic.
        /// </summary>
        /// 
        public NormalDistribution StatisticDistribution
        {
            get { return NormalDistribution.Standard; }
        }

        /// <summary>
        ///   Creates a new Kappa test.
        /// </summary>
        /// 
        /// <param name="matrix">The contingency table to test.</param>
        /// <param name="type">The type of hypothesis to test.</param>
        /// 
        public KappaTest(GeneralConfusionMatrix matrix, Hypothesis type = Hypothesis.TwoTail)
        {
            this.k = matrix.Kappa;

            this.variance = matrix.Variance;
            this.stdError = matrix.StandardError;

            confidence = new DoubleRange(k - 1.9599 * stdError, k + 1.9599 * stdError);

            this.Statistic = k / stdError;
            this.Hypothesis = type;

            if (this.Hypothesis == Hypothesis.TwoTail)
            {
                this.PValue = 2.0 * NormalDistribution.Standard.
                      DistributionFunction(-Math.Abs(Statistic));
            }
            else
            {
                this.PValue = NormalDistribution.Standard.
                      DistributionFunction(-Math.Abs(Statistic));
            }
        }


    }
}
