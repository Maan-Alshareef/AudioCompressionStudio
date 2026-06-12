namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelDragDrop = new System.Windows.Forms.Panel();
            this.lblDragDrop = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.lblSelectAlgo = new System.Windows.Forms.Label();
            this.cmbAlgorithm = new System.Windows.Forms.ComboBox();

            
            
            
            this.lblQuant = new System.Windows.Forms.Label();
            this.txtQuantLevels = new System.Windows.Forms.TextBox();
            this.lblStep = new System.Windows.Forms.Label();
            this.txtStepSize = new System.Windows.Forms.TextBox();

            this.btnCompress = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();

            
            
            
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblReport = new System.Windows.Forms.Label();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelLeft.SuspendLayout();
            this.panelDragDrop.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.SuspendLayout();


            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1150, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Audio Compression Pipeline Studio ";

            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);


            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Controls.Add(this.panelDragDrop);
            this.panelLeft.Controls.Add(this.btnBrowse);
            this.panelLeft.Controls.Add(this.btnPlay);
            this.panelLeft.Controls.Add(this.btnStop);
            this.panelLeft.Controls.Add(this.lblFileInfo);
            this.panelLeft.Controls.Add(this.lblSelectAlgo);
            this.panelLeft.Controls.Add(this.cmbAlgorithm);


            this.panelLeft.Controls.Add(this.lblQuant);
            this.panelLeft.Controls.Add(this.txtQuantLevels);
            this.panelLeft.Controls.Add(this.lblStep);
            this.panelLeft.Controls.Add(this.txtStepSize);

            this.panelLeft.Controls.Add(this.btnCompress);
            this.panelLeft.Controls.Add(this.btnCancel);
            this.panelLeft.Controls.Add(this.btnDecompress);
            this.panelLeft.Controls.Add(this.btnReset);
            this.panelLeft.Controls.Add(this.progressBar);
            this.panelLeft.Location = new System.Drawing.Point(20, 20);
            this.panelLeft.Size = new System.Drawing.Size(460, 680);
            this.panelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelLeft_Paint);

            
            this.panelDragDrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.panelDragDrop.Controls.Add(this.lblDragDrop);
            this.panelDragDrop.Location = new System.Drawing.Point(20, 20);
            this.panelDragDrop.Size = new System.Drawing.Size(420, 90);
            this.panelDragDrop.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDragDrop_Paint);

            this.lblDragDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDragDrop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDragDrop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.lblDragDrop.Location = new System.Drawing.Point(0, 0);
            this.lblDragDrop.Size = new System.Drawing.Size(420, 90);
            this.lblDragDrop.Text = " DRAG & DROP AUDIO FILE HERE";
            this.lblDragDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(20, 125);
            this.btnBrowse.Size = new System.Drawing.Size(420, 38);
            this.btnBrowse.Text = " Browse Local Audio File...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);

            
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(20, 175);
            this.btnPlay.Size = new System.Drawing.Size(200, 35);
            this.btnPlay.Text = " Play ";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);

            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(240, 175);
            this.btnStop.Size = new System.Drawing.Size(200, 35);
            this.btnStop.Text = " Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);

            this.lblFileInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.lblFileInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblFileInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFileInfo.Location = new System.Drawing.Point(20, 225);
            this.lblFileInfo.Size = new System.Drawing.Size(420, 150);
            this.lblFileInfo.Text = " AUDIO FILE METADATA:\n\nNo file loaded. Please drop or browse a .wav clip to inspect properties.";

            
            this.lblSelectAlgo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSelectAlgo.Location = new System.Drawing.Point(20, 395);
            this.lblSelectAlgo.Size = new System.Drawing.Size(200, 20);
            this.lblSelectAlgo.Text = "Select Compression Method:";

            this.cmbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlgorithm.FormattingEnabled = true;
            this.cmbAlgorithm.Items.AddRange(new object[] {
            "Nonlinear Quantization (μ-law)",
            "Delta Modulation (DM)",
            "Differential PCM (DPCM)"});
            this.cmbAlgorithm.Location = new System.Drawing.Point(20, 420);
            this.cmbAlgorithm.Size = new System.Drawing.Size(420, 29);
            this.cmbAlgorithm.SelectedIndex = 0;

            
            this.lblQuant.Text = "Quantization Levels:";
            this.lblQuant.Location = new System.Drawing.Point(20, 465);
            this.lblQuant.Size = new System.Drawing.Size(130, 20);
            this.lblQuant.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);

            this.txtQuantLevels.Location = new System.Drawing.Point(155, 462);
            this.txtQuantLevels.Size = new System.Drawing.Size(65, 24);
            this.txtQuantLevels.Text = "256";

            this.lblStep.Text = "Delta Step Size:";
            this.lblStep.Location = new System.Drawing.Point(235, 465);
            this.lblStep.Size = new System.Drawing.Size(110, 20);
            this.lblStep.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);

            this.txtStepSize.Location = new System.Drawing.Point(350, 462);
            this.txtStepSize.Size = new System.Drawing.Size(90, 24);
            this.txtStepSize.Text = "250";


            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.btnCompress.Enabled = false;
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCompress.ForeColor = System.Drawing.Color.White;
            this.btnCompress.Location = new System.Drawing.Point(20, 505);
            this.btnCompress.Size = new System.Drawing.Size(260, 38);
            this.btnCompress.Text = " Execute Compression";
            this.btnCompress.UseVisualStyleBackColor = false;
            this.btnCompress.Click += new System.EventHandler(this.BtnCompress_Click);

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(290, 505);
            this.btnCancel.Size = new System.Drawing.Size(150, 38);
            this.btnCancel.Text = " Cancell Process";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnDecompress.Enabled = false;
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.Location = new System.Drawing.Point(20, 555);
            this.btnDecompress.Size = new System.Drawing.Size(420, 42);
            this.btnDecompress.Text = " Decompress && Recover Native Audio Signal";
            this.btnDecompress.UseVisualStyleBackColor = false;
            this.btnDecompress.Click += new System.EventHandler(this.BtnDecompress_Click);

            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(20, 605);
            this.btnReset.Size = new System.Drawing.Size(420, 35);
            this.btnReset.Text = " Reset Original Values";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);

            this.progressBar.Location = new System.Drawing.Point(20, 650);
            this.progressBar.Size = new System.Drawing.Size(420, 25);

            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.lblReport);
            this.panelRight.Controls.Add(this.chartPerformance);
            this.panelRight.Location = new System.Drawing.Point(500, 20);
            this.panelRight.Size = new System.Drawing.Size(630, 680);
            this.panelRight.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelRight_Paint);

            
            this.lblReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.lblReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblReport.Location = new System.Drawing.Point(20, 20);
            this.lblReport.Size = new System.Drawing.Size(590, 100);
            this.lblReport.Text = "Pipeline idle. Dashboard monitoring ready. Statistical optimization reports will be populated here.";


            chartArea1.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartPerformance.Legends.Add(legend1);
            this.chartPerformance.Location = new System.Drawing.Point(20, 140);
            this.chartPerformance.Size = new System.Drawing.Size(590, 515);


            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            series1.Legend = "Legend1";
            series1.Name = "Savings Ratio %";
            series1.BorderWidth = 3;


            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(126)))), ((int)(((byte)(20)))));
            series2.Legend = "Legend1";
            series2.Name = "Speed (kSamples/sec)";
            series2.BorderWidth = 3;

            this.chartPerformance.Series.Add(series1);
            this.chartPerformance.Series.Add(series2);

            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);

            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelDragDrop.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion


        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelDragDrop;
        private System.Windows.Forms.Label lblDragDrop;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.Label lblSelectAlgo;
        private System.Windows.Forms.ComboBox cmbAlgorithm;

        
        private System.Windows.Forms.Label lblQuant;
        private System.Windows.Forms.TextBox txtQuantLevels;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.TextBox txtStepSize;

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
    }
}