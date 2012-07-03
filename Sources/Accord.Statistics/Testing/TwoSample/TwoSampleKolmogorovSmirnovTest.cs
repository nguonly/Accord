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
    using Accord.Math;
    using Accord.Statistics.Distributions.Univariate;

    /// <summary>
    ///   Test hypothesis for the two-sample
    ///   Kolmogorov-Smirnov tests.
    /// </summary>
    /// 
    public enum TwoSampleKolmogorovSmirnovTestHypothesis
    {
        /// <summary>
        ///   Tests whether samples have been drawn 
        ///   from significantly unequal distributions.
        /// </summary>
        /// 
        SamplesDistributionsAreUnequal,

        /// <summary>
        ///   Tests whether the distribution of one sample is
        ///   greater than the other, in a statistical sense.
        /// </summary>
        /// 
        FirstSampleIsLargerThanSecond,

        /// <summary>
        ///   Tests whether the distribution of one sample is
        ///   smaller than the other, in a statistical sense.
        /// </summary>
        /// 
        FirstSampleIsSmallerThanSecond,
    }

    /// <summary>
    ///   Two-sample Kolmogorov-Smirnov (KS) test.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   The Kolmogorov-Smirnov test tries to determine if two samples have been 
    ///   drawn from the same probability distribution. The Kolmogorov-Smirnov test
    ///   has an interesting advantage in which it does not requires any assumptions
    ///   about the data. The distribution of the K-S test statistic does not depend
    ///   on which distribution is being tested.</para>
    /// <para>
    ///   The K-S test has also the advantage of being an exact test (other tests, such as the
    ///   chi-square goodness-of-fit test depends on an adequate sample size). One disadvantage
    ///   is that it requires a fully defined distribution which should not have been estimated
    ///   from the data. If the parameters of the theoretical distribution have been estimated
    ///   from the data, the critical region of the K-S test will be no longer valid.</para>
    /// <para>
    ///   The two-sample KS test is one of the most useful and general nonparametric methods for
    ///   comparing two samples, as it is sensitive to differences in both location and shape of
    ///   the empirical cumulative distribution functions of the two samples.</para>
    /// <para>
    ///   This class uses an efficient and high-accuracy algorithm based on work by Richard
    ///   Simard (2010). Please see <see cref="KolmogorovSmirnovDistribution"/> for more details.</para>
    ///   
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description><a href="http://en.wikipedia.org/wiki/Kolmogorov%E2%80%93Smirnov_test">
    ///       Wikipedia, The Free Encyclopedia. Kolmogorov-Smirnov Test. 
    ///       Available on: http://en.wikipedia.org/wiki/Kolmogorov%E2%80%93Smirnov_test </a></description></item>
    ///     <item><description><a href="http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm">
    ///       NIST/SEMATECH e-Handbook of Statistical Methods. Kolmogorov-Smirnov Goodness-of-Fit Test.
    ///       Available on: http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm </a></description></item>
    ///     <item><description><a href="http://www.iro.umontreal.ca/~lecuyer/myftp/papers/ksdist.pdf">
    ///       Richard Simard, Pierre L’Ecuyer. Computing the Two-Sided Kolmogorov-Smirnov Distribution.
    ///       Journal of Statistical Software. Volume VV, Issue II. Available on:
    ///       http://www.iro.umontreal.ca/~lecuyer/myftp/papers/ksdist.pdf </a></description></item>
    ///   </list></para>
    /// </remarks>
    /// 
    [Serializable]
    public class TwoSampleKolmogorovSmirnovTest : HypothesisTest, IHypothesisTest<KolmogorovSmirnovDistribution>
    {

        /// <summary>
        ///   Gets the first empirical distribution being tested.
        /// </summary>
        /// 
        public EmpiricalDistribution EmpiricalDistribution1 { get; private set; }

        /// <summary>
        ///   Gets the second empirical distribution being tested.
        /// </summary>
        /// 
        public EmpiricalDistribution EmpiricalDistribution2 { get; private set; }

        /// <summary>
        ///   Gets the distribution associated with the test statistic.
        /// </summary>
        /// 
        public KolmogorovSmirnovDistribution StatisticDistribution { get; private set; }


        /// <summary>
        ///   Creates a new Two-Sample Kolmogorov test.
        /// </summary>
        /// 
        /// <param name="sample1">The first sample.</param>
        /// <param name="sample2">The second sample.</param>
        /// 
        public TwoSampleKolmogorovSmirnovTest(double[] sample1, double[] sample2)
            : this(sample1, sample2, TwoSampleKolmogorovSmirnovTestHypothesis.SamplesDistributionsAreUnequal)
        {
        }

        /// <summary>
        ///   Creates a new Two-Sample Kolmogorov test.
        /// </summary>
        /// 
        /// <param name="sample1">The first sample.</param>
        /// <param name="sample2">The second sample.</param>
        /// <param name="hypothesis">The hypothesis to test.</param>
        /// 
        public TwoSampleKolmogorovSmirnovTest(double[] sample1, double[] sample2,
            TwoSampleKolmogorovSmirnovTestHypothesis hypothesis)
        {

            int n1 = sample1.Length;
            int n2 = sample2.Length;

            // Compute the effective number of parameters (degrees of freedom)
            int n = (n1 * n2) / (n1 + n2);

            // Create the test statistic distribution with given d. f.
            StatisticDistribution = new KolmogorovSmirnovDistribution(n);

            // Create the two empirical distributions from the data
            EmpiricalDistribution1 = new EmpiricalDistribution(sample1, 0);
            EmpiricalDistribution2 = new EmpiricalDistribution(sample2, 0);

            // Merge the samples to measure the empirical CDFs in same steps
            double[] Y = new double[n1 + n2 + 1];
            double[] D = new double[Y.Length];

            Y[0] = Double.NegativeInfinity;
            for (int i = 0; i < sample1.Length; i++) Y[i + 1] = sample1[i];
            for (int i = 0; i < sample2.Length; i++) Y[i + n1 + 1] = sample2[i];

            // Sort the samples
            Array.Sort(Y);

            // Create the two empirical CDFs
            Func<double, double> F = EmpiricalDistribution1.DistributionFunction;
            Func<double, double> G = EmpiricalDistribution2.DistributionFunction;

            // Finally, compute the test statistic and perform actual testing.
            if (hypothesis == TwoSampleKolmogorovSmirnovTestHypothesis.SamplesDistributionsAreUnequal)
            {
                // Test if the samples have been drawn from different distributions.

                // This is a correction on the common formulation found in many places
                //  such as in Wikipedia. Please see the Engineering Statistics Handbook,
                //  section "1.3.5.16. Kolmogorov-Smirnov Goodness-of-Fit Test" for more
                //  details: http://www.itl.nist.gov/div898/handbook/eda/section3/eda35g.htm

                for (int i = 0; i < Y.Length - 1; i++)
                    D[i] = Math.Max(Math.Abs(F(Y[i]) - G(Y[i + 1])), Math.Abs(F(Y[i]) - G(Y[i])));

                Statistic = D.Max(); // This is the two-sided "Dn" statistic.
                PValue = StatisticDistribution.ComplementaryDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.TwoTail;
            }
            else if (hypothesis == TwoSampleKolmogorovSmirnovTestHypothesis.FirstSampleIsLargerThanSecond)
            {
                // Test if the first sample's distribution is significantly "larger"
                // than the second sample's distribution, in a statistical sense.

                for (int i = 0; i < Y.Length - 1; i++)
                    D[i] = Math.Max(F(Y[i]) - G(Y[i + 1]), F(Y[i]) - G(Y[i]));

                Statistic = D.Max(); // This is the one-sided "Dn+" statistic.
                PValue = StatisticDistribution.OneSideDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneUpper;
            }
            else
            {
                // Test if the first sample's distribution is significantly "smaller"
                // than the second sample's distribution, in a statistical sense.

                for (int i = 0; i < Y.Length - 1; i++)
                    D[i] = Math.Max(G(Y[i + 1]) - F(Y[i]), G(Y[i]) - F(Y[i]));

                Statistic = D.Max(); // This is the one-sided "Dn-" statistic.
                PValue = StatisticDistribution.OneSideDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneLower;
            }
        }


    }
}