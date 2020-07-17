namespace MID2VPiano
{
    partial class Midi2VPBinds
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
            this.label1 = new System.Windows.Forms.Label();
            this.txKBPlayback = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txKBTempoPlus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txKBTempoMinus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Play/Stop:";
            // 
            // txKBPlayback
            // 
            this.txKBPlayback.Location = new System.Drawing.Point(15, 32);
            this.txKBPlayback.Name = "txKBPlayback";
            this.txKBPlayback.Size = new System.Drawing.Size(198, 20);
            this.txKBPlayback.TabIndex = 1;
            this.txKBPlayback.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txKBPlayback_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tempo Increment(+5):";
            // 
            // txKBTempoPlus
            // 
            this.txKBTempoPlus.Location = new System.Drawing.Point(15, 71);
            this.txKBTempoPlus.Name = "txKBTempoPlus";
            this.txKBTempoPlus.Size = new System.Drawing.Size(198, 20);
            this.txKBTempoPlus.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.txKBTempoMinus);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txKBTempoPlus);
            this.groupBox1.Controls.Add(this.txKBPlayback);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 212);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txKBTempoMinus
            // 
            this.txKBTempoMinus.Location = new System.Drawing.Point(15, 110);
            this.txKBTempoMinus.Name = "txKBTempoMinus";
            this.txKBTempoMinus.Size = new System.Drawing.Size(198, 20);
            this.txKBTempoMinus.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tempo Decrement(-5):";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(150, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "Defaults";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(220, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 20);
            this.button3.TabIndex = 4;
            this.button3.Text = "Change...";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(219, 110);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 20);
            this.button4.TabIndex = 5;
            this.button4.Text = "Change...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(219, 70);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(68, 20);
            this.button5.TabIndex = 6;
            this.button5.Text = "Change...";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(219, 152);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(68, 20);
            this.button6.TabIndex = 9;
            this.button6.Text = "Change...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 152);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(198, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pause/Resume:";
            // 
            // Midi2VPBinds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 282);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Midi2VPBinds";
            this.Text = "Key Binds";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txKBPlayback;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txKBTempoPlus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txKBTempoMinus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
    }
}