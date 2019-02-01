using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Smf;
using MidiSharp;
using WinApi.User32;

namespace Midi2KBOut
{
    public partial class MidiToVPianoMain : Form
    {
        private MidiFile _dryWetMidiFile;
        private int _id;

        public double ImidTempo = 120;
        private MidiNotes _mid;
        private OpenFileDialog _midiDialog;
        private MidiSequence _midiseqFile;

        public MidiToVPianoMain()
        {
            InitializeComponent();
        }

        private void LoadStart()
        {
            try
            {
                if (string.IsNullOrEmpty(_midiDialog.FileName))
                    Utils.Pprint("No file was selected.\n", ConsoleColor.Red);
                else
                    using (Stream midiStream = File.OpenRead(_midiDialog.FileName))
                    {
                        _midiseqFile = MidiSequence.Open(midiStream);
                        _dryWetMidiFile = MidiFile.Read(_midiDialog.FileName);

                        _mid = new MidiNotes(_midiseqFile, _dryWetMidiFile);

                        Utils.Pprint("\n==Midi Info==\n\n", ConsoleColor.White);
                        Utils.Pprint($"Midi Name: {_midiDialog.SafeFileName}\n", ConsoleColor.White);
                        Utils.Pprint($"Size: {File.OpenRead(_midiDialog.FileName).Length / 1024f:F3} KB\n",
                            ConsoleColor.White);
                        Utils.Pprint($"Number of tracks: {_midiseqFile.Tracks.Count}\n", ConsoleColor.White);
                        Utils.Pprint($"Division Type: {_midiseqFile.DivisionType}\n", ConsoleColor.White);
                        Utils.Pprint($"Division: {_midiseqFile.Division}\n", ConsoleColor.White);
                        Utils.Pprint($"Format: {_midiseqFile.Format}\n", ConsoleColor.White);

                        Utils.Pprint(
                            _midiseqFile.DivisionType == DivisionType.FramesPerSecond
                                ? $"FPS: {_midiseqFile.FramesPerSecond}\n"
                                : $"Ticks Per Beat or Frame: {_midiseqFile.TicksPerBeatOrFrame}\n", ConsoleColor.White);

                        if (_midiseqFile.Format == Format.Two)
                        {
                            Utils.Pprint("This midi file's format is Two, which is not supported yet.",
                                ConsoleColor.Red);
                            _midiseqFile = null;
                        }

                        lbTempo.Text = $"Tempo: {Math.Round(_mid.Tempo)}";
                    }
            }
            catch (Exception exc)
            {
                Utils.Pprint($"[{exc.GetType()}] {exc.Message}", ConsoleColor.Red);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = $"MIDI To VirtualPiano Converter [{Program.SVersion}]";
            if (!Directory.Exists($"{Environment.CurrentDirectory}\\MIDI Files"))
                Directory.CreateDirectory($"{Environment.CurrentDirectory}\\MIDI Files");

            btnRefresh.PerformClick();
            Utils.Pprint("Initialized successfully!\n\n", ConsoleColor.Green);

            Utils.Pprint("Warning! This converter may not play correctly tracks that have mutliple instruments.\n",
                ConsoleColor.Yellow);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _midiDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Midi Sequence|*.mid",
                Title = "Select a MIDI file...",
                InitialDirectory = $"{Environment.CurrentDirectory}\\MIDI Files"
            };

            _midiDialog.ShowDialog();
            LoadStart();
            txFileLocation.Text = _midiDialog.FileName;
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmWinList.Text = "";
            cmWinList.Items.Clear();
            var procList = Process.GetProcesses();
            foreach (var process in procList)
            {
                if (string.IsNullOrEmpty(process.MainWindowTitle)) continue;
                cmWinList.Items.Add($"{process.ProcessName}:{process.Id} -- '{process.MainWindowTitle}'");
            }

            cmWinList.Text = cmWinList.Items[0].ToString();
            _id = int.Parse(cmWinList.Text.Split(':')[1].Split(' ')[0]);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.Text == "Play")
            {
                User32Methods.SetForegroundWindow(Process.GetProcessById(_id).MainWindowHandle);
                Utils.Pprint(
                    $"Window with Process ID:'{_id}' is focused automatically. Waiting 2 seconds before playing the MIDI track...\n",
                    ConsoleColor.White);
                Thread.Sleep(2000);
                _mid?.BeginPlayingTracks();
                btnPlay.Text = "Stop";
            }
            else if (btnPlay.Text == "Stop")
            {
                _mid?.TrackPlayThread.Abort();
                _mid.TrackPlayThread = null;
                Utils.Pprint("Player has stopped.\n", ConsoleColor.Yellow);
                btnPlay.Text = "Play";
            }
        }

        private void cmWinList_DropDownClosed(object sender, EventArgs e)
        {
            _id = int.Parse(cmWinList.Text.Split(':')[1].Split(' ')[0]);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<Mid2VpAbout>().Any()) new Mid2VpAbout().Show();
        }
    }
}