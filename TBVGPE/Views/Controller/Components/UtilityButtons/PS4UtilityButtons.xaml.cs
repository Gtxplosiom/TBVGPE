using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TBVGPE.Views.Controller.Components.UtilityButtons
{
    public partial class PS4UtilityButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray);

        public PS4UtilityButtons()
        {
            InitializeComponent();
            Loaded += UtilityButtons_Loaded;
        }

        private void UtilityButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(ShareBtn, Xbox360Button.Back);
            AttachTouchHandlers(PsBtn, Xbox360Button.Guide);
            AttachTouchHandlers(OptionsBtn, Xbox360Button.Start);

            AttachMouseHandlers(ShareBtn, Xbox360Button.Back);
            AttachMouseHandlers(PsBtn, Xbox360Button.Guide);
            AttachMouseHandlers(OptionsBtn, Xbox360Button.Start);
        }

        private void AttachTouchHandlers(Rectangle button, Xbox360Button utilityButtons)
        {
            button.TouchDown += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(utilityButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(utilityButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(utilityButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                if (App.EditMode) return; // temporary blocker la anay kay mahubya pa

                App.Vigem.Set360ButtonState(utilityButtons, true);
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
