using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Controller.Components.FaceButtons
{
    public partial class PlayStationFaceButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        public PlayStationFaceButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(TriangleBtn, Xbox360Button.Y);
            AttachTouchHandlers(SquareBtn, Xbox360Button.X);
            AttachTouchHandlers(CircleBtn, Xbox360Button.B);
            AttachTouchHandlers(CrossBtn, Xbox360Button.A);

            AttachComboHandlers(TrianglePlusSquareBtn, TrianglePlusSquareTxt, Xbox360Button.Y, Xbox360Button.X);
            AttachComboHandlers(SquarePlusCrossBtn, SquarePlusCrossTxt, Xbox360Button.X, Xbox360Button.A);
            AttachComboHandlers(CrossPlusCircleBtn, CrossPlusCircleTxt, Xbox360Button.A, Xbox360Button.B);
            AttachComboHandlers(CirclePlusTriangleBtn, CirclePlusTriangleTxt, Xbox360Button.B, Xbox360Button.Y);
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
                button.Fill =_defaultButtonFill;
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

        private void AttachComboHandlers(Ellipse button, TextBlock buttonName, Xbox360Button faceButton1, Xbox360Button faceButton2)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButton1, true);
                App.Vigem.Set360ButtonState(faceButton2, true);
                button.Fill = _pressedButtonFill;
                ToggleComboVisibility(button, buttonName, true);
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButton1, false);
                App.Vigem.Set360ButtonState(faceButton2, false);
                button.Fill = _defaultButtonFill;
                ToggleComboVisibility(button, buttonName, false);
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                App.Vigem.Set360ButtonState(faceButton1, false);
                App.Vigem.Set360ButtonState(faceButton2, false);
                button.Fill = _defaultButtonFill;
                ToggleComboVisibility(button, buttonName, false);
                e.Handled = true;
            };
        }

        private void ToggleComboVisibility(Ellipse button, TextBlock buttonText, bool state)
        {
            button.Opacity = state ? 0.5 : 0.0;
            buttonText.Opacity = state ? 0.5 : 0.0;
        }
    }
}
