using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;

namespace TBVGPE.Views.Presets.Switch
{
    public partial class UtilityButtons : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

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
                foreach (var key in ParseKeyTag(tag))
                {
                    _inputSimulator.Keyboard.KeyDown(key);
                }

                btn.Background = _pressedButtonBackground;
                e.Handled = true;
            }
        }

        private void Button_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                foreach (var key in ParseKeyTag(tag))
                {
                    _inputSimulator.Keyboard.KeyUp(key);
                }

                btn.Background = _defaultButtonBackground;
                e.Handled = true;
            }
        }

        private void Button_TouchLeave(object sender, TouchEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                foreach (var key in ParseKeyTag(tag))
                {
                    _inputSimulator.Keyboard.KeyUp(key);
                }

                btn.Background = _defaultButtonBackground;
                e.Handled = true;
            }
        }

        private IEnumerable<VirtualKeyCode> ParseKeyTag(string tag)
        {
            var keys = tag.Split(',');
            foreach (var key in keys)
            {
                if (Enum.TryParse(key.Trim(), out VirtualKeyCode vk))
                    yield return vk;
            }
        }
    }
}
