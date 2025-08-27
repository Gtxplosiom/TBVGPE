using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TBVGPE.ViewModels.Controllers
{
    public class ControllerWrapperViewModel : ContentControl
    {
        private bool _isDragging;
        private Point _clickPosition;

        public ControllerWrapperViewModel()
        {
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;

            Loaded += (s, e) =>
            {
                App.EditModeToggled += OnEditModeToggled;
                var parent = this.Parent as Canvas;
                if (parent == null) return;

                this.TouchDown += (sender, args) =>
                {
                    // input blocking yeah
                    if (App.EditMode == false) return;

                    _isDragging = true;
                    _clickPosition = args.GetTouchPoint(parent).Position;
                    CaptureTouch(args.TouchDevice);
                };

                this.TouchMove += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    if (_isDragging)
                    {
                        var canvas = parent;
                        var position = args.GetTouchPoint(canvas).Position;

                        double offsetX = position.X - _clickPosition.X;
                        double offsetY = position.Y - _clickPosition.Y;

                        double newLeft = Canvas.GetLeft(this) + offsetX;
                        double newTop = Canvas.GetTop(this) + offsetY;

                        Canvas.SetLeft(this, newLeft);
                        Canvas.SetTop(this, newTop);

                        _clickPosition = position;
                    }
                };

                this.TouchUp += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    _isDragging = false;
                    ReleaseTouchCapture(args.TouchDevice);

                    if (DataContext is ILayoutElement element)
                    {
                        double left = Canvas.GetLeft(this);
                        double top = Canvas.GetTop(this);
                        element.X = left;
                        element.Y = top;
                    }
                };
            };
        }

        private void OnEditModeToggled(object? sender, EventArgs e)
        {
            if (App.EditMode)
            {
                this.BorderBrush = new SolidColorBrush(Colors.Blue);
                this.BorderThickness = new Thickness(2);
            }
            else
            {
                this.BorderBrush = null;
                this.BorderThickness = new Thickness(0);
            }
        }
    }
}
