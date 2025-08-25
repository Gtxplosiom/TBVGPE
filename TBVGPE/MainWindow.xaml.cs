using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using TBVGPE.ViewModels;

namespace TBVGPE
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        private readonly Vector2 _screenDimentions;

        public MainWindow()
        {
            InitializeComponent();

            _screenDimentions = new Vector2((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);

            this.Width = _screenDimentions.X;
            this.Height = _screenDimentions.Y;

            this.Loaded += MainWindow_Loaded;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Get the window handle (HWND)
            var helper = new WindowInteropHelper(this);

            // Get the current extended window style and add the WS_EX_NOACTIVATE flag
            int exStyle = GetWindowLong(helper.Handle, GWL_EXSTYLE);
            SetWindowLong(helper.Handle, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if the DataContext is our ViewModel
            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                // Now that the window is loaded, run the update check
                await mainWindowViewModel.RunUpdateChecker();
            }
        }
    }
}

// TODO: do something about that * 2 in the setleft
// * 2 because when i don't the button is cut in half and the rest is outside the screen
