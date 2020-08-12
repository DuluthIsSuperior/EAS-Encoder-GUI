using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace EAS_Encoder_GUI {
	public partial class MainWindow : Form {
		private static Dictionary<string, Timer> timers;
		private readonly int initialTimerDuration = 500;

		private Timer GenerateTimer(ElapsedEventHandler evnt) {
			Timer timer = new Timer {
				Interval = initialTimerDuration,		// interval in milliseconds
				Enabled = false,
				AutoReset = true
			};
			timer.Elapsed += evnt;
			return timer;
		}

		public MainWindow() {
			timers = new Dictionary<string, Timer>();
			timers.Add("HourUp", GenerateTimer(HourUpEvent));
			timers.Add("HourDown", GenerateTimer(HourDownEvent));
			timers.Add("MinuteUp", GenerateTimer(MinuteUpEvent));
			timers.Add("MinuteDown", GenerateTimer(MinuteDownEvent));

			InitializeComponent();
		}

		public void AddStates(string[] values) {
			stateBox.Items.AddRange(values);
		}

		private void stateBox_SelectedIndexChanged(object sender, EventArgs e) {
			string state = (string) stateBox.SelectedItem;
			string[] counties = Program.GetCounties(state);
			countyBox.Items.Clear();
			countyBox.Items.AddRange(counties);
		}

		DataTable dt = new DataTable();
		private void addCountyButton_Click(object sender, EventArgs e) {
			string county = (string) countyBox.SelectedItem;
			string state = (string) stateBox.SelectedItem;
			string sameCode = Program.GetSAMECode(county, state);

			dt.Rows.Add(county, state, sameCode);
			RequestGenerateButtonEnable("PSSCCC");
		}

		private void MainWindow_Load(object sender, EventArgs e) {
			dt.Columns.Add(new DataColumn("County", typeof(string)));
			dt.Columns.Add(new DataColumn("State", typeof(string)));
			dt.Columns.Add(new DataColumn("SAME Code", typeof(string)));

			//dt.Columns["colStatus"].Expression = String.Format("IIF(colBestBefore < #{0}#, 'Ok','Not ok')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			countiesAddedTable.DataSource = dt;
			countiesAddedTable.Columns[0].Width = 300;
			countiesAddedTable.Columns[1].Width = 50;
			countiesAddedTable.Columns[2].Width = 50;
		}

		private void countyBox_SelectedIndexChanged(object sender, EventArgs e) {
			addCountyButton.Enabled = true;
		}

		private bool CheckTimeIncrement(int minutesToExpire, int increment, string timeMessage, string incrementMessage) {
			if (increment == 0) {
				return true;
			}

			if (minutesToExpire % increment != 0) {
				DialogResult response = MessageBox.Show($"Specification states that expiration times {timeMessage} must be in {incrementMessage} increments. Continue?", "Time Outside Specifications",
					MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				if (response == DialogResult.Yes) {
					return true;
				}
				return false;
			}
			return true;
		}

		private int durationHoursDisplayed = 0;
		private int durationMinutesDisplayed = 0;

		private void generate_Click(object sender, EventArgs e) {
			int hoursToExpire = durationHoursDisplayed;
			int minutesToExpire = durationMinutesDisplayed;
			if (hoursToExpire < 1) {
				if (!CheckTimeIncrement(minutesToExpire, 15, "up to an hour", "15 minute")) {
					return;
				}
			} else if (hoursToExpire < 6) {
				if (!CheckTimeIncrement(minutesToExpire, 30, "up to six hours", "30 minute")) {
					return;
				}
			} else {
				if (!CheckTimeIncrement(minutesToExpire, 0, "beyond six hours", "hour")) {
					return;
				}
			}

			if (originatorMenu.SelectedItem.Equals("National Weather Service")) {
				if (!Regex.IsMatch(senderIDTextBox.Text, "K[A-Z]{3}/NWS")) {
					DialogResult response = MessageBox.Show("Specification states that alerts originating from the National Weather Service have a sender ID that follows the format:\n" +
						"K<3 letter ID of the NWS office issuing the alert>/NWS. Continue?", "Sender ID outside of specifications", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
					if (response != DialogResult.Yes) {
						return;
					}
				}
			}

			if (!Regex.IsMatch(senderIDTextBox.Text, "[A-Z0-9]{8}")) {
				DialogResult response = MessageBox.Show("Specification states that the sender ID is in all caps. Bring sender ID to all caps?\n" +
					"Click cancel to terminate tone generation.", "Sender ID outside of specifications",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
				if (response == DialogResult.Yes) {
					senderIDTextBox.Text = senderIDTextBox.Text.ToUpper();
				} else if (response == DialogResult.Cancel) {
					return;
				}
			}

			Program.GenerateEASTones((string) originatorMenu.SelectedItem, senderIDTextBox.Text, eventCodeMenu.Text, eventStartDatePicker.Value, durationHoursDisplayed,
				durationMinutesDisplayed, dt.Rows, ebsCheckBox.Checked, nwsCheckBox.Checked);
		}

		private static bool originatorSelected = false;
		private static bool eventCodeSelected = false;
		private static bool countySelected = false;
		private static bool senderIDEntered = false;
		private void RequestGenerateButtonEnable(string field) {
			switch (field) {
				case "ORG":
					originatorSelected = true;
					break;
				case "PSSCCC":
					countySelected = true;
					break;
				case "EEE":
					eventCodeSelected = true;
					break;
				case "LLLLLLLL":
					senderIDEntered = true;
					break;
			}

			if (originatorSelected && eventCodeSelected && countySelected && senderIDEntered) {
				generateEASButton.Enabled = true;
			}
		}

		private void DisableGenerateButton(string field) {
			switch (field) {
				case "LLLLLLLL":
					senderIDEntered = false;
					break;
			}
			generateEASButton.Enabled = false;
		}

		private void originatorMenu_SelectedIndexChanged(object sender, EventArgs e) {
			RequestGenerateButtonEnable("ORG");
		}

		private void eventCodeMenu_SelectedIndexChanged(object sender, EventArgs e) {
			RequestGenerateButtonEnable("EEE");
		}

		private void senderIDTextBox_TextChanged(object sender, EventArgs e) {
			string syntaxField = "LLLLLLLL";
			if (senderIDTextBox.Text.Length == 8) {
				RequestGenerateButtonEnable(syntaxField);
			} else {
				DisableGenerateButton(syntaxField);
			}
		}

		private void ChangeDurationTimeDisplayed() {
			string hours = (durationHoursDisplayed < 10 ? "0" : "") + durationHoursDisplayed;
			string minutes = (durationMinutesDisplayed < 10 ? "0" : "") + durationMinutesDisplayed;
			Invoke(new MethodInvoker(delegate () {
				durationTimeLabel.Text = $"{hours}:{minutes}";
			}));
		}

		private bool timerFirstRun = false;
		private void AdjustTimerInterval(Timer timer) {
			if (!timerFirstRun) {
				timer.Enabled = false;
				timer.Interval = 50;
				timer.Enabled = true;
				timerFirstRun = true;
			}
		}

		private void MinuteUpEvent(object source, ElapsedEventArgs e) {
			if (durationMinutesDisplayed < 59) {
				durationMinutesDisplayed++;
				ChangeDurationTimeDisplayed();
			}
			AdjustTimerInterval(timers["MinuteUp"]);
		}

		private void MinuteDownEvent(object source, ElapsedEventArgs e) {
			if (durationMinutesDisplayed > 0) {
				durationMinutesDisplayed--;
				ChangeDurationTimeDisplayed();
			}
			AdjustTimerInterval(timers["MinuteDown"]);
		}

		private void HourUpEvent(object source, ElapsedEventArgs e) {
			if (durationHoursDisplayed < 99) {
				durationHoursDisplayed++;
				ChangeDurationTimeDisplayed();
			}
			AdjustTimerInterval(timers["HourUp"]);
		}

		private void HourDownEvent(object source, ElapsedEventArgs e) {
			if (durationHoursDisplayed > 0) {
				durationHoursDisplayed--;
				ChangeDurationTimeDisplayed();
			}
			AdjustTimerInterval(timers["HourDown"]);
		}

		private void DisableTimer(Timer timer) {
			timer.Enabled = false;
			timer.Interval = initialTimerDuration;
		}

		private void minuteUpButton_MouseDown(object sender, MouseEventArgs e) {
			timerFirstRun = false;
			timers["MinuteUp"].Enabled = true;
		}

		private void minuteUpButton_MouseUp(object sender, MouseEventArgs e) {
			DisableTimer(timers["MinuteUp"]);
		}

		private void minuteUpButton_Click(object sender, EventArgs e) {
			if (!timerFirstRun) {
				MinuteUpEvent(null, null);
			}
		}

		private void minuteDownButton_MouseDown(object sender, MouseEventArgs e) {
			timerFirstRun = false;
			timers["MinuteDown"].Enabled = true;
		}

		private void minuteDownButton_MouseUp(object sender, MouseEventArgs e) {
			DisableTimer(timers["MinuteDown"]);
		}

		private void minuteDownButton_Click(object sender, EventArgs e) {
			if (!timerFirstRun) {
				MinuteDownEvent(null, null);
			}
		}

		private void hourUpButton_MouseDown(object sender, MouseEventArgs e) {
			timerFirstRun = false;
			timers["HourUp"].Enabled = true;
		}

		private void hourUpButton_MouseUp(object sender, MouseEventArgs e) {
			DisableTimer(timers["HourUp"]);
		}

		private void hourUpButton_Click(object sender, EventArgs e) {
			if (!timerFirstRun) {
				HourUpEvent(null, null);
			}
		}

		private void hourDownButton_MouseDown(object sender, MouseEventArgs e) {
			timerFirstRun = false;
			timers["HourDown"].Enabled = true;
		}

		private void hourDownButton_MouseUp(object sender, MouseEventArgs e) {
			DisableTimer(timers["HourDown"]);
		}

		private void hourDownButton_Click(object sender, EventArgs e) {
			if (!timerFirstRun) {
				HourDownEvent(null, null);
			}
		}
	}
}
