using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WinApi.User32;

namespace Midi2KBOut
{
    public static class Utils
    {
        /// <summary>
        /// Console.Write function with color support.
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

        
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")]
        private static extern uint VkKeyScan(char ch);
        [DllImport("user32.dll")]
        static extern void keybd_event(ushort bVk, ushort bScan, uint dwFlags, UIntPtr dwExtraInfo);

        public static void SendKey(string key)
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
        public static void SendShift(bool press)
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

        private static void SendKeybdShift(bool press)
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
        public static void keybdSendKey(string key, bool shifted)
        {
            int delayTime = 12;
           ushort bKey = (ushort) VkKeyScan(char.Parse(key));
            ushort bScan = char.Parse(key);

            if (shifted)
            {
                SendKeybdShift(true);
                keybd_event(bKey, bScan, 0, UIntPtr.Zero);
                Thread.Sleep(delayTime);
                SendKeybdShift(false);
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

    }
}
