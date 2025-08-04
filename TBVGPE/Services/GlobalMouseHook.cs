using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class GlobalMouseHook : IDisposable
{
    private const int WH_MOUSE_LL = 14;
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_MOUSEMOVE = 0x0200;

    private LowLevelMouseProc _proc;
    private IntPtr _hookID = IntPtr.Zero;

    public event EventHandler<MouseHookEventArgs>? MouseDown;
    public event EventHandler<MouseHookEventArgs>? MouseUp;
    public event EventHandler<MouseHookEventArgs>? MouseMove;

    public GlobalMouseHook()
    {
        _proc = HookCallback;
    }

    public void SetHook()
    {
        _hookID = SetWindowsHookEx(WH_MOUSE_LL, _proc,
            GetModuleHandle(Process.GetCurrentProcess().MainModule?.ModuleName), 0);
    }

    public void Unhook()
    {
        if (_hookID != IntPtr.Zero)
        {
            UnhookWindowsHookEx(_hookID);
            _hookID = IntPtr.Zero;
        }
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT))!;
            MouseHookEventArgs args = new MouseHookEventArgs(hookStruct.pt.x, hookStruct.pt.y);

            if (wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                MouseDown?.Invoke(this, args);
            }
            else if (wParam == (IntPtr)WM_LBUTTONUP)
            {
                MouseUp?.Invoke(this, args);
            }
            else if (wParam == (IntPtr)WM_MOUSEMOVE)
            {
                MouseMove?.Invoke(this, args);
            }
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    public class MouseHookEventArgs : EventArgs
    {
        public int X { get; }
        public int Y { get; }
        public MouseHookEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    #region P/Invoke Declarations
    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
    #endregion

    public void Dispose()
    {
        Unhook();
    }
}
