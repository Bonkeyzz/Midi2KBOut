namespace Midi2KBOut
{
    partial class MidiToVPianoMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MidiToVPianoMain));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txFileLocation = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbox_noteeventlogs = new System.Windows.Forms.CheckBox();
            this.btnKeyBinds = new System.Windows.Forms.Button();
            this.rBkeybdevent = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rBSendInput = new System.Windows.Forms.RadioButton();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmWinList = new System.Windows.Forms.ComboBox();
            this.lbTempo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tBTempo = new System.Windows.Forms.TrackBar();
            this.PlayKeyPressDetect = new System.Windows.Forms.Timer(this.components);
            this.pauseKeyDetect = new System.Windows.Forms.Timer(this.components);
            this.tempoKeysDetect = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBTempo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(786, 21);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(29, 20);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txFileLocation);
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(821, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MIDI File";
            // 
            // txFileLocation
            // 
            this.txFileLocation.Location = new System.Drawing.Point(6, 21);
            this.txFileLocation.Name = "txFileLocation";
            this.txFileLocation.ReadOnly = true;
            this.txFileLocation.Size = new System.Drawing.Size(774, 20);
            this.txFileLocation.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbox_noteeventlogs);
            this.groupBox2.Controls.Add(this.btnKeyBinds);
            this.groupBox2.Controls.Add(this.rBkeybdevent);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rBSendInput);
            this.groupBox2.Controls.Add(this.btnAbout);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Controls.Add(this.cmWinList);
            this.groupBox2.Location = new System.Drawing.Point(12, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(821, 92);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Window";
            // 
            // cbox_noteeventlogs
            // 
            this.cbox_noteeventlogs.AutoSize = true;
            this.cbox_noteeventlogs.Checked = true;
            this.cbox_noteeventlogs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbox_noteeventlogs.Location = new System.Drawing.Point(9, 66);
            this.cbox_noteeventlogs.Name = "cbox_noteeventlogs";
            this.cbox_noteeventlogs.Size = new System.Drawing.Size(144, 17);
            this.cbox_noteeventlogs.TabIndex = 10;
            this.cbox_noteeventlogs.Text = "Disable Note Event Logs";
            this.cbox_noteeventlogs.UseVisualStyleBackColor = true;
            this.cbox_noteeventlogs.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnKeyBinds
            // 
            this.btnKeyBinds.Enabled = false;
            this.btnKeyBinds.Location = new System.Drawing.Point(553, 46);
            this.btnKeyBinds.Name = "btnKeyBinds";
            this.btnKeyBinds.Size = new System.Drawing.Size(75, 37);
            this.btnKeyBinds.TabIndex = 9;
            this.btnKeyBinds.Text = "Key Binds";
            this.btnKeyBinds.UseVisualStyleBackColor = true;
            this.btnKeyBinds.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // rBkeybdevent
            // 
            this.rBkeybdevent.AutoSize = true;
            this.rBkeybdevent.Enabled = false;
            this.rBkeybdevent.Location = new System.Drawing.Point(123, 44);
            this.rBkeybdevent.Name = "rBkeybdevent";
            this.rBkeybdevent.Size = new System.Drawing.Size(236, 17);
            this.rBkeybdevent.TabIndex = 8;
            this.rBkeybdevent.Text = "keybd_event (Slower, works on Garry\'s Mod)";
            this.rBkeybdevent.UseVisualStyleBackColor = true;
            this.rBkeybdevent.CheckedChanged += new System.EventHandler(this.rBkeybdevent_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mode:";
            // 
            // rBSendInput
            // 
            this.rBSendInput.AutoSize = true;
            this.rBSendInput.Checked = true;
            this.rBSendInput.Enabled = false;
            this.rBSendInput.Location = new System.Drawing.Point(45, 44);
            this.rBSendInput.Name = "rBSendInput";
            this.rBSendInput.Size = new System.Drawing.Size(74, 17);
            this.rBSendInput.TabIndex = 6;
            this.rBSendInput.TabStop = true;
            this.rBSendInput.Text = "SendInput";
            this.rBSendInput.UseVisualStyleBackColor = true;
            this.rBSendInput.CheckedChanged += new System.EventHandler(this.rBSendInput_CheckedChanged);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(634, 46);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 37);
            this.btnAbout.TabIndex = 5;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(715, 46);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(97, 37);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(737, 19);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 21);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmWinList
            // 
            this.cmWinList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmWinList.FormattingEnabled = true;
            this.cmWinList.Location = new System.Drawing.Point(6, 19);
            this.cmWinList.Name = "cmWinList";
            this.cmWinList.Size = new System.Drawing.Size(725, 21);
            this.cmWinList.Sorted = true;
            this.cmWinList.TabIndex = 0;
            this.cmWinList.DropDownClosed += new System.EventHandler(this.cmWinList_DropDownClosed);
            // 
            // lbTempo
            // 
            this.lbTempo.AutoSize = true;
            this.lbTempo.Location = new System.Drawing.Point(6, 25);
            this.lbTempo.Name = "lbTempo";
            this.lbTempo.Size = new System.Drawing.Size(52, 13);
            this.lbTempo.TabIndex = 4;
            this.lbTempo.Text = "Tempo: 0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.tBTempo);
            this.groupBox3.Controls.Add(this.lbTempo);
            this.groupBox3.Location = new System.Drawing.Point(12, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(821, 108);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MIDI Properties";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "New Tempo (Max: 200):";
            // 
            // tBTempo
            // 
            this.tBTempo.Enabled = false;
            this.tBTempo.Location = new System.Drawing.Point(9, 68);
            this.tBTempo.Maximum = 200;
            this.tBTempo.Minimum = 50;
            this.tBTempo.Name = "tBTempo";
            this.tBTempo.Size = new System.Drawing.Size(803, 45);
            this.tBTempo.TabIndex = 5;
            this.tBTempo.Value = 120;
            this.tBTempo.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // PlayKeyPressDetect
            // 
            this.PlayKeyPressDetect.Interval = 200;
            this.PlayKeyPressDetect.Tick += new System.EventHandler(this.keyPressDetect_Tick);
            // 
            // pauseKeyDetect
            // 
            this.pauseKeyDetect.Enabled = true;
            this.pauseKeyDetect.Interval = 180;
            this.pauseKeyDetect.Tick += new System.EventHandler(this.pauseKeyDetect_Tick);
            // 
            // tempoKeysDetect
            // 
            this.tempoKeysDetect.Enabled = true;
            this.tempoKeysDetect.Tick += new System.EventHandler(this.tempoKeysDetect_Tick);
            // 
            // MidiToVPianoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 287);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MidiToVPianoMain";
            this.Text = "MIDI To VirtualPiano Converter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBTempo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txFileLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmWinList;
        public System.Windows.Forms.Label lbTempo;
        private System.Windows.Forms.Button btnAbout;
        public System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton rBkeybdevent;
        public System.Windows.Forms.RadioButton rBSendInput;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TrackBar tBTempo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnKeyBinds;
        public System.Windows.Forms.Timer PlayKeyPressDetect;
        private System.Windows.Forms.Timer pauseKeyDetect;
        private System.Windows.Forms.Timer tempoKeysDetect;
        private System.Windows.Forms.CheckBox cbox_noteeventlogs;
    }
}

