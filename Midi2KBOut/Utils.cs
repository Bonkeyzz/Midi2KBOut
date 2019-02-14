using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.MusicTheory;
using WinApi.User32;
using Note = Melanchall.DryWetMidi.Smf.Interaction.Note;

namespace Midi2KBOut
{
    public static class Utils
    {
        private const string SvPianoScale = "1!2@34$5%6^78*9(0qQwWeErtTyYuiIoOpPasSdDfgGhHjJklLzZxcCvVbBnm";
        /// <summary>
        /// Console.Write but with colors!
        /// </summary>
        /// <param name="sText">Text to print</param>
        /// <param name="fColor">The text color</param>
        /// <param name="bColor">The background text color</param>
        public static void Pprint(string sText, ConsoleColor fColor, ConsoleColor bColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;

            Console.Write(sText);

            Console.ResetColor();
        }

        public static string ConvertToKBNote(byte note)
        {
            var mapNote = note - 23 - 12 - 1;

            while (mapNote >= SvPianoScale.Length) mapNote -= 12;

            while (mapNote < 0) mapNote += 12;

            return SvPianoScale[mapNote].ToString();
        }

        public enum KeyPressMode
        {
            KBDEVENT,
            SENDINPUT,
        }

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")]
        private static extern uint VkKeyScan(char ch);
        [DllImport("user32.dll")]
        private static extern void keybd_event(ushort bVk, ushort bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private static void SendKey(string key)
        {
            Input inputKey = new Input();
            inputKey.Type = InputType.INPUT_KEYBOARD;
            inputKey.Packet.KeyboardInput.VirtualKeyCode = (ushort)VkKeyScan(char.Parse(key));
            inputKey.Packet.KeyboardInput.Flags = KeyboardInputFlags.KEYEVENTF_UNICODE;
            inputKey.Packet.KeyboardInput.ScanCode = char.Parse(key);
            User32Helpers.SendInput(new[] {inputKey});
            inputKey.Packet.KeyboardInput.Flags = KeyboardInputFlags.KEYEVENTF_UNICODE | 
                                                  KeyboardInputFlags.KEYEVENTF_KEYUP;
            User32Helpers.SendInput(new[] { inputKey });

        }
        private static void SendShift(bool press)
        {
            Input inputShift = new Input();
            uint shiftKey = MapVirtualKey((uint)Keys.ShiftKey, 0x0);
            inputShift.Type = InputType.INPUT_KEYBOARD;
            inputShift.Packet.KeyboardInput.Flags = KeyboardInputFlags.KEYEVENTF_SCANCODE;
            if (!press)
            {
                inputShift.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_KEYUP;
            }

            inputShift.Packet.KeyboardInput.ScanCode = (ushort)shiftKey;

            User32Helpers.SendInput(new[] { inputShift });
        }

        private static void KeybdSendShift(bool press)
        {
            if (press)
            {
                keybd_event(0x10, 0x45, (uint)KeyboardInputFlags.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                Thread.Sleep(20);
            }
            else
            {
                keybd_event(0x10, 0x45, (uint)KeyboardInputFlags.KEYEVENTF_EXTENDEDKEY | (uint)KeyboardInputFlags.KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }
        private static void KeybdSendKey(string key, bool shifted)
        {
            int delayTime = 12;
           ushort bKey = (ushort) VkKeyScan(char.Parse(key));
            ushort bScan = char.Parse(key);

            if (shifted)
            {
                KeybdSendShift(true);
                keybd_event(bKey, bScan, 0, UIntPtr.Zero);
                Thread.Sleep(delayTime);
                KeybdSendShift(false);
            }
            else
            {
                keybd_event(bKey, bScan, 0, UIntPtr.Zero);
            }
            Thread.Sleep(delayTime);
            keybd_event(bKey, bScan, (uint)KeyboardInputFlags.KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        public static double GetTime()
        {
            TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return timeSpan.TotalSeconds;
        }

        public static void PressKeys(string keys, KeyPressMode mode)
        {
         var specialKeys = ")!@#$%^&*(";
            foreach (var key in keys)
            {
                var isSpecialCharOrUpper = specialKeys.Contains(key.ToString()) | char.IsUpper(key);
                switch (mode)
                {
                    case KeyPressMode.KBDEVENT:
                        KeybdSendKey(key.ToString(), isSpecialCharOrUpper);
                        break;
                    case KeyPressMode.SENDINPUT:
                        if (isSpecialCharOrUpper)
                        {
                            SendShift(true);
                            SendKey(key.ToString());
                            SendShift(false);
                        }
                        else
                        {
                            SendKey(key.ToString());
                        }
                        break;
                }
            }
        }

        public static void PrintNote(Note note, double time)
        {
           Pprint("[Note] ", ConsoleColor.Cyan);
           Pprint($"NTime / Time(s): {note.Time} / {time:F2}\t", ConsoleColor.Yellow);
           Pprint($"NoteName / KBNote: {note.NoteName}({note.NoteNumber}) / '{ConvertToKBNote(note.NoteNumber)}', ", ConsoleColor.Magenta);
           Pprint($"Vel: {note.Velocity}, ", ConsoleColor.DarkMagenta);
           Pprint($"Oct: {note.Octave}, ", ConsoleColor.DarkGreen);
           Pprint($"Ch: {note.Channel}, ", ConsoleColor.Gray);
           Pprint($"Len: {note.Length}\r\n", ConsoleColor.Green);
        }
    }
}
