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

        public MidiNotes(MidiFile file)
        {
            Tempo = file.GetTempoMap().Tempo.AtTime(0).MicrosecondsPerQuarterNote * DTempoDivider;
            Division = Convert.ToDouble(file.TimeDivision.ToString().Split(' ')[0]);
            CurrentMidiFile = file;
        }

        public double Tempo { get; }
        private double Division { get; }
        private MidiFile CurrentMidiFile { get; }

        private static string NoteToVPianoKey(byte note)
        {
            var mapNote = note - 23 - 12 - 1;

            while (mapNote >= SvPianoScale.Length) mapNote -= 12;

            while (mapNote < 0) mapNote += 12;

            return SvPianoScale[mapNote].ToString();
        }

        private TimeSpan ParseTime(double time)
        {
            var goTime = (time - _offset) * (60 / Math.Round(Tempo));

            if (goTime - (Utils.GetTime() - _tStart) > 0)
                return TimeSpan.FromSeconds(goTime - (Utils.GetTime() - _tStart));
            return new TimeSpan();
        }

        private void PlayTrack(TrackChunk track)
        {
            Utils.Pprint("[MidiNotes::PlayTrack()] Started!\n", ConsoleColor.Green);
            _tStart = Utils.GetTime();

            var notes = new List<Note>(track.GetNotes().ToList());
            var parsedNotes = new List<string>();

            foreach (var t in notes)
                parsedNotes.Add($"{(double) t.Time / Division:F3}\t{NoteToVPianoKey(t.NoteNumber)}");

            foreach (var sortedNote in parsedNotes)
            {
                if (bIsPlaying)
                {
                    var noteTime = double.Parse(sortedNote.Split('\t')[0]);
                    var noteName = sortedNote.Split('\t')[1];

                    Utils.Pprint($"[MidiNotes::PlayTrack()] Time:{noteTime}\tNote:'{noteName}' Mode: {(usingkeybdevent ? "keybd_event" : "SendInput")}\n",
                        ConsoleColor.Magenta);

                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (_offset == -1) _offset = noteTime;

                    var isLetterOrDigit = SSpecialChars.Contains(noteName) | char.IsUpper(char.Parse(noteName));
                    Thread.Sleep(ParseTime(noteTime));
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
                            Utils.keybdSendKey(noteName,true);
                        }
                        else
                        {
                            Utils.keybdSendKey(noteName,false);
                        }
                    }
                }
                else break;
            }

            bIsPlaying = false;
            Utils.Pprint("[MidiNotes::PlayTrack()] Finished!\n", ConsoleColor.Green);
            if (Application.OpenForms.OfType<MidiToVPianoMain>().Any())
                Application.OpenForms.OfType<MidiToVPianoMain>().First().btnPlay.Invoke(new MethodInvoker(() =>
                {
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().btnPlay.Text = "Play";
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().rBSendInput.Enabled = true;
                    Application.OpenForms.OfType<MidiToVPianoMain>().First().rBkeybdevent.Enabled = true;
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