using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;
using System.Windows.Shapes;

namespace TBVGPE.Views.Presets.Switch
{
    public partial class FaceButtons : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        public FaceButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(XBtn, VirtualKeyCode.VK_I);
            AttachTouchHandlers(YBtn, VirtualKeyCode.VK_J);
            AttachTouchHandlers(BBtn, VirtualKeyCode.VK_K);
            AttachTouchHandlers(ABtn, VirtualKeyCode.VK_L);
        }

        private void AttachTouchHandlers(Ellipse button, VirtualKeyCode key)
        {
            button.TouchDown += (s, e) =>
            {
                _inputSimulator.Keyboard.KeyDown(key);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                _inputSimulator.Keyboard.KeyUp(key);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };
        }
    }
}
