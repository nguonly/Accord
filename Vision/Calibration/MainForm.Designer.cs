namespace KinectCalibration
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.numMaxTrials = new System.Windows.Forms.NumericUpDown();
            this.numSamples = new System.Windows.Forms.NumericUpDown();
            this.numThreshold = new System.Windows.Forms.NumericUpDown();
            this.numProbability = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthVisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackerVisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faceControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new AForge.Controls.PictureBox();
            this.pbColor = new AForge.Controls.PictureBox();
            this.numF = new System.Windows.Forms.NumericUpDown();
            this.numC = new System.Windows.Forms.NumericUpDown();
            this.numH = new System.Windows.Forms.NumericUpDown();
            this.numG = new System.Windows.Forms.NumericUpDown();
            this.numE = new System.Windows.Forms.NumericUpDown();
            this.numD = new System.Windows.Forms.NumericUpDown();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.numA = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTrials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(943, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(21, 44);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(320, 240);
            this.videoSourcePlayer1.TabIndex = 5;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            this.videoSourcePlayer1.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer1_NewFrame);
            // 
            // numMaxTrials
            // 
            this.numMaxTrials.Location = new System.Drawing.Point(0, 0);
            this.numMaxTrials.Name = "numMaxTrials";
            this.numMaxTrials.Size = new System.Drawing.Size(120, 20);
            this.numMaxTrials.TabIndex = 0;
            // 
            // numSamples
            // 
            this.numSamples.Location = new System.Drawing.Point(0, 0);
            this.numSamples.Name = "numSamples";
            this.numSamples.Size = new System.Drawing.Size(120, 20);
            this.numSamples.TabIndex = 0;
            // 
            // numThreshold
            // 
            this.numThreshold.Location = new System.Drawing.Point(0, 0);
            this.numThreshold.Name = "numThreshold";
            this.numThreshold.Size = new System.Drawing.Size(120, 20);
            this.numThreshold.TabIndex = 0;
            // 
            // numProbability
            // 
            this.numProbability.Location = new System.Drawing.Point(0, 0);
            this.numProbability.Name = "numProbability";
            this.numProbability.Size = new System.Drawing.Size(120, 20);
            this.numProbability.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(519, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Maximum Trials";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(519, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Sample size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Error threshold:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inlier probability:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.showToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(943, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectCameraToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectCameraToolStripMenuItem
            // 
            this.selectCameraToolStripMenuItem.Name = "selectCameraToolStripMenuItem";
            this.selectCameraToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.selectCameraToolStripMenuItem.Text = "Select camera";
            this.selectCameraToolStripMenuItem.Click += new System.EventHandler(this.btnSelectCamera_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.depthVisionToolStripMenuItem,
            this.trackerVisionToolStripMenuItem,
            this.faceControlsToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // depthVisionToolStripMenuItem
            // 
            this.depthVisionToolStripMenuItem.Name = "depthVisionToolStripMenuItem";
            this.depthVisionToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.depthVisionToolStripMenuItem.Text = "Depth vision";
            // 
            // trackerVisionToolStripMenuItem
            // 
            this.trackerVisionToolStripMenuItem.Name = "trackerVisionToolStripMenuItem";
            this.trackerVisionToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.trackerVisionToolStripMenuItem.Text = "Tracker vision";
            // 
            // faceControlsToolStripMenuItem
            // 
            this.faceControlsToolStripMenuItem.Name = "faceControlsToolStripMenuItem";
            this.faceControlsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.faceControlsToolStripMenuItem.Text = "Face control (experimental)";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.Location = new System.Drawing.Point(558, 491);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "Rectify";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(273, 491);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Correlation";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(396, 491);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "RANSAC";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(21, 491);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Still";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(21, 290);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(646, 177);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // pbColor
            // 
            this.pbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColor.Image = null;
            this.pbColor.Location = new System.Drawing.Point(347, 44);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(320, 240);
            this.pbColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbColor.TabIndex = 7;
            this.pbColor.TabStop = false;
            // 
            // numF
            // 
            this.numF.DecimalPlaces = 3;
            this.numF.Location = new System.Drawing.Point(843, 88);
            this.numF.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numF.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numF.Name = "numF";
            this.numF.Size = new System.Drawing.Size(63, 20);
            this.numF.TabIndex = 19;
            this.numF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numF.Value = new decimal(new int[] {
            168,
            0,
            0,
            65536});
            // 
            // numC
            // 
            this.numC.DecimalPlaces = 3;
            this.numC.Location = new System.Drawing.Point(843, 62);
            this.numC.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numC.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numC.Name = "numC";
            this.numC.Size = new System.Drawing.Size(63, 20);
            this.numC.TabIndex = 18;
            this.numC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numC.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // numH
            // 
            this.numH.DecimalPlaces = 3;
            this.numH.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numH.Location = new System.Drawing.Point(774, 114);
            this.numH.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numH.Name = "numH";
            this.numH.Size = new System.Drawing.Size(63, 20);
            this.numH.TabIndex = 21;
            this.numH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numG
            // 
            this.numG.DecimalPlaces = 3;
            this.numG.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numG.Location = new System.Drawing.Point(705, 114);
            this.numG.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numG.Name = "numG";
            this.numG.Size = new System.Drawing.Size(63, 20);
            this.numG.TabIndex = 20;
            this.numG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numE
            // 
            this.numE.DecimalPlaces = 3;
            this.numE.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numE.Location = new System.Drawing.Point(774, 88);
            this.numE.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numE.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numE.Name = "numE";
            this.numE.Size = new System.Drawing.Size(63, 20);
            this.numE.TabIndex = 15;
            this.numE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numE.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numD
            // 
            this.numD.DecimalPlaces = 3;
            this.numD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numD.Location = new System.Drawing.Point(705, 88);
            this.numD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numD.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numD.Name = "numD";
            this.numD.Size = new System.Drawing.Size(63, 20);
            this.numD.TabIndex = 14;
            this.numD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numB
            // 
            this.numB.DecimalPlaces = 3;
            this.numB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numB.Location = new System.Drawing.Point(774, 62);
            this.numB.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numB.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(63, 20);
            this.numB.TabIndex = 17;
            this.numB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numA
            // 
            this.numA.DecimalPlaces = 3;
            this.numA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numA.Location = new System.Drawing.Point(705, 62);
            this.numA.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numA.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numA.Name = "numA";
            this.numA.Size = new System.Drawing.Size(63, 20);
            this.numA.TabIndex = 16;
            this.numA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numA.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 564);
            this.Controls.Add(this.numF);
            this.Controls.Add(this.numC);
            this.Controls.Add(this.numH);
            this.Controls.Add(this.numG);
            this.Controls.Add(this.numE);
            this.Controls.Add(this.numD);
            this.Controls.Add(this.numB);
            this.Controls.Add(this.numA);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbColor);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(415, 430);
            this.Name = "MainForm";
            this.Text = "Head-based Controller";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTrials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProbability)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private AForge.Controls.PictureBox pbColor;
        private System.Windows.Forms.NumericUpDown numMaxTrials;
        private System.Windows.Forms.NumericUpDown numSamples;
        private System.Windows.Forms.NumericUpDown numThreshold;
        private System.Windows.Forms.NumericUpDown numProbability;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthVisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackerVisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faceControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private AForge.Controls.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numF;
        private System.Windows.Forms.NumericUpDown numC;
        private System.Windows.Forms.NumericUpDown numH;
        private System.Windows.Forms.NumericUpDown numG;
        private System.Windows.Forms.NumericUpDown numE;
        private System.Windows.Forms.NumericUpDown numD;
        private System.Windows.Forms.NumericUpDown numB;
        private System.Windows.Forms.NumericUpDown numA;
    }
}

