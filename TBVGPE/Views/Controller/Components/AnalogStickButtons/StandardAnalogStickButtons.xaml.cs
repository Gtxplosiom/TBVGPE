using Microsoft.Win32;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TBVGPE.Views.Controller.Components.AnalogStickButtons
{
    public partial class StandardAnalogStickButtons : UserControl
    {
        private Vector2 _screenDimentions;

        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray);

        public StandardAnalogStickButtons()
        {
            InitializeComponent();
            UpdateDimensions();
            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
            Loaded += AnalogStickButtons_Loaded;
        }

        private void OnDisplaySettingsChanged(object? sender, EventArgs e)
        {
            UpdateDimensions();
        }

        private void UpdateDimensions()
        {
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;
            _screenDimentions = new Vector2(width, height);
            this.Width = _screenDimentions.X;
        }

        private void AnalogStickButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(LStickBtn, Xbox360Button.LeftThumb);
            AttachTouchHandlers(RStickBtn, Xbox360Button.RightThumb);
        }

        private void AttachTouchHandlers(Rectangle button, Xbox360Button analogStickButtons)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(analogStickButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(analogStickButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(analogStickButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(analogStickButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }
    }
}
