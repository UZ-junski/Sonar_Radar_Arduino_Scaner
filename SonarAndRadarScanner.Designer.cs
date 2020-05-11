namespace SonarAndRadarScanner
{
	partial class SonarAndRadarScanner
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
			this.components = new System.ComponentModel.Container();
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.btnScanPort = new System.Windows.Forms.Button();
			this.cmbxPorts = new System.Windows.Forms.ComboBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnDisconnect = new System.Windows.Forms.Button();
			this.tbMessages = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnCreateBaseLine = new System.Windows.Forms.Button();
			this.btnDoMeasurement = new System.Windows.Forms.Button();
			this.btnSaveBaseLine = new System.Windows.Forms.Button();
			this.btnSaveBmp = new System.Windows.Forms.Button();
			this.btnOpenCsvFile = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnScanPort
			// 
			this.btnScanPort.Location = new System.Drawing.Point(23, 12);
			this.btnScanPort.Name = "btnScanPort";
			this.btnScanPort.Size = new System.Drawing.Size(75, 23);
			this.btnScanPort.TabIndex = 0;
			this.btnScanPort.Text = "Scan Port";
			this.btnScanPort.UseVisualStyleBackColor = true;
			this.btnScanPort.Click += new System.EventHandler(this.btnScanPort_Click);
			// 
			// cmbxPorts
			// 
			this.cmbxPorts.FormattingEnabled = true;
			this.cmbxPorts.Location = new System.Drawing.Point(105, 13);
			this.cmbxPorts.Name = "cmbxPorts";
			this.cmbxPorts.Size = new System.Drawing.Size(121, 21);
			this.cmbxPorts.TabIndex = 1;
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(23, 41);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(270, 23);
			this.btnConnect.TabIndex = 2;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnDisconnect
			// 
			this.btnDisconnect.Location = new System.Drawing.Point(299, 41);
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new System.Drawing.Size(274, 23);
			this.btnDisconnect.TabIndex = 3;
			this.btnDisconnect.Text = "Disconnect";
			this.btnDisconnect.UseVisualStyleBackColor = true;
			this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
			// 
			// tbMessages
			// 
			this.tbMessages.Location = new System.Drawing.Point(23, 809);
			this.tbMessages.Multiline = true;
			this.tbMessages.Name = "tbMessages";
			this.tbMessages.ReadOnly = true;
			this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbMessages.Size = new System.Drawing.Size(550, 160);
			this.tbMessages.TabIndex = 4;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(23, 128);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(786, 586);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// btnCreateBaseLine
			// 
			this.btnCreateBaseLine.Location = new System.Drawing.Point(23, 70);
			this.btnCreateBaseLine.Name = "btnCreateBaseLine";
			this.btnCreateBaseLine.Size = new System.Drawing.Size(550, 23);
			this.btnCreateBaseLine.TabIndex = 6;
			this.btnCreateBaseLine.Text = "Create BaseLine";
			this.btnCreateBaseLine.UseVisualStyleBackColor = true;
			this.btnCreateBaseLine.Click += new System.EventHandler(this.btnCreateBaseLine_ClickAsync);
			// 
			// btnDoMeasurement
			// 
			this.btnDoMeasurement.Location = new System.Drawing.Point(23, 99);
			this.btnDoMeasurement.Name = "btnDoMeasurement";
			this.btnDoMeasurement.Size = new System.Drawing.Size(550, 23);
			this.btnDoMeasurement.TabIndex = 7;
			this.btnDoMeasurement.Text = "Do Measurement";
			this.btnDoMeasurement.UseVisualStyleBackColor = true;
			this.btnDoMeasurement.Click += new System.EventHandler(this.btnDoMeasurement_Click);
			// 
			// btnSaveBaseLine
			// 
			this.btnSaveBaseLine.Location = new System.Drawing.Point(23, 749);
			this.btnSaveBaseLine.Name = "btnSaveBaseLine";
			this.btnSaveBaseLine.Size = new System.Drawing.Size(550, 23);
			this.btnSaveBaseLine.TabIndex = 8;
			this.btnSaveBaseLine.Text = "Save to .CSV";
			this.btnSaveBaseLine.UseVisualStyleBackColor = true;
			this.btnSaveBaseLine.Click += new System.EventHandler(this.btnSaveBaseLine_Click);
			// 
			// btnSaveBmp
			// 
			this.btnSaveBmp.Location = new System.Drawing.Point(23, 720);
			this.btnSaveBmp.Name = "btnSaveBmp";
			this.btnSaveBmp.Size = new System.Drawing.Size(550, 23);
			this.btnSaveBmp.TabIndex = 9;
			this.btnSaveBmp.Text = "Save to .bmp";
			this.btnSaveBmp.UseVisualStyleBackColor = true;
			this.btnSaveBmp.Click += new System.EventHandler(this.btnSaveBmp_Click);
			// 
			// btnOpenCsvFile
			// 
			this.btnOpenCsvFile.Location = new System.Drawing.Point(23, 975);
			this.btnOpenCsvFile.Name = "btnOpenCsvFile";
			this.btnOpenCsvFile.Size = new System.Drawing.Size(75, 23);
			this.btnOpenCsvFile.TabIndex = 10;
			this.btnOpenCsvFile.Text = "Open csv file";
			this.btnOpenCsvFile.UseVisualStyleBackColor = true;
			this.btnOpenCsvFile.Click += new System.EventHandler(this.button1_Click);
			// 
			// SonarAndRadarScanner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 1096);
			this.Controls.Add(this.btnOpenCsvFile);
			this.Controls.Add(this.btnSaveBmp);
			this.Controls.Add(this.btnSaveBaseLine);
			this.Controls.Add(this.btnDoMeasurement);
			this.Controls.Add(this.btnCreateBaseLine);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.tbMessages);
			this.Controls.Add(this.btnDisconnect);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.cmbxPorts);
			this.Controls.Add(this.btnScanPort);
			this.Name = "SonarAndRadarScanner";
			this.Text = "SJU_RadarScanner";
			this.Load += new System.EventHandler(this.SonarAndRadarScanner_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Button btnScanPort;
		private System.Windows.Forms.ComboBox cmbxPorts;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnDisconnect;
		private System.Windows.Forms.TextBox tbMessages;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnCreateBaseLine;
		private System.Windows.Forms.Button btnDoMeasurement;
		private System.Windows.Forms.Button btnSaveBaseLine;
		private System.Windows.Forms.Button btnSaveBmp;
		private System.Windows.Forms.Button btnOpenCsvFile;
	}
}

