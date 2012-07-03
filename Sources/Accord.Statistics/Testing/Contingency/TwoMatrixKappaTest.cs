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
    ///   Kappa Test for two contingency tables.
    /// </summary>
    ///
    /// <remarks>
    ///   <para>
    ///   The two-matrix Kappa test tries to assert whether the Kappa measure 
    ///   of two contingency tables, each of which created by a different rater
    ///   or classification model, differs significantly. </para>
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
    public class TwoMatrixKappaTest : HypothesisTest, IHypothesisTest<NormalDistribution>
    {

        private double k1;
        private double variance1;

        private double k2;
        private double variance2;

        private double k;
        private double variance;
        private double stdError;
        private DoubleRange confidence;

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
        ///   Gets the summed Kappa variance
        ///   for the two contigency tables.
        /// </summary>
        /// 
        public double Variance
        {
            get { return variance; }
        }

        /// <summary>
        ///   Gets the standard error
        ///   for the test statistic.
        /// </summary>
        /// 
        public double StandardError
        {
            get { return stdError; }
        }

        /// <summary>
        ///   Gets the confidence interval
        ///   for the test statistic.
        /// </summary>
        /// 
        public DoubleRange Confidence
        {
            get { return confidence; }
        }

        /// <summary>
        ///   Creates a new Two-Table Kappa test.
        /// </summary>
        /// 
        /// <param name="kappa1">The kappa value for the first contingency table to test.</param>
        /// <param name="kappa2">The kappa value for the second contingency table to test.</param>
        /// <param name="var1">The variance of the kappa value for the first contingency table to test.</param>
        /// <param name="var2">The variance of the kappa value for the second contingency table to test.</param>
        /// <param name="type">The type of hypothesis to test.</param>
        /// 
        public TwoMatrixKappaTest(double kappa1, double var1, double kappa2, double var2, Hypothesis type = Hypothesis.TwoTail)
        {
            this.k1 = kappa1;
            this.k2 = kappa2;

            this.variance1 = var1;
            this.variance2 = var2;

            this.Hypothesis = type;

            compute();
        }

        /// <summary>
        ///   Creates a new Two-Table Kappa test.
        /// </summary>
        /// 
        /// <param name="matrix1">The first contingency table to test.</param>
        /// <param name="matrix2">The second contingency table to test.</param>
        /// <param name="type">The type of hypothesis to test.</param>
        /// 
        public TwoMatrixKappaTest(GeneralConfusionMatrix matrix1, GeneralConfusionMatrix matrix2, Hypothesis type = Hypothesis.TwoTail)
        {
            this.k1 = matrix1.Kappa;
            this.k2 = matrix2.Kappa;

            this.variance1 = matrix1.Variance;
            this.variance2 = matrix2.Variance;

            this.Hypothesis = type;

            compute();
        }

        private void compute()
        {
            this.k = Math.Abs(k1 - k2);
            this.variance = variance1 + variance2;
            this.stdError = Math.Sqrt(variance1 + variance2);

            this.Statistic = k / stdError;

            confidence = new DoubleRange(k - 1.9599 * stdError, k + 1.9599 * stdError);


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
