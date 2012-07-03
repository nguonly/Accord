// Accord.NET Sample Applications
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

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using Accord.Controls;
using Accord.Math;
using Accord.Statistics.Formats;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using ZedGraph;

namespace Gestures
{
    public partial class MainForm : Form
    {

        HiddenMarkovClassifier classifier;


        private Bitmap ToBitmap(double[][] sequence)
        {
            if (sequence.Length == 0)
                return null;

            int xmax = (int)sequence.Max(x => x[0]);
            int xmin = (int)sequence.Min(x => x[0]);

            int ymax = (int)sequence.Max(x => x[1]);
            int ymin = (int)sequence.Min(x => x[1]);

            int width = xmax - xmin;
            int height = ymax - ymin;

            Bitmap bmp = new Bitmap(width + 1, height + 1);
            for (int i = 0; i < sequence.Length; i++)
            {
                int x = (int)sequence[i][0] - xmin;
                int y = (int)sequence[i][1] - ymin;
                int p = (int)Accord.Math.Tools.Scale(0, sequence.Length, 0, 255, i);
                bmp.SetPixel(x, y, Color.FromArgb(255 - p, 0, p));
            }

            return bmp;
        }

        private void addSequence(int label)
        {
            var sequence = inputCanvas.GetSequence();
            var bitmap = ToBitmap(sequence);

            var row = dataGridView1.Rows.Add(bitmap, label, null);
            dataGridView1.Rows[row].Tag = sequence;
        }


        public MainForm()
        {
            InitializeComponent();

            openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Resources");
        }

        public int[] GetFeatures(double[][] sequence)
        {
            if (sequence.Length == 0)
                return new int[0];

            int[] features = new int[sequence.Length];

            features[0] = (int)System.Math.Floor(
                    Accord.Math.Tools.Angle(sequence[0][0], sequence[0][1]) * 3.183098861837907);

            for (int i = 1; i < sequence.Length; i++)
            {
                double[] prev = sequence[i - 1];
                double[] next = sequence[i];

                double dy = next[1] - prev[1];
                double dx = next[0] - prev[0];

                features[i] = (int)System.Math.Floor(
                    Accord.Math.Tools.Angle(dx, dy) * 3.183098861837907);
            }

            return features;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Please load or insert some data first.");
                return;
            }

            int states = (int)numStates.Value;
            int iterations = (int)numIterations.Value;
            double tolerance = (double)numConvergence.Value;

            if (rbStopIterations.Checked) tolerance = 0.0;
            if (rbStopConvergence.Checked) iterations = 0;


            // Retrieve the training data from the data grid view

            int rows = dataGridView1.Rows.Count;
            int[] outputs = new int[rows];
            var sequences = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                outputs[i] = (int)dataGridView1.Rows[i].Cells["colLabel"].Value - 1;
                sequences[i] = GetFeatures((double[][])dataGridView1.Rows[i].Tag);
            }

            int classes = outputs.Distinct().Count();


            string[] labels = new string[classes];
            for (int i = 0; i < labels.Length; i++)
                labels[i] = (i+1).ToString();


            // Create a sequence classifier for 3 classes
            classifier = new HiddenMarkovClassifier(labels.Length,
                new Forward(states), symbols: 20, names: labels);


            // Create the learning algorithm for the ensemble classifier
            var teacher = new HiddenMarkovClassifierLearning(classifier,

                // Train each model using the selected convergence criteria
                i => new BaumWelchLearning(classifier.Models[i])
                {
                    Tolerance = tolerance,
                    Iterations = iterations,
                }
            );

            // Create and use a rejection threshold model
            teacher.Rejection = cbRejection.Checked;
            teacher.Empirical = true;
            teacher.Smoothing = (double)numSmoothing.Value;


            // Run the learning algorithm
            teacher.Run(sequences, outputs);

            double error = classifier.LogLikelihood(sequences, outputs);


            int hits = 0;
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Maximum = dataGridView1.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                double likelihood;
                int index = classifier.Compute(sequences[i], out likelihood);

                DataGridViewRow row = dataGridView1.Rows[i];

