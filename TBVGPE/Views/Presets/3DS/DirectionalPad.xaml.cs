using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;

namespace TBVGPE.Views.Presets._3DS
{
    public partial class DirectionalPad : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        // Original background color for buttons
        private readonly SolidColorBrush _defaultButtonBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _defaultDiagonalButtonBackground = new SolidColorBrush(Colors.Gray);
        // Color for pressed stateb
        private readonly SolidColorBrush _pressedButtonBackground = new SolidColorBrush(Colors.DarkGray);

        public DirectionalPad()
        {
            InitializeComponent();
        }

        private void UpBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.UP);
            UpBtn.Background = _pressedButtonBackground;
        }

        private void UpBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.UP);
            UpBtn.Background = _defaultButtonBackground;
        }

        private void UpBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.UP);
                UpBtn.Background = _defaultButtonBackground;
                Debug.WriteLine("D-Pad: Up KeyUp (on MouseLeave)");
            }
        }

        private void LeftBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
            LeftBtn.Background = _pressedButtonBackground;
        }

        private void LeftBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
            LeftBtn.Background = _defaultButtonBackground;
        }

        private void LeftBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
                LeftBtn.Background = _defaultButtonBackground;
                Debug.WriteLine("D-Pad: Left KeyUp (on MouseLeave)");
            }
        }

        private void DownBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
            DownBtn.Background = _pressedButtonBackground;
        }

        private void DownBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
            DownBtn.Background = _defaultButtonBackground;
        }

        private void DownBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                DownBtn.Background = _defaultButtonBackground;
                Debug.WriteLine("D-Pad: Down KeyUp (on MouseLeave)");
            }
        }

        private void RightBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
            RightBtn.Background = _pressedButtonBackground;
        }

        private void RightBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
            RightBtn.Background = _defaultButtonBackground;
        }

        private void RightBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
                RightBtn.Background = _defaultButtonBackground;
            }
        }

        // Diagonal Buttons
        private void UpLeftBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.UP);
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
            UpLeftBtn.Background = _pressedButtonBackground;
        }

        private void UpLeftBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.UP);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
            UpLeftBtn.Background = _defaultDiagonalButtonBackground;
        }

        private void UpLeftBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.UP);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
                UpLeftBtn.Background = _defaultDiagonalButtonBackground;
            }
        }

        private void UpRightBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.UP);
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
            UpRightBtn.Background = _pressedButtonBackground;
        }

        private void UpRightBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.UP);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
            UpRightBtn.Background = _defaultDiagonalButtonBackground;
        }

        private void UpRightBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.UP);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
                UpRightBtn.Background = _defaultDiagonalButtonBackground;
            }
        }

        private void DownLeftBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
            DownLeftBtn.Background = _pressedButtonBackground;
        }

        private void DownLeftBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
            DownLeftBtn.Background = _defaultDiagonalButtonBackground;
        }

        private void DownLeftBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
                DownLeftBtn.Background = _defaultDiagonalButtonBackground;
            }
        }

        private void DownRightBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
            DownRightBtn.Background = _pressedButtonBackground;
        }

        private void DownRightBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
            DownRightBtn.Background = _defaultDiagonalButtonBackground;
        }

        private void DownRightBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
                DownRightBtn.Background = _defaultDiagonalButtonBackground;
            }
        }
    }
}
