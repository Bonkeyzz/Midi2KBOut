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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MidiToVPianoMain));
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txFileLocation = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.lbTempo = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmWinList = new System.Windows.Forms.ComboBox();
            this.rBSendInput = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rBkeybdevent = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox2.Controls.Add(this.rBkeybdevent);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rBSendInput);
            this.groupBox2.Controls.Add(this.btnAbout);
            this.groupBox2.Controls.Add(this.lbTempo);
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
            // lbTempo
            // 
            this.lbTempo.AutoSize = true;
            this.lbTempo.Location = new System.Drawing.Point(6, 46);
            this.lbTempo.Name = "lbTempo";
            this.lbTempo.Size = new System.Drawing.Size(52, 13);
            this.lbTempo.TabIndex = 4;
            this.lbTempo.Text = "Tempo: 0";
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
            // rBSendInput
            // 
            this.rBSendInput.AutoSize = true;
            this.rBSendInput.Checked = true;
            this.rBSendInput.Enabled = false;
            this.rBSendInput.Location = new System.Drawing.Point(45, 66);
            this.rBSendInput.Name = "rBSendInput";
            this.rBSendInput.Size = new System.Drawing.Size(74, 17);
            this.rBSendInput.TabIndex = 6;
            this.rBSendInput.TabStop = true;
            this.rBSendInput.Text = "SendInput";
            this.rBSendInput.UseVisualStyleBackColor = true;
            this.rBSendInput.CheckedChanged += new System.EventHandler(this.rBSendInput_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mode:";
            // 
            // rBkeybdevent
            // 
            this.rBkeybdevent.AutoSize = true;
            this.rBkeybdevent.Enabled = false;
            this.rBkeybdevent.Location = new System.Drawing.Point(123, 66);
            this.rBkeybdevent.Name = "rBkeybdevent";
            this.rBkeybdevent.Size = new System.Drawing.Size(236, 17);
            this.rBkeybdevent.TabIndex = 8;
            this.rBkeybdevent.Text = "keybd_event (Slower, works on Garry\'s Mod)";
            this.rBkeybdevent.UseVisualStyleBackColor = true;
            this.rBkeybdevent.CheckedChanged += new System.EventHandler(this.rBkeybdevent_CheckedChanged);
            // 
            // MidiToVPianoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 170);
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
    }
}

