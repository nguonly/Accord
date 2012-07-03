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

namespace Gestures
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Canvas : UserControl
    {
        private bool capturing;
        private List<double[]> sequence;

        private bool continuous;
        private int capacity;

        public event EventHandler SequenceChanged;

        public bool Continuous
        {
            get { return continuous; }
            set { continuous = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }


        public Canvas()
        {
            InitializeComponent();

            sequence = new List<double[]>();
            this.DoubleBuffered = true;
        }

        public double[][] GetSequence()
        {
            double[][] s = new double[sequence.Count][];
            for (int i = 0; i < s.Length; i++)
                s[i] = (double[])sequence[i].Clone();
            return s;
        }



        public void Clear()
        {
            sequence.Clear();
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.DesignMode)
            {
                for (int i = 0; i < sequence.Count; i++)
                {
                    int x = (int)sequence[i][0];
                    int y = (int)sequence[i][1];
                    int p = (int)Accord.Math.Tools.Scale(0, sequence.Count, 0, 255, i);

                    using (Brush brush = new SolidBrush(Color.FromArgb(255 - p, 0, p)))
                    {
                        e.Graphics.FillRectangle(brush, x * 4, y * 4, 4, 4);
                    }
                }
            }

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!continuous)
                capturing = true;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            capturing = false;

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (continuous)
            {
                timer.Enabled = true;
                timeout.Enabled = true;
            }

            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            timer.Enabled = false;
            timeout.Enabled = false;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (capturing)
            {
                if (e.X > 0 && e.Y > 0)
                {
                    double[] h = { e.X / 4.0, e.Y / 4.0 };

                    sequence.Add(h);
                    this.Refresh();
                }
            }

            base.OnMouseMove(e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Point p = PointToClient(Cursor.Position);

            if (p.X > 0 && p.Y > 0)
            {
                

                double[] h = { p.X / 4.0, p.Y / 4.0 };

                if (sequence.Count > 0)
                {
                    if (sequence.Count == capacity)
                        sequence.RemoveAt(0);

                    double[] l = sequence.Last();

                    if (h[0] != l[0] || h[1] != l[1])
                    {
                        sequence.Add(h);
                        timeout.Stop();
                        timeout.Start();

                        if (SequenceChanged != null)
                            SequenceChanged(this, EventArgs.Empty);

                        this.Refresh();
                    }
                    else
                    {
                        sequence.RemoveAt(0);
                    }
                }
                else
                {
                    sequence.Add(h);
                    timeout.Stop();
                    timeout.Start();

                    if (SequenceChanged != null)
                        SequenceChanged(this, EventArgs.Empty);

                    this.Refresh();
                }

            }
        }

        private void timeout_Tick(object sender, EventArgs e)
        {
            if (sequence.Count > 0)
                sequence.RemoveAt(0);

            if (SequenceChanged != null)
                SequenceChanged(this, EventArgs.Empty);

            this.Refresh();
        }


    }
}
