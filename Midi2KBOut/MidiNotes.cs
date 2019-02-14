using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Keys = System.Windows.Forms.Keys;

namespace Midi2KBOut
{
    public class MidiNotes
    {
        private const double Freq = 0.00024;
        private double _offset = -1;

        public Thread TrackPlayThread;
        public bool bIsPlaying = false;
        public bool usingkeybdevent = false;
        private double _tStart;

        public MidiNotes(MidiFile file)
        {
            Tempo = file.GetTempoMap().Tempo.AtTime(0).MicrosecondsPerQuarterNote * Freq;
            Division = Convert.ToDouble(file.TimeDivision.ToString().Split(' ')[0]);
            CurrentMidiFile = file;
        }

        public double Tempo { get; set; }
        private double Division { get; }
        private MidiFile CurrentMidiFile { get; }

        private void NotePressSleep(double time)
        {
            var goTime = (time - _offset) * (60 / Math.Round(Tempo));

            if (goTime - (Utils.GetTime() - _tStart) > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(goTime - (Utils.GetTime() - _tStart)));
            }
        }

        private void PlayTrack(TrackChunk track)
        {
            
            Utils.Pprint($"Playback of the track has started! KeyPress Mode: {(usingkeybdevent ? "keybd_event" : "SendInput")}\n", ConsoleColor.Green);
            _tStart = Utils.GetTime();

            var notes = new List<Note>(track.GetNotes().ToList());


            foreach (var note in notes)
            {

                if (bIsPlaying)
                {
                    var noteTime = ((double) note.Time / Division);
                    var noteName = Utils.ConvertToKBNote(note.NoteNumber);

                        Utils.PrintNote(note, noteTime);

                        if (_offset == -1) _offset = noteTime;

                        NotePressSleep(noteTime);
                        Utils.PressKeys(noteName, usingkeybdevent ? Utils.KeyPressMode.KBDEVENT : Utils.KeyPressMode.SENDINPUT);
                }
            }

            bIsPlaying = false;
            Utils.Pprint("Playback of the track was finished!\n", ConsoleColor.Green);
            if (Application.OpenForms.OfType<MidiToVPianoMain>().Any())
                Application.OpenForms.OfType<MidiToVPianoMain>().First().btnPlay.Invoke(new MethodInvoker(() =>
                {
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().btnPlay.Text = "Play";
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().rBSendInput.Enabled = true;
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().rBkeybdevent.Enabled = true;
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().tBTempo.Enabled = true;
                }));
        }

        public void BeginPlayingTracks()
        {
            TrackPlayThread = new Thread(() =>
            {
                bIsPlaying = true;
                PlayTrack(CurrentMidiFile.GetTrackChunks().Merge());
            });
            TrackPlayThread.Start();
        }
    }
}