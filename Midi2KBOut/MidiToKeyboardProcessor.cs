using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;

namespace Midi2KBOut
{
    public class MidiToKeyboardProcessor
    {
        private double dNoteStartOffset = -1;

        public Thread thrTrackPlayThread;
        public bool bIsPlaying = false;
        public bool bHasStartedPlaying = false;
        public bool bUsingkeybdevent = false;

        public bool bDisableNoteEvents = false;
        private double dNoteOriginalStartTime;


        public MidiToKeyboardProcessor(MidiFile file)
        {
            Tempo = file.GetTempoMap().Tempo.AtTime(0).MicrosecondsPerQuarterNote * 0.00024;
            Division = Convert.ToDouble(file.TimeDivision.ToString().Split(' ')[0]);
            CurrentMidiFile = file;
        }

        public double Tempo { get; set; }
        private double Division { get; }
        private MidiFile CurrentMidiFile { get; }

        public double lastUtcTimeSincePause = 0;

        /// <summary>
        /// Function to properly delay key presses between notes
        /// </summary>
        /// <param name="time">Note MS Time</param>
        private void NotePressSleep(double time)
        {
            if (lastUtcTimeSincePause != 0)
            {
                dNoteOriginalStartTime = lastUtcTimeSincePause;
                dNoteStartOffset = time;
                lastUtcTimeSincePause = 0;
            }
            var goTime = (time - dNoteStartOffset) * (60 / Math.Round(Tempo));
            double currentTime = Utils.GetTime();
            var sleepTime = goTime - (currentTime - dNoteOriginalStartTime);

            if (sleepTime > 0)
            {
                Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
            }
        }

        /// <summary>
        /// Secondary Thread to play the notes of a MIDI track
        /// </summary>
        public void PlayTrack(object track)
        {
            Utils.Pprint($"Playback of the track has started! KeyPress Mode: {(bUsingkeybdevent ? "keybd_event" : "SendInput")}\n", ConsoleColor.Green);
            dNoteOriginalStartTime = Utils.GetTime();
            dNoteStartOffset = -1;
            lastUtcTimeSincePause = 0;
            TrackChunk trackChunk = (TrackChunk)track;

            var noteQueue = new Queue<Note>(trackChunk.GetNotes().ToList());
            bIsPlaying = true;
            bHasStartedPlaying = true;
            while (noteQueue.Any())
            {
                if (bIsPlaying)
                {
                    var note = noteQueue.Dequeue();
                    var noteTime = ((double)note.Time / Division);
                    var noteName = Utils.ConvertToKBNote(note.NoteNumber);

                    if(bDisableNoteEvents) Utils.PrintNote(note, noteTime);

                    if (dNoteStartOffset == -1) dNoteStartOffset = noteTime;


                    NotePressSleep(noteTime);
                    Utils.PressKeys(noteName, bUsingkeybdevent ? Utils.KeyPressMode.KBDEVENT : Utils.KeyPressMode.SENDINPUT);
                }
            }

            bIsPlaying = false;
            bHasStartedPlaying = false;
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
            thrTrackPlayThread = new Thread(new ParameterizedThreadStart(PlayTrack));
            thrTrackPlayThread.Start(CurrentMidiFile.GetTrackChunks().Merge());
        }
    }
}