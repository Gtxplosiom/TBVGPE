using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TBVGPE.Views.Controller.Components.AnalogSticks
{
    public partial class RightAnalogStick : UserControl
    {
        private bool _isDragging;
        private Point _center;
        private double _radius;
        private readonly double _extensionMultiplier = 2.0;

        // circlepad axes
        private double _xValue = 0.0;
        private double _yValue = 0.0;

        private Point _initialTouchPoint;
        private Vector _initialTouchOffset;

        public RightAnalogStick()
        {
            InitializeComponent();
        }

        private void PadCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // amo ini an area han joystick mismo, inner circle
            _center = new Point(e.NewSize.Width / 2, e.NewSize.Height / 2);

            // outer circle
            _radius = (Math.Min(e.NewSize.Width, e.NewSize.Height) / 2) - (Thumb.Width / 2);
            ResetThumbPosition();
        }

        private void Thumb_TouchDown(object sender, TouchEventArgs e)
        {
            _isDragging = true;
            Thumb.CaptureTouch(e.TouchDevice);

            // hain nag touch an user
            _initialTouchPoint = e.GetTouchPoint(PadCanvas).Position;

            // ngan an value han initial touch - ha center
            // rather thatn an touch point mismo an ig reference
            _initialTouchOffset = new Vector(
                Canvas.GetLeft(Thumb) + Thumb.Width / 2 - _center.X,
                Canvas.GetTop(Thumb) + Thumb.Height / 2 - _center.Y);

            e.Handled = true;
        }

        private void Thumb_TouchMove(object sender, TouchEventArgs e)
        {
            if (!_isDragging) return;

            // how far the finger moved since TouchDown
            Point currentPos = e.GetTouchPoint(PadCanvas).Position;
            Vector dragAmount = currentPos - _initialTouchPoint;

            // so basically ma no-normalize an position han touch point, diri na ma auto move dayon
            // ma base na ha drag amount an movement
            Vector offset = _initialTouchOffset + dragAmount;

            // clamp inside radius
            // extended based ha multiplier (2.0 rn)
            // para diri ma hard clamped an joystick ha inside outer circle
            // allowing easier control labi kun kailangan precise diri sensitive hinduro
            // especially useful ini ha right stick, kay camera movement
            double extendedRadius = _radius * _extensionMultiplier;

            if (offset.Length > extendedRadius)
            {
                offset.Normalize();
                offset *= extendedRadius;
            }

            // normalize to [-1, 1]
            _xValue = offset.X / extendedRadius;
            _yValue = offset.Y / extendedRadius;

            // send to Vigem (y reversed)
            App.Vigem.Set360RightStick(_xValue, _yValue * -1);

            // update thumb position
            Canvas.SetLeft(Thumb, _center.X + offset.X - (Thumb.Width / 2));
            Canvas.SetTop(Thumb, _center.Y + offset.Y - (Thumb.Height / 2));

            e.Handled = true;
        }

        private void Thumb_TouchUp(object sender, TouchEventArgs e)
        {
            _isDragging = false;
            Thumb.ReleaseTouchCapture(e.TouchDevice);

            // reset axes values
            _xValue = 0.0;
            _yValue = 0.0;
            App.Vigem.Set360RightStick(_xValue, _yValue);

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

//TODO: consider making this user control reusable
// as well as iba na user control na pirme gagamit na diri gud nagiiba
// like an d-pad ngan face buttons
