using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace TBVGPE.Views.Presets.PS4
{
    public partial class UtilityButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonBackground = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _pressedButtonBackground = new SolidColorBrush(Colors.DarkGray);

        public UtilityButtons()
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
                ApplyControllerInput(tag, false);
                btn.Background = _pressedButtonBackground;
                e.Handled = true;
            }
        }

        private void ApplyControllerInput(string tag, bool isPressed)
        {
            switch (tag)
            {
                case "Options":
                    App.Vigem.SetDS4ButtonState(DualShock4Button.Options, isPressed);
                    break;
                case "Share":
                    App.Vigem.SetDS4ButtonState(DualShock4Button.Share, isPressed);
                    break;
                case "Ps":
                    App.Vigem.SetDS4ButtonState(DualShock4SpecialButton.Ps, isPressed);
                    break;
            }
        }
    }
}
