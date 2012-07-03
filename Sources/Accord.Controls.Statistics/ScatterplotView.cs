// Accord Statistics Controls Library
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

namespace Accord.Controls
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Accord.Math;
    using Accord.Statistics.Visualizations;
    using ZedGraph;

    /// <summary>
    ///   Scatterplot visualization control.
    /// </summary>
    /// 
    public partial class ScatterplotView : UserControl
    {

        private object dataSource;
        private Scatterplot scatterplot;

        private string xAxisDataMember;
        private string yAxisDataMember;
        private string labelDataMember;

        private CurveList classes;


        #region Constructor
        /// <summary>
        ///   Constructs a new instance of the ScatterplotView.
        /// </summary>
        public ScatterplotView()
        {
            InitializeComponent();

            scatterplot = new Scatterplot();

            classes = new CurveList();

            zedGraphControl.GraphPane.Title.Text = "Scatter Plot";
            zedGraphControl.GraphPane.XAxis.Title.Text = "X";
            zedGraphControl.GraphPane.YAxis.Title.Text = "Y";
            zedGraphControl.GraphPane.Fill = new Fill(Color.WhiteSmoke);
            zedGraphControl.GraphPane.CurveList = classes;
        }
        #endregion


        #region Properties
        /// <summary>
        ///   Gets the underlying scatterplot being shown by this control.
        /// </summary>
        /// 
        public Scatterplot Scatterplot { get { return scatterplot; } }

        /// <summary>
        ///   Gets or sets a data source for this control.
        /// </summary>
        /// 
        [DefaultValue(null)]
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;

                if (!this.DesignMode)
                    OnDataBind();
            }
        }

        /// <summary>
        ///   Gets or sets the member of the data source 
        ///   to be shown, if applicable.
        /// </summary>
        /// 
        [DefaultValue(null)]
        public string DataMemberX
        {
            get { return xAxisDataMember; }
            set
            {
                xAxisDataMember = value;

                if (!this.DesignMode)
                    OnDataBind();
            }
        }

        /// <summary>
        ///   Gets or sets the member of the data source 
        ///   to be shown, if applicable.
        /// </summary>
        /// 
        [DefaultValue(null)]
        public string DataMemberY
        {
            get { return yAxisDataMember; }
            set
            {
                yAxisDataMember = value;

                if (!this.DesignMode)
                    OnDataBind();
            }
        }

        /// <summary>
        ///   Gets or sets the member of the data source 
        ///   to be shown, if applicable.
        /// </summary>
        /// 
        [DefaultValue(null)]
        public string DataMemberLabels
        {
            get { return labelDataMember; }
            set
            {
                labelDataMember = value;

                if (!this.DesignMode)
                    OnDataBind();
            }
        }

        /// <summary>
        ///   Gets a reference to the underlying ZedGraph
        ///   control used to draw the histogram.
        /// </summary>
        /// 
        public ZedGraphControl Graph
        {
            get { return zedGraphControl; }
        }
        #endregion


        /// <summary>
        ///   Forces a update of the scatter plot.
        /// </summary>
        /// 
        public void UpdateGraph()
        {
            classes.Clear();

            if (scatterplot.LabelAxis == null)
            {
                // Create space for unlabelled data
                PointPairList unlabelled = new PointPairList(scatterplot.XAxis, scatterplot.YAxis);

                LineItem item = new LineItem(String.Empty, unlabelled, Color.Black, SymbolType.Diamond);

                item.Line.IsVisible = false;
                item.Symbol.Border.IsVisible = false;
                item.Symbol.Fill = new Fill(Color.Black);

                classes.Add(item);
            }
            else
            {
                ColorSequenceCollection colors = new ColorSequenceCollection(scatterplot.Classes.Count);

                // Create a curve item for each of the labels
                for (int i = 0; i < scatterplot.Classes.Count; i++)
                {
                    // retrieve the x,y pairs for the label
                    double[] x = scatterplot.Classes[i].XAxis;
                    double[] y = scatterplot.Classes[i].YAxis;
                    PointPairList list = new PointPairList(x, y);

                    LineItem item = new LineItem(String.Empty, list, colors[i], SymbolType.Diamond);

                    item.Line.IsVisible = false;
                    item.Symbol.Border.IsVisible = false;
                    item.Symbol.Fill = new Fill(colors[i]);

                    classes.Add(item);
                }

                zedGraphControl.AxisChange();
                zedGraphControl.Invalidate();
            }
        }



        private void OnDataBind()
        {
            if (dataSource == null)
                return;

            if (scatterplot == null)
                scatterplot = new Scatterplot();

            double[] x = null;
            double[] y = null;
            int[] z = null;

            if (dataSource is DataTable)
            {
                DataTable table = dataSource as DataTable;

                if (String.IsNullOrEmpty(xAxisDataMember) &&
                    table.Columns.Contains(xAxisDataMember))
                    x = table.Columns[xAxisDataMember].ToArray();

                if (String.IsNullOrEmpty(yAxisDataMember) &&
                    table.Columns.Contains(yAxisDataMember))
                    y = table.Columns[yAxisDataMember].ToArray();

                if (String.IsNullOrEmpty(labelDataMember) &&
                    table.Columns.Contains(labelDataMember))
                    z = table.Columns[labelDataMember].ToArray().ToInt32();
            }
            else if (dataSource is double[][])
            {
                double[][] source = dataSource as double[][];

                if (source.Length > 0)
                {
                    if (source[0].Length > 0)
                        x = source.GetColumn(0);

                    if (source[0].Length > 1)
                        y = source.GetColumn(1);

                    if (source[0].Length > 2)
                        z = source.GetColumn(2).ToInt32();
                }
            }
            else if (dataSource is double[,])
            {
                double[,] source = dataSource as double[,];

                if (source.Length > 0)
                {
                    int cols = source.GetLength(1);

                    if (cols > 0)
                        x = source.GetColumn(0);

                    if (cols > 1)
                        y = source.GetColumn(1);

                    if (cols > 2)
                        z = source.GetColumn(2).ToInt32();
                }
            }
            else
            {
                return; // invalid data source
            }

            if (x != null && y == null)
                y = new double[x.Length];

            else if (y != null && x == null)
                x = new double[y.Length];

            this.scatterplot.Compute(x, y, z);

            this.UpdateGraph();


        }

    }
}
