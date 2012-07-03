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

namespace Accord.Statistics.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.ComponentModel;

    /// <summary>
    ///   Codification Filter class.
    /// </summary>
    /// <remarks>
    ///   The codification filter performs an integer codification of classes in
    ///   given in a string form. An unique integer identifier will be assigned
    ///   for each of the string classes.
    /// </remarks>
    /// 
    [Serializable]
    public class Codification : BaseFilter<Codification.Options>, IAutoConfigurableFilter
    {

        /// <summary>
        ///   Creates a new Codification Filter.
        /// </summary>
        /// 
        public Codification()
        {
        }

        /// <summary>
        ///   Creates a new Codification Filter.
        /// </summary>
        /// 
        public Codification(DataTable data)
        {
            this.Detect(data);
        }

        /// <summary>
        ///   Gets options, including mappings and dictionaries
        ///   associated with a given variable (data column).
        /// </summary>
        /// 
        /// <param name="columnName">The name of the variable.</param>
        /// 
        /// <returns>
        ///   An <see cref="Options"/> object listing the main
        ///   properties, such as number of possible symbols and
        ///   symbol-value mapping for the given column.</returns>
        /// 
        public Options this[string columnName]
        {
            get { return Columns[columnName]; }
        }

        /// <summary>
        ///   Gets options, including mappings and dictionaries
        ///   associated with a given variable (data column).
        /// </summary>
        /// 
        /// <param name="index">The column's index for the variable.</param>
        /// 
        /// <returns>
        ///   An <see cref="Options"/> object listing the main
        ///   properties, such as number of possible symbols and
        ///   symbol-value mapping for the given column.</returns>
        /// 
        public Options this[int index]
        {
            get { return Columns[index]; }
        }

        /// <summary>
        ///   Translates a value of a given variable
        ///   into its integer (codeword) representation.
        /// </summary>
        /// 
        /// <param name="columnName">The name of the variable's data column.</param>
        /// <param name="value">The value to be translated.</param>
        /// 
        /// <returns>An integer which uniquely identifies the given value
        /// for the given variable.</returns>
        /// 
        public int Translate(string columnName, string value)
        {
            return Columns[columnName].Mapping[value];
        }

        /// <summary>
        ///   Translates an array of values into their
        ///   integer representation, assuming values
        ///   are given in original order of columns.
        /// </summary>
        /// 
        /// <param name="data">The values to be translated.</param>
        /// 
        /// <returns>An array of integers in which each value
        /// uniquely identifies the given value for each of
        /// the variables.</returns>
        /// 
        public int[] Translate(params string[] data)
        {
            int[] result = new int[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < Columns.Count; j++)
                {
                    Options options = this.Columns[i];
                    if (options.Mapping.TryGetValue(data[i], out result[i]))
                        break;
                }
            }

            return result;
        }

        /// <summary>
        ///   Translates a value of the given variables
        ///   into their integer (codeword) representation.
        /// </summary>
        /// 
        /// <param name="columnNames">The names of the variable's data column.</param>
        /// <param name="values">The values to be translated.</param>
        /// 
        /// <returns>An array of integers in which each integer
        /// uniquely identifies the given value for the given 
        /// variables.</returns>
        /// 
        public int[] Translate(string[] columnNames, string[] values)
        {
            int[] result = new int[values.Length];

            for (int i = 0; i < columnNames.Length; i++)
            {
                Options options = this.Columns[columnNames[i]];
                result[i] = options.Mapping[values[i]];
            }

            return result;
        }

        /// <summary>
        ///   Translates an integer (codeword) representation of
        ///   the value of a given variable into its original
        ///   value.
        /// </summary>
        /// 
        /// <param name="columnName">The name of the variable's data column.</param>
        /// <param name="codeword">The codeword to be translated.</param>
        /// 
        /// <returns>The original meaning of the given codeword.</returns>
        /// 
        public string Translate(string columnName, int codeword)
        {
            Options options = this.Columns[columnName];
            foreach (var pair in options.Mapping)
            {
                if (pair.Value == codeword)
                    return pair.Key;
            }

            return null;
        }

        /// <summary>
        ///   Translates the integer (codeword) representations of
        ///   the values of the given variables into their original
        ///   values.
        /// </summary>
        /// 
        /// <param name="columnNames">The name of the variables' columns.</param>
        /// <param name="codewords">The codewords to be translated.</param>
        /// 
        /// <returns>The original meaning of the given codewords.</returns>
        /// 
        public string[] Translate(string[] columnNames, int[] codewords)
        {
            string[] result = new string[codewords.Length];

            for (int i = 0; i < columnNames.Length; i++)
            {
                Options options = this.Columns[columnNames[i]];
                foreach (var pair in options.Mapping)
                {
                    if (pair.Value == codewords[i])
                    {
                        result[i] = pair.Key;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///   Processes the current filter.
        /// </summary>
        /// 
        protected override DataTable ProcessFilter(DataTable data)
        {
            // Copy only the schema (Clone)
            DataTable result = data.Clone();

            // For each column having a mapping
            foreach (Options options in Columns)
            {
                // Change its type from string to integer
                result.Columns[options.ColumnName].DataType = typeof(int);
            }


            // Now for each row on the original table
            foreach (DataRow inputRow in data.Rows)
            {
                // We'll import to the result table
                DataRow resultRow = result.NewRow();

                // For each column in original table
                foreach (DataColumn column in data.Columns)
                {
                    string name = column.ColumnName;

                    // If the column has a mapping
                    if (Columns.Contains(name))
                    {
                        var map = Columns[name].Mapping;

                        // Retrieve string value
                        string label = inputRow[name] as string;

                        // Get its corresponding integer
                        int value = map[label];

                        // Set the row to the integer
                        resultRow[name] = value;
                    }
                    else
                    {
                        // The column does not have a mapping
                        //  so we'll just copy the value over
                        resultRow[name] = inputRow[name];
                    }
                }

                // Finally, add the row into the result table
                result.Rows.Add(resultRow);
            }

            return result;
        }

        /// <summary>
        ///   Auto detects the filter options by analyzing a given <see cref="System.Data.DataTable"/>.
        /// </summary> 
        ///  
        public void Detect(DataTable data)
        {
            foreach (DataColumn column in data.Columns)
            {
                // If the column has string type
                if (column.DataType == typeof(String))
                {
                    // We'll create a mapping
                    string name = column.ColumnName;
                    var map = new Dictionary<string, int>();
                    Columns.Add(new Options(name, map));

                    // Do a select distinct to get distinct values
                    DataTable d = data.DefaultView.ToTable(true, name);

                    // For each distinct value, create a corresponding integer
                    for (int i = 0; i < d.Rows.Count; i++)
                    {
                        // And register the String->Integer mapping
                        map.Add(d.Rows[i][0] as string, i);
                    }
                }
            }
        }

        /// <summary>
        ///   Options for processing a column.
        /// </summary>
        /// 
        [Serializable]
        public class Options : ColumnOptionsBase
        {
            /// <summary>
            ///   Gets or sets the label mapping for translating
            ///   integer labels to the original string labels.
            /// </summary>
            /// 
            public Dictionary<string, int> Mapping { get; private set; }

            /// <summary>
            ///   Gets the number of symbols used to code this variable.
            /// </summary>
            /// 
            public int Symbols { get { return Mapping.Count; } }

            /// <summary>
            ///   Constructs a new Options object for the given column.
            /// </summary>
            /// 
            /// <param name="name">
            ///   The name of the column to create this options for.
            /// </param>
            /// 
            public Options(String name)
                : base(name)
            {
                this.Mapping = new Dictionary<string, int>();
            }

            /// <summary>
            ///   Constructs a new Options object for the given column.
            /// </summary>
            /// 
            /// <param name="name">
            ///   The name of the column to create this options for.
            /// </param>
            /// 
            /// <param name="map">The initial mapping for this column.</param>
            /// 
            public Options(String name, Dictionary<string, int> map)
                : base(name)
            {
                this.Mapping = map;
            }

            /// <summary>
            ///   Constructs a new Options object.
            /// </summary>
            /// 
            public Options()
                : this("New column")
            {

            }
        }
    }
}
