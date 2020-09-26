using System.Collections.Generic;
using System.Security.Permissions;

namespace EAS_Encoder_GUI {
	partial class MainWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.originatorMenu = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.eventCodeMenu = new System.Windows.Forms.ComboBox();
			this.senderIDTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.eventStartDatePicker = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.stateBox = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.countyBox = new System.Windows.Forms.ComboBox();
			this.addCountyButton = new System.Windows.Forms.Button();
			this.countiesAddedTable = new System.Windows.Forms.DataGridView();
			this.generateEASButton = new System.Windows.Forms.Button();
			this.nwsCheckBox = new System.Windows.Forms.CheckBox();
			this.ebsCheckBox = new System.Windows.Forms.CheckBox();
			this.durationTimeLabel = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.hourUpButton = new System.Windows.Forms.Button();
			this.minuteUpButton = new System.Windows.Forms.Button();
			this.minuteDownButton = new System.Windows.Forms.Button();
			this.hourDownButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.countiesAddedTable)).BeginInit();
			this.SuspendLayout();
			// 
			// originatorMenu
			// 
			this.originatorMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.originatorMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.originatorMenu.FormattingEnabled = true;
			this.originatorMenu.Items.AddRange(new object[] {
            "Civil Authorities",
            "Emergency Action Notification Network",
            "Emergency Alert System Participant",
            "National Weather Service",
            "Primary Entry Point Station"});
			this.originatorMenu.Location = new System.Drawing.Point(21, 35);
			this.originatorMenu.Name = "originatorMenu";
			this.originatorMenu.Size = new System.Drawing.Size(328, 28);
			this.originatorMenu.TabIndex = 0;
			this.originatorMenu.SelectedIndexChanged += new System.EventHandler(this.originatorMenu_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(18, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Originator:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(18, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Event Code:";
			// 
			// eventCodeMenu
			// 
			this.eventCodeMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.eventCodeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.eventCodeMenu.FormattingEnabled = true;
			this.eventCodeMenu.Items.AddRange(new object[] {
            "ADR - Administrative Message",
            "AVA - Avalanche Watch",
            "AVW - Avalanche Warning",
			"BLU - Blue Alert",
            "BZW - Blizzard Warning",
            "CAE - Child Abduction Emergency",
            "CDW - Civil Danger Warning",
            "CEM - Civil Emergency Message",
            "CFA - Coastal Flood Watch",
            "CFW - Coastal Flood Warning",
            "DMO - Practice/Demo Warning",
            "DSW - Dust Storm Warning",
            "EAN - Emergency Action Notification",
            "EAT - Emergency Action Termination",
            "EQW - Earthquake Warning",
            "EVI - Evacuation Immediate",
            "FFA - Flash Flood Watch",
            "FFS - Flash Flood Statement",
            "FFW - Flash Flood Warning",
            "FLA - Flood Watch",
            "FLS - Flood Statement",
            "FLW - Flood Warning",
            "FRW - Fire Warning",
            "HLS - Hurricane Statement",
            "HMW - Hazardous Materials Warning",
            "HUA - Hurricane Watch",
            "HUW - Hurricane Warning",
            "HWA - High Wind Watch",
            "HWW - High Wind Warning",
            "LAE - Local Area Emergency",
            "LEW - Law Enforcement Warning",
            "NIC - National Information Center",
            "NMN - Network Message Notification",
            "NPT - National Periodic Test",
            "NUW - Nuclear Power Plant Warning",
            "RHW - Radiological Hazard Warning",
            "RMT - Required Monthly Test",
            "RWT - Required Weekly Test",
            "SMW - Special Marine Warning",
            "SPS - Special Weather Statement",
            "SPW - Shelter in Place Warning",
			"SQA - Snow Squall Watch",
			"SQW - Snow Squall Warning",
			"SSW - Storm Surge Warning",
            "SVA - Severe Thunderstorm Watch",
            "SVR - Severe Thunderstorm Warning",
            "SVS - Severe Weather Statement",
            "TOA - Tornado Watch",
            "TOE - 911 Telephone Outage Emergency",
            "TOR - Tornado Warning",
            "TRA - Tropical Storm Watch",
            "TRW - Tropical Storm Warning",
            "TSA - Tsunami Watch",
            "TSW - Tsunami Warning",
            "VOW - Volcano Warning",
            "WSA - Winter Storm Watch",
            "WSW - Winter Storm Warning"});
			this.eventCodeMenu.Location = new System.Drawing.Point(21, 102);
			this.eventCodeMenu.Name = "eventCodeMenu";
			this.eventCodeMenu.Size = new System.Drawing.Size(328, 28);
			this.eventCodeMenu.Sorted = true;
			this.eventCodeMenu.TabIndex = 2;
			this.eventCodeMenu.SelectedIndexChanged += new System.EventHandler(this.eventCodeMenu_SelectedIndexChanged);
			// 
			// senderIDTextBox
			// 
			this.senderIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.senderIDTextBox.Location = new System.Drawing.Point(365, 35);
			this.senderIDTextBox.MaxLength = 8;
			this.senderIDTextBox.Name = "senderIDTextBox";
			this.senderIDTextBox.Size = new System.Drawing.Size(104, 26);
			this.senderIDTextBox.TabIndex = 4;
			this.senderIDTextBox.TextChanged += new System.EventHandler(this.senderIDTextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(362, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Sender ID";
			// 
			// eventStartDatePicker
			// 
			this.eventStartDatePicker.CustomFormat = "MM/dd/yyyy - HH:mm";
			this.eventStartDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.eventStartDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.eventStartDatePicker.Location = new System.Drawing.Point(21, 178);
			this.eventStartDatePicker.Name = "eventStartDatePicker";
			this.eventStartDatePicker.Size = new System.Drawing.Size(171, 26);
			this.eventStartDatePicker.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(18, 159);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Event Start Date:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(18, 232);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(42, 16);
			this.label7.TabIndex = 13;
			this.label7.Text = "State:";
			// 
			// stateBox
			// 
			this.stateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.stateBox.FormattingEnabled = true;
			this.stateBox.Location = new System.Drawing.Point(21, 251);
			this.stateBox.Name = "stateBox";
			this.stateBox.Size = new System.Drawing.Size(66, 28);
			this.stateBox.Sorted = true;
			this.stateBox.TabIndex = 12;
			this.stateBox.SelectedIndexChanged += new System.EventHandler(this.stateBox_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(118, 232);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 16);
			this.label8.TabIndex = 15;
			this.label8.Text = "County:";
			// 
			// countyBox
			// 
			this.countyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.countyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.countyBox.FormattingEnabled = true;
			this.countyBox.Location = new System.Drawing.Point(121, 251);
			this.countyBox.Name = "countyBox";
			this.countyBox.Size = new System.Drawing.Size(228, 28);
			this.countyBox.Sorted = true;
			this.countyBox.TabIndex = 14;
			this.countyBox.SelectedIndexChanged += new System.EventHandler(this.countyBox_SelectedIndexChanged);
			// 
			// addCountyButton
			// 
			this.addCountyButton.Enabled = false;
			this.addCountyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.addCountyButton.Location = new System.Drawing.Point(365, 251);
			this.addCountyButton.Name = "addCountyButton";
			this.addCountyButton.Size = new System.Drawing.Size(104, 27);
			this.addCountyButton.TabIndex = 16;
			this.addCountyButton.Text = "Add County";
			this.addCountyButton.UseVisualStyleBackColor = true;
			this.addCountyButton.Click += new System.EventHandler(this.addCountyButton_Click);
			// 
			// countiesAddedTable
			// 
			this.countiesAddedTable.AllowUserToAddRows = false;
			this.countiesAddedTable.AllowUserToDeleteRows = false;
			this.countiesAddedTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.countiesAddedTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.countiesAddedTable.Location = new System.Drawing.Point(21, 288);
			this.countiesAddedTable.Name = "countiesAddedTable";
			this.countiesAddedTable.ReadOnly = true;
			this.countiesAddedTable.Size = new System.Drawing.Size(448, 150);
			this.countiesAddedTable.TabIndex = 17;
			// 
			// generateEASButton
			// 
			this.generateEASButton.Enabled = false;
			this.generateEASButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.generateEASButton.Location = new System.Drawing.Point(487, 411);
			this.generateEASButton.Name = "generateEASButton";
			this.generateEASButton.Size = new System.Drawing.Size(163, 27);
			this.generateEASButton.TabIndex = 18;
			this.generateEASButton.Text = "Generate EAS Tone";
			this.generateEASButton.UseVisualStyleBackColor = true;
			this.generateEASButton.Click += new System.EventHandler(this.generate_Click);
			// 
			// nwsCheckBox
			// 
			this.nwsCheckBox.AutoSize = true;
			this.nwsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nwsCheckBox.Location = new System.Drawing.Point(382, 113);
			this.nwsCheckBox.Name = "nwsCheckBox";
			this.nwsCheckBox.Size = new System.Drawing.Size(207, 24);
			this.nwsCheckBox.TabIndex = 19;
			this.nwsCheckBox.Text = "Use NWS Attention Tone";
			this.nwsCheckBox.UseVisualStyleBackColor = true;
			// 
			// ebsCheckBox
			// 
			this.ebsCheckBox.AutoSize = true;
			this.ebsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ebsCheckBox.Location = new System.Drawing.Point(382, 83);
			this.ebsCheckBox.Name = "ebsCheckBox";
			this.ebsCheckBox.Size = new System.Drawing.Size(203, 24);
			this.ebsCheckBox.TabIndex = 20;
			this.ebsCheckBox.Text = "Use EBS Attention Tone";
			this.ebsCheckBox.UseVisualStyleBackColor = true;
			// 
			// durationTimeLabel
			// 
			this.durationTimeLabel.AutoSize = true;
			this.durationTimeLabel.Font = new System.Drawing.Font("taxi meter", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.durationTimeLabel.Location = new System.Drawing.Point(235, 159);
			this.durationTimeLabel.Name = "durationTimeLabel";
			this.durationTimeLabel.Size = new System.Drawing.Size(195, 72);
			this.durationTimeLabel.TabIndex = 21;
			this.durationTimeLabel.Text = "00:00";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(244, 139);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(147, 16);
			this.label9.TabIndex = 22;
			this.label9.Text = "Alert Duration (HH:MM):";
			// 
			// hourUpButton
			// 
			this.hourUpButton.Location = new System.Drawing.Point(215, 166);
			this.hourUpButton.Name = "hourUpButton";
			this.hourUpButton.Size = new System.Drawing.Size(24, 23);
			this.hourUpButton.TabIndex = 23;
			this.hourUpButton.Text = "▲";
			this.hourUpButton.UseVisualStyleBackColor = true;
			this.hourUpButton.Click += new System.EventHandler(this.hourUpButton_Click);
			this.hourUpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hourUpButton_MouseDown);
			this.hourUpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hourUpButton_MouseUp);
			// 
			// minuteUpButton
			// 
			this.minuteUpButton.Location = new System.Drawing.Point(420, 166);
			this.minuteUpButton.Name = "minuteUpButton";
			this.minuteUpButton.Size = new System.Drawing.Size(24, 23);
			this.minuteUpButton.TabIndex = 24;
			this.minuteUpButton.Text = "▲";
			this.minuteUpButton.UseVisualStyleBackColor = true;
			this.minuteUpButton.Click += new System.EventHandler(this.minuteUpButton_Click);
			this.minuteUpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.minuteUpButton_MouseDown);
			this.minuteUpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.minuteUpButton_MouseUp);
			// 
			// minuteDownButton
			// 
			this.minuteDownButton.Location = new System.Drawing.Point(420, 201);
			this.minuteDownButton.Name = "minuteDownButton";
			this.minuteDownButton.Size = new System.Drawing.Size(24, 23);
			this.minuteDownButton.TabIndex = 25;
			this.minuteDownButton.Text = "▼";
			this.minuteDownButton.UseVisualStyleBackColor = true;
			this.minuteDownButton.Click += new System.EventHandler(this.minuteDownButton_Click);
			this.minuteDownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.minuteDownButton_MouseDown);
			this.minuteDownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.minuteDownButton_MouseUp);
			// 
			// hourDownButton
			// 
			this.hourDownButton.Location = new System.Drawing.Point(215, 201);
			this.hourDownButton.Name = "hourDownButton";
			this.hourDownButton.Size = new System.Drawing.Size(24, 23);
			this.hourDownButton.TabIndex = 26;
			this.hourDownButton.Text = "▼";
			this.hourDownButton.UseVisualStyleBackColor = true;
			this.hourDownButton.Click += new System.EventHandler(this.hourDownButton_Click);
			this.hourDownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hourDownButton_MouseDown);
			this.hourDownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hourDownButton_MouseUp);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.hourDownButton);
			this.Controls.Add(this.minuteDownButton);
			this.Controls.Add(this.minuteUpButton);
			this.Controls.Add(this.hourUpButton);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.durationTimeLabel);
			this.Controls.Add(this.ebsCheckBox);
			this.Controls.Add(this.nwsCheckBox);
			this.Controls.Add(this.generateEASButton);
			this.Controls.Add(this.countiesAddedTable);
			this.Controls.Add(this.addCountyButton);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.countyBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.stateBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.eventStartDatePicker);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.senderIDTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.eventCodeMenu);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.originatorMenu);
			this.Name = "MainWindow";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			((System.ComponentModel.ISupportInitialize)(this.countiesAddedTable)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox originatorMenu;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox eventCodeMenu;
		private System.Windows.Forms.TextBox senderIDTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker eventStartDatePicker;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox stateBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox countyBox;
		private System.Windows.Forms.Button addCountyButton;
		private System.Windows.Forms.DataGridView countiesAddedTable;
		private System.Windows.Forms.Button generateEASButton;
		private System.Windows.Forms.CheckBox nwsCheckBox;
		private System.Windows.Forms.CheckBox ebsCheckBox;
		private System.Windows.Forms.Label durationTimeLabel;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button hourUpButton;
		private System.Windows.Forms.Button minuteUpButton;
		private System.Windows.Forms.Button minuteDownButton;
		private System.Windows.Forms.Button hourDownButton;
	}
}

