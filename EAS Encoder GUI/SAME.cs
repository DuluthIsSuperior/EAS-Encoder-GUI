namespace EAS_Encoder_GUI {
	public class SAMEInfo {
		public string county;
		public string state;
		public string code;

		public SAMEInfo(object countyName, object stateAbbreviation, object SAMECode) {
			county = (string) countyName;
			state = (string) stateAbbreviation;
			code = (string) SAMECode;
		}
	}

	public class SAMEAudioBit {
		public int frequency;
		public decimal length;
		public int volume;

		public SAMEAudioBit(int freq, decimal len, int vol) {
			frequency = freq;
			length = len;
			volume = vol;
		}
	}
}
