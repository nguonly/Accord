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

namespace Accord.Statistics.Distributions.Multivariate
{
    using System;
    using Accord.Statistics.Distributions.Fitting;

    /// <summary>
    ///   Univariate generic discrete distribution, also referred as the
    ///   Categorical distribution.
    /// </summary>
    /// <remarks>
    ///  <para>
    ///   An univariate categorical distribution is a statistical distribution
    ///   whose variables can take on only discrete values. Each discrete value
    ///   defined within the interval of the distribution has an associated 
    ///   probability value indicating its frequency of occurrence.</para>
    ///  <para>
    ///   The discrete uniform distribution is a special case of a generic
    ///   discrete distribution whose probability values are constant.</para>
    /// </remarks>
    /// 
    [Serializable]
    public class JointDistribution : MultivariateDiscreteDistribution
        //IFittableDistribution<int[], GeneralDiscreteOptions>
    {

        // distribution parameters
        private double[] probabilities;

        private int[] symbols;
        private int[] positions;

        /// <summary>
        ///   Gets the frequency of observation of each discrete variable.
        /// </summary>
        /// 
        public double[] Frequencies
        {
            get { return probabilities; }
        }

        /// <summary>
        ///   Gets the number of symbols for each discrete variable.
        /// </summary>
        /// 
        public int[] Symbols
        {
            get { return symbols; }
        }

        /// <summary>
        ///   Constructs a new joint discrete distribution.
        /// </summary>
        ///   
        public JointDistribution(int[] symbols)
            : base(symbols.Length)
        {
            this.symbols = symbols;

            int total = 1;
            for (int i = 0; i < symbols.Length; i++)
                total *= symbols[i];

            this.probabilities = new double[total];
            for (int i = 0; i < probabilities.Length; i++)
                probabilities[i] = 1.0 / total;

            this.positions = new int[symbols.Length];
            positions[positions.Length - 1] = 1;
            for (int i = positions.Length - 2; i >= 0; i--)
                positions[i] = positions[i + 1] * symbols[i + 1];
        }




        /// <summary>
        ///   Gets the probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// <param name="x">
        ///   A single point in the distribution range.</param>
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// <returns>
        ///   The probability of <c>x</c> occurring
        ///   in the current distribution.</returns>
        ///   
        public override double ProbabilityMassFunction(int[] x)
        {
            int index = 0;
            for (int i = 0; i < x.Length; i++)
                index += x[i] * positions[i];

            return probabilities[index];
        }

        /// <summary>
        ///   Gets the log-probability mass function (pmf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// <param name="x">A single point in the distribution range.</param>
        /// <returns>
        ///   The logarithm of the probability of <c>x</c>
        ///   occurring in the current distribution.
        /// </returns>
        /// <remarks>
        ///   The Probability Mass Function (PMF) describes the
        ///   probability that a given value <c>x</c> will occur.
        /// </remarks>
        /// 
        public override double LogProbabilityMassFunction(int[] x)
        {
            int index = 0;
            for (int i = 0; i < x.Length; i++)
                index += x[i] * positions[i];

            return Math.Log(probabilities[index]);
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
        public override void Fit(double[][] observations, double[] weights, IFittingOptions options)
        {
            if (options != null)
                throw new ArgumentException("This method does not accept fitting options.");

            if (observations.Length != weights.Length)
                throw new ArgumentException("The weight vector should have the same size as the observations", "weights");

            for (int i = 0; i < probabilities.Length; i++)
                probabilities[i] = 0;

            for (int i = 0; i < observations.Length; i++)
            {
                double[] x = observations[i];

                int index = 0;
                for (int j = 0; j < x.Length; j++)
                    index += (int)x[j] * positions[j];

                probabilities[index] += weights[i];
            }


            double sum = 0;
            for (int i = 0; i < probabilities.Length; i++)
                sum += probabilities[i];

            if (sum != 0 && sum != 1)
            {
                // avoid locking a parameter in zero.
                // if (num == 0) num = 1e-10;

                // assert that probabilities sum up to 1.
                for (int i = 0; i < probabilities.Length; i++)
                    probabilities[i] /= sum;
            }
        }



        /// <summary>
        /// Gets the mean for this distribution.
        /// </summary>
        /// <value>
        /// An array of double-precision values containing
        /// the mean values for this distribution.
        /// </value>
        public override double[] Mean
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the mean for this distribution.
        /// </summary>
        /// <value>
        /// An array of double-precision values containing
        /// the variance values for this distribution.
        /// </value>
        public override double[] Variance
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the variance for this distribution.
        /// </summary>
        /// <value>
        /// An multidimensional array of double-precision values
        /// containing the covariance values for this distribution.
        /// </value>
        public override double[,] Covariance
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the cumulative distribution function (cdf) for
        /// this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// <param name="x">A single point in the distribution range.</param>
        /// <returns></returns>
        /// <remarks>
        /// The Cumulative Distribution Function (CDF) describes the cumulative
        /// probability that a given value or any value smaller than it will occur.
        /// </remarks>
        public override double DistributionFunction(int[] x)
        {
            throw new NotSupportedException();
        }

        private JointDistribution(int dimension)
            : base(dimension)
        {
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            JointDistribution d = new JointDistribution(base.Dimension);
            d.positions = (int[])this.positions.Clone();
            d.probabilities = (double[])this.probabilities.Clone();
            d.symbols = (int[])this.symbols.Clone();

            return d;
        }
    }
}
