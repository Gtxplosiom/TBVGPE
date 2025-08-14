using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Nefarius.ViGEm.Client.Targets.Xbox360;

namespace TBVGPE.Views.Presets.Switch
{
    public partial class DirectionalButtons : UserControl
    {
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33));
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        public DirectionalButtons()
        {
            InitializeComponent();
            Loaded += DirectionalButtons_Loaded;
        }

        private void DirectionalButtons_Loaded(object sender, RoutedEventArgs e)
        {
            AttachTouchHandlers(UpBtn, Xbox360Button.Up);
            AttachTouchHandlers(LeftBtn, Xbox360Button.Left);
            AttachTouchHandlers(DownBtn, Xbox360Button.Down);
            AttachTouchHandlers(RightBtn, Xbox360Button.Right);
        }

        private void AttachTouchHandlers(Ellipse button, Xbox360Button directionalButtons)
        {
            button.TouchDown += (s, e) =>
            {
                App.Vigem.SetButtonState(directionalButtons, true);
                button.Fill = _pressedButtonFill;
                e.Handled = true;
            };

            button.TouchUp += (s, e) =>
            {
                App.Vigem.SetButtonState(directionalButtons, false);
                button.Fill = _defaultButtonFill;
                e.Handled = true;
            };
        }
    }
}
