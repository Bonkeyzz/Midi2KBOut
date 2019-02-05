using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Smf;
using MID2VPiano;
using WinApi.User32;

namespace Midi2KBOut
{
    public partial class MidiToVPianoMain : Form
    {
        private MidiFile _dryWetMidiFile;
        private int _id;

        private MidiNotes _mid;
        private OpenFileDialog _midiDialog;

        public MidiToVPianoMain()
        {
            InitializeComponent();
        }

        private void LoadStart()
        {
            try
            {
                if (string.IsNullOrEmpty(_midiDialog.FileName))
                {
                    Utils.Pprint("No file was selected.\n", ConsoleColor.Red);
                    rBkeybdevent.Enabled = false;
                    rBSendInput.Enabled = false;
                    tBTempo.Enabled = false;
                    keyPressDetect.Enabled = false;
                }
                else
                {
                    txFileLocation.Text = _midiDialog.FileName;
                    _dryWetMidiFile = MidiFile.Read(_midiDialog.FileName);

                    _mid = new MidiNotes(_dryWetMidiFile);
                    rBkeybdevent.Enabled = true;
                    rBSendInput.Enabled = true;
                    tBTempo.Enabled = true;
                    tBTempo.Value = (int)_mid.Tempo;
                    _mid.usingkeybdevent = rBkeybdevent.Checked;
                    keyPressDetect.Enabled = true;

                    Utils.Pprint("\n==Midi Info==\n\n", ConsoleColor.White);
                    Utils.Pprint($"Midi Name: {_midiDialog.SafeFileName}\n", ConsoleColor.White);
                    Utils.Pprint($"Size: {File.OpenRead(_midiDialog.FileName).Length / 1024f:F3} KB\n",
                        ConsoleColor.White);
                    Utils.Pprint($"Number of tracks: {_dryWetMidiFile.GetTrackChunks().Count()}\n", ConsoleColor.White);
                    Utils.Pprint(
                        $"Division Type: {_dryWetMidiFile.TimeDivision.GetType().ToString().Replace("Melanchall.DryWetMidi.Smf.", string.Empty)}\n",
                        ConsoleColor.White);
                    Utils.Pprint($"Division: {_dryWetMidiFile.TimeDivision.ToString().Split(' ')[0]}\n",
                        ConsoleColor.White);
                    Utils.Pprint($"Format: {_dryWetMidiFile.OriginalFormat}\n", ConsoleColor.White);

                    if (_dryWetMidiFile.OriginalFormat == MidiFileFormat.MultiSequence)
                    {
                        Utils.Pprint("This midi file's format is MultiSequence which is not supported yet.",
                            ConsoleColor.Red);
                        _dryWetMidiFile = null;
                    }

                    lbTempo.Text = $"Tempo: {Math.Round(_mid.Tempo)}";
                }
            }
            catch (Exception exc)
            {
                Utils.Pprint($"[{exc.GetType()}] {exc.Message}\n", ConsoleColor.Red);
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

            Utils.Pprint("\n==Key Binds==\n\nKey: DELETE -- Play/Stop MIDI Track", ConsoleColor.Yellow);
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
            if (_mid != null)
            {
                if (btnPlay.Text == "Play")
                {
                    User32Methods.SetForegroundWindow(Process.GetProcessById(_id).MainWindowHandle);
                    Utils.Pprint(
                        $"Window with Process ID:'{_id}' is focused automatically. Waiting 2 seconds before playing the MIDI track...\n",
                        ConsoleColor.White);
                    btnPlay.Text = "Stop";
                    rBSendInput.Enabled = false;
                    rBkeybdevent.Enabled = false;
                    tBTempo.Enabled = false;
                    Thread.Sleep(2000);
                    _mid?.BeginPlayingTracks();
                }
                else if (btnPlay.Text == "Stop")
                {
                    if (_mid.bIsPlaying) _mid.bIsPlaying = false;
                    _mid?.TrackPlayThread.Abort();
                    _mid.TrackPlayThread = null;
                    Utils.Pprint("Player has stopped.\n", ConsoleColor.Yellow);
                    btnPlay.Text = "Play";
                    rBSendInput.Enabled = true;
                    rBkeybdevent.Enabled = true;
                    tBTempo.Enabled = true;
                }
            }
            else
            {
                Utils.Pprint("No song has been selected.\n", ConsoleColor.Red);
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

        private void rBSendInput_CheckedChanged(object sender, EventArgs e)
        {
            if (rBSendInput.Checked) _mid.usingkeybdevent = false;
        }

        private void rBkeybdevent_CheckedChanged(object sender, EventArgs e)
        {
            if (rBkeybdevent.Checked)
            {
                _mid.usingkeybdevent = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_mid != null)
            {
                _mid.Tempo = tBTempo.Value;
                lbTempo.Text = $"Tempo: {tBTempo.Value}";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<Midi2VPBinds>().Any()) new Midi2VPBinds().Show();
        }
        private void keyPressDetect_Tick(object sender, EventArgs e)
        {
            if (_mid != null)
            {
                KeyState DeleteKeyPressed = User32Methods.GetAsyncKeyState(VirtualKey.DELETE);
                if (DeleteKeyPressed.IsPressed)
                {
                    if (!_mid.bIsPlaying)
                    {
                        SystemSounds.Asterisk.Play();
                        Utils.Pprint(
                            "Beginning play of MIDI file. Waiting 2 seconds before playing the MIDI track...\n",
                            ConsoleColor.White);
                        btnPlay.Text = "Stop";
                        rBSendInput.Enabled = false;
                        rBkeybdevent.Enabled = false;
                        tBTempo.Enabled = false;

                        Thread.Sleep(1000);
                        _mid.BeginPlayingTracks();
                    }
                    else
                    {
                        SystemSounds.Asterisk.Play();
                        Utils.Pprint(
                            "Player has stopped.\n",
                            ConsoleColor.Yellow);
                        _mid.bIsPlaying = false;
                        _mid.TrackPlayThread.Abort();
                        _mid.TrackPlayThread = null;

                        btnPlay.Text = "Play";
                        rBSendInput.Enabled = true;
                        rBkeybdevent.Enabled = true;
                        tBTempo.Enabled = true;

                        Thread.Sleep(500);
                    }
                }
            }
        }
    }
}