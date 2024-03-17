using System;
using System.Runtime.InteropServices;
using ccode.core.Services.Interfaces;

namespace CCode.Infrastructure.Services;

public class OsWindowsClipboardService : IClipboardService
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    private const uint KEYEVENTF_KEYUP = 0x0002;
    private const ushort VK_CONTROL = 0x11; // Rzutowanie na ushort
    private const ushort VK_V = 0x56; // Rzutowanie na ushort

    public void PasteFromClipboard()
    {
        INPUT[] inputs = new INPUT[4];

        // Ctrl down
        inputs[0].type = 1; // Keyboard input
        inputs[0].ki.wVk = VK_CONTROL;

        // V down
        inputs[1].type = 1; // Keyboard input
        inputs[1].ki.wVk = VK_V;

        // V up
        inputs[2].type = 1; // Keyboard input
        inputs[2].ki.wVk = VK_V;
        inputs[2].ki.dwFlags = KEYEVENTF_KEYUP;

        // Ctrl up
        inputs[3].type = 1; // Keyboard input
        inputs[3].ki.wVk = VK_CONTROL;
        inputs[3].ki.dwFlags = KEYEVENTF_KEYUP;

        SendInput((uint)inputs.Length, ref inputs[0], Marshal.SizeOf(typeof(INPUT)));
    }
}
