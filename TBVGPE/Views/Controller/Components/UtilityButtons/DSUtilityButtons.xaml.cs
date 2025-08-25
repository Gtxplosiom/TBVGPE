using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TBVGPE.Views.Controller.Components.UtilityButtons
{
    public partial class DSUtilityButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)); // #AAA
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.Gray);

        public DSUtilityButtons()
        {
            InitializeComponent();
            Loaded += UtilityButtons_Loaded;
        }

        private void UtilityButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(SelectBtn, Xbox360Button.Back);
            AttachTouchHandlers(StartBtn, Xbox360Button.Start);
        }

        private void AttachTouchHandlers(Rectangle button, Xbox360Button utilityButtons)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.Set360ButtonState(utilityButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.Set360ButtonState(utilityButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchLeave += (s, e) =>
            {
                App.Vigem.Set360ButtonState(utilityButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };

            button.TouchEnter += (s, e) =>
            {
                App.Vigem.Set360ButtonState(utilityButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };
        }
    }
}
