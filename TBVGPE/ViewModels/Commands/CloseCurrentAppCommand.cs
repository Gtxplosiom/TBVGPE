using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace TBVGPE.ViewModels.Commands
{
    public class CloseCurrentAppCommand : CommandBase
    {
        // P/invokes para ma close an current active na window
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_CLOSE = 0x0010;

        // P/invokes para makuha an process id han active na window
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // P/invokes para makuha an ngaran han active/focused na window
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            IntPtr handle = GetForegroundWindow();

            // kuhaon an process name mismo ngan ig parenthesis and process id para maupay kitaon
            string processName = GetActiveWindowProcessName(handle);
            string activeWindowTitle = GetActiveWindowTitle(handle);

            if (activeWindowTitle != null && processName != "TBVGPE")
            {
                MessageBoxResult option = MessageBox.Show($"Do you want to close {activeWindowTitle} ({processName})?", "Confirmation",
                                                      MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (option == MessageBoxResult.Yes)
                {
                    PostMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBoxResult option = MessageBox.Show($"Focus on the app you want to close by clicking it", "Tip",
                                                          MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // thanks stackoverflow
        private string GetActiveWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private string GetActiveWindowProcessName(IntPtr handle)
        {
            uint processId;
            GetWindowThreadProcessId(handle, out processId);

            try
            {
                Process proc = Process.GetProcessById((int)processId);
                return proc.ProcessName;
            }
            catch
            {
                return null;
            }
        }
    }
}
