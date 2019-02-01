using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Midi2KBOut
{
    public partial class Mid2VpAbout : Form
    {
        public Mid2VpAbout()
        {
            InitializeComponent();
        }

        private void lbLinkCredit1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Stereo101");
        }

        private void Mid2VPAbout_Load(object sender, EventArgs e)
        {

        }
    }
}
