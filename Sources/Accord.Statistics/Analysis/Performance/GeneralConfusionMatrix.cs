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

namespace Accord.Statistics.Analysis
{
    using System;
    using Accord.Math;

    /// <summary>
    ///   General confusion matrix for 
    ///   multi-class decision problems.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description>
    ///       <a href="http://uwf.edu/zhu/evr6930/2.pdf">
    ///       R.  G.  Congalton. A Review  of Assessing  the Accuracy  of Classifications 
    ///       of Remotely  Sensed  Data. Available on: http://uwf.edu/zhu/evr6930/2.pdf </a></description></item>
    ///     <item><description>
    ///       <a href="http://www.iiasa.ac.at/Admin/PUB/Documents/IR-98-081.pdf">
    ///       G. Banko. A Review of Assessing the Accuracy of Classiﬁcations of Remotely Sensed Data and
    ///       of Methods Including Remote Sensing Data in Forest Inventory. Interim report. Available on:
    ///       http://www.iiasa.ac.at/Admin/PUB/Documents/IR-98-081.pdf </a></description></item>
    ///     </list></para>  
    /// </remarks>
    /// 
    [Serializable]
    public class GeneralConfusionMatrix
    {

        private int[,] matrix;
        private int samples;
        private int classes;

        // Association measures
        private double? kappa;
        private double? kappaVariance;
        private double? kappaStdError;
        private double? tau;
        private double? chiSquare;

        private int[] rowSum;
        private int[] colSum;

        /// <summary>
        ///   Gets the confusion matrix, in which each element e_ij 
        ///   represents the number of elements from class i classified
        ///   as belonging to class j.
        /// </summary>
        /// 
        public int[,] Matrix
        {
            get { return matrix; }
        }

        /// <summary>
        ///   Gets the number of samples.
        /// </summary>
        /// 
        public int Samples
        {
            get { return samples; }
        }

        /// <summary>
        ///   Gets the number of classes.
        /// </summary>
        /// 
        public int Classes
        {
            get { return classes; }
        }

        /// <summary>
        ///   Creates a new Confusion Matrix.
        /// </summary>
        /// 
        public GeneralConfusionMatrix(int[,] matrix)
        {
            this.matrix = matrix;
            this.classes = matrix.GetLength(0);
            this.samples = matrix.Sum().Sum();
        }

        /// <summary>
        ///   Creates a new Confusion Matrix.
        /// </summary>
        /// 
        public GeneralConfusionMatrix(int classes, int[] expected, int[] predicted)
        {
            if (expected.Length != predicted.Length)
                throw new DimensionMismatchException("predicted",
                    "The number of expected and predicted observations must match.");

            this.samples = expected.Length;
            this.classes = classes;
            this.matrix = new int[classes, classes];

            // Each element ij represents the number of elements
            // from class i classified as belonging to class j.

            // For each classification,
            for (int k = 0; k < expected.Length; k++)
            {
                // Make sure the expected and predicted
                // values are from valid classes.

                int i = expected[k];
                int j = predicted[k];

                if (i < 0 || i >= classes)
                    throw new ArgumentOutOfRangeException("expected");

                if (j < 0 || j >= classes)
                    throw new ArgumentOutOfRangeException("predicted");


                matrix[i, j]++;
            }
        }

        /// <summary>
        ///   Gets the row totals.
        /// </summary>
        /// 
        public int[] RowTotals
        {
            get
            {
                if (rowSum == null)
                    rowSum = matrix.Sum(1);
                return rowSum;
            }
        }

        /// <summary>
        ///   Gets the column totals.
        /// </summary>
        /// 
        public int[] ColumnTotals
        {
            get
            {
                if (colSum == null)
                    colSum = matrix.Sum(0);
                return colSum;
            }
        }


        /// <summary>
        ///   Gets the Kappa coefficient of performance.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   References:
        ///   <list type="bullet">
        ///     <item><description>
        ///       CONGALTON, R.G.A., Review of assessing the accuracy of classifications of
        ///      remotely data, Remote sensing of the Environment, 37:35-46, 1991. </description></item>
        ///     </list></para>  
        /// </remarks>
        /// 
        /// 
        public double Kappa
        {
            get
            {
                if (kappa == null)
                {
                    int diagonalSum = matrix.Trace();

                    int directionSum = 0;
                    for (int i = 0; i < classes; i++)
                        directionSum += RowTotals[i] * ColumnTotals[i];

                    double p0 = diagonalSum / (double)samples;
                    double pr = directionSum / (double)(samples * samples);

                    kappa = (p0 - pr) / (1.0 - pr);
                }

                return kappa.Value;
            }
        }

        /// <summary>
        ///   Gets the standard error of the <see cref="Kappa"/>
        ///   coefficient of performance. 
        /// </summary>
        /// 
        public double StandardError
        {
            get
            {
                if (kappaStdError == null)
                {
                    computeKappaVariance();
                }

                return kappaStdError.Value;
            }
        }

        /// <summary>
        ///   Gets the variance of the <see cref="Kappa"/>
        ///   coefficient of performance. 
        /// </summary>
        /// 
        public double Variance
        {
            get
            {
                if (kappaVariance == null)
                {
                    computeKappaVariance();
                }

                return kappaVariance.Value;
            }
        }

        private void computeKappaVariance()
        {
            // References: Statistical Methods for Rates and Proportions

            int[,] m = matrix;
            double k = Kappa;

            double[] colMarginal = ColumnTotals.Divide((double)samples);
            double[] rowMarginal = RowTotals.Divide((double)samples);


            double directionSum = 0;
            for (int i = 0; i < classes; i++)
                directionSum += rowMarginal[i] * colMarginal[i];

            double pe = directionSum;


            // Compute A (eq. 18.16)
            double A = 0;
            for (int i = 0; i < classes; i++)
            {
                double u = 1 - (rowMarginal[i] + colMarginal[i]) * (1 - k);
                A += (m[i, i] / (double)samples) * u * u;
            }

            // Compute B (eq. 18.17)
            double sum = 0;
            for (int i = 0; i < rowMarginal.Length; i++)
                for (int j = 0; j < colMarginal.Length; j++)
                    if (i != j)
                        sum += (m[i, j] / (double)samples) * (colMarginal[i] + rowMarginal[j]);

            double B = (1 - k) * (1 - k) * sum;

            // Compute C
            double v = k - pe * (1 - k);
            double C = v * v;

            // Compute variance using A, B and C
            kappaVariance = (A + B - C) / ((1 - pe) * (1 - pe) * samples);

            // Compute standard error directly
            kappaStdError = Math.Sqrt(A + B - C) / ((1 - pe) * Math.Sqrt(samples));
        }


        /// <summary>
        ///   Gets the Tau coefficient of performance.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   References:
        ///   <list type="bullet">
        ///     <item><description>
        ///       MA, Z.; REDMOND, R. L. Tau coefficients for accuracy assessment of
        ///       classification of remote sensing data. </description></item>
        ///     </list></para>  
        /// </remarks>
        /// 
        public double Tau
        {
            get
            {
                if (tau == null)
                {
                    int N = samples;

                    int diagonalSum = 0;
                    for (int i = 0; i < classes; i++)
                        diagonalSum += matrix[i, i];


                    int directionSum = 0;
                    for (int i = 0; i < classes; i++)
                    {
                        // Compute the row sum for the class
                        int rowSum = 0;
                        for (int j = 0; j < classes; j++)
                            rowSum += matrix[i, j];

                        directionSum += matrix[i, i] * rowSum;
                    }

                    double p0 = diagonalSum / (double)N;
                    double pr = directionSum / (double)(N * N);

                    tau = (p0 - pr) / (1.0 - pr);
                }
                return tau.Value;
            }
        }

        /// <summary>
        ///   Phi coefficient.
        /// </summary>
        /// <remarks>
        ///   The Pearson correlation coefficient ranges from −1 to +1, where ±1 indicates
        ///   perfect agreement or disagreement, and 0 indicates no relationship. 
        ///   
        ///   References:
        ///     http://en.wikipedia.org/wiki/Phi_coefficient
        ///     http://www.psychstat.missouristate.edu/introbook/sbk28m.htm
        /// </remarks>
        /// 
        public double Phi
        {
            get { return Math.Sqrt(ChiSquare / samples); }
        }

        /// <summary>
        ///   Gets the Chi-Square statistic for the contingency table.
        /// </summary>
        /// 
        public double ChiSquare
        {
            get
            {
                if (chiSquare == null)
                {
                    double x = 0;
                    for (int i = 0; i < Classes; i++)
                    {
                        for (int j = 0; j < Classes; j++)
                        {
                            double e = (RowTotals[i] * ColumnTotals[j]) / (double)samples;
                            double o = matrix[i, j];

                            x += ((o - e) * (o - e)) / e;
                        }
                    }

                    chiSquare = x;
                }

                return chiSquare.Value;
            }
        }

        /// <summary>
        ///   Tschuprow's T association measure.
        /// </summary>
        /// <remarks>
        ///   Tschuprow's T is a measure of association between two nominal variables, giving 
        ///   a value between 0 and 1 (inclusive). It is closely related to <see cref="Cramer">
        ///   Cramér's V</see>, coinciding with it for square contingency tables. 
        ///   
        ///   References:
        ///     http://en.wikipedia.org/wiki/Tschuprow's_T
        /// </remarks>
        /// 
        public double Tschuprow
        {
            get { return Math.Sqrt(Phi / (samples * (classes - 1))); }
        }

        /// <summary>
        ///   Pearson's contingency coefficient C.
        /// </summary>
        /// 
        /// <remarks>
        ///   Pearson's C measures the degree of association between the two variables. However,
        ///   C suffers from the disadvantage that it does not reach a maximum of 1 or the minimum 
        ///   of -1; the highest it can reach in a 2 x 2 table is .707; the maximum it can reach in
        ///   a 4 × 4 table is 0.870. It can reach values closer to 1 in contingency tables with more
        ///   categories. It should, therefore, not be used to compare associations among tables with
        ///   different numbers of categories. For a improved version of C, see <see cref="Sakoda"/>.
        ///   
        ///   References:
        ///     http://en.wikipedia.org/wiki/Contingency_table
        /// </remarks>
        /// 
        public double Pearson
        {
            get { return Math.Sqrt(ChiSquare / (ChiSquare + samples)); }
        }

        /// <summary>
        ///   Sakoda's contingency coefficient V.
        /// </summary>
        /// 
        /// <remarks>
        ///   Sakoda's V is an adjusted version of <see cref="Pearson">Pearson's C</see>
        ///   so it reaches a maximum of 1 when there is complete association in a table
        ///   of any number of rows and columns. 
        ///   
        ///   References:
        ///     http://en.wikipedia.org/wiki/Contingency_table
        /// </remarks>
        /// 
        public double Sakoda
        {
            get { return Pearson / Math.Sqrt((classes - 1) / (double)classes); }
        }

        /// <summary>
        ///   Cramer's V association measure.
        /// </summary>
        /// 
        /// <remarks>
        ///   Cramér's V varies from 0 (corresponding to no association between the variables)
        ///   to 1 (complete association) and can reach 1 only when the two variables are equal
        ///   to each other. In practice, a value of 0.1 already provides a good indication that
        ///   there is substantive relationship between the two variables.
        ///   
        ///   References:
        ///    http://en.wikipedia.org/wiki/Cram%C3%A9r%27s_V
        ///    http://www.acastat.com/Statbook/chisqassoc.htm
        /// </remarks>
        /// 
        public double Cramer
        {
            get { return Math.Sqrt(ChiSquare / (samples * (classes - 1))); }
        }

        /// <summary>
        ///   Overall agreement.
        /// </summary>
        /// 
        /// <remarks>
        ///   The overall agreement is the sum of the diagonal elements
        ///   of the contigency table divided by the number of samples.
        /// </remarks>
        /// 
        public double OverralAgreement
        {
            get { return matrix.Trace() / (double)samples; }
        }

        /// <summary>
        ///   Chance agreement.
        /// </summary>
        /// 
        /// <remarks>
        ///   The chance agreement tells how many samples
        ///   were correctly classified by chance alone.
        /// </remarks>
        /// 
        public double ChanceAgreement
        {
            get
            {
                double chance = 0;
                for (int i = 0; i < classes; i++)
                    chance += RowTotals[i] * ColumnTotals[i];
                return 1.0 / (samples * samples) * chance;
            }
        }

    }
}
