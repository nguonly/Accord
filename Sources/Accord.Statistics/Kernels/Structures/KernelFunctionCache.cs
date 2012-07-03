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

namespace Accord.Statistics.Kernels
{
    using System.Collections.Generic;

    /// <summary>
    ///   Cache for storing computations
    ///   of a kernel (Gram) matrix.
    /// </summary>
    /// 
    public class KernelFunctionCache
    {

        private int cacheSize;
        private int size;
        private int capacity;

        private Dictionary<int, double> data;

        private LinkedList<int> lruIndices;
        private Dictionary<int, LinkedListNode<int>> lruIndicesLookupTable;

        private double[] diagonal;

        private double[][] inputs;
        private IKernel kernel;

        private int misses;
        private int hits;

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="KernelFunctionCache"/> is enabled.
        /// </summary>
        /// 
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        /// 
        public bool Enabled { get; set; }

        /// <summary>
        ///   Gets the size of this cache.
        /// </summary>
        /// 
        /// <value>The size of this cache.</value>
        /// 
        public int CacheSize { get { return cacheSize; } }

        /// <summary>
        ///   Constructs a new <see cref="KernelFunctionCache"/>.
        /// </summary>
        /// 
        /// <param name="kernel">The kernel function.</param>
        /// <param name="inputs">The inputs values.</param>
        /// <param name="cacheSize">The size for the cache.</param>
        /// 
        public KernelFunctionCache(IKernel kernel, double[][] inputs, int cacheSize)
        {
            this.kernel = kernel;
            this.inputs = inputs;
            this.size = inputs.Length;

            if (cacheSize < 0)
            {
                Enabled = false;
                return;
            }
            else
            {
                Enabled = true;
            }

            if (cacheSize == 0)
                cacheSize = inputs.Length;


                this.cacheSize = cacheSize;
                this.capacity = (size * size) / 2 + 1;

                // Create lookup tables
                this.lruIndices = new LinkedList<int>();
                this.lruIndicesLookupTable = new Dictionary<int, LinkedListNode<int>>(capacity);

                // Create cache for off-diagonal elements
                this.data = new Dictionary<int, double>(capacity);

                // Create cache for diagonal elements
                this.diagonal = new double[inputs.Length];
                for (int i = 0; i < inputs.Length; i++)
                    this.diagonal[i] = kernel.Function(inputs[i], inputs[i]);
        }

        /// <summary>
        ///   Attempts to retrieve the value of the kernel function
        ///   from the diagonal of the kernel matrix. If the value
        ///   is not available, it is immediatelly computed and inserted
        ///   in the cache.
        /// </summary>
        /// 
        /// <param name="i">Index of the point to compute.</param>
        /// 
        /// <remarks>The result of the kernel function k(p[i], p[i]).</remarks>
        /// 
        public double GetOrCompute(int i)
        {
            if (!Enabled)
                return kernel.Function(inputs[i], inputs[i]);

            hits++; // Diagonal elements are always available

            return diagonal[i];
        }

        /// <summary>
        ///   Attempts to retrieve the kernel function evaluated between point at index i
        ///   and j. If it is not cached, it will be computed and the cache will be updated.
        /// </summary>
        /// 
        /// <param name="i">The index of the first point <c>p</c> to compute.</param>
        /// <param name="j">The index of the second point <c>p</c> to compute.</param>
        /// 
        /// <remarks>The result of the kernel function k(p[i], p[j]).</remarks>
        /// 
        public double GetOrCompute(int i, int j)
        {
            if (!Enabled)
                return kernel.Function(inputs[i], inputs[j]);

            if (i == j)
                return diagonal[i];

            if (j > i)
            {
                int t = i;
                i = j;
                j = t;
            }

            int key = (i * (i - 1)) / 2 + j;

            double value;

            // Check if the data is in the cache
            if (!data.TryGetValue(key, out value))
            {
                // It is not. Compute the function and update
                value = kernel.Function(inputs[i], inputs[j]);

                // Save evaluation
                data[key] = value;

                // Register the use of the variable in the LRU list
                lruIndicesLookupTable[key] = lruIndices.AddLast(key);

                misses++;

                // If we are over capacity,
                if (data.Count > capacity)
                {
                    // Remove the cached item which is
                    // at the end of the LRU list, and
                    data.Remove(lruIndices.First.Value);

                    // Remove the index for the item from the LRU list.
                    lruIndicesLookupTable.Remove(lruIndices.First.Value);
                    lruIndices.RemoveFirst();
                }
            }
            else
            {
                // It is. Update the LRU list to 
                // indicate the item has been used.

                LinkedListNode<int> node = lruIndicesLookupTable[key];

                // Remove from middle
                lruIndices.Remove(node);
                lruIndicesLookupTable.Remove(key);

                // Insert at the and and update the lookup table
                lruIndicesLookupTable[key] = lruIndices.AddLast(key);

                hits++;
            }

            return value;
        }

        /// <summary>
        ///   Clears the cache.
        /// </summary>
        /// 
        public void Clear()
        {
            if (data != null)
            {
                data.Clear();
                lruIndices.Clear();
                lruIndicesLookupTable.Clear();
            }
        }

    }

}
