using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarAndRadarScanner
{
	public class ScannerResult
	{
		public int ultrasonicAngle { get; set; }
		public int ulstrasonicDistanceCm { get; set; }
		public int radarDistanceMm { get; set; }
		public int id { get; set; }

		public ScannerResult()
		{
			id = int.MinValue;
			ultrasonicAngle = 0;
			ulstrasonicDistanceCm = 0;
			radarDistanceMm = 0;
		}

		public ScannerResult(int ultrasonicAngle, int ulstrasonicDistanceCm, int radarDistanceMm)
		{
			this.ultrasonicAngle = ultrasonicAngle;
			this.ulstrasonicDistanceCm = ulstrasonicDistanceCm;
			this.radarDistanceMm = radarDistanceMm;
		}

	}
}
