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
    using Accord.Math.Optimization;

    /// <summary>
    ///   Quasi-Newton (L-BFGS) learning algorithm for <see cref="HiddenConditionalRandomField{T}">
    ///   Hidden Conditional Hidden Fields</see>.
    /// </summary>
    /// 
    public class QuasiNewtonHiddenLearning<T> : BaseHiddenConditionalRandomFieldLearning<T>, IHiddenConditionalRandomFieldLearning<T>
    {

        private BroydenFletcherGoldfarbShanno lbfgs;

        /// <summary>
        ///   Constructs a new L-BFGS learning algorithm.
        /// </summary>
        /// 
        public QuasiNewtonHiddenLearning(HiddenConditionalRandomField<T> model)
            : base(model)
        {
            lbfgs = new BroydenFletcherGoldfarbShanno(model.Function.Weights.Length);
            lbfgs.Tolerance = 1e-3;
            lbfgs.Function = base.Objective;
            lbfgs.Gradient = base.Gradient;
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

            try
            {
                lbfgs.Minimize(Model.Function.Weights);
            }
            catch (LineSearchFailedException)
            {
                // TODO: Restructure LBFGS to avoid exceptions.
            }

            Model.Function.Weights = lbfgs.Solution;

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
