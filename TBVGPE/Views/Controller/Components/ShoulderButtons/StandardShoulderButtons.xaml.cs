using Microsoft.Win32;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TBVGPE.Views.Controller.Components.ShoulderButtons
{
    public partial class StandardShoulderButtons : UserControl
    {
        private Vector2 _screenDimentions;

        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray);

        public StandardShoulderButtons()
        {
            InitializeComponent();
            UpdateDimensions();
            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
            Loaded += ShoulderButtons_Loaded;
        }

        private void OnDisplaySettingsChanged(object? sender, EventArgs e)
        {
            UpdateDimensions();
            Loaded += ShoulderButtons_Loaded;
        }

        private void UpdateDimensions()
        {
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;
            _screenDimentions = new Vector2(width, height);
            this.Width = _screenDimentions.X;
        }

        private void ShoulderButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(LBtn, Xbox360Button.LeftShoulder);
            AttachTouchHandlers(RBtn, Xbox360Button.RightShoulder);

            AttachMouseHandlers(LBtn, Xbox360Button.LeftShoulder);
            AttachMouseHandlers(RBtn, Xbox360Button.RightShoulder);
        }

        private void AttachTouchHandlers(Rectangle button, Xbox360Button shoulderButtons)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }

        private void AttachMouseHandlers(Rectangle button, Xbox360Button shoulderButtons)
        {
            button.PreviewMouseDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.PreviewMouseUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.MouseLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.MouseEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(shoulderButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }
    }
}
