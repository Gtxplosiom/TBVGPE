using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;

namespace TBVGPE.Views.Presets.Switch
{
    public partial class LeftStick : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        private bool _isLeftKeyPressed;
        private bool _isRightKeyPressed;
        private bool _isUpKeyPressed;
        private bool _isDownKeyPressed;

        private bool _isDragging;
        private Point _center;
        private double _radius;

        // Events are still here, but we will handle the key press logic locally.
        public event EventHandler? MoveLeft;
        public event EventHandler? MoveRight;
        public event EventHandler? MoveUp;
        public event EventHandler? MoveDown;

        public LeftStick()
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

            double centralDeadZone = _radius * 0.20;

            // an imaginary cone ine para diri sensitive hinduro an joystick ha diagonal directions
            // bisan guti la na deviation. 
            double cardinalConeTolerance = _radius * 3.5;

            // These booleans track the desired movement state for this frame.
            bool moveLeft = false;
            bool moveRight = false;
            bool moveUp = false;
            bool moveDown = false;

            if (offset.Length > centralDeadZone)
            {
                bool isPrimarilyHorizontal = Math.Abs(offset.X) > Math.Abs(offset.Y);

                if (isPrimarilyHorizontal && Math.Abs(offset.Y) < cardinalConeTolerance)
                {
                    if (offset.X < 0) moveLeft = true;
                    else moveRight = true;
                }
                else if (!isPrimarilyHorizontal && Math.Abs(offset.X) < cardinalConeTolerance)
                {
                    if (offset.Y < 0) moveUp = true;
                    else moveDown = true;
                }
                else
                {
                    if (offset.X < 0) moveLeft = true;
                    else if (offset.X > 0) moveRight = true;

                    if (offset.Y < 0) moveUp = true;
                    else if (offset.Y > 0) moveDown = true;
                }
            }

            if (moveLeft && !_isLeftKeyPressed)
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_A);
                _isLeftKeyPressed = true;
                OnMoveLeft();
            }
            else if (!moveLeft && _isLeftKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_A);
                _isLeftKeyPressed = false;
            }

            // Right Key
            if (moveRight && !_isRightKeyPressed)
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_D);
                _isRightKeyPressed = true;
                OnMoveRight();
            }
            else if (!moveRight && _isRightKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_D);
                _isRightKeyPressed = false;
            }

            if (moveUp && !_isUpKeyPressed)
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_W);
                _isUpKeyPressed = true;
                OnMoveUp();
            }
            else if (!moveUp && _isUpKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                _isUpKeyPressed = false;
            }

            if (moveDown && !_isDownKeyPressed)
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_S);
                _isDownKeyPressed = true;
                OnMoveDown();
            }
            else if (!moveDown && _isDownKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
                _isDownKeyPressed = false;
            }

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
            ResetThumbPosition();

            if (_isLeftKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_A);
                _isLeftKeyPressed = false;
            }
            if (_isRightKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_D);
                _isRightKeyPressed = false;
            }
            if (_isUpKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                _isUpKeyPressed = false;
            }
            if (_isDownKeyPressed)
            {
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
                _isDownKeyPressed = false;
            }

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

        public void OnMoveLeft()
        {
            MoveLeft?.Invoke(this, EventArgs.Empty);
        }

        public void OnMoveRight()
        {
            MoveRight?.Invoke(this, EventArgs.Empty);
        }

        public void OnMoveUp()
        {
            MoveUp?.Invoke(this, EventArgs.Empty);
        }

        public void OnMoveDown()
        {
            MoveDown?.Invoke(this, EventArgs.Empty);
        }
    }
}
