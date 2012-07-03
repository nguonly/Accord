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
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///   Stochastic Gradient Descent learning algorithm for <see cref="HiddenConditionalRandomField{T}">
    ///   Hidden Conditional Hidden Fields</see>.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the observations.</typeparam>
    /// 
    public class GradientDescentHiddenLearning<T> : BaseHiddenConditionalRandomFieldLearning<T>, 
        IHiddenConditionalRandomFieldLearning<T>
    {
        private double learningRate = 100;
        private int iterations = 0;

        //private double decay = 0.9;
        //private double tau = 0.5;
        private double stepSize;

        private bool stochastic = true;
        private double[] gradient;
        

        private Object lockObj = new Object();

        /// <summary>
        ///   Gets or sets the learning rate to use as the gradient
        ///   descent step size. Default value is 1e-1.
        ///   
        /// </summary>
        public double LearningRate
        {
            get { return learningRate; }
            set { learningRate = value; }
        }


        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="GradientDescentHiddenLearning&lt;T&gt;"/>
        ///   should use stochastic gradient updates.
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
        ///   Initializes a new instance of the <see cref="GradientDescentHiddenLearning&lt;T&gt;"/> class.
        /// </summary>
        /// 
        /// <param name="model">The model to be trained.</param>
        /// 
        public GradientDescentHiddenLearning(HiddenConditionalRandomField<T> model)
            : base(model)
        {
            gradient = new double[Model.Function.Weights.Length];
        }

        /// <summary>
        ///   Resets the step size.
        /// </summary>
        /// 
        public void Reset()
        {
            iterations = 0;
            stepSize = 0;
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
                Parallel.For(0, observations.Length, i =>
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
                });

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
            stepSize = learningRate / iterations;

            // Update the model using a dynamic step size
            for (int i = 0; i < parameters.Length; i++)
            {
                if (Double.IsInfinity(parameters[i])) continue;

                parameters[i] -= stepSize * gradient[i];
            }

#if DEBUG
            for (int j = 0; j < parameters.Length; j++)
                if (Double.IsNaN(parameters[j]))
                    throw new Exception();
#endif

            return error;
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
            this.Inputs = new[] { observations };
            this.Outputs = new[] { output };

            double[] gradient = base.Gradient();
            double[] parameters = Model.Function.Weights;
            double stepSize = learningRate / iterations;

            // Update the model using a dynamic step size
            for (int i = 0; i < parameters.Length; i++)
            {
                if (Double.IsInfinity(parameters[i])) continue;

                parameters[i] -= stepSize * gradient[i];
            }

            return LastError;
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

    }
}
