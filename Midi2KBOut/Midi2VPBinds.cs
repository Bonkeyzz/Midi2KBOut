using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Midi2KBOut;
using WinApi.User32;

namespace MID2VPiano
{
    public partial class Midi2VPBinds : Form
    {
        public Midi2VPBinds()
        {
            InitializeComponent();
        }

        private void txKBPlayback_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
