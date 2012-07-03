// Accord Neural Net Library
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
    using System.Threading.Tasks;
    using System.Threading;
    using System.ComponentModel;

    /// <summary>
    ///   Resilient Gradient Learning.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the observations being modeled.</typeparam>
    ///
    public class ResilientGradientHiddenLearning<T> : BaseHiddenConditionalRandomFieldLearning<T>,
        IHiddenConditionalRandomFieldLearning<T>
    {

        private double initialStep = 0.0125;
        private double deltaMax = 50.0;
        private double deltaMin = 1e-6;
        private int iterations = 0;

        private double etaMinus = 0.5;
        private double etaPlus = 1.2;
        private bool stochastic = true;

        private double[] gradient;
        private double[] previousGradient;

        private Object lockObj = new Object();

        // update values, also known as deltas
        private double[] weightsUpdates;


        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="GradientDescentHiddenLearning&lt;T&gt;"/>
        ///   should use stochastic gradient updates. Default is true.
        /// </summary>
        /// 
        /// <value><c>true</c> for stochastic updates; otherwise, <c>false</c>.</value>
        /// 
        public bool Stochastic
        {
            get { return stochastic; }
            set { stochastic = value; }
        }

        /// <summary>
        ///   Occurs when the current learning progress has changed.
        /// </summary>
        /// 
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        ///   Gets or sets the maximum possible update step,
        ///   also referred as delta min. Default is 50.
        /// </summary>
        /// 
        public double UpdateUpperBound
        {
            get { return deltaMax; }
            set { deltaMax = value; }
        }

        /// <summary>
        ///   Gets or sets the minimum possible update step,
        ///   also referred as delta max. Default is 1e-6.
        /// </summary>
        /// 
        public double UpdateLowerBound
        {
            get { return deltaMin; }
            set { deltaMin = value; }
        }

        /// <summary>
        ///   Gets the decrease parameter, also 
        ///   referred as eta minus. Default is 0.5.
        /// </summary>
        /// 
        public double DecreaseFactor
        {
            get { return etaMinus; }
            set
            {
                if (value <= 0 || value >= 1)
                    throw new ArgumentOutOfRangeException("value", "Value should be between 0 and 1.");
                etaMinus = value;
            }
        }

        /// <summary>
        ///   Gets the increase parameter, also
        ///   referred as eta plus. Default is 1.2.
        /// </summary>
        /// 
        public double IncreaseFactor
        {
            get { return etaPlus; }
            set
            {
                if (value <= 1)
                    throw new ArgumentOutOfRangeException("value", "Value should be higher than 1.");
                etaPlus = value;
            }
        }


        /// <summary>
        ///   Initializes a new instance of the <see cref="ResilientGradientHiddenLearning{T}"/> class.
        /// </summary>
        /// 
        /// <param name="model">Model to teach.</param>
        /// 
        public ResilientGradientHiddenLearning(HiddenConditionalRandomField<T> model)
            : base(model)
        {
            int parameters = Model.Function.Weights.Length;
            gradient = new double[parameters];
            previousGradient = new double[parameters];
            weightsUpdates = new double[parameters];

            // Intialize steps
            Reset(initialStep);
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
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Runs the learning algorithm with the specified input
        ///   training observations and corresponding output labels.
        /// </summary>
        /// 
        /// <param name="observations">The training observations.</param>
        /// <param name="outputs">The observation's labels.</param>
        /// 
        /// <returns>The error in the last iteration.</returns>
        /// 
        public double RunEpoch(T[][] observations, int[] outputs)
        {

            iterations++;

            double error = 0;

            if (stochastic)
            {

                // In batch mode, we will use the average of the gradients
                // at each point as a better estimate of the true gradient.
                Array.Clear(gradient, 0, gradient.Length);

                int progress = 0;

                // For each training point
#if DEBUG
                for (int i = 0; i < observations.Length; i++)
#else
                Parallel.For(0, observations.Length, i =>
#endif
                {
                    base.Inputs = new[] { observations[i] };
                    base.Outputs = new[] { outputs[i] };

                    // Compute the estimated gradient
                    double[] estimate = base.Gradient();

                    lock (lockObj)
                    {
                        // Accumulate
                        for (int j = 0; j < estimate.Length; j++)
                            gradient[j] += estimate[j];
                        error += LastError;
                    }

                    int current = Interlocked.Increment(ref progress);
                    double percent = current / (double)observations.Length * 100.0;
                    OnProgressChanged(new ProgressChangedEventArgs((int)percent, i));
#if DEBUG
                    for (int j = 0; j < gradient.Length; j++)
                        if (Double.IsNaN(gradient[j]))
                            throw new Exception();
#endif

                }
#if !DEBUG
);
#endif

                // Compute the average gradient
                for (int i = 0; i < gradient.Length; i++)
                    gradient[i] /= observations.Length;
            }
            else
            {
                base.Inputs = observations;
                base.Outputs = outputs;

                // Compute the true gradient
                gradient = base.Gradient();

                error = LastError;
            }

            double[] parameters = Model.Function.Weights;

            for (int k = 0; k < Parameters.Length; k++)
            {
                if (Double.IsInfinity(parameters[k])) continue;

                double S = previousGradient[k] * gradient[k];

                if (S > 0.0)
                {
                    weightsUpdates[k] = Math.Min(weightsUpdates[k] * etaPlus, deltaMax);
                    parameters[k] -= Math.Sign(gradient[k]) * weightsUpdates[k];
                    previousGradient[k] = gradient[k];
                }
                else if (S < 0.0)
                {
                    weightsUpdates[k] = Math.Max(weightsUpdates[k] * etaMinus, deltaMin);
                    previousGradient[k] = 0.0;
                }
                else
                {
                    parameters[k] -= Math.Sign(gradient[k]) * weightsUpdates[k];
                    previousGradient[k] = gradient[k];
                }
            }

#if DEBUG
            for (int j = 0; j < Model.Function.Weights.Length; j++)
                if (Double.IsNaN(Model.Function.Weights[j]))
                    throw new Exception();
#endif

            return error;
        }

        /// <summary>
        ///   Raises the <see cref="E:ProgressChanged"/> event.
        /// </summary>
        /// 
        /// <param name="args">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        /// 
        protected void OnProgressChanged(ProgressChangedEventArgs args)
        {
            if (ProgressChanged != null)
                ProgressChanged(this, args);
        }

        /// <summary>
        ///   Resets the current update steps using the given learning rate.
        /// </summary>
        /// 
        public void Reset(double rate)
        {
            Parallel.For(0, weightsUpdates.Length, i =>
            {
                for (int j = 0; j < weightsUpdates.Length; j++)
                    weightsUpdates[i] = rate;
            });
        }

    }
}