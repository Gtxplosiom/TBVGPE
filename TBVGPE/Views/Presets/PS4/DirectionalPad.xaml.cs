using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace TBVGPE.Views.Presets.PS4
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
            if (!isPressed)
            {
                App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.None);
                return;
            }

            switch (tag)
            {
                case "North":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.North);
                    break;
                case "West":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.West);
                    break;
                case "South":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.South);
                    break;
                case "East":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.East);
                    break;

                // Diagonals
                case "Northwest":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.Northwest);
                    break;
                case "Southwest":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.Southwest);
                    break;
                case "Southeast":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.Southeast);
                    break;
                case "Northeast":
                    App.Vigem.SetDS4DPadDirection(DualShock4DPadDirection.Northeast);
                    break;
            }
        }
    }
}
