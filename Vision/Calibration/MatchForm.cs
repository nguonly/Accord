using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge;

namespace KinectCalibration
{
    public partial class MatchForm : Form
    {
        public List<IntPoint> correlationPoints1;
        public List<IntPoint> correlationPoints2;

        public MatchForm()
        {
            InitializeComponent();
        }

        private void pbStill2_Click(object sender, EventArgs e)
        {

        }

        private void pbStill1_MouseClick(object sender, MouseEventArgs e)
        {
            if (correlationPoints1 == null)
                correlationPoints1 = new List<IntPoint>();
            correlationPoints1.Add(new IntPoint(e.X, e.Y));
        }

        private void pbStill2_MouseClick(object sender, MouseEventArgs e)
        {
            if (correlationPoints2 == null)
                correlationPoints2 = new List<IntPoint>();
            correlationPoints2.Add(new IntPoint(e.X, e.Y));
        }
    }
}
