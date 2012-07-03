using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;

namespace HMM
{
  public partial class WordSeg : Form
  {
    public WordSeg()
    {
      InitializeComponent();
    }
    HiddenMarkovClassifier _hmmc;
    private void btnModel_Click(object sender, EventArgs e)
    {
      var transition = new double[,]
                                {
                           {2.0/8, 1.0/8, 2.0/8, 3.0/8},
                           {0, 0, 0, 1.0/8},
                           {0, 0, 0, 0},
                           {1, 0, 0, 0},
                         };

      var emission = new[,]
                       {
                         {2.0/8, 0, 0, 0, 0},
                         {0, 0, 0, 0, 0},
                         {0, 0, 1, 0, 0},
                         {0, 1, 0, 1, 0},
                       };

      var start = new double[] {1, 0, 0, 0};
      
      var hmm = new HiddenMarkovModel(transition, emission, start, false);

      var liklyhood = 0d;
      var x = hmm.Decode(new[] {1}, out liklyhood);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var classes = 4;
      var states = new[]{1,2,2,3};
      var cat = new[] {"ខ្ញុំ", "ទៅ", "ខ្លួន", "ក"};
      //var cat = new[] { "A", "B" };

      _hmmc = new HiddenMarkovClassifier(classes, states, 4, cat);

      // Train the ensemble
      var sequences = new[]
                        {
                          new[] {1, 1, 1},
                          new[] {0, 2},
                          new[] {0, 1, 2},
                          new[] {1, 2}
                        };

      var labels = new[] {0, 1, 2, 3};

      var teacher = new HiddenMarkovClassifierLearning(_hmmc, i =>
          new BaumWelchLearning(_hmmc.Models[i])
          {
            Iterations = 0,
            Tolerance = 0.0001
          }
      );

      teacher.Run(sequences, labels);

      var m = _hmmc.Models;

      var test = new[]{1,2};
      double likelihood;
      var label = _hmmc.Compute(test, out likelihood);
      MessageBox.Show(_hmmc.Models[label].Tag.ToString()+ " P =" + likelihood);
    }
  }
}
