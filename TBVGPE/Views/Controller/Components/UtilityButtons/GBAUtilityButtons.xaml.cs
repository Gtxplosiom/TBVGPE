using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Controller.Components.UtilityButtons
{
    public partial class GBAUtilityButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray);

        public GBAUtilityButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(StartBtn, Xbox360Button.Start);
            AttachTouchHandlers(SelectBtn, Xbox360Button.Back);

            AttachMouseHandlers(StartBtn, Xbox360Button.Start);
            AttachMouseHandlers(SelectBtn, Xbox360Button.Back);
        }

        private void AttachTouchHandlers(Ellipse button, Xbox360Button utilityButtons)
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

        private void AttachMouseHandlers(Ellipse button, Xbox360Button shoulderButtons)
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
