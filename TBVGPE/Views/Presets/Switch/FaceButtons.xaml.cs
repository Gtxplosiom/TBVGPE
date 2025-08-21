using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Presets.Switch
{
    public partial class FaceButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        public FaceButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(XBtn, Xbox360Button.Y);
            AttachTouchHandlers(YBtn, Xbox360Button.X);
            AttachTouchHandlers(BBtn, Xbox360Button.A);
            AttachTouchHandlers(ABtn, Xbox360Button.B);
        }

        private void AttachTouchHandlers(Ellipse button, Xbox360Button faceButtons)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }
    }
}
