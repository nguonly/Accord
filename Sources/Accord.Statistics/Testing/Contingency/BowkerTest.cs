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
    ///   Bowker test of symmetry for contingency tables.
    /// </summary>
    /// 
    [Serializable]
    public class BowkerTest : HypothesisTest, IHypothesisTest<ChiSquareDistribution>
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
        ///   Creates a new Bowker test.
        /// </summary>
        /// 
        /// <param name="matrix">The contingency table to test.</param>
        /// 
        public BowkerTest(GeneralConfusionMatrix matrix)
        {
            int classes = matrix.Classes;
            int[,] n = matrix.Matrix;

            double Qb = 0;

            for (int j = 0; j < classes; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    double q = (n[i, j] - n[j, i]);
                    Qb += (q * q) / (n[i, j] + n[j, i]);
                }
            }

            this.Statistic = Qb;
            this.distribution = new ChiSquareDistribution((classes * (classes - 1)) / 2);
            this.PValue = distribution.ComplementaryDistributionFunction(Statistic);
        }

    }
}
