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

        private MidiToKeyboardProcessor _mid;
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
                    PlayKeyPressDetect.Enabled = false;
                }
                else
                {
                    txFileLocation.Text = _midiDialog.FileName;
                    _dryWetMidiFile = MidiFile.Read(_midiDialog.FileName);

                    _mid = new MidiToKeyboardProcessor(_dryWetMidiFile);
                    rBkeybdevent.Enabled = true;
                    rBSendInput.Enabled = true;
                    tBTempo.Enabled = true;
                    tBTempo.Value = (int)_mid.Tempo;
                    _mid.bUsingkeybdevent = rBkeybdevent.Checked;
                    PlayKeyPressDetect.Enabled = true;

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

            Utils.Pprint("\n==Key Binds==\n\n" +
                "INSERT -- Play/Stop MIDI Track\n" +
                "DELETE -- Pause/Resume MIDI Track\n" +
                "-/+ -- Decrease/Increase Tempo", ConsoleColor.Yellow);
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
                    _mid?.thrTrackPlayThread.Abort();
                    _mid.thrTrackPlayThread = null;
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
            if (rBSendInput.Checked) _mid.bUsingkeybdevent = false;
        }

        private void rBkeybdevent_CheckedChanged(object sender, EventArgs e)
        {
            if (rBkeybdevent.Checked)
            {
                _mid.bUsingkeybdevent = true;
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
                KeyState DeleteKeyPressed = User32Methods.GetAsyncKeyState(VirtualKey.INSERT);
                if (DeleteKeyPressed.IsPressed)
                {
                    if (!_mid.bIsPlaying && !_mid.bHasStartedPlaying)
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
                        _mid.bHasStartedPlaying = false;
                        _mid.thrTrackPlayThread.Abort();
                        _mid.thrTrackPlayThread = null;

                        btnPlay.Text = "Play";
                        rBSendInput.Enabled = true;
                        rBkeybdevent.Enabled = true;
                        tBTempo.Enabled = true;

                        Thread.Sleep(500);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _mid.bIsPlaying = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _mid.bIsPlaying = false;
        }

        private void pauseKeyDetect_Tick(object sender, EventArgs e)
        {
            KeyState PauseKey = User32Methods.GetAsyncKeyState(VirtualKey.DELETE);
            if(PauseKey.IsPressed)
            {
                if (_mid != null)
                {
                    if (_mid.bHasStartedPlaying)
                    {
                        _mid.bIsPlaying = !_mid.bIsPlaying;
                        _mid.lastUtcTimeSincePause = Utils.GetTime();
                        Utils.Pprint($"Player has {(_mid.bIsPlaying ? "resumed!" : "paused")}.\n", ConsoleColor.Yellow);
                    }
                }
            }
        }

        private void tempoKeysDetect_Tick(object sender, EventArgs e)
        {
            KeyState TempoIncreaseKey = User32Methods.GetAsyncKeyState(VirtualKey.ADD);
            KeyState TempoDecreaseKey = User32Methods.GetAsyncKeyState(VirtualKey.SUBTRACT);
            if(_mid != null)
            {
                if(TempoIncreaseKey.IsPressed)
                {
                    tBTempo.Value += 5;
                    _mid.Tempo = tBTempo.Value;
                    lbTempo.Text = $"Tempo: {tBTempo.Value}";
                }
                else if (TempoDecreaseKey.IsPressed)
                {
                    tBTempo.Value -= 5;
                    _mid.Tempo = tBTempo.Value;
                    lbTempo.Text = $"Tempo: {tBTempo.Value}";
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _mid.bDisableNoteEvents = cbox_noteeventlogs.Checked;
        }
    }
}