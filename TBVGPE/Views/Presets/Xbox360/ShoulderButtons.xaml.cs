using Microsoft.Win32;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Presets.Xbox360
{
    public partial class ShoulderButtons : UserControl
    {
        private Vector2 _screenDimensions;
        private readonly SolidColorBrush _defaultButtonBackground = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA));
        private readonly SolidColorBrush _pressedButtonBackground = new SolidColorBrush(Colors.DarkGray);

        public ShoulderButtons()
        {
            InitializeComponent();

            UpdateDimensions();
            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
        }

        private void OnDisplaySettingsChanged(object? sender, EventArgs e) => UpdateDimensions();

        private void UpdateDimensions()
        {
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;
            _screenDimensions = new Vector2(width, height);
            this.Width = _screenDimensions.X;
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

        private void ApplyControllerInput(string tag, bool isPressed)
        {
            switch (tag)
            {
                case "LB":
                    App.Vigem.SetButtonState(Xbox360Button.LeftShoulder, isPressed);
                    break;
                case "RB":
                    App.Vigem.SetButtonState(Xbox360Button.RightShoulder, isPressed);
                    break;
                case "LT":
                    App.Vigem.SetTriggerValue(Xbox360Slider.LeftTrigger, isPressed ? (byte)255 : (byte)0);
                    break;
                case "RT":
                    App.Vigem.SetTriggerValue(Xbox360Slider.RightTrigger, isPressed ? (byte)255 : (byte)0);
                    break;
            }
        }
    }
}
