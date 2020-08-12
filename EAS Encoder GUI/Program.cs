using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EAS_Encoder_GUI {
	static class Program {
		static Dictionary<string, Dictionary<string, string>> SAMECodes = new Dictionary<string, Dictionary<string, string>>();

		public static string[] GetCounties(string state) {
			return SAMECodes[state].Keys.ToArray();
		}

		public static string GetSAMECode(string county, string state) {
			return SAMECodes[state][county];
		}

		private static List<int> PackBytes(string format, decimal sample) {
			var output = new List<int>();
			foreach (var c in format) {
				var arg = sample;

				switch (c) {
					case 'v': // little-endian unsigned short
						output.Add((int) arg & 255);
						output.Add(((int) arg >> 8) & 255);

						break;
				}
			}
			return output;
		}

		private static int[] RenderTone(SAMEAudioBit byteSpec) {
			if (byteSpec.length != 0.00192M) {
				_ = 1;
			}
			
			Dictionary<decimal, List<int>> headerByteCache = new Dictionary<decimal, List<int>>();
			var computedSamples = new List<int>();
			for (var i = 0; i < (44100 * byteSpec.length); i++) {
				for (var c = 0; c < 2; c++) {
					var sample = (decimal) (byteSpec.volume * Math.Sin((2 * Math.PI) * (i / 44100d) * byteSpec.frequency));
					if (headerByteCache.ContainsKey(sample)) {
						computedSamples.AddRange(headerByteCache[sample]);
					} else {
						List<int> thisSample = PackBytes("v", sample);
						computedSamples.AddRange(thisSample);
						headerByteCache.Add(sample, thisSample);
					}
				}

			}
			if (computedSamples.Count() != 340) {
				_ = false;
			}
			return computedSamples.ToArray();
		}

		private static string AddLeadingZero(int value) {
			if (value < 10) {
				return $"0{value}";
			}
			return $"{value}";
		}
		
		static int[] GenerateBinaryHeaders(string str, int volume) {
			SAMEAudioBit Mark = new SAMEAudioBit(2083, (decimal) 0.00192, volume);
			SAMEAudioBit Space = new SAMEAudioBit(1563, (decimal) 0.00192, volume);

			byte[] byteArray = Encoding.Default.GetBytes(str);  // return byte array representation of the string

			// converts bits in array to store information about how to render each bit
			List<SAMEAudioBit> byteSpec = new List<SAMEAudioBit>();
			SAMEAudioBit[] oneByte;
			foreach (byte thisByte in byteArray) {
				oneByte = new SAMEAudioBit[8];
				char c = (char) thisByte;
				for (int bit = 0; bit < 8; bit++) {
					SAMEAudioBit thisBit = (thisByte & (short) Math.Pow(2, bit)) != 0 ? Mark : Space;
					byteSpec.Add(new SAMEAudioBit(thisBit.frequency, thisBit.length, volume));
					oneByte[bit] = thisBit;
				}
			}

			// begin rendering tones from binary data
			List<int> toneFrequencies = new List<int>();
			foreach (SAMEAudioBit currentSpec in byteSpec) {
				int[] returnedBytes = RenderTone(currentSpec);
				toneFrequencies.AddRange(returnedBytes);
			}

			bool IsExpected = true && (toneFrequencies[4] == 80 || toneFrequencies[4] == 182) && (toneFrequencies[5] == 5 || toneFrequencies[5] == 4);
			IsExpected &= (toneFrequencies[6] == 182 || toneFrequencies[6] == 80) && (toneFrequencies[7] == 5 || toneFrequencies[7] == 4);
			for (int i = 0; i <= 3; i++) {
				if (toneFrequencies[i] != 0) {
					IsExpected = false;
				}
			}
			if (!IsExpected) {
				_ = 1;
			}

			//string output = "[";
			//foreach (int t in toneFrequencies) {
			//	output += $"{(t < 10 ? "  " : t < 100 ? " " : "")}{t},";
			//}
			//output += output.Substring(0, output.Length - 1) + $"] -> {str}\n";
			//File.AppendAllText("characters.txt", output);
			return toneFrequencies.ToArray();
		}

		private static byte[] GenerateTone(bool ebs) {
			var stream = new MemoryStream();
			var writer = new BinaryWriter(stream);
			var samplesPerSecond = 44100;
			var samples = samplesPerSecond * 2;
			double ampl = 5000;
			var concert = 1.0;
			for (int i = 0; i < samples / 2; i++) {
				double frequency = !ebs ? 1050.0 : i % 2 == 0 ? 853.0 : 960.0;
				var t = i / (double) samplesPerSecond;
				var s = (short) (ampl * (Math.Sin(t * frequency * concert * 2.0 * Math.PI)));
				writer.Write(s);
				writer.Write(s);
			}
			writer.Close();
			stream.Close();
			return stream.ToArray();
		}

		static void WriteTone(byte[] toneData, int[] silenceData, BinaryWriter wr) {
			if (toneData.Length != 0) {
				for (int seconds = 0; seconds < 8; seconds++) {
					for (int i = 0; i < toneData.Length; i++) {
						wr.Write(toneData[i]);
					}
				}

				foreach (int thisChar in silenceData) {
					wr.Write((byte) thisChar);
				}
			}
		}

		static void GenerateWavFile(string filename, MemoryStream stream) {
			var f = new FileStream(filename + ".wav", FileMode.Create);
			using (var wr = new BinaryWriter(f)) {
				stream.CopyTo(wr.BaseStream);
			}
			f.Close();
		}

		private static int totalSilenceData = 176400;

		private static void WriteWAVHeaders(BinaryWriter wr, int totalDataLength) {
			ushort bitsPerSample = 16;
			ushort bytesPerSample = (ushort) (bitsPerSample / 8);
			uint sampleRate = 44100;
			ushort channels = 2;
			wr.Write("RIFF".ToArray()); // can't write string on its own otherwise it adds the EOT ASCII character
			wr.Write(36 + totalDataLength * channels * bytesPerSample);
			wr.Write("WAVE".ToArray());
			wr.Write("fmt ".ToArray()); //  Sub-chunk 1
			wr.Write(16);  //  Sub-chunk 1 ID
			wr.Write((ushort) 1);  // Audio format (floating point (3) or PCM (1)). Any other format indicates compression
			wr.Write(channels);
			wr.Write(sampleRate);
			wr.Write(sampleRate * channels * bytesPerSample);  // bit rate
			wr.Write((ushort) (channels * bytesPerSample));   // block align - ushort * ushort = int for some reason; cast is required
			wr.Write(bitsPerSample);
			wr.Write("data".ToArray()); // sub-chunk 2
			wr.Write(totalDataLength / channels * bytesPerSample);   // sub-chunk 2 size
		}

		public static void GenerateEASTones(string originator, string senderID, string eventCode, DateTime startDate, int durHours, int durMinutes, DataRowCollection rows, bool ebsTone, 
			bool nwsTone) {
			string preamble = "";
			for (int i = 0; i < 16; i++) {
				preamble += "\xAB";
			}

			// below variable names follow the formatting used for SAME codes
			string ORG = "";
			switch (originator) {
				case "Primary Entry Point Station":
					ORG = "PEP";
					break;
				case "Civil Authorities":
					ORG = "CIV";
					break;
				case "National Weather Service":
					ORG = "WXR";
					break;
				case "Emergency Alert System Participant":
					ORG = "EAS";
					break;
				case "Emergency Action Notification Network":
					ORG = "EAN";
					break;
			}

			string EEE = eventCode.Substring(0, 3);

			string PSSCCC = "";	// stores string of SAME codes for selected counties
			for (int i = 0; i < rows.Count; i++) {
				PSSCCC += (string) rows[i][2];
				if (i != rows.Count - 1) {
					PSSCCC += "-";
				}
			}

			string TTTT = "";
			TTTT += AddLeadingZero(durHours);
			TTTT += AddLeadingZero(durMinutes);

			int JJJ = startDate.DayOfYear;
			string HH = AddLeadingZero(startDate.Hour);		// TODO: Adjust for UTC
			string MM = AddLeadingZero(startDate.Minute);   // TODO: Adjust for UTC

			string LLLLLL = senderID;

			int volume = 5000;
			int[] headerData = GenerateBinaryHeaders($"{preamble}ZCZC-{ORG}-{EEE}-{PSSCCC}+{TTTT}-{JJJ}{HH}{MM}-{LLLLLL}-", volume);
			int[] eomData = GenerateBinaryHeaders($"{preamble}NNNN", volume);
			int[] silenceData = new int[totalSilenceData];   // compiler initalizes all elements to 0

			// header length + silence gaps between header & eom bursts + EOM length + silence gap after EOM
			int totalDataLength = (headerData.Length * 3) + (totalSilenceData * 7) + (eomData.Length * 3) + totalSilenceData;
			//byte[] _announcementStream = new byte[0];
			//int _announcementSamples = _announcementStream.Length;
			//if (!string.IsNullOrEmpty(announcment)) {
			//	totalDataLength += _announcementSamples;
			//}

			byte[] ebsToneData = new byte[0];
			if (ebsTone) {
				ebsToneData = GenerateTone(true);
				totalDataLength += ebsToneData.Length * 8 + totalSilenceData;
			}

			byte[] nwsToneData = new byte[0];
			if (nwsTone) {
				nwsToneData = GenerateTone(false);
				totalDataLength += nwsToneData.Length * 8 + totalSilenceData;
			}

			
			MemoryStream f = new MemoryStream();
			BinaryWriter wr = new BinaryWriter(f);

			WriteWAVHeaders(wr, totalDataLength);

			// pause before EAS tones
			foreach (int thisChar in silenceData) {
				wr.Write((byte) thisChar);
			}

			// header tones
			for (int i = 0; i < 3; i++) {
				foreach (int thisChar in headerData) {
					wr.Write((byte) thisChar);
				}
				// silent gap between header tones
				foreach (int thisChar in silenceData) {
					wr.Write((byte) thisChar);
				}
			}

			WriteTone(ebsToneData, silenceData, wr);
			WriteTone(nwsToneData, silenceData, wr);

			//Spoken announcement
			//if (!string.IsNullOrEmpty(announcment)) {
			//	for (int i = 0; i < _announcementSamples; i++) {
			//		wr.Write(_announcementStream[i]);
			//	}
			//}

			// EOM tones
			for (int i = 0; i < 3; i++) {
				// silent gap between EOM tones
				foreach (int thisChar in silenceData) {
					wr.Write((byte) thisChar);
				}
				foreach (int thisChar in eomData) {
					wr.Write((byte) thisChar);
				}
			}

			// silence after tone
			foreach (int thisChar in silenceData) {
				wr.Write((byte) thisChar);
			}
			wr.Flush(); // write to buffer writer
			f.Position = 0; // set memory stream to the beginning

			GenerateWavFile(@"output", f);
		}


		private static void Inception(JObject o, string str, char character) {
			int i;
			if (str.Length > 3) {
				for (i = 0; i < 4; i++) {
					if (str[i] == ',') {
						break;
					}
				}
				if (i == 4) {
					_ = 1;
				}
			} else {
				i = str.Length;
			}

			string numberToAdd = str.Substring(0, i);
			JObject j = (JObject) o[numberToAdd];
			if (j == null) {
				j = new JObject();
				j["parent"] = numberToAdd;
				o[numberToAdd] = j;
			}

			if (str.Length > 3) {
				Inception(j, str.Substring(i + 1), character);
			} else {
				j["value"] = character;
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			MainWindow window = new MainWindow();
			List<string> rawLines = new List<string>();

			string uri = "https://www.weather.gov/source/nwr/SameCode.txt";
			WebClient webClient = new WebClient();
			try {
				webClient.Headers.Add(HttpRequestHeader.UserAgent, "blah");
				webClient.DownloadFile(uri, @"SAME Codes.txt"); // will attempt to download file first before writing it to the disk
				rawLines = new List<string>(File.ReadAllLines("SAME Codes.txt"));
			} catch (WebException e) {
				if (e.InnerException is UnauthorizedAccessException || e.InnerException is IOException) {
					rawLines = new List<string>(webClient.DownloadString(uri).Split('\n'));
					MessageBox.Show("Could not write SAME code file to disk.", "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				} else if (e.InnerException == null) {
					try {
						rawLines = new List<string>(File.ReadAllLines(@"C:\SAME Codes.txt"));
					} catch (IOException) {
						MessageBox.Show("Could not connect to the internet to download SAME codes. No cached list is available.", "No Internet Connection", MessageBoxButtons.OK, 
							MessageBoxIcon.Error);
						return;
					} catch (Exception) {
						MessageBox.Show("An unknown error has occured.", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					MessageBox.Show("Could not connect to the internet to download SAME codes. Using cached list.", "No Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				} else {
					MessageBox.Show("An unknown error has occured. The program maybe unstable.", "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			foreach (string line in rawLines) {
				string[] split = line.Split(',');
				string state = split[2].Substring(1);
				Dictionary<string, string> dict;
				try {
					dict = SAMECodes[state];
				} catch (KeyNotFoundException) {
					SAMECodes.Add(state, new Dictionary<string, string>());
					dict = SAMECodes[state];
				}
				dict.Add(split[1], split[0]);	// C# stores dict value as pointer to dictionary; no need to reassign dict to SAMECodes[state]
			}

			window.AddStates(SAMECodes.Keys.ToArray()); ;

			Application.Run(window);
		}
	}
}
