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
    public partial class DirectionalButtons : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33));
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        public DirectionalButtons()
        {
            InitializeComponent();
            Loaded += DirectionalButtons_Loaded;
        }

        private void DirectionalButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(UpBtn, VirtualKeyCode.UP);
            AttachTouchHandlers(LeftBtn, VirtualKeyCode.LEFT);
            AttachTouchHandlers(DownBtn, VirtualKeyCode.DOWN);
            AttachTouchHandlers(RightBtn, VirtualKeyCode.RIGHT);
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