                if (index == -1)
                {
                    row.Cells["colClassification"].Value = String.Empty;
                }
                else
                {
                    row.Cells["colClassification"].Value = classifier.Models[index].Tag;
                }

                int expected = (int)row.Cells["colLabel"].Value;

                if (expected == index + 1)
                {
                    row.Cells[0].Style.BackColor = Color.LightGreen;
                    row.Cells[1].Style.BackColor = Color.LightGreen;
                    row.Cells[2].Style.BackColor = Color.LightGreen;
                    hits++;
                }
                else
                {
                    row.Cells[0].Style.BackColor = Color.White;
                    row.Cells[1].Style.BackColor = Color.White;
                    row.Cells[2].Style.BackColor = Color.White;
                }

                toolStripProgressBar1.PerformStep();
            }

            dgvModels.DataSource = classifier.Models;

            toolStripProgressBar1.Visible = false;

            toolStripStatusLabel1.Text = String.Format("Training complete. Hits: {0}/{1} ({2:0%})",
                hits, dataGridView1.Rows.Count, (double)hits / dataGridView1.Rows.Count);
        }

        private void dgvModels_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvModels.CurrentRow != null)
            {
                HiddenMarkovModel model = dgvModels.CurrentRow.DataBoundItem as HiddenMarkovModel;
                dgvProbabilities.DataSource = new ArrayDataView(model.Probabilities);
                dgvTransitions.DataSource = new ArrayDataView(model.Transitions);
            }
        }



        private void btnCanvasClear_Click(object sender, EventArgs e)
        {
            inputCanvas.Clear();
        }

        private void canvas2_MouseUp(object sender, MouseEventArgs e)
        {
            classify(canvas2, graphClassification, lbClassification);
        }

        private void classify(Canvas canvas, ZedGraphControl graph,
            System.Windows.Forms.Label output)
        {
            int[] sequence = GetFeatures(canvas.GetSequence());

            if (classifier == null || sequence.Length <= 10)
            {
                output.Text = "-";
                return;
            }


            double[] responses;
            int index = classifier.Compute(sequence, out responses);

            output.Text = (index == -1) ?
                "-" : classifier.Models[index].Tag as string;

            // Scale the responses to a [0,1] interval
            double max = responses.Max();
            double min = responses.Min();

            for (int i = 0; i < responses.Length; i++)
                responses[i] = Accord.Math.Tools.Scale(min, max, 0, 1, responses[i]);

            // Create the bar graph to show the relative responses
            CreateBarGraph(graph, responses);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            canvas2.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            classify(canvas2, graphClassification, lbClassification);
        }

        private void canvas2_MouseDown(object sender, MouseEventArgs e)
        {
            canvas2.Clear();
        }



        public void CreateBarGraph(ZedGraphControl zgc, double[] responses)
        {
            GraphPane myPane = zgc.GraphPane;

            myPane.CurveList.Clear();

            myPane.Title.IsVisible = false;
            myPane.Legend.IsVisible = false;
            myPane.Border.IsVisible = false;
            myPane.Border.IsVisible = false;
            myPane.Margin.Bottom = 20f;
            myPane.Margin.Right = 20f;
            myPane.Margin.Left = 20f;
            myPane.Margin.Top = 30f;

            myPane.YAxis.Title.IsVisible = true;
            myPane.YAxis.IsVisible = true;
            myPane.YAxis.MinorGrid.IsVisible = false;
            myPane.YAxis.MajorGrid.IsVisible = false;
            myPane.YAxis.IsAxisSegmentVisible = false;
            myPane.YAxis.Scale.Max = responses.Length + 0.5;
            myPane.YAxis.Scale.Min = -0.5;
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            myPane.YAxis.Title.Text = "Classes";
            myPane.YAxis.MinorTic.IsOpposite = false;
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsInside = false;
            myPane.YAxis.MajorTic.IsInside = false;
            myPane.YAxis.MinorTic.IsOutside = false;
            myPane.YAxis.MajorTic.IsOutside = false;

            myPane.XAxis.MinorTic.IsOpposite = false;
            myPane.XAxis.MajorTic.IsOpposite = false;
            myPane.XAxis.Title.IsVisible = true;
            myPane.XAxis.Title.Text = "Relative class response";
            myPane.XAxis.IsVisible = true;
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 100;
            myPane.XAxis.IsAxisSegmentVisible = false;
            myPane.XAxis.MajorGrid.IsVisible = false;
            myPane.XAxis.MajorGrid.IsZeroLine = false;
            myPane.XAxis.MinorTic.IsOpposite = false;
            myPane.XAxis.MinorTic.IsInside = false;
            myPane.XAxis.MinorTic.IsOutside = false;
            myPane.XAxis.Scale.Format = "0'%";


            // Create data points for three BarItems using Random data
            PointPairList list = new PointPairList();

            for (int i = 0; i < responses.Length; i++)
                list.Add(responses[i] * 100, i + 1);

            BarItem myCurve = myPane.AddBar("b", list, Color.DarkBlue);


            // Set BarBase to the YAxis for horizontal bars
            myPane.BarSettings.Base = BarBase.Y;


            zgc.AxisChange();
            zgc.Invalidate();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Extract data
            int rows = dataGridView1.Rows.Count;
            TrainingSample[] samples = new TrainingSample[rows];
            for (int i = 0; i < rows; i++)
            {
                samples[i] = new TrainingSample();
                samples[i].Output = (int)dataGridView1.Rows[i].Cells["colLabel"].Value - 1;
                samples[i].Sequence = (double[][])dataGridView1.Rows[i].Tag;
            }


            XmlSerializer serializer = new XmlSerializer(typeof(TrainingSample[]));
            using (var stream = saveFileDialog1.OpenFile())
            {
                serializer.Serialize(stream, samples);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }



        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TrainingSample[] samples = null;

            string filename = openFileDialog.FileName;
            string extension = Path.GetExtension(filename);
            if (extension == ".xls" || extension == ".xlsx")
            {
                ExcelReader db = new ExcelReader(filename, true, false);
                TableSelectDialog t = new TableSelectDialog(db.GetWorksheetList());

                if (t.ShowDialog(this) == DialogResult.OK)
                {
                    var sampleTable = db.GetWorksheet(t.Selection);
                    samples = new TrainingSample[sampleTable.Rows.Count];
                    for (int i = 0; i < samples.Length; i++)
                    {
                        samples[i] = new TrainingSample();
                        samples[i].Sequence = new double[(sampleTable.Columns.Count - 1) / 2][];
                        for (int j = 0; j < samples[i].Sequence.Length; j++)
                        {
                            samples[i].Sequence[j] =
                            new double[] 
                            { 
                                    (double)sampleTable.Rows[i][j] * 50, 
                                    (double)sampleTable.Rows[i][j+1] * 50
                            };
                        }
                        samples[i].Output = (int)(double)sampleTable.Rows[i][sampleTable.Columns.Count - 1] - 1;
                    }
                }
            }
            else if (extension == ".xml")
            {
                using (var stream = openFileDialog.OpenFile())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TrainingSample[]));
                    samples = (TrainingSample[])serializer.Deserialize(stream);
                }
            }


            dataGridView1.Rows.Clear();
            for (int i = 0; i < samples.Length; i++)
            {
                var sequence = samples[i].Sequence;
                var label = samples[i].Output + 1;
                var bitmap = ToBitmap(sequence);

                var row = dataGridView1.Rows.Add(bitmap, label, null);
                dataGridView1.Rows[row].Tag = sequence;
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }


        [Serializable]
        public class TrainingSample
        {
            public int Output;
            public double[][] Sequence;
        }

        private void canvas1_SequenceChanged(object sender, EventArgs e)
        {
            classify(canvas1, zedGraphControl1, label9);
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void saveFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (Stream stream = saveFileDialog2.OpenFile())
            {
                BinaryFormatter fmt = new BinaryFormatter();
                fmt.Serialize(stream, classifier);
            }
        }

        private void btnAddToClass_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int c = int.Parse((string)btn.Tag);
            addSequence(c);
            inputCanvas.Clear();
        }
    }
}
