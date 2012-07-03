namespace KinectCalibration
{
    partial class MatchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbStill2 = new AForge.Controls.PictureBox();
            this.pbStill1 = new AForge.Controls.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbStill2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStill1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbStill2
            // 
            this.pbStill2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbStill2.Image = null;
            this.pbStill2.Location = new System.Drawing.Point(641, 2);
            this.pbStill2.Name = "pbStill2";
            this.pbStill2.Size = new System.Drawing.Size(640, 480);
            this.pbStill2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbStill2.TabIndex = 9;
            this.pbStill2.TabStop = false;
            this.pbStill2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbStill2_MouseClick);
            // 
            // pbStill1
            // 
            this.pbStill1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbStill1.Image = null;
            this.pbStill1.Location = new System.Drawing.Point(-1, 2);
            this.pbStill1.Name = "pbStill1";
            this.pbStill1.Size = new System.Drawing.Size(640, 480);
            this.pbStill1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbStill1.TabIndex = 8;
            this.pbStill1.TabStop = false;
            this.pbStill1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbStill1_MouseClick);
            // 
            // MatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 520);
            this.Controls.Add(this.pbStill2);
            this.Controls.Add(this.pbStill1);
            this.Name = "MatchForm";
            this.Text = "MatchForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbStill2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStill1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AForge.Controls.PictureBox pbStill2;
        public AForge.Controls.PictureBox pbStill1;

    }
}