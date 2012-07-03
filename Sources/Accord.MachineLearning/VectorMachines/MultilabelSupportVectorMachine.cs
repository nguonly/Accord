// Accord Machine Learning Library
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

namespace Accord.MachineLearning.VectorMachines
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading.Tasks;
    using Accord.Math;
    using Accord.Statistics.Kernels;


    /// <summary>
    ///   One-against-all Multi-label Kernel Support Vector Machine Classifier.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   The Support Vector Machine is by nature a binary classifier. Multiple label
    ///   problems are problems in which an input sample is allowed to belong to one
    ///   or more classes. A way to implement multi-label classes in support vector
    ///   machines is to build a one-against-all decision scheme where multiple SVMs
    ///   are trained to detect each of the available classes. </para>
    /// <para>
    ///   Currently this class supports only Kernel machines as the underlying classifiers.
    ///   If a Linear Support Vector Machine is needed, specify a Linear kernel in the
    ///   constructor at the moment of creation. </para>
    ///   
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description>
    ///       <a href="http://courses.media.mit.edu/2006fall/mas622j/Projects/aisen-project/index.html">
    ///        http://courses.media.mit.edu/2006fall/mas622j/Projects/aisen-project/index.html </a></description></item>
    ///     <item><description>
    ///       <a href="http://nlp.stanford.edu/IR-book/html/htmledition/multiclass-svms-1.html">
    ///        http://nlp.stanford.edu/IR-book/html/htmledition/multiclass-svms-1.html </a></description></item>
    ///     </list></para>
    ///     
    /// </remarks>
    ///
    /// <example>
    ///   <code>
    ///   // Sample data
    ///   //   The following is simple auto association function
    ///   //   where each input correspond to its own class. This
    ///   //   problem should be easily solved by a Linear kernel.
    ///
    ///   // Sample input data
    ///   double[][] inputs =
    ///   {
    ///       new double[] { 0 },
    ///       new double[] { 3 },
    ///       new double[] { 1 },
    ///       new double[] { 2 },
    ///   };
    ///   
    ///   // Outputs for each of the inputs
    ///   int[][] outputs =
    ///   {
    ///       new[] { -1,  1, -1 },
    ///       new[] { -1, -1,  1 },
    ///       new[] {  1,  1, -1 },
    ///       new[] { -1, -1, -1 },
    ///   };
    ///   
    ///   
    ///   // Create a new Linear kernel
    ///   IKernel kernel = new Linear();
    ///   
    ///   // Create a new Multi-class Support Vector Machine with one input,
    ///   //  using the linear kernel and for four disjoint classes.
    ///   var machine = new MultilabelSupportVectorMachine(1, kernel, 3);
    ///   
    ///   // Create the Multi-label learning algorithm for the machine
    ///   var teacher = new MultilabelSupportVectorLearning(machine, inputs, outputs);
    ///   
    ///   // Configure the learning algorithm to use SMO to train the
    ///   //  underlying SVMs in each of the binary class subproblems.
    ///   teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
    ///       new SequentialMinimalOptimization(svm, classInputs, classOutputs);
    ///   
    ///   // Run the learning algorithm
    ///   double error = teacher.Run();
    ///   </code>
    /// </example>
    ///
    /// <seealso cref="Learning.MultilabelSupportVectorLearning"/>
    ///
    [Serializable]
    public class MultilabelSupportVectorMachine : ISupportVectorMachine
    {

        // Underlying classifiers
        private KernelSupportVectorMachine[] machines;


        /// <summary>
        ///   Constructs a new Multi-label Kernel Support Vector Machine
        /// </summary>
        /// 
        /// <param name="kernel">The chosen kernel for the machine.</param>
        /// <param name="inputs">The number of inputs for the machine.</param>
        /// <param name="classes">The number of classes in the classification problem.</param>
        /// 
        /// <remarks>
        ///   If the number of inputs is zero, this means the machine
        ///   accepts a indefinite number of inputs. This is often the
        ///   case for kernel vector machines using a sequence kernel.
        /// </remarks>
        /// 
        public MultilabelSupportVectorMachine(int inputs, IKernel kernel, int classes)
        {
            if (classes <= 1)
                throw new ArgumentException("The machine must have at least two classes.", "classes");

            // Create the kernel machines
            machines = new KernelSupportVectorMachine[classes];
            for (int i = 0; i < machines.Length; i++)
                machines[i] = new KernelSupportVectorMachine(kernel, inputs);
        }

        /// <summary>
        ///   Constructs a new Multi-label Kernel Support Vector Machine
        /// </summary>
        /// 
        /// <param name="machines">
        ///   The machines to be used for each class.
        /// </param>
        /// 
        public MultilabelSupportVectorMachine(KernelSupportVectorMachine[] machines)
        {
            if (machines == null) throw new ArgumentNullException("machines");

            this.machines = machines;
        }


        /// <summary>
        ///   Gets the number of classes.
        /// </summary>
        /// 
        public int Classes
        {
            get { return machines.Length; }
        }

        /// <summary>
        ///   Gets the number of inputs of the machines.
        /// </summary>
        /// 
        public int Inputs
        {
            get { return machines[0].Inputs; }
        }

        /// <summary>
        ///   Gets a value indicating whether this machine produces probabilistic outputs.
        /// </summary>
        /// 
        /// <value>
        ///   <c>true</c> if this machine produces probabilistic outputs; otherwise, <c>false</c>.
        /// </value>
        /// 
        public bool IsProbabilistic
        {
            get { return machines[0].IsProbabilistic; }
        }

        /// <summary>
        ///   Gets the subproblems classifiers.
        /// </summary>
        /// 
        public KernelSupportVectorMachine[] Machines
        {
            get { return machines; }
        }


        /// <summary>
        ///   Computes the given input to produce the corresponding outputs.
        /// </summary>
        /// 
        /// <param name="inputs">An input vector.</param>
        /// <param name="responses">The model response for each class.</param>
        /// 
        /// <returns>The decision label for the given input.</returns>
        /// 
        public int[] Compute(double[] inputs, out double[] responses)
        {
            int[] labels = new int[machines.Length];
            double[] outputs = new double[machines.Length];

            // For each machine
            Parallel.For(0, machines.Length, i =>
            {
                labels[i] = machines[i].Compute(inputs, out outputs[i]);
            });

            responses = outputs;

            return labels;
        }

        /// <summary>
        ///   Computes the given input to produce the corresponding outputs.
        /// </summary>
        /// 
        /// <param name="inputs">An input vector.</param>
        /// 
        /// <returns>The decision label for the given input.</returns>
        /// 
        public int[] Compute(double[] inputs)
        {
            double[] responses;
            return Compute(inputs, out responses);
        }



        /// <summary>
        ///   Saves the machine to a stream.
        /// </summary>
        /// 
        /// <param name="stream">The stream to which the machine is to be serialized.</param>
        /// 
        public void Save(Stream stream)
        {
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(stream, this);
        }

        /// <summary>
        ///   Saves the machine to a file.
        /// </summary>
        /// 
        /// <param name="path">The path to the file to which the machine is to be serialized.</param>
        /// 
        public void Save(string path)
        {
            Save(new FileStream(path, FileMode.Create));
        }

        /// <summary>
        ///   Loads a machine from a stream.
        /// </summary>
        /// 
        /// <param name="stream">The stream from which the machine is to be deserialized.</param>
        /// 
        /// <returns>The deserialized machine.</returns>
        /// 
        public static MultilabelSupportVectorMachine Load(Stream stream)
        {
            BinaryFormatter b = new BinaryFormatter();
            return (MultilabelSupportVectorMachine)b.Deserialize(stream);
        }

        /// <summary>
        ///   Loads a machine from a file.
        /// </summary>
        /// 
        /// <param name="path">The path to the file from which the machine is to be deserialized.</param>
        /// 
        /// <returns>The deserialized machine.</returns>
        /// 
        public static MultilabelSupportVectorMachine Load(string path)
        {
            return Load(new FileStream(path, FileMode.Open));
        }


        #region ISupportVectorMachine Members

        /// <summary>
        ///   Computes the given input to produce the corresponding output.
        /// </summary>
        /// 
        /// <param name="inputs">An input vector.</param>
        /// <param name="output">The output for the given input.</param>
        /// 
        /// <returns>The decision label for the given input.</returns>
        /// 
        int ISupportVectorMachine.Compute(double[] inputs, out double output)
        {
            double[] responses;
            Compute(inputs, out responses);

            int imax;
            output = responses.Max(out imax);
            return imax;
        }

        #endregion
    }
}
