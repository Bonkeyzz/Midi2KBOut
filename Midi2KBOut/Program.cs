﻿using System;
using System.Windows.Forms;
namespace Midi2KBOut
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public const string SVersion = "v2.0.0";




        [STAThread]
        static void Main()
        {
            Console.Title = "Debug Log";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MidiToVPianoMain());

        }
    }
}
