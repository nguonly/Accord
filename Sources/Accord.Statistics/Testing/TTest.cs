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
    public enum TTestHypotesis
    {
        /// <summary>
        ///   Tests if the sample's mean is significantly
        ///   different than the hypothesized mean value.
        /// </summary>
        /// 
        MeanIsDifferentThanHypothesis,

        /// <summary>
        ///   Tests if the sample's mean is significantly
        ///   greater than the hypothesized mean value.
        /// </summary>
        /// 
        MeanIsGreaterThanHypothesis,

        /// <summary>
        ///   Tests if the sample's mean is significantly
        ///   smaller than the hypothesized mean value.
        /// </summary>
        MeanIsSmallerThanHypothesis,
    }

    /// <summary>
    ///   One-sample Student's T test.
    /// </summary>
    /// 
    /// <remarks>
    ///  <para>
    ///   The one-sample t-test assesses whether the mean of a sample is
    ///   statistically different from a hypothetized value.</para>
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
    /// <example>
    ///   <code>
    ///   // Consider a sample generated from a Gaussian
    ///   // distribution with mean 0.5 and unit variance.
    ///   
    ///   double[] sample = 
    ///   { 
    ///       -0.849886940156521,	3.53492346633185,  1.22540422494611, 0.436945126810344, 1.21474290382610,
    ///        0.295033941700225, 0.375855651783688, 1.98969760778547, 1.90903448980048,	1.91719241342961
    ///   };
    ///
    ///   // One may rise the hypothesis that the mean of the sample is not
    ///   // significantly different from zero. In other words, the fact that
    ///   // this particular sample has mean 0.5 may be attributed to chance.
    ///
    ///   double hypothesizedMean = 0;
    ///
    ///   // Create a T-Test to check this hypothesis
    ///   TTest test = new TTest(sample, hypothesizedMean,
    ///          TTestHypotesis.MeanIsDifferentThanHypothesis);
    ///
    ///   // Check if the mean is significantly different
    ///   test.Significant should be true
    ///
    ///   // Now, we would like to test if the sample mean is
    ///   // significantly greater than the hypothetised zero.
    ///
    ///   // Create a T-Test to check this hypothesis
    ///   TTest greater = new TTest(sample, hypothesizedMean,
    ///          TTestHypotesis.MeanIsGreaterThanHypothesis);
    ///
    ///   // Check if the mean is significantly larger
    ///   greater.Significant should be true
    ///
    ///   // Now, we would like to test if the sample mean is
    ///   // significantly smaller than the hypothetised zero.
    ///
    ///   // Create a T-Test to check this hypothesis
    ///   TTest smaller = new TTest(sample, hypothesizedMean,
    ///          TTestHypotesis.MeanIsSmallerThanHypothesis);
    ///
    ///   // Check if the mean is significantly smaller
    ///   smaller.Significant should be false
    ///   </code>
    /// </example>
    [Serializable]
    public class TTest : HypothesisTest, IHypothesisTest<TDistribution>
    {

        /// <summary>
        ///   Gets the probability distribution associated
        ///   with the test statistic.
        /// </summary>
        public TDistribution StatisticDistribution { get; private set; }

        /// <summary>
        ///   Tests the null hypothesis that the population mean is equal to a specified value.
        /// </summary>
        /// 
        /// <param name="sample">The data samples from which the test will be performed.</param>
        /// <param name="hypothesizedMean">The constant to be compared with the samples.</param>
        /// <param name="type">The type of hypothesis to test.</param>
        /// 
        public TTest(double[] sample, double hypothesizedMean, TTestHypotesis type)
        {
            int n = sample.Length;
            double x = Accord.Statistics.Tools.Mean(sample);
            double s = Accord.Statistics.Tools.StandardDeviation(sample, x);

            StatisticDistribution = new TDistribution(n - 1);
            Statistic = (x - hypothesizedMean) / (s / Math.Sqrt(n));


            if (type == TTestHypotesis.MeanIsDifferentThanHypothesis)
            {
                PValue = 2.0 * StatisticDistribution.ComplementaryDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.TwoTail;
            }
            else if (type == TTestHypotesis.MeanIsGreaterThanHypothesis)
            {
                PValue = StatisticDistribution.ComplementaryDistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneUpper;
            }
            else if (type == TTestHypotesis.MeanIsSmallerThanHypothesis)
            {
                PValue = StatisticDistribution.DistributionFunction(Statistic);
                Hypothesis = Testing.Hypothesis.OneLower;
            }
        }

    }

}
