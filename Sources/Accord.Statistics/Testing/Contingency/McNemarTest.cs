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

    /// <summary>
    ///   McNemar test of homogeneity for <c>2 x 2</c> contigency tables.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   McNemar's test is a non-parametric method used on nominal data. It is applied to 
    ///   2 × 2 contingency tables with a dichotomous trait, with matched pairs of subjects,
    ///   to determine whether the row and column marginal frequencies are equal, i.e. if
    ///   the contingency table presents marginal homogeneity.</para>
    ///
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description>
    ///       Wikipedia contributors, "McNemar's test," Wikipedia, The Free Encyclopedia,
    ///       Available on: http://http://en.wikipedia.org/wiki/McNemar's_test. </description></item>
    ///    </list></para>
    /// </remarks>
    ///
    [Serializable]
    public class McNemarTest : HypothesisTest, IHypothesisTest<ChiSquareDistribution>
    {

        private ChiSquareDistribution distribution;


        /// <summary>
        ///   Gets the degrees of freedom for the Chi-Square distribution.
        /// </summary>
        /// 
        public int DegreesOfFreedom
        {
            get { return distribution.DegreesOfFreedom; }
        }

        /// <summary>
        ///   Gets the distribution associated
        ///   with the test statistic.
        /// </summary>
        /// 
        public ChiSquareDistribution StatisticDistribution
        {
            get { return distribution; }
        }

        /// <summary>
        ///   Creates a new McNemar test.
        /// </summary>
        /// 
        /// <param name="matrix">The contingency table to test.</param>
        /// <param name="yatesCorrection">True to use Yate's correction of
        ///   continuity, falser otherwise. Default is false.</param>
        /// 
        public McNemarTest(ConfusionMatrix matrix, bool yatesCorrection = false)
        {
            int a = matrix.TruePositives;
            int b = matrix.FalseNegatives;
            int c = matrix.FalsePositives;
            int d = matrix.TrueNegatives;

            double u = (yatesCorrection) ? Math.Abs(b - c) - 0.5 : u = b - c;

            this.Statistic = (u * u) / (b + c);

            this.distribution = new ChiSquareDistribution(1);

            this.PValue = distribution.ComplementaryDistributionFunction(Statistic);
        }

    }
}
