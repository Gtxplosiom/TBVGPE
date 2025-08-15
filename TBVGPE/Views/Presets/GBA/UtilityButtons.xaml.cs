using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Presets.GBA
{
    public partial class UtilityButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray);

        public UtilityButtons()
        {
            InitializeComponent();
            Loaded += FaceButtons_Loaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(StartBtn, Xbox360Button.Start);
            AttachTouchHandlers(SelectBtn, Xbox360Button.Back);
        }

        private void AttachTouchHandlers(Ellipse button, Xbox360Button utilityButtons)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.SetButtonState(utilityButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.SetButtonState(utilityButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };
        }
    }
}
