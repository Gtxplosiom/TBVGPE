using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Presets._3DS
{
    public partial class DirectionalPad : UserControl
    {
        private readonly SolidColorBrush _defaultButtonBackground = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _defaultDiagButtonBackground = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333
        private readonly SolidColorBrush _pressedButtonBackground = new SolidColorBrush(Colors.DarkGray);

        public DirectionalPad()
        {
            InitializeComponent();
        }

        private void Button_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, true);
                btn.Background = _pressedButtonBackground;
                e.Handled = true;
            }
        }

        private void Button_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, false);
                btn.Background = _defaultButtonBackground;
                e.Handled = true;
            }
        }

        private void Button_TouchLeave(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, false);
                btn.Background = _defaultButtonBackground;
                e.Handled = true;
            }
        }

        private void Button_TouchEnter(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, true);
                btn.Background = _pressedButtonBackground;
                e.Handled = true;
            }
        }

        private void DiagButton_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, true);
                btn.Background = _pressedButtonBackground;
                e.Handled = true;
            }
        }

        private void DiagButton_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, false);
                btn.Background = _defaultDiagButtonBackground;
                e.Handled = true;
            }
        }

        private void DiagButton_TouchLeave(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, false);
                btn.Background = _defaultDiagButtonBackground;
                e.Handled = true;
            }
        }

        private void DiagButton_TouchEnter(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                ApplyControllerInput(tag, true);
                btn.Background = _pressedButtonBackground;
                e.Handled = true;
            }
        }

        private void ApplyControllerInput(string tag, bool isPressed)
        {
            switch (tag)
            {
                case "Up":
                    App.Vigem.Set360ButtonState(Xbox360Button.Up, isPressed);
                    break;
                case "Left":
                    App.Vigem.Set360ButtonState(Xbox360Button.Left, isPressed);
                    break;
                case "Down":
                    App.Vigem.Set360ButtonState(Xbox360Button.Down, isPressed);
                    break;
                case "Right":
                    App.Vigem.Set360ButtonState(Xbox360Button.Right, isPressed);
                    break;

                // Diagonals
                case "UpLeft":
                    App.Vigem.Set360ButtonState(Xbox360Button.Up, isPressed);
                    App.Vigem.Set360ButtonState(Xbox360Button.Left, isPressed);
                    break;
                case "DownLeft":
                    App.Vigem.Set360ButtonState(Xbox360Button.Down, isPressed);
                    App.Vigem.Set360ButtonState(Xbox360Button.Left, isPressed);
                    break;
                case "DownRight":
                    App.Vigem.Set360ButtonState(Xbox360Button.Down, isPressed);
                    App.Vigem.Set360ButtonState(Xbox360Button.Right, isPressed);
                    break;
                case "UpRight":
                    App.Vigem.Set360ButtonState(Xbox360Button.Up, isPressed);
                    App.Vigem.Set360ButtonState(Xbox360Button.Right, isPressed);
                    break;
            }
        }
    }
}
