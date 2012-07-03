namespace Gestures
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClassification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer11 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numSmoothing = new System.Windows.Forms.NumericUpDown();
            this.cbRejection = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.inputCanvas = new Gestures.Canvas();
            this.btnCanvasClear = new System.Windows.Forms.Button();
            this.btnAddClass3 = new System.Windows.Forms.Button();
            this.btnAddClass2 = new System.Windows.Forms.Button();
            this.btnAddClass1 = new System.Windows.Forms.Button();
            this.btnAddClass6 = new System.Windows.Forms.Button();
            this.btnAddClass9 = new System.Windows.Forms.Button();
            this.btnAddClass5 = new System.Windows.Forms.Button();
            this.btnAddClass8 = new System.Windows.Forms.Button();
            this.btnAddClass4 = new System.Windows.Forms.Button();
            this.btnAddClass7 = new System.Windows.Forms.Button();
            this.numConvergence = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.rbStopConvergence = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.rbStopIterations = new System.Windows.Forms.RadioButton();
            this.numStates = new System.Windows.Forms.NumericUpDown();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.dgvModels = new System.Windows.Forms.DataGridView();
            this.colModelsTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModelsStates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.dgvTransitions = new System.Windows.Forms.DataGridView();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.dgvProbabilities = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.splitContainer12 = new System.Windows.Forms.SplitContainer();
            this.splitContainer13 = new System.Windows.Forms.SplitContainer();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.lbClassification = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.canvas2 = new Gestures.Canvas();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.graphClassification = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.canvas1 = new Gestures.Canvas();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tabSamples = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numThreshold = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClassify = new System.Windows.Forms.Button();
            this.btnSampleRunAnalysis = new System.Windows.Forms.Button();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.colComponent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEigenValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProportion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCumulativeProportion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.dgvClasses = new System.Windows.Forms.DataGridView();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lvClass = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lbCanvasClassification = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tbPenWidth = new System.Windows.Forms.TrackBar();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSmoothing)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numConvergence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStates)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.groupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModels)).BeginInit();
            this.groupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransitions)).BeginInit();
            this.groupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProbabilities)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).BeginInit();
            this.splitContainer12.Panel1.SuspendLayout();
            this.splitContainer12.Panel2.SuspendLayout();
            this.splitContainer12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).BeginInit();
            this.splitContainer13.Panel1.SuspendLayout();
            this.splitContainer13.Panel2.SuspendLayout();
            this.splitContainer13.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox22.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClasses)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPenWidth)).BeginInit();
            this.groupBox16.SuspendLayout();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start learning process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colLabel,
            this.colClassification});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.Size = new System.Drawing.Size(350, 384);
            this.dataGridView1.TabIndex = 2;
            // 
            // colImage
            // 
            this.colImage.HeaderText = "Gesture";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            // 
            // colLabel
            // 
            this.colLabel.HeaderText = "Label";
            this.colLabel.Name = "colLabel";
            this.colLabel.ReadOnly = true;
            // 
            // colClassification
            // 
            this.colClassification.HeaderText = "Classification";
            this.colClassification.Name = "colClassification";
            this.colClassification.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 435);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(614, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Samples (input)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer11
            // 
            this.splitContainer11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer11.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer11.Location = new System.Drawing.Point(3, 3);
            this.splitContainer11.Name = "splitContainer11";
            // 
            // splitContainer11.Panel1
            // 
            this.splitContainer11.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer11.Panel2
            // 
            this.splitContainer11.Panel2.Controls.Add(this.label12);
            this.splitContainer11.Panel2.Controls.Add(this.numSmoothing);
            this.splitContainer11.Panel2.Controls.Add(this.cbRejection);
            this.splitContainer11.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer11.Panel2.Controls.Add(this.numConvergence);
            this.splitContainer11.Panel2.Controls.Add(this.label1);
            this.splitContainer11.Panel2.Controls.Add(this.numIterations);
            this.splitContainer11.Panel2.Controls.Add(this.rbStopConvergence);
            this.splitContainer11.Panel2.Controls.Add(this.label7);
            this.splitContainer11.Panel2.Controls.Add(this.rbStopIterations);
            this.splitContainer11.Panel2.Controls.Add(this.numStates);
            this.splitContainer11.Panel2.Controls.Add(this.button1);
            this.splitContainer11.Size = new System.Drawing.Size(608, 403);
            this.splitContainer11.SplitterDistance = 356;
            this.splitContainer11.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 403);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Training Dataset";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 335);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Threshold smoothing";
            // 
            // numSmoothing
            // 
            this.numSmoothing.DecimalPlaces = 2;
            this.numSmoothing.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSmoothing.Location = new System.Drawing.Point(143, 333);
            this.numSmoothing.Name = "numSmoothing";
            this.numSmoothing.Size = new System.Drawing.Size(50, 20);
            this.numSmoothing.TabIndex = 22;
            this.numSmoothing.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            // 
            // cbRejection
            // 
            this.cbRejection.AutoSize = true;
            this.cbRejection.Location = new System.Drawing.Point(6, 312);
            this.cbRejection.Name = "cbRejection";
            this.cbRejection.Size = new System.Drawing.Size(171, 17);
            this.cbRejection.TabIndex = 21;
            this.cbRejection.Text = "Use rejection (threshold model)";
            this.cbRejection.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.inputCanvas);
            this.groupBox2.Controls.Add(this.btnCanvasClear);
            this.groupBox2.Controls.Add(this.btnAddClass3);
            this.groupBox2.Controls.Add(this.btnAddClass2);
            this.groupBox2.Controls.Add(this.btnAddClass1);
            this.groupBox2.Controls.Add(this.btnAddClass6);
            this.groupBox2.Controls.Add(this.btnAddClass9);
            this.groupBox2.Controls.Add(this.btnAddClass5);
            this.groupBox2.Controls.Add(this.btnAddClass8);
            this.groupBox2.Controls.Add(this.btnAddClass4);
            this.groupBox2.Controls.Add(this.btnAddClass7);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 156);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gesture Canvas";
            // 
            // inputCanvas
            // 
            this.inputCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inputCanvas.Capacity = 0;
            this.inputCanvas.Continuous = false;
            this.inputCanvas.Location = new System.Drawing.Point(6, 18);
            this.inputCanvas.MaximumSize = new System.Drawing.Size(128, 128);
            this.inputCanvas.MinimumSize = new System.Drawing.Size(128, 128);
            this.inputCanvas.Name = "inputCanvas";
            this.inputCanvas.Size = new System.Drawing.Size(128, 128);
            this.inputCanvas.TabIndex = 10;
            // 
            // btnCanvasClear
            // 
            this.btnCanvasClear.Location = new System.Drawing.Point(140, 105);
            this.btnCanvasClear.Name = "btnCanvasClear";
            this.btnCanvasClear.Size = new System.Drawing.Size(45, 41);
            this.btnCanvasClear.TabIndex = 4;
            this.btnCanvasClear.Text = "Clear";
            this.btnCanvasClear.UseVisualStyleBackColor = true;
            this.btnCanvasClear.Click += new System.EventHandler(this.btnCanvasClear_Click);
            // 
            // btnAddClass3
            // 
            this.btnAddClass3.Location = new System.Drawing.Point(206, 18);
            this.btnAddClass3.Name = "btnAddClass3";
            this.btnAddClass3.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass3.TabIndex = 5;
            this.btnAddClass3.Tag = "3";
            this.btnAddClass3.Text = "3";
            this.btnAddClass3.UseVisualStyleBackColor = true;
            this.btnAddClass3.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass2
            // 
            this.btnAddClass2.Location = new System.Drawing.Point(174, 18);
            this.btnAddClass2.Name = "btnAddClass2";
            this.btnAddClass2.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass2.TabIndex = 5;
            this.btnAddClass2.Tag = "2";
            this.btnAddClass2.Text = "2";
            this.btnAddClass2.UseVisualStyleBackColor = true;
            this.btnAddClass2.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass1
            // 
            this.btnAddClass1.Location = new System.Drawing.Point(140, 18);
            this.btnAddClass1.Name = "btnAddClass1";
            this.btnAddClass1.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass1.TabIndex = 5;
            this.btnAddClass1.Tag = "1";
            this.btnAddClass1.Text = "1";
            this.btnAddClass1.UseVisualStyleBackColor = true;
            this.btnAddClass1.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass6
            // 
            this.btnAddClass6.Location = new System.Drawing.Point(206, 47);
            this.btnAddClass6.Name = "btnAddClass6";
            this.btnAddClass6.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass6.TabIndex = 7;
            this.btnAddClass6.Tag = "6";
            this.btnAddClass6.Text = "6";
            this.btnAddClass6.UseVisualStyleBackColor = true;
            this.btnAddClass6.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass9
            // 
            this.btnAddClass9.Location = new System.Drawing.Point(206, 76);
            this.btnAddClass9.Name = "btnAddClass9";
            this.btnAddClass9.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass9.TabIndex = 9;
            this.btnAddClass9.Tag = "9";
            this.btnAddClass9.Text = "9";
            this.btnAddClass9.UseVisualStyleBackColor = true;
            this.btnAddClass9.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass5
            // 
            this.btnAddClass5.Location = new System.Drawing.Point(174, 47);
            this.btnAddClass5.Name = "btnAddClass5";
            this.btnAddClass5.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass5.TabIndex = 7;
            this.btnAddClass5.Tag = "5";
            this.btnAddClass5.Text = "5";
            this.btnAddClass5.UseVisualStyleBackColor = true;
            this.btnAddClass5.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass8
            // 
            this.btnAddClass8.Location = new System.Drawing.Point(174, 76);
            this.btnAddClass8.Name = "btnAddClass8";
            this.btnAddClass8.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass8.TabIndex = 9;
            this.btnAddClass8.Tag = "8";
            this.btnAddClass8.Text = "8";
            this.btnAddClass8.UseVisualStyleBackColor = true;
            this.btnAddClass8.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass4
            // 
            this.btnAddClass4.Location = new System.Drawing.Point(140, 47);
            this.btnAddClass4.Name = "btnAddClass4";
            this.btnAddClass4.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass4.TabIndex = 7;
            this.btnAddClass4.Tag = "4";
            this.btnAddClass4.Text = "4";
            this.btnAddClass4.UseVisualStyleBackColor = true;
            this.btnAddClass4.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // btnAddClass7
            // 
            this.btnAddClass7.Location = new System.Drawing.Point(140, 76);
            this.btnAddClass7.Name = "btnAddClass7";
            this.btnAddClass7.Size = new System.Drawing.Size(28, 23);
            this.btnAddClass7.TabIndex = 9;
            this.btnAddClass7.Tag = "7";
            this.btnAddClass7.Text = "7";
            this.btnAddClass7.UseVisualStyleBackColor = true;
            this.btnAddClass7.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // numConvergence
            // 
            this.numConvergence.DecimalPlaces = 3;
            this.numConvergence.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numConvergence.Location = new System.Drawing.Point(144, 260);
            this.numConvergence.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numConvergence.Name = "numConvergence";
            this.numConvergence.Size = new System.Drawing.Size(50, 20);
            this.numConvergence.TabIndex = 19;
            this.numConvergence.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 60);
            this.label1.TabIndex = 14;
            this.label1.Text = "To insert a gesture in the training dataset, draw your gesture in the box above a" +
                "nd click the desired label on the right.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numIterations
            // 
            this.numIterations.Location = new System.Drawing.Point(144, 234);
            this.numIterations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(50, 20);
            this.numIterations.TabIndex = 20;
            this.numIterations.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // rbStopConvergence
            // 
            this.rbStopConvergence.AutoSize = true;
            this.rbStopConvergence.Checked = true;
            this.rbStopConvergence.Location = new System.Drawing.Point(6, 260);
            this.rbStopConvergence.Name = "rbStopConvergence";
            this.rbStopConvergence.Size = new System.Drawing.Size(135, 17);
            this.rbStopConvergence.TabIndex = 17;
            this.rbStopConvergence.TabStop = true;
            this.rbStopConvergence.Text = "Convergence threshold";
            this.rbStopConvergence.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Hidden states number";
            // 
            // rbStopIterations
            // 
            this.rbStopIterations.AutoSize = true;
            this.rbStopIterations.Location = new System.Drawing.Point(6, 234);
            this.rbStopIterations.Name = "rbStopIterations";
            this.rbStopIterations.Size = new System.Drawing.Size(128, 17);
            this.rbStopIterations.TabIndex = 18;
            this.rbStopIterations.Text = "Max iterations number";
            this.rbStopIterations.UseVisualStyleBackColor = true;
            // 
            // numStates
            // 
            this.numStates.Location = new System.Drawing.Point(144, 286);
            this.numStates.Name = "numStates";
            this.numStates.Size = new System.Drawing.Size(50, 20);
            this.numStates.TabIndex = 15;
            this.numStates.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.splitContainer6);
            this.tabPage7.Controls.Add(this.groupBox23);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(614, 409);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "Hidden Markov Models";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(3, 3);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.groupBox24);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.groupBox25);
            this.splitContainer6.Size = new System.Drawing.Size(608, 306);
            this.splitContainer6.SplitterDistance = 256;
            this.splitContainer6.TabIndex = 2;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.dgvModels);
            this.groupBox24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox24.Location = new System.Drawing.Point(0, 0);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(256, 306);
            this.groupBox24.TabIndex = 0;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Models";
            // 
            // dgvModels
            // 
            this.dgvModels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvModels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colModelsTag,
            this.colModelsStates});
            this.dgvModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModels.Location = new System.Drawing.Point(3, 16);
            this.dgvModels.Name = "dgvModels";
            this.dgvModels.Size = new System.Drawing.Size(250, 287);
            this.dgvModels.TabIndex = 1;
            this.dgvModels.CurrentCellChanged += new System.EventHandler(this.dgvModels_CurrentCellChanged);
            // 
            // colModelsTag
            // 
            this.colModelsTag.DataPropertyName = "Tag";
            this.colModelsTag.HeaderText = "Class";
            this.colModelsTag.Name = "colModelsTag";
            // 
            // colModelsStates
            // 
            this.colModelsStates.DataPropertyName = "States";
            this.colModelsStates.HeaderText = "States";
            this.colModelsStates.Name = "colModelsStates";
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.dgvTransitions);
            this.groupBox25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox25.Location = new System.Drawing.Point(0, 0);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(348, 306);
            this.groupBox25.TabIndex = 1;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Transitions";
            // 
            // dgvTransitions
            // 
            this.dgvTransitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransitions.Location = new System.Drawing.Point(3, 16);
            this.dgvTransitions.Name = "dgvTransitions";
            this.dgvTransitions.Size = new System.Drawing.Size(342, 287);
            this.dgvTransitions.TabIndex = 1;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.dgvProbabilities);
            this.groupBox23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox23.Location = new System.Drawing.Point(3, 309);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(608, 97);
            this.groupBox23.TabIndex = 0;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "State Probabilities";
            // 
            // dgvProbabilities
            // 
            this.dgvProbabilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProbabilities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProbabilities.Location = new System.Drawing.Point(3, 16);
            this.dgvProbabilities.Name = "dgvProbabilities";
            this.dgvProbabilities.Size = new System.Drawing.Size(602, 78);
            this.dgvProbabilities.TabIndex = 1;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.splitContainer12);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(614, 409);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Classification";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // splitContainer12
            // 
            this.splitContainer12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer12.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer12.Location = new System.Drawing.Point(3, 3);
            this.splitContainer12.Name = "splitContainer12";
            // 
            // splitContainer12.Panel1
            // 
            this.splitContainer12.Panel1.Controls.Add(this.splitContainer13);
            // 
            // splitContainer12.Panel2
            // 
            this.splitContainer12.Panel2.Controls.Add(this.groupBox20);
            this.splitContainer12.Size = new System.Drawing.Size(608, 403);
            this.splitContainer12.SplitterDistance = 141;
            this.splitContainer12.TabIndex = 15;
            // 
            // splitContainer13
            // 
            this.splitContainer13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer13.Location = new System.Drawing.Point(0, 0);
            this.splitContainer13.Name = "splitContainer13";
            this.splitContainer13.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer13.Panel1
            // 
            this.splitContainer13.Panel1.Controls.Add(this.groupBox19);
            // 
            // splitContainer13.Panel2
            // 
            this.splitContainer13.Panel2.Controls.Add(this.groupBox21);
            this.splitContainer13.Size = new System.Drawing.Size(141, 403);
            this.splitContainer13.SplitterDistance = 201;
            this.splitContainer13.TabIndex = 15;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.lbClassification);
            this.groupBox19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox19.Location = new System.Drawing.Point(0, 0);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(141, 201);
            this.groupBox19.TabIndex = 11;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Classification";
            // 
            // lbClassification
            // 
            this.lbClassification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbClassification.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClassification.Location = new System.Drawing.Point(3, 16);
            this.lbClassification.Name = "lbClassification";
            this.lbClassification.Size = new System.Drawing.Size(135, 182);
            this.lbClassification.TabIndex = 3;
            this.lbClassification.Text = "0";
            this.lbClassification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.button11);
            this.groupBox21.Controls.Add(this.button12);
            this.groupBox21.Controls.Add(this.canvas2);
            this.groupBox21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox21.Location = new System.Drawing.Point(0, 0);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(141, 198);
            this.groupBox21.TabIndex = 14;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Gesture Canvas";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(6, 149);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(56, 23);
            this.button11.TabIndex = 11;
            this.button11.Text = "Classify";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(68, 149);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(65, 23);
            this.button12.TabIndex = 12;
            this.button12.Text = "Clear";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // canvas2
            // 
            this.canvas2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas2.Capacity = 0;
            this.canvas2.Continuous = false;
            this.canvas2.Location = new System.Drawing.Point(6, 18);
            this.canvas2.MaximumSize = new System.Drawing.Size(128, 128);
            this.canvas2.MinimumSize = new System.Drawing.Size(128, 128);
            this.canvas2.Name = "canvas2";
            this.canvas2.Size = new System.Drawing.Size(128, 128);
            this.canvas2.TabIndex = 10;
            this.canvas2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas2_MouseDown);
            this.canvas2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas2_MouseUp);
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.graphClassification);
            this.groupBox20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox20.Location = new System.Drawing.Point(0, 0);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(463, 403);
            this.groupBox20.TabIndex = 10;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Models relative response";
            // 
            // graphClassification
            // 
            this.graphClassification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphClassification.Location = new System.Drawing.Point(3, 16);
            this.graphClassification.Name = "graphClassification";
            this.graphClassification.ScrollGrace = 0D;
            this.graphClassification.ScrollMaxX = 0D;
            this.graphClassification.ScrollMaxY = 0D;
            this.graphClassification.ScrollMaxY2 = 0D;
            this.graphClassification.ScrollMinX = 0D;
            this.graphClassification.ScrollMinY = 0D;
            this.graphClassification.ScrollMinY2 = 0D;
            this.graphClassification.Size = new System.Drawing.Size(457, 384);
            this.graphClassification.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(614, 409);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "Continuous classification";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.Location = new System.Drawing.Point(3, 3);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer8);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.groupBox22);
            this.splitContainer5.Size = new System.Drawing.Size(608, 403);
            this.splitContainer5.SplitterDistance = 141;
            this.splitContainer5.TabIndex = 16;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.groupBox13);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.groupBox14);
            this.splitContainer8.Size = new System.Drawing.Size(141, 403);
            this.splitContainer8.SplitterDistance = 201;
            this.splitContainer8.TabIndex = 15;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label9);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox13.Location = new System.Drawing.Point(0, 0);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(141, 201);
            this.groupBox13.TabIndex = 11;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Classification";
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 182);
            this.label9.TabIndex = 3;
            this.label9.Text = "0";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label11);
            this.groupBox14.Controls.Add(this.label10);
            this.groupBox14.Controls.Add(this.numericUpDown2);
            this.groupBox14.Controls.Add(this.numericUpDown1);
            this.groupBox14.Controls.Add(this.canvas1);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox14.Location = new System.Drawing.Point(0, 0);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(141, 198);
            this.groupBox14.TabIndex = 14;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Gesture Canvas";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 176);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Interval:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 154);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Window:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(69, 174);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown2.TabIndex = 11;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(69, 152);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown1.TabIndex = 11;
            // 
            // canvas1
            // 
            this.canvas1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas1.Capacity = 20;
            this.canvas1.Continuous = true;
            this.canvas1.Location = new System.Drawing.Point(6, 18);
            this.canvas1.MaximumSize = new System.Drawing.Size(128, 128);
            this.canvas1.MinimumSize = new System.Drawing.Size(128, 128);
            this.canvas1.Name = "canvas1";
            this.canvas1.Size = new System.Drawing.Size(128, 128);
            this.canvas1.TabIndex = 10;
            this.canvas1.SequenceChanged += new System.EventHandler(this.canvas1_SequenceChanged);
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.zedGraphControl1);
            this.groupBox22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox22.Location = new System.Drawing.Point(0, 0);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(463, 403);
            this.groupBox22.TabIndex = 10;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Models relative response";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 16);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(457, 384);
            this.zedGraphControl1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(622, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(145, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveAsToolStripMenuItem.Text = "Save Classifier";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 459);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(622, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(607, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // tabSamples
            // 
            this.tabSamples.Controls.Add(this.splitContainer3);
            this.tabSamples.Location = new System.Drawing.Point(4, 22);
            this.tabSamples.Name = "tabSamples";
            this.tabSamples.Padding = new System.Windows.Forms.Padding(3);
            this.tabSamples.Size = new System.Drawing.Size(636, 370);
            this.tabSamples.TabIndex = 0;
            this.tabSamples.Text = "Samples (Input)";
            this.tabSamples.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer7);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer3.Size = new System.Drawing.Size(630, 364);
            this.splitContainer3.SplitterDistance = 448;
            this.splitContainer3.TabIndex = 8;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.groupBox15);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.groupBox7);
            this.splitContainer7.Size = new System.Drawing.Size(448, 364);
            this.splitContainer7.SplitterDistance = 156;
            this.splitContainer7.TabIndex = 9;
            // 
            // groupBox15
            // 
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox15.Location = new System.Drawing.Point(0, 0);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(156, 364);
            this.groupBox15.TabIndex = 8;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Training";
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 100);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numThreshold);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.btnClassify);
            this.groupBox6.Controls.Add(this.btnSampleRunAnalysis);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(178, 364);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Settings";
            // 
            // numThreshold
            // 
            this.numThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numThreshold.DecimalPlaces = 6;
            this.numThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numThreshold.Location = new System.Drawing.Point(89, 203);
            this.numThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThreshold.Name = "numThreshold";
            this.numThreshold.Size = new System.Drawing.Size(82, 20);
            this.numThreshold.TabIndex = 7;
            this.numThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            262144});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 12;
            // 
            // btnClassify
            // 
            this.btnClassify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassify.Enabled = false;
            this.btnClassify.Location = new System.Drawing.Point(7, 310);
            this.btnClassify.Name = "btnClassify";
            this.btnClassify.Size = new System.Drawing.Size(165, 48);
            this.btnClassify.TabIndex = 1;
            this.btnClassify.Text = "Classify";
            this.btnClassify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClassify.UseVisualStyleBackColor = true;
            // 
            // btnSampleRunAnalysis
            // 
            this.btnSampleRunAnalysis.Location = new System.Drawing.Point(0, 0);
            this.btnSampleRunAnalysis.Name = "btnSampleRunAnalysis";
            this.btnSampleRunAnalysis.Size = new System.Drawing.Size(75, 23);
            this.btnSampleRunAnalysis.TabIndex = 13;
            // 
            // tabOverview
            // 
            this.tabOverview.Location = new System.Drawing.Point(0, 0);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Size = new System.Drawing.Size(200, 100);
            this.tabOverview.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Size = new System.Drawing.Size(630, 364);
            this.splitContainer2.SplitterDistance = 207;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer4.Size = new System.Drawing.Size(630, 207);
            this.splitContainer4.SplitterDistance = 407;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 100);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            // 
            // colComponent
            // 
            this.colComponent.Name = "colComponent";
            // 
            // colEigenValue
            // 
            this.colEigenValue.Name = "colEigenValue";
            // 
            // colProportion
            // 
            this.colProportion.Name = "colProportion";
            // 
            // colCumulativeProportion
            // 
            this.colCumulativeProportion.Name = "colCumulativeProportion";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(624, 134);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(636, 370);
            this.tabPage3.TabIndex = 8;
            this.tabPage3.Text = "Classes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer9
            // 
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.groupBox9);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.groupBox10);
            this.splitContainer9.Size = new System.Drawing.Size(636, 370);
            this.splitContainer9.SplitterDistance = 256;
            this.splitContainer9.TabIndex = 3;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.dgvClasses);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(256, 370);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Classes";
            // 
            // dgvClasses
            // 
            this.dgvClasses.Location = new System.Drawing.Point(0, 0);
            this.dgvClasses.Name = "dgvClasses";
            this.dgvClasses.Size = new System.Drawing.Size(240, 150);
            this.dgvClasses.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Location = new System.Drawing.Point(0, 0);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(200, 100);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // lvClass
            // 
            this.lvClass.Location = new System.Drawing.Point(0, 0);
            this.lvClass.Name = "lvClass";
            this.lvClass.Size = new System.Drawing.Size(121, 97);
            this.lvClass.TabIndex = 0;
            this.lvClass.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(0, 0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(200, 100);
            this.tabPage4.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(200, 100);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            // 
            // lbCanvasClassification
            // 
            this.lbCanvasClassification.Location = new System.Drawing.Point(0, 0);
            this.lbCanvasClassification.Name = "lbCanvasClassification";
            this.lbCanvasClassification.Size = new System.Drawing.Size(100, 23);
            this.lbCanvasClassification.TabIndex = 0;
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox11.Controls.Add(this.button5);
            this.groupBox11.Controls.Add(this.tbPenWidth);
            this.groupBox11.Controls.Add(this.button7);
            this.groupBox11.Location = new System.Drawing.Point(8, 176);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(204, 191);
            this.groupBox11.TabIndex = 8;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Drawing Canvas";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 153);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 23);
            this.button5.TabIndex = 1;
            this.button5.Text = "Classify";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // tbPenWidth
            // 
            this.tbPenWidth.Location = new System.Drawing.Point(149, 19);
            this.tbPenWidth.Minimum = 1;
            this.tbPenWidth.Name = "tbPenWidth";
            this.tbPenWidth.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbPenWidth.Size = new System.Drawing.Size(45, 128);
            this.tbPenWidth.TabIndex = 5;
            this.tbPenWidth.Value = 3;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(111, 153);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(83, 23);
            this.button7.TabIndex = 2;
            this.button7.Text = "Clear";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Location = new System.Drawing.Point(218, 0);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(418, 370);
            this.groupBox12.TabIndex = 7;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Discriminant functions relative response";
            // 
            // groupBox16
            // 
            this.groupBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox16.Controls.Add(this.label8);
            this.groupBox16.Location = new System.Drawing.Point(14, 3);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(198, 167);
            this.groupBox16.TabIndex = 9;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Classification";
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 99.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(192, 148);
            this.label8.TabIndex = 3;
            this.label8.Text = "0";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox17
            // 
            this.groupBox17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox17.Controls.Add(this.button8);
            this.groupBox17.Controls.Add(this.trackBar1);
            this.groupBox17.Controls.Add(this.button9);
            this.groupBox17.Location = new System.Drawing.Point(8, 176);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(204, 191);
            this.groupBox17.TabIndex = 8;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Drawing Canvas";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(15, 153);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(90, 23);
            this.button8.TabIndex = 1;
            this.button8.Text = "Classify";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(149, 19);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 128);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 3;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(111, 153);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(83, 23);
            this.button9.TabIndex = 2;
            this.button9.Text = "Clear";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox18.Location = new System.Drawing.Point(218, 0);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(418, 370);
            this.groupBox18.TabIndex = 7;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Discriminant functions relative response";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "XML files|*.xml|All files|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "XML files|*.xml|All files|*.*";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "bin";
            this.saveFileDialog2.Filter = "All files|*.*";
            this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 481);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Continuous density Hidden Markov Models for Mouse Gesture Recognition";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel2.ResumeLayout(false);
            this.splitContainer11.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSmoothing)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numConvergence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStates)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.groupBox24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModels)).EndInit();
            this.groupBox25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransitions)).EndInit();
            this.groupBox23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProbabilities)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.splitContainer12.Panel1.ResumeLayout(false);
            this.splitContainer12.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer12)).EndInit();
            this.splitContainer12.ResumeLayout(false);
            this.splitContainer13.Panel1.ResumeLayout(false);
            this.splitContainer13.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer13)).EndInit();
            this.splitContainer13.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox22.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabSamples.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClasses)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPenWidth)).EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassification;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Gestures.Canvas inputCanvas;
        private System.Windows.Forms.Button btnAddClass7;
        private System.Windows.Forms.Button btnAddClass4;
        private System.Windows.Forms.Button btnAddClass1;
        private System.Windows.Forms.Button btnCanvasClear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TabPage tabSamples;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numThreshold;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClassify;
        private System.Windows.Forms.Button btnSampleRunAnalysis;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComponent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEigenValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProportion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCumulativeProportion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer9;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.DataGridView dgvClasses;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ListView lvClass;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lbCanvasClassification;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TrackBar tbPenWidth;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numStates;
        private System.Windows.Forms.NumericUpDown numConvergence;
        private System.Windows.Forms.RadioButton rbStopIterations;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.RadioButton rbStopConvergence;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.GroupBox groupBox21;
        private Gestures.Canvas canvas2;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Label lbClassification;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.DataGridView dgvModels;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModelsTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModelsStates;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.DataGridView dgvProbabilities;
        private System.Windows.Forms.SplitContainer splitContainer11;
        private System.Windows.Forms.SplitContainer splitContainer12;
        private System.Windows.Forms.SplitContainer splitContainer13;
        private ZedGraph.ZedGraphControl graphClassification;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.DataGridView dgvTransitions;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckBox cbRejection;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Gestures.Canvas canvas1;
        private System.Windows.Forms.GroupBox groupBox22;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numSmoothing;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Button btnAddClass3;
        private System.Windows.Forms.Button btnAddClass2;
        private System.Windows.Forms.Button btnAddClass6;
        private System.Windows.Forms.Button btnAddClass9;
        private System.Windows.Forms.Button btnAddClass5;
        private System.Windows.Forms.Button btnAddClass8;
    }
}

