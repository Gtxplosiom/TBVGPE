using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Presets.Switch
{
    public partial class DirectionalButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33));
        private readonly SolidColorBrush _defaultDiagButtonFill = new SolidColorBrush(Colors.Transparent);
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        public DirectionalButtons()
        {
            InitializeComponent();
            Loaded += DirectionalButtons_Loaded;
        }

        private void DirectionalButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(UpBtn, Xbox360Button.Up);
            AttachTouchHandlers(LeftBtn, Xbox360Button.Left);
            AttachTouchHandlers(DownBtn, Xbox360Button.Down);
            AttachTouchHandlers(RightBtn, Xbox360Button.Right);

            // Diagonals
            AttachDiagTouchHandlers(UpLeftBtn, Xbox360Button.Up, Xbox360Button.Left);
            AttachDiagTouchHandlers(DownLeftBtn, Xbox360Button.Down, Xbox360Button.Left);
            AttachDiagTouchHandlers(DownRightBtn, Xbox360Button.Down, Xbox360Button.Right);
            AttachDiagTouchHandlers(UpRightBtn, Xbox360Button.Up, Xbox360Button.Right);
        }

        private void AttachTouchHandlers(Ellipse button, Xbox360Button directionalButtons)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }

        private void AttachDiagTouchHandlers(Ellipse button, Xbox360Button directionalButton1, Xbox360Button directionalButton2)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButton1, true);
                App.Vigem.Set360ButtonState(directionalButton2, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButton1, false);
                App.Vigem.Set360ButtonState(directionalButton2, false);
                button.Fill = _defaultDiagButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButton1, false);
                App.Vigem.Set360ButtonState(directionalButton2, false);
                button.Fill = _defaultDiagButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                App.Vigem.Set360ButtonState(directionalButton1, true);
                App.Vigem.Set360ButtonState(directionalButton2, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }
    }
}
