using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TBVGPE.Views.Presets.PS4
{
    public partial class RightStick : UserControl
    {
        private bool _isDragging;
        private Point _center;
        private double _radius;

        // Right stick axes values
        private double _xValue = 0;
        private double _yValue = 0;

        public RightStick()
        {
            InitializeComponent();
        }

        private void PadCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _center = new Point(e.NewSize.Width / 2, e.NewSize.Height / 2);
            _radius = (Math.Min(e.NewSize.Width, e.NewSize.Height) / 2) - (Thumb.Width / 2);
            ResetThumbPosition();
        }

        private void Thumb_TouchDown(object sender, TouchEventArgs e)
        {
            _isDragging = true;
            Thumb.CaptureTouch(e.TouchDevice);
            e.Handled = true;
        }

        private void Thumb_TouchMove(object sender, TouchEventArgs e)
        {
            if (!_isDragging) return;

            Point currentPos = e.GetTouchPoint(PadCanvas).Position;
            Vector offset = currentPos - _center;

            // Normalize offset to [-1, 1] range
            _xValue = Math.Max(-1, Math.Min(1, offset.X / _radius));
            _yValue = Math.Max(-1, Math.Min(1, offset.Y / _radius));

            // same here an ginhimo ha left stick
            App.Vigem.SetRightStick(_xValue, _yValue * -1);

            if (offset.Length > _radius)
            {
                offset.Normalize();
                offset *= _radius;
            }

            Canvas.SetLeft(Thumb, _center.X + offset.X - (Thumb.Width / 2));
            Canvas.SetTop(Thumb, _center.Y + offset.Y - (Thumb.Height / 2));

            e.Handled = true;
        }

        private void Thumb_TouchUp(object sender, TouchEventArgs e)
        {
            _isDragging = false;
            Thumb.ReleaseTouchCapture(e.TouchDevice);

            // reset an axes values
            _xValue = 0;
            _yValue = 0;
            App.Vigem.SetRightStick(_xValue, _yValue);

            ResetThumbPosition();

            e.Handled = true;
        }

        private void ResetThumbPosition()
        {
            if (_center != default)
            {
                Canvas.SetLeft(Thumb, _center.X - (Thumb.Width / 2));
                Canvas.SetTop(Thumb, _center.Y - (Thumb.Height / 2));
            }
        }
    }
}
