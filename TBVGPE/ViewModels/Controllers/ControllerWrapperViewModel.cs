// ControllerWrapperViewModel.cs

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
                    if (App.EditMode == false) return;

                    _isDragging = true;
                    _clickPosition = args.GetTouchPoint(parent).Position;
                    CaptureTouch(args.TouchDevice);
                };

                this.TouchMove += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    if (_isDragging && DataContext is ILayoutElement element)
                    {
                        var canvas = parent;
                        var currentPosition = args.GetTouchPoint(canvas).Position;

                        double offsetX = currentPosition.X - _clickPosition.X;
                        double offsetY = currentPosition.Y - _clickPosition.Y;

                        // update the elements' viewmodel values instead of position the canavs.left and canvas.top in the view
                        element.X += offsetX;
                        element.Y += offsetY;

                        _clickPosition = currentPosition;
                    }
                };

                this.TouchUp += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    _isDragging = false;
                    ReleaseTouchCapture(args.TouchDevice);
                };

                this.PreviewMouseDown += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    _isDragging = true;
                    _clickPosition = args.GetPosition(parent);
                    CaptureMouse();
                };

                this.PreviewMouseMove += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    if (_isDragging && DataContext is ILayoutElement element)
                    {
                        var canvas = parent;
                        var currentPosition = args.GetPosition(canvas);

                        double offsetX = currentPosition.X - _clickPosition.X;
                        double offsetY = currentPosition.Y - _clickPosition.Y;

                        element.X += offsetX;
                        element.Y += offsetY;

                        _clickPosition = currentPosition;
                    }
                };

                this.PreviewMouseUp += (sender, args) =>
                {
                    if (App.EditMode == false) return;

                    _isDragging = false;
                    ReleaseMouseCapture();
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
