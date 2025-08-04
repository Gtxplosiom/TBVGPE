using Microsoft.Win32;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;

namespace TBVGPE.Views.Presets._3DS
{
    public partial class ShoulderButtons : UserControl
    {
        private Vector2 _screenDimentions;

        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        // Original background color for buttons
        private readonly SolidColorBrush _defaultButtonBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xAA, 0xAA, 0xAA)); // #AAA
        // Color for pressed state
        private readonly SolidColorBrush _pressedButtonBackground = new SolidColorBrush(Colors.DarkGray);

        public ShoulderButtons()
        {
            InitializeComponent();

            UpdateDimensions();

            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
        }

        private void OnDisplaySettingsChanged(object? sender, EventArgs e)
        {
            UpdateDimensions();
        }

        private void UpdateDimensions()
        {
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;
            _screenDimentions = new Vector2(width, height);

            this.Width = _screenDimentions.X;
        }

        private void LBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_1);
            LBtn.Background = _pressedButtonBackground;
        }

        private void LBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_1);
            LBtn.Background = _defaultButtonBackground;
        }

        private void LBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_1);
                LBtn.Background = _defaultButtonBackground;
            }
        }

        private void ZLBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_2);
            ZLBtn.Background = _pressedButtonBackground;
        }

        private void ZLBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_2);
            ZLBtn.Background = _defaultButtonBackground;
        }

        private void ZLBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_2);
                ZLBtn.Background = _defaultButtonBackground;
            }
        }

        private void RBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_0);
            RBtn.Background = _pressedButtonBackground;
        }

        private void RBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_0);
            RBtn.Background = _defaultButtonBackground;
        }

        private void RBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_0);
                RBtn.Background = _defaultButtonBackground;
            }
        }

        private void ZRBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_9);
            ZRBtn.Background = _pressedButtonBackground;
        }

        private void ZRBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_9);
            ZRBtn.Background = _defaultButtonBackground;
        }

        private void ZRBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_9);
                ZRBtn.Background = _defaultButtonBackground;
            }
        }
    }
}
