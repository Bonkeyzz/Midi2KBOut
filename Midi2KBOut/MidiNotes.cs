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
        private const double DTempoDivider = 0.00024;
        private const string SvPianoScale = "1!2@34$5%6^78*9(0qQwWeErtTyYuiIoOpPasSdDfgGhHjJklLzZxcCvVbBnm";
        private const string SSpecialChars = ")!@#$%^&*(";
        private double _offset = -1;

        public Thread TrackPlayThread;
        public bool bIsPlaying = false;
        public bool usingkeybdevent = false;
        private double _tStart;

        public EventWaitHandle wh = new AutoResetEvent(true);

        public bool bIsPaused = false;

        public MidiNotes(MidiFile file)
        {
            Tempo = file.GetTempoMap().Tempo.AtTime(0).MicrosecondsPerQuarterNote * DTempoDivider;
            Division = Convert.ToDouble(file.TimeDivision.ToString().Split(' ')[0]);
            CurrentMidiFile = file;
        }

        public double Tempo { get; set; }
        private double Division { get; }
        private MidiFile CurrentMidiFile { get; }

        private static string NoteToVPianoKey(byte note)
        {
            var mapNote = note - 23 - 12 - 1;

            while (mapNote >= SvPianoScale.Length) mapNote -= 12;

            while (mapNote < 0) mapNote += 12;

            return SvPianoScale[mapNote].ToString();
        }

        private void MidiClockDelay(double time)
        {
            var goTime = (time - _offset) * (60 / Math.Round(Tempo));

            if (goTime - (Utils.GetTime() - _tStart) > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(goTime - (Utils.GetTime() - _tStart)));
            }
        }

        private double timeSincePause = -1;

        private void PlayTrack(TrackChunk track)
        {
            Utils.Pprint($"[MidiNotes::PlayTrack()] Started! Output Mode: {(usingkeybdevent ? "keybd_event" : "SendInput")}\n", ConsoleColor.Green);
            _tStart = Utils.GetTime();

            var notes = new List<Note>(track.GetNotes().ToList());

            foreach (var note in notes)
            {
                if (bIsPlaying)
                {
                    var noteTime = ((double) note.Time / Division);
                    var noteName = NoteToVPianoKey(note.NoteNumber);


                    Utils.Pprint($"[MidiNotes::PlayTrack()] Time: {noteTime:F3}\tNote / VPNote: '{note.NoteName}' / '{noteName}', Vel: {note.Velocity}, Oct: {note.Octave}, Len: {note.Length}, Ch: {note.Channel}\r\n", ConsoleColor.Magenta);

                    if (_offset == -1) _offset = noteTime;

                    var isLetterOrDigit = SSpecialChars.Contains(noteName) | char.IsUpper(char.Parse(noteName));

                    MidiClockDelay(noteTime);
                    if (!usingkeybdevent)
                    {
                        if (isLetterOrDigit)
                        {
                            Utils.SendShift(true);
                            Utils.SendKey(noteName);
                            Utils.SendShift(false);
                        }
                        else
                        {
                            Utils.SendKey(noteName);
                        }
                    }
                    else
                    {
                        if (isLetterOrDigit)
                        {
                            Utils.keybdSendKey(noteName, true);
                        }
                        else
                        {
                            Utils.keybdSendKey(noteName, false);
                        }
                    }
                }
            }

            bIsPlaying = false;
            Utils.Pprint("[MidiNotes::PlayTrack()] Finished!\n", ConsoleColor.Green);
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