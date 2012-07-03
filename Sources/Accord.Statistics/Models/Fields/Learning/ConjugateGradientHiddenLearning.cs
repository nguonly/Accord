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

namespace Accord.Statistics.Models.Fields.Learning
{
    using System;
    using System.ComponentModel;
    using Accord.Math.Optimization;

    /// <summary>
    ///   Conjugate Gradient learning algorithm for <see cref="HiddenConditionalRandomField{T}">
    ///   Hidden Conditional Hidden Fields</see>.
    /// </summary>
    /// 
    public class ConjugateGradientHiddenLearning<T> : BaseHiddenConditionalRandomFieldLearning<T>,
        IHiddenConditionalRandomFieldLearning<T>
    {

        private ConjugateGradient cg;

        /// <summary>
        ///   Gets or sets the tolerance threshold to detect convergence
        ///   of the log-likelihood function between two iterations. The
        ///   default value is 0 (run until convergence).
        /// </summary>
        /// 
        public double Tolerance
        {
            get { return cg.Tolerance; }
            set { cg.Tolerance = value; }
        }

        /// <summary>
        ///   Gets or sets the maximum number of iterations which
        ///   should be performed. The default is 0 (iterate until
        ///   convergence).
        /// </summary>
        /// 
        public int MaxIterations
        {
            get { return cg.MaxIterations; }
            set { cg.MaxIterations = value; }
        }

        /// <summary>
        ///   Gets the total number of iterations performed
        ///   by the conjugate gradient algorithm.
        /// </summary>
        /// 
        public int Iterations
        {
            get { return cg.Iterations; }
        }

        /// <summary>
        ///   Gets whether the model has converged
        ///   or if the line search has failed.
        /// </summary>
        /// 
        public bool Converged { get; private set; }

        /// <summary>
        ///   Occurs when the current learning progress has changed.
        /// </summary>
        /// 
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        ///   Constructs a new Conjugate Gradient learning algorithm.
        /// </summary>
        /// 
        public ConjugateGradientHiddenLearning(HiddenConditionalRandomField<T> model)
            : base(model)
        {
            cg = new ConjugateGradient(model.Function.Weights.Length);
            cg.Progress += new EventHandler<OptimizationProgressEventArgs>(cg_Progress);
            cg.Function = base.Objective;
            cg.Gradient = base.Gradient;
        }

        private void cg_Progress(object sender, OptimizationProgressEventArgs e)
        {
            int percentage;

            double ratio = e.GradientNorm / e.SolutionNorm;
            if (Double.IsNaN(ratio))
                percentage = 100;
            else
                percentage = (int)Math.Max(0, Math.Min(100, (1.0 - ratio) * 100));

            if (ProgressChanged != null)
                ProgressChanged(this, new ProgressChangedEventArgs(percentage, e));
        }

        /// <summary>
        ///   Runs the learning algorithm with the specified input
        ///   training observations and corresponding output labels.
        /// </summary>
        /// 
        /// <param name="observations">The training observations.</param>
        /// <param name="outputs">The observation's labels.</param>
        /// 
        public double RunEpoch(T[][] observations, int[] outputs)
        {
            this.Inputs = observations;
            this.Outputs = outputs;

            Converged = true;

            try
            {
                cg.Minimize(Model.Function.Weights);
            }
            catch (LineSearchFailedException)
            {
                // TODO: Restructure CG to avoid exceptions.
                Converged = false;
            }

            Model.Function.Weights = cg.Solution;

            // Return negative log-likelihood as error function
            return -Model.LogLikelihood(observations, outputs);
        }

        /// <summary>
        ///   Runs one iteration of the learning algorithm with the
        ///   specified input training observation and corresponding
        ///   output label.
        /// </summary>
        /// 
        /// <param name="observations">The training observations.</param>
        /// <param name="output">The observation's labels.</param>
        /// 
        /// <returns>The error in the last iteration.</returns>
        /// 
        public double Run(T[] observations, int output)
        {
            throw new NotSupportedException();
        }

    }
}
