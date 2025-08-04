using InputSimulatorStandard;
using InputSimulatorStandard.Native;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TBVGPE.Views.Presets._3DS
{
    public partial class UtilityButtons : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        // Original background color for buttons
        private readonly SolidColorBrush _defaultButtonBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xAA, 0xAA, 0xAA)); // #AAA
        // Color for pressed state
        private readonly SolidColorBrush _pressedButtonBackground = new SolidColorBrush(Colors.DarkGray);

        public UtilityButtons()
        {
            InitializeComponent();
        }

        private void StartBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.RETURN);
            StartBtn.Background = _pressedButtonBackground;
        }

        private void StartBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RETURN);
            StartBtn.Background = _defaultButtonBackground;
        }

        private void StartBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.RETURN);
                StartBtn.Background = _defaultButtonBackground;
            }
        }

        private void SelectBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.SPACE);
            SelectBtn.Background = _pressedButtonBackground;
        }

        private void SelectBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SPACE);
            SelectBtn.Background = _defaultButtonBackground;
        }

        private void SelectBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.SPACE);
                SelectBtn.Background = _defaultButtonBackground;
            }
        }

        private void HomeBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.HOME);
            HomeBtn.Background = _pressedButtonBackground;
        }

        private void HomeBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.HOME);
            HomeBtn.Background = _defaultButtonBackground;
        }

        private void HomeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.HOME);
                HomeBtn.Background = _defaultButtonBackground;
            }
        }
    }
}
