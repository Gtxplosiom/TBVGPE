using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Controller.Components.FaceButtons
{
    public partial class PlayStationFaceButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA));
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray); // A darker gray for pressed state

        private bool _isTouchDevice;

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

            AttachMouseHandlers(TriangleBtn, Xbox360Button.Y);
            AttachMouseHandlers(SquareBtn, Xbox360Button.X);
            AttachMouseHandlers(CircleBtn, Xbox360Button.B);
            AttachMouseHandlers(CrossBtn, Xbox360Button.A);

            AttachTouchComboHandlers(TrianglePlusSquareBtn, TrianglePlusSquareTxt, Xbox360Button.Y, Xbox360Button.X);
            AttachTouchComboHandlers(SquarePlusCrossBtn, SquarePlusCrossTxt, Xbox360Button.X, Xbox360Button.A);
            AttachTouchComboHandlers(CrossPlusCircleBtn, CrossPlusCircleTxt, Xbox360Button.A, Xbox360Button.B);
            AttachTouchComboHandlers(CirclePlusTriangleBtn, CirclePlusTriangleTxt, Xbox360Button.B, Xbox360Button.Y);

            AttachMouseComboHandlers(TrianglePlusSquareBtn, TrianglePlusSquareTxt, Xbox360Button.Y, Xbox360Button.X);
            AttachMouseComboHandlers(SquarePlusCrossBtn, SquarePlusCrossTxt, Xbox360Button.X, Xbox360Button.A);
            AttachMouseComboHandlers(CrossPlusCircleBtn, CrossPlusCircleTxt, Xbox360Button.A, Xbox360Button.B);
            AttachMouseComboHandlers(CirclePlusTriangleBtn, CirclePlusTriangleTxt, Xbox360Button.B, Xbox360Button.Y);
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
                button.Fill =_defaultButtonFill;
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
