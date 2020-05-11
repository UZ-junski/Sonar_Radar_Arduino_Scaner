using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SonarAndRadarScanner
{
	public partial class SonarAndRadarScanner : Form
	{
		private const int ULTRASONIC_ZERO_AMGLE = 10;
		private const int RADAR_ZERO_ANGLE = 49;
		private int WIDTH = 786, HEIGHT = 586;
		private int degree;
		private int cx, cy;
		private int x, y;
		private int tx, ty, lim = 20;

		private Bitmap bmp;
		private Pen p;
		private Graphics g;
		private Dictionary<int, Dictionary<int, int>> ultrasonic;
		private Dictionary<int, Dictionary<int, int>> radar;
		private bool isMeasure;
		private Random rnd;


		public SonarAndRadarScanner()
		{
			InitializeComponent();
			btnConnect.Enabled = false;
			btnDisconnect.Enabled = false;
			btnCreateBaseLine.Enabled = false;
			btnDoMeasurement.Enabled = false;
			btnSaveBaseLine.Enabled = false;
			btnSaveBmp.Enabled = false;
			isMeasure = false;			
		}

		private void btnScanPort_Click(object sender, EventArgs e)
		{
			var ports = System.IO.Ports.SerialPort.GetPortNames();
			cmbxPorts.Items.AddRange(ports);
			cmbxPorts.SelectedIndex = cmbxPorts.Items.Count - 1;
			btnConnect.Enabled = true;
		}

		private void SonarAndRadarScanner_Load(object sender, EventArgs e)
		{

		}

		private async void btnCreateBaseLine_ClickAsync(object sender, EventArgs e)
		{
			isMeasure = false;
			btnSaveBaseLine.Enabled = false;
			btnDoMeasurement.Enabled = false;
			btnCreateBaseLine.Enabled = false;
			btnSaveBmp.Enabled = false;
			bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
			pictureBox1.BackColor = Color.Black;
			cx = WIDTH / 2;
			cy = HEIGHT;
			degree = 0;
			tbMessages.Clear();
			serialPort1.Write("#START");
			await SerialPortReader(10);
			//Task.Factory.StartNew(() => SerialPortReader());
			btnSaveBaseLine.Enabled = true;
			btnDoMeasurement.Enabled = true;
			btnCreateBaseLine.Enabled = true;
			btnSaveBmp.Enabled = true;
		}

		private async void btnDoMeasurement_Click(object sender, EventArgs e)
		{
			isMeasure = true;
			btnSaveBaseLine.Enabled = false;
			btnDoMeasurement.Enabled = false;
			btnCreateBaseLine.Enabled = false;
			btnSaveBmp.Enabled = false;
			bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
			pictureBox1.BackColor = Color.Black;
			cx = WIDTH / 2;
			cy = HEIGHT;
			degree = 0;
			tbMessages.Clear();
			serialPort1.Write("#START");
			await SerialPortReader(10);
			//Task.Factory.StartNew(() => SerialPortReader());
			btnSaveBaseLine.Enabled = true;
			btnDoMeasurement.Enabled = true;
			btnCreateBaseLine.Enabled = true;
			btnSaveBmp.Enabled = true;
		}

		private void btnSaveBaseLine_Click(object sender, EventArgs e)
		{
			btnSaveBaseLine.Enabled = false;
			btnDoMeasurement.Enabled = false;
			btnCreateBaseLine.Enabled = false;
			btnSaveBmp.Enabled = false;
			string dateTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
			SaveFileDialog savefile = new SaveFileDialog();
			string typeMeasurement;
			if (isMeasure)
			{
				typeMeasurement = "measurement";
			}
			else
			{
				typeMeasurement = "baseline";
			}
			savefile.FileName = $"{typeMeasurement}_ultrasonic_{dateTimeNow}.csv";
			savefile.Filter = "CSV files (*.csv)|*.csv";

			if (savefile.ShowDialog() == DialogResult.OK)
			{
				string ultrasonicFile = savefile.FileName;
				savefile = new SaveFileDialog();
				savefile.FileName = $"{typeMeasurement}_radar_{dateTimeNow}.csv";
				savefile.Filter = "CSV files (*.csv)|*.csv";
				if (savefile.ShowDialog() == DialogResult.OK)
				{
					using (StreamWriter sw = new StreamWriter(ultrasonicFile))
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine("ScanNo;Angle;Distance");
						foreach (var ultrasonicRound in ultrasonic)
						{
							foreach (var ultrasonicResult in ultrasonicRound.Value)
							{
								stringBuilder.AppendLine(ultrasonicRound.Key + ";" + ultrasonicResult.Key + ";" + ultrasonicResult.Value);
							}
						}
						sw.Write(stringBuilder);
					}
					using (StreamWriter sw = new StreamWriter(savefile.FileName))
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine("ScanNo;Angle;Distance");
						foreach (var radarRound in radar)
						{
							foreach (var radarResult in radarRound.Value)
							{
								stringBuilder.AppendLine(radarRound.Key + ";" + radarResult.Key + ";" + radarResult.Value);
							}
						}
						sw.Write(stringBuilder);
					}
				}
			}
			btnSaveBaseLine.Enabled = true;
			btnDoMeasurement.Enabled = true;
			btnCreateBaseLine.Enabled = true;
			btnSaveBmp.Enabled = true;
		}

		private void btnSaveBmp_Click(object sender, EventArgs e)
		{
			string dateTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
			SaveFileDialog savefile = new SaveFileDialog();
			string typeMeasurement;
			if (isMeasure)
			{
				typeMeasurement = "measurement";
			}
			else
			{
				typeMeasurement = "baseline";
			}
			savefile.FileName = $"{typeMeasurement}_ultrasonic_{dateTimeNow}.bmp";
			savefile.Filter = "Image files (*.bmp)|*.bmp";

			if (savefile.ShowDialog() == DialogResult.OK)
			{
				pictureBox1.Image.Save(savefile.FileName);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{		
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "csv files (*.csv)|*.csv";
			//dialog.InitialDirectory = @"C:\";
			//dialog.Title = "Please select an image file to encrypt.";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				rnd = new Random();
				int id = 0;
				Dictionary<int, List<RararElement>> allScans = new Dictionary<int, List<RararElement>>();
				var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.CurrentCulture);
				config.Delimiter = ";";
				config.HasHeaderRecord = true;
				bool isRadar = false;
				if (dialog.FileName.Contains("radar"))
				{
					isRadar = true;
				}
				using (CsvReader csv = new CsvReader(new StreamReader(dialog.FileName), config))
				{
					csv.Read();
					csv.ReadHeader();
					while (csv.Read())
					{
						int scanNo = int.Parse(csv["ScanNo"]);
						int angle = int.Parse(csv["Angle"]);
						int distance = int.Parse(csv["Distance"]);
						if(isRadar)
						{
							distance /= 10;
						}
						if (!allScans.ContainsKey(scanNo))
						{
							allScans.Add(scanNo, new List<RararElement>() { new RararElement() { Anglee = angle, Distance = distance } });
						}
						else
						{
							allScans[scanNo].Add(new RararElement() { Anglee = angle, Distance = distance });
						}
					}

				}
				isMeasure = false;
				btnSaveBaseLine.Enabled = false;
				btnDoMeasurement.Enabled = false;
				btnCreateBaseLine.Enabled = false;
				btnSaveBmp.Enabled = false;
				bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
				pictureBox1.BackColor = Color.Black;
				cx = WIDTH / 2;
				cy = HEIGHT;
				degree = 0;
				tbMessages.Clear();
				RadarScanner(allScans);
			}
		}

		public void RadarScanner(Dictionary<int, List<RararElement>> allScans)
		{
			foreach (var scanSeria in allScans)
			{
				var color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
				foreach (var singleSeria in scanSeria.Value)
				{
					if (singleSeria.Anglee < 91)
					{
						singleSeria.Anglee += 270;
					}
					else
					{
						singleSeria.Anglee -= 90;
					}
					GraphicWrite(singleSeria, color);
				}				
			}
			btnSaveBaseLine.Enabled = true;
			btnDoMeasurement.Enabled = true;
			btnCreateBaseLine.Enabled = true;
			btnSaveBmp.Enabled = true;
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			btnConnect.Enabled = false;
			btnDisconnect.Enabled = true;
			var portName = cmbxPorts.GetItemText(cmbxPorts.SelectedItem);
			serialPort1 = new SerialPort();
			serialPort1.PortName = portName;
			serialPort1.BaudRate = 115200;
			serialPort1.Parity = Parity.None;
			serialPort1.DataBits = 8;
			serialPort1.StopBits = StopBits.One;
			serialPort1.RtsEnable = true;
			serialPort1.Handshake = Handshake.None;
			serialPort1.Open();
			btnCreateBaseLine.Enabled = true;
			btnDoMeasurement.Enabled = true;
			btnSaveBaseLine.Enabled = false;
			btnSaveBmp.Enabled = false;

		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			btnDisconnect.Enabled = false;
			btnConnect.Enabled = true;
			btnCreateBaseLine.Enabled = false;
			btnDoMeasurement.Enabled = false;
			btnSaveBaseLine.Enabled = false;
			btnSaveBmp.Enabled = false;
			serialPort1.Write("#STOP");
			serialPort1.Close();
		}

		private async Task SerialPortReader(int count = 10)
		{
			radar = new Dictionary<int, Dictionary<int, int>>();
			ultrasonic = new Dictionary<int, Dictionary<int, int>>();
			bool incrementReady = false;
			for (int baseLineCnt = 0; baseLineCnt < count; baseLineCnt++)
			{
				radar.Add(baseLineCnt, new Dictionary<int, int>());
				ultrasonic.Add(baseLineCnt, new Dictionary<int, int>());
				while (!incrementReady)
				{
					string read;
					try
					{
						read = serialPort1.ReadLine();
						read = read.Replace("\r", "");
					}
					catch
					{
						return;
					}
					if (!string.IsNullOrEmpty(read))
					{
						List<ScannerResult> scannerResults = GetAngleFromMessage(read);
						if (scannerResults.Count > 0)
						{
							foreach (var scannerResult in scannerResults)
							{
								radar[baseLineCnt].Add(scannerResult.ultrasonicAngle - RADAR_ZERO_ANGLE + ULTRASONIC_ZERO_AMGLE, scannerResult.radarDistanceMm);
								ultrasonic[baseLineCnt].Add(scannerResult.ultrasonicAngle, scannerResult.ulstrasonicDistanceCm);
								if (scannerResult.ultrasonicAngle == 170)
								{
									incrementReady = true;
								}
								if (scannerResult.ultrasonicAngle < 91)
								{
									scannerResult.ultrasonicAngle += 270;
								}
								else
								{
									scannerResult.ultrasonicAngle -= 90;
								}
								GraphicWrite(scannerResult, baseLineCnt);
							}
						}
						else
						{
							Console.WriteLine("empty from line:" + read);
						}
						tbMessages.Invoke(new Action(() =>
						{
							tbMessages.AppendText(read + Environment.NewLine);
						}));
					}
					await Task.Delay(100);
				}
				incrementReady = false;
			}
			serialPort1.Write("#STOP");
		}

		private List<ScannerResult> GetAngleFromMessage(string readed)
		{
			List<ScannerResult> scannerResults = new List<ScannerResult>();
			if (!string.IsNullOrEmpty(readed))
			{
				bool canRead = false;
				ScannerResult scannerResult = new ScannerResult();
				foreach (var line in readed.Split('|'))
				{
					if (line.Contains("Kat czujnika: "))
					{
						if (canRead)
						{
							var tempLine = line.Replace("Kat czujnika: ", "");
							scannerResult.ultrasonicAngle = int.Parse(tempLine) - ULTRASONIC_ZERO_AMGLE;
						}
					}
					else if (line.Contains("Czujnik ultradzwiekowy: "))
					{
						if (canRead)
						{
							var tempLine = line.Replace("Czujnik ultradzwiekowy: ", "");
							scannerResult.ulstrasonicDistanceCm = int.Parse(tempLine);
							scannerResults.Add(scannerResult);
							canRead = false;
						}
					}
					else if (line.Contains("Czujnik radarowy: "))
					{
						if (canRead)
						{
							var tempLine = line.Replace("Czujnik radarowy: ", "");
							scannerResult.radarDistanceMm = int.Parse(tempLine);
						}
					}
					else
					{
						int tempval;
						if (int.TryParse(line, out tempval))
						{
							scannerResult = new ScannerResult();
							scannerResult.id = tempval;
							canRead = true;
						}
					}
				}
			}
			return scannerResults;
		}

		private void GraphicWrite(ScannerResult scannerResults, int colorID)
		{
			degree = scannerResults.ultrasonicAngle;
			p = new Pen(Color.Green, 1f);
			g = Graphics.FromImage(bmp);
			int tu = (degree - lim) % 360;
			if (degree >= 0 && degree <= 180)
			{
				x = cx + (int)(scannerResults.ulstrasonicDistanceCm * Math.Sin(Math.PI * degree / 180));
				y = cy - (int)(scannerResults.ulstrasonicDistanceCm * Math.Cos(Math.PI * degree / 180));
			}
			else
			{
				x = cx - (int)(scannerResults.ulstrasonicDistanceCm * -Math.Sin(Math.PI * degree / 180));
				y = cy - (int)(scannerResults.ulstrasonicDistanceCm * Math.Cos(Math.PI * degree / 180));
			}

			if (tu >= 0 && tu <= 180)
			{
				tx = cx + (int)(scannerResults.ulstrasonicDistanceCm * Math.Sin(Math.PI * tu / 180));
				ty = cy - (int)(scannerResults.ulstrasonicDistanceCm * Math.Cos(Math.PI * tu / 180));
			}
			else
			{
				tx = cx - (int)(scannerResults.ulstrasonicDistanceCm * -Math.Sin(Math.PI * tu / 180));
				ty = cy - (int)(scannerResults.ulstrasonicDistanceCm * Math.Cos(Math.PI * tu / 180));
			}
			//g.DrawEllipse(p, 0, 0, WIDTH, HEIGHT);
			//g.DrawEllipse(p, 80, 80, WIDTH - 160, HEIGHT - 160);
			g.DrawLine(p, new Point(cx, 0), new Point(cx, HEIGHT));
			g.DrawLine(p, new Point(0, cy), new Point(WIDTH, cy));
			//g.DrawLine(new Pen(Color.Black, 1f), new Point(cx, cy), new Point(tx, ty));
			//g.DrawLine(p, new Point(cx, cy), new Point(x, y));
			Brush brush;
			switch (colorID)
			{
				case 0:
					brush = Brushes.White;
					break;
				case 1:
					brush = Brushes.Yellow;
					break;
				case 2:
					brush = Brushes.GreenYellow;
					break;
				case 3:
					brush = Brushes.MediumOrchid;
					break;
				case 4:
					brush = Brushes.Green;
					break;
				case 5:
					brush = Brushes.Orange;
					break;
				case 6:
					brush = Brushes.DarkViolet;
					break;
				case 7:
					brush = Brushes.Firebrick;
					break;
				case 8:
					brush = Brushes.DodgerBlue;
					break;
				case 9:
					brush = Brushes.DeepPink;
					break;
				default:
					brush = Brushes.White;
					break;
			}
			g.FillRectangle(brush, x, y, 1, 1);


			pictureBox1.Invoke(new Action(() =>
			{
				pictureBox1.Image = bmp;
			}));
			p.Dispose();
			g.Dispose();

		}

		private void GraphicWrite(RararElement radarElement, Color color)
		{
			degree = radarElement.Anglee;
			p = new Pen(Color.Green, 1f);
			g = Graphics.FromImage(bmp);
			int tu = (degree - lim) % 360;
			if (degree >= 0 && degree <= 180)
			{
				x = cx + (int)(radarElement.Distance * Math.Sin(Math.PI * degree / 180));
				y = cy - (int)(radarElement.Distance * Math.Cos(Math.PI * degree / 180));
			}
			else
			{
				x = cx - (int)(radarElement.Distance * -Math.Sin(Math.PI * degree / 180));
				y = cy - (int)(radarElement.Distance * Math.Cos(Math.PI * degree / 180));
			}

			if (tu >= 0 && tu <= 180)
			{
				tx = cx + (int)(radarElement.Distance * Math.Sin(Math.PI * tu / 180));
				ty = cy - (int)(radarElement.Distance * Math.Cos(Math.PI * tu / 180));
			}
			else
			{
				tx = cx - (int)(radarElement.Distance * -Math.Sin(Math.PI * tu / 180));
				ty = cy - (int)(radarElement.Distance * Math.Cos(Math.PI * tu / 180));
			}
			//g.DrawEllipse(p, 0, 0, WIDTH, HEIGHT);
			//g.DrawEllipse(p, 80, 80, WIDTH - 160, HEIGHT - 160);
			g.DrawLine(p, new Point(cx, 0), new Point(cx, HEIGHT));
			g.DrawLine(p, new Point(0, cy), new Point(WIDTH, cy));
			//g.DrawLine(new Pen(Color.Black, 1f), new Point(cx, cy), new Point(tx, ty));
			//g.DrawLine(p, new Point(cx, cy), new Point(x, y));
			Brush brush = new SolidBrush(color);			
			g.FillRectangle(brush, x, y, 1, 1);


			pictureBox1.Invoke(new Action(() =>
			{
				pictureBox1.Image = bmp;
			}));
			p.Dispose();
		}
	}
}
