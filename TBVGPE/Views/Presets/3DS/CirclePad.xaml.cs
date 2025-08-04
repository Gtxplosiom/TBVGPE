using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TBVGPE.Views.Presets._3DS
{
    public partial class CirclePad : UserControl
    {
        private bool _isDragging;
        private Point _center;
        private double _radius;

        public event EventHandler? MoveLeft;
        public event EventHandler? MoveRight;
        public event EventHandler? MoveUp;
        public event EventHandler? MoveDown;

        private bool _isMovingLeft;
        private bool _isMovingRight;
        private bool _isMovingUp;
        private bool _isMovingDown;

        public CirclePad()
        {
            InitializeComponent();
        }

        private void PadCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // to center the inner sirkol
            _center = new Point(e.NewSize.Width / 2, e.NewSize.Height / 2);

            // The radius is the radius of the outer area minus the radius of the thumb itself.
            _radius = (Math.Min(e.NewSize.Width, e.NewSize.Height) / 2) - (Thumb.Width / 2);

            // Default posisyon
            ResetThumbPosition();
        }

        private void Thumb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            // Capture the mouse to ensure we receive MouseMove events even if the
            // cursor leaves the bounds of the thumb while dragging.
            Thumb.CaptureMouse();
        }

        private void Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            Point currentPos = e.GetPosition(PadCanvas);

            // Get the vector from the center to the current mouse position.
            Vector offset = currentPos - _center;

            // deadzone to prevent accidental presses
            double deadZoneRadius = _radius * 0.25;

            if (offset.X < -deadZoneRadius) // Left
            {
                if (!_isMovingLeft)
                {
                    OnMoveLeft();
                    _isMovingLeft = true;
                }
            }
            else if (offset.X > deadZoneRadius) // Right
            {
                if (!_isMovingRight)
                {
                    OnMoveRight();
                    _isMovingRight = true;
                }
            }
            else
            {
                // If we are back in the center horizontally, release the keys.
                _isMovingLeft = false;
                _isMovingRight = false;
            }

            if (offset.Y < -deadZoneRadius) // Up
            {
                if (!_isMovingUp)
                {
                    OnMoveUp();
                    _isMovingUp = true;
                }
            }
            else if (offset.Y > deadZoneRadius) // Down
            {
                if (!_isMovingDown)
                {
                    OnMoveDown();
                    _isMovingDown = true;
                }
            }
            else
            {
                // If we are back in the center vertically, release the keys.
                _isMovingUp = false;
                _isMovingDown = false;
            }

            // Clamp the inner circle
            if (offset.Length > _radius)
            {
                offset.Normalize();
                offset *= _radius;
            }

            // Update the thumb's position based on the (potentially clamped) offset.
            Canvas.SetLeft(Thumb, _center.X + offset.X - (Thumb.Width / 2));
            Canvas.SetTop(Thumb, _center.Y + offset.Y - (Thumb.Height / 2));
        }

        private void Thumb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            Thumb.ReleaseMouseCapture();

            // Snap the thumb back to the center.
            ResetThumbPosition();
        }

        // Snap back to the center when not touched
        private void ResetThumbPosition()
        {
            // Ensure _center has been initialized before trying to use it.
            if (_center != default)
            {
                Canvas.SetLeft(Thumb, _center.X - (Thumb.Width / 2));
                Canvas.SetTop(Thumb, _center.Y - (Thumb.Height / 2));
            }
        }

        protected virtual void OnMoveLeft() => MoveLeft?.Invoke(this, EventArgs.Empty);
        protected virtual void OnMoveRight() => MoveRight?.Invoke(this, EventArgs.Empty);
        protected virtual void OnMoveUp() => MoveUp?.Invoke(this, EventArgs.Empty);
        protected virtual void OnMoveDown() => MoveDown?.Invoke(this, EventArgs.Empty);
    }
}
