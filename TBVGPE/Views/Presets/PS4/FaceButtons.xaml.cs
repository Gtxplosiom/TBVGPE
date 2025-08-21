using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace TBVGPE.Views.Presets.PS4
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
            AttachTouchHandlers(TriangleBtn, DualShock4Button.Triangle);
            AttachTouchHandlers(SquareBtn, DualShock4Button.Square);
            AttachTouchHandlers(CircleBtn, DualShock4Button.Circle);
            AttachTouchHandlers(CrossBtn, DualShock4Button.Cross);
        }

        private void AttachTouchHandlers(Ellipse button, DualShock4Button faceButtons)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.SetDS4ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.SetDS4ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                App.Vigem.SetDS4ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                App.Vigem.SetDS4ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }
    }
}
