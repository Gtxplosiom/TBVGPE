using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TBVGPE.Views.Controller.Components.DirectionalButtons
{
    public partial class StandardDirectionalButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _defaultDiagButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray);

        private bool _isTouchDevice;

        public StandardDirectionalButtons()
        {
            InitializeComponent();
            UpdateSizes();
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

            AttachMouseHandlers(UpBtn, Xbox360Button.Up);
            AttachMouseHandlers(LeftBtn, Xbox360Button.Left);
            AttachMouseHandlers(DownBtn, Xbox360Button.Down);
            AttachMouseHandlers(RightBtn, Xbox360Button.Right);

            // Diagonals
            AttachDiagMouseHandlers(UpLeftBtn, Xbox360Button.Up, Xbox360Button.Left);
            AttachDiagMouseHandlers(DownLeftBtn, Xbox360Button.Down, Xbox360Button.Left);
            AttachDiagMouseHandlers(DownRightBtn, Xbox360Button.Down, Xbox360Button.Right);
            AttachDiagMouseHandlers(UpRightBtn, Xbox360Button.Up, Xbox360Button.Right);
        }

        private void AttachTouchHandlers(Rectangle button, Xbox360Button directionalButtons)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(directionalButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(directionalButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }

        private void AttachDiagTouchHandlers(Rectangle button, Xbox360Button directionalButton1, Xbox360Button directionalButton2)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(directionalButton1, true);
                App.Vigem.Set360ButtonState(directionalButton2, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(directionalButton1, false);
                App.Vigem.Set360ButtonState(directionalButton2, false);
                button.Fill = _defaultDiagButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = false;

                App.Vigem.Set360ButtonState(directionalButton1, false);
                App.Vigem.Set360ButtonState(directionalButton2, false);
                button.Fill = _defaultDiagButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                _isTouchDevice = true;

                App.Vigem.Set360ButtonState(directionalButton1, true);
                App.Vigem.Set360ButtonState(directionalButton2, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }

        private void AttachMouseHandlers(Rectangle button, Xbox360Button directionalButtons)
        {
            button.PreviewMouseDown += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(directionalButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.PreviewMouseUp += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.MouseLeave += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };
        }

        private void AttachDiagMouseHandlers(Rectangle button, Xbox360Button directionalButton1, Xbox360Button directionalButton2)
        {
            button.PreviewMouseDown += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(directionalButton1, true);
                App.Vigem.Set360ButtonState(directionalButton2, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.PreviewMouseUp += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(directionalButton1, false);
                App.Vigem.Set360ButtonState(directionalButton2, false);
                button.Fill = _defaultDiagButtonFill;
                e.Handled = true;
            };

            button.MouseLeave += (s, e) =>
            {
                if (App.EditMode || _isTouchDevice) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(directionalButton1, false);
                App.Vigem.Set360ButtonState(directionalButton2, false);
                button.Fill = _defaultDiagButtonFill;
                e.Handled = true;
            };
        }

        public static readonly DependencyProperty ButtonSizeProperty =
        DependencyProperty.Register("ButtonSize", typeof(double), typeof(StandardDirectionalButtons), new PropertyMetadata(60.0, OnSizePropertyChanged));

        public double ButtonSize
        {
            get { return (double)GetValue(ButtonSizeProperty); }
            set { SetValue(ButtonSizeProperty, value); }
        }

        public static readonly DependencyProperty CanvasSizeProperty =
            DependencyProperty.Register("CanvasSize", typeof(double), typeof(StandardDirectionalButtons), new PropertyMetadata(180.0, OnSizePropertyChanged));

        public double CanvasSize
        {
            get { return (double)GetValue(CanvasSizeProperty); }
            set { SetValue(CanvasSizeProperty, value); }
        }

        // A single callback for both properties to avoid redundant updates.
        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StandardDirectionalButtons)?.UpdateSizes();
        }

        private void UpdateSizes()
        {
            // Set the size of the canvas itself
            PadCanvas.Width = CanvasSize;
            PadCanvas.Height = CanvasSize;

            // Define the dimensions of the buttons and a central empty space
            double buttonSize = ButtonSize;
            double canvasCenter = CanvasSize / 2;
            double buttonHalf = buttonSize / 2;
            double spacing = canvasCenter - buttonHalf;

            // Set dimensions for all buttons and the center rectangle
            UpBtn.Width = UpBtn.Height = buttonSize;
            LeftBtn.Width = LeftBtn.Height = buttonSize;
            DownBtn.Width = DownBtn.Height = buttonSize;
            RightBtn.Width = RightBtn.Height = buttonSize;
            UpLeftBtn.Width = UpLeftBtn.Height = buttonSize;
            UpRightBtn.Width = UpRightBtn.Height = buttonSize;
            DownLeftBtn.Width = DownLeftBtn.Height = buttonSize;
            DownRightBtn.Width = DownRightBtn.Height = buttonSize;
            FillerRect.Width = FillerRect.Height = buttonSize;

            // Position the primary directional buttons
            Canvas.SetLeft(UpBtn, spacing);
            Canvas.SetLeft(RightBtn, CanvasSize - buttonSize);
            Canvas.SetTop(RightBtn, spacing);
            Canvas.SetLeft(DownBtn, spacing);
            Canvas.SetTop(DownBtn, CanvasSize - buttonSize);
            Canvas.SetTop(LeftBtn, spacing);

            // Position the diagonal buttons
            Canvas.SetLeft(UpLeftBtn, 0);
            Canvas.SetTop(UpLeftBtn, 0);
            Canvas.SetLeft(UpRightBtn, CanvasSize - buttonSize);
            Canvas.SetTop(UpRightBtn, 0);
            Canvas.SetLeft(DownLeftBtn, 0);
            Canvas.SetTop(DownLeftBtn, CanvasSize - buttonSize);
            Canvas.SetLeft(DownRightBtn, CanvasSize - buttonSize);
            Canvas.SetTop(DownRightBtn, CanvasSize - buttonSize);

            // Position the center filler rectangle
            Canvas.SetLeft(FillerRect, spacing);
            Canvas.SetTop(FillerRect, spacing);
        }
    }
}
