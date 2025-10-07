using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Controller.Components.FaceButtons
{
    public partial class XboxFaceButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA));
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray); // A darker gray for pressed state

        private bool _isTouchDevice;

        public XboxFaceButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(XBtn, Xbox360Button.X);
            AttachTouchHandlers(YBtn, Xbox360Button.Y);
            AttachTouchHandlers(BBtn, Xbox360Button.B);
            AttachTouchHandlers(ABtn, Xbox360Button.A);

            AttachMouseHandlers(XBtn, Xbox360Button.X);
            AttachMouseHandlers(YBtn, Xbox360Button.Y);
            AttachMouseHandlers(BBtn, Xbox360Button.B);
            AttachMouseHandlers(ABtn, Xbox360Button.A);

            AttachTouchComboHandlers(YPlusXBtn, YPlusXTxt, Xbox360Button.Y, Xbox360Button.X);
            AttachTouchComboHandlers(XPlusABtn, XPlusATxt, Xbox360Button.X, Xbox360Button.A);
            AttachTouchComboHandlers(APlusBBtn, APlusBTxt, Xbox360Button.A, Xbox360Button.B);
            AttachTouchComboHandlers(BPlusYBtn, BPlusYTxt, Xbox360Button.B, Xbox360Button.Y);

            AttachMouseComboHandlers(YPlusXBtn, YPlusXTxt, Xbox360Button.Y, Xbox360Button.X);
            AttachMouseComboHandlers(XPlusABtn, XPlusATxt, Xbox360Button.X, Xbox360Button.A);
            AttachMouseComboHandlers(APlusBBtn, APlusBTxt, Xbox360Button.A, Xbox360Button.B);
            AttachMouseComboHandlers(BPlusYBtn, BPlusYTxt, Xbox360Button.B, Xbox360Button.Y);
        }

        private void AttachTouchHandlers(Ellipse button, Xbox360Button faceButtons)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }

        private void AttachMouseHandlers(Ellipse button, Xbox360Button faceButtons)
        {
            button.PreviewMouseDown += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(faceButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.PreviewMouseUp += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.MouseLeave += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(faceButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };
        }

        private void AttachTouchComboHandlers(Ellipse button, TextBlock buttonName, Xbox360Button faceButton1, Xbox360Button faceButton2)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(faceButton1, true);
                App.Vigem.Set360ButtonState(faceButton2, true);
                button.Fill = _pressedButtonFill;
                ToggleComboVisibility(button, buttonName, true);
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(faceButton1, false);
                App.Vigem.Set360ButtonState(faceButton2, false);
                button.Fill = _defaultButtonFill;
                ToggleComboVisibility(button, buttonName, false);
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(faceButton1, false);
                App.Vigem.Set360ButtonState(faceButton2, false);
                button.Fill = _defaultButtonFill;
                ToggleComboVisibility(button, buttonName, false);
                e.Handled = true;
            };
        }

        private void AttachMouseComboHandlers(Ellipse button, TextBlock buttonName, Xbox360Button faceButton1, Xbox360Button faceButton2)
        {
            button.PreviewMouseDown += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(faceButton1, true);
                App.Vigem.Set360ButtonState(faceButton2, true);
                button.Fill = _pressedButtonFill;
                ToggleComboVisibility(button, buttonName, true);
                e.Handled = true;
            };

            button.PreviewMouseUp += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(faceButton1, false);
                App.Vigem.Set360ButtonState(faceButton2, false);
                button.Fill = _defaultButtonFill;
                ToggleComboVisibility(button, buttonName, false);
                e.Handled = true;
            };

            button.MouseLeave += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

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
