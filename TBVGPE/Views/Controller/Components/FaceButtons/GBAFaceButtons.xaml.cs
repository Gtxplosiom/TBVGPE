using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Controller.Components.FaceButtons
{
    public partial class GBAFaceButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray);

        public GBAFaceButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(BBtn, Xbox360Button.A);
            AttachTouchHandlers(ABtn, Xbox360Button.B);

            AttachComboHandlers(APlusBBtn, APlusBTxt, Xbox360Button.B, Xbox360Button.A);
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

        private void AttachComboHandlers(Ellipse button, Label buttonName, Xbox360Button faceButton1, Xbox360Button faceButton2)
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

        private void ToggleComboVisibility(Ellipse button, Label buttonName, bool state)
        {
            button.Opacity = state ? 0.5 : 0.0;
            buttonName.Opacity = state ? 0.5 : 0.0;
        }
    }
}
