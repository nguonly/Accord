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
    using Accord.Statistics.Distributions.Univariate;


    /// <summary>
    ///   Test Hypothesis for the T-Test.
    /// </summary>
    /// 
    public enum TwoSampleTTestHypotesis
    {
        /// <summary>
        ///   Test if the first sample's mean if just different
        ///   than the second sample's mean, without making any
        ///   assumptions if it is greater or smaller.
        /// </summary>
        /// 
        FirstSampleMeanIsDifferentThanSecondSampleMean,

        /// <summary>
        ///   Test if the first sample's mean is "greater" than
        ///   the second sample's mean.
        /// </summary>
        /// 
        FirstSampleMeanIsGreaterThanSecondSampleMean,

        /// <summary>
        ///   Test if the second sample's mean is "smaller"
        ///   than the first sample's mean.
        /// </summary>
        /// 
        FirstSampleMeanIsSmallerThanSecondSampleMean,
    }

    /// <summary>
    ///   Two-sample Student's T test.
    /// </summary>
    /// 
    /// <remarks>
    ///  <para>
    ///   The two-sample t-test assesses whether the means of two groups are statistically 
    ///   different from each other.</para>
    ///   
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description><a href="http://en.wikipedia.org/wiki/Student's_t-test">
    ///       Wikipedia, The Free Encyclopedia. Student's T-Test. </a></description></item>
    ///     <item><description><a href="http://www.le.ac.uk/bl/gat/virtualfc/Stats/ttest.html">
    ///       William M.K. Trochim. The T-Test. Research methods Knowledge Base, 2009. 
    ///       Available on: http://www.le.ac.uk/bl/gat/virtualfc/Stats/ttest.html </a></description></item>
    ///     <item><description><a href="http://en.wikipedia.org/wiki/One-way_ANOVA">
    ///       Graeme D. Ruxton. The unequal variance t-test is an underused alternative to Student's
    ///       t-test and the Mann–Whitney U test. Oxford Journals, Behavioral Ecology Volume 17, Issue 4, pp.
    ///       688-690. 2006. Available on: http://beheco.oxfordjournals.org/content/17/4/688.full </a></description></item>
    ///   </list></para>
    /// </remarks>
    /// 
    [Serializable]
    public class TwoSampleTTest : HypothesisTest, IHypothesisTest<TDistribution>
    {

        /// <summary>
        ///   Gets whether the test assumes equal sample variance.
        /// </summary>
        /// 
        public bool AssumeEqualVariance { get; private set; }

        /// <summary>
        ///   Gets whether the test assumes equal sample size.
        /// </summary>
        /// 
        public bool EqualSampleSize { get; private set; }

        /// <summary>
        ///   Gets the probability distribution associated
        ///   with the test statistic.
        /// </summary>
        /// 
        public TDistribution StatisticDistribution { get; private set; }

        /// <summary>
        ///   Tests whether the means of two samples are different.
        /// </summary>
        /// 
        public TwoSampleTTest(double[] sample1, double[] sample2, bool assumeEqualVariances, TwoSampleTTestHypotesis type)
        {
            // References: http://en.wikipedia.org/wiki/Student's_t-test#Worked_examples

            AssumeEqualVariance = assumeEqualVariances;
            EqualSampleSize = sample1.Length == sample2.Length;

            double x1 = Tools.Mean(sample1);
            double x2 = Tools.Mean(sample2);

            double s1 = Tools.Variance(sample1);
            double s2 = Tools.Variance(sample2);

            int n1 = sample1.Length;
            int n2 = sample2.Length;

            int df;

            if (AssumeEqualVariance)
            {
                if (EqualSampleSize)
                {
                    // Samples have the same size and assume same variance.
                    double Sp = Math.Sqrt(0.5 * (s1 + s2));
                    Statistic = (x1 - x2) / (Sp * Math.Sqrt(2.0 / n1));

                    df = 2 * n1 - 2;
                }
                else
                {
                    // Samples have unequal sizes, but assume same variance.
                    double Sp = Statistics.Tools.PooledVariance(sample1, sample2);
                    Statistic = (x1 - x2) / (Sp * Math.Sqrt(1.0 / n1 + 1.0 / n2));

                    df = n1 + n2 - 2;
                }
            }
            else
            {
                // Unequal sample sizes, assume nothing about variance.
                double Sd = Math.Sqrt(s1 / n1 + s2 / n2);
                Statistic = (x1 - x2) / Sd;

                double r1 = s1 / n1, r2 = s2 / n2;
                df = (int)(((r1 + r2) * (r1 + r2)) / ((r1 * r1) / (n1 - 1) + (r2 * r2) / (n2 - 1)));
            }


            StatisticDistribution = new TDistribution(df);

            if (type == TwoSampleTTestHypotesis.FirstSampleMeanIsDifferentThanSecondSampleMean)
            {
                PValue = 2.0 * StatisticDistribution.ComplementaryDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.TwoTail;
            }
            else if (type == TwoSampleTTestHypotesis.FirstSampleMeanIsGreaterThanSecondSampleMean)
            {
                PValue = StatisticDistribution.ComplementaryDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneUpper;
            }
            else if (type == TwoSampleTTestHypotesis.FirstSampleMeanIsSmallerThanSecondSampleMean)
            {
                PValue = StatisticDistribution.DistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneLower;
            }
        }


    }
}