using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using InputSimulatorStandard;
using InputSimulatorStandard.Native;
using System.Windows.Threading; // Required for Dispatcher.Invoke

namespace TBVGPE.Views.Presets._3DS
{
    public partial class FaceButtons : UserControl
    {
        private readonly IInputSimulator _inputSimulator = new InputSimulator();

        // Default and pressed colors for the Ellipses
        private readonly SolidColorBrush _defaultButtonFill = new SolidColorBrush(Color.FromArgb(0x80, 0x33, 0x33, 0x33)); // #333 with 0.5 opacity
        private readonly SolidColorBrush _pressedButtonFill = new SolidColorBrush(Colors.DarkGray); // A darker gray for pressed state

        // Para masabtan kun ano an hihimo hin mouse
        // need ini kay diri man button an control na gin i-interact
        // mga ellipses man
        private GlobalMouseHook? _mouseHook;
        // Screen bounds for each Ellipse button
        private Rect _xBtnBounds;
        private Rect _yBtnBounds;
        private Rect _bBtnBounds;
        private Rect _aBtnBounds;

        // Flag to prevent multiple key presses if mouse is dragged quickly
        private bool _isAnyBtnPressed = false;

        public FaceButtons()
        {
            InitializeComponent();

            this.Loaded += FaceButtons_Loaded;
            this.Unloaded += FaceButtons_Unloaded;
        }

        private void FaceButtons_Loaded(object sender, RoutedEventArgs e)
        {
            _mouseHook = new GlobalMouseHook();
            _mouseHook.MouseDown += GlobalMouseDown;
            _mouseHook.MouseUp += GlobalMouseUp;
            _mouseHook.SetHook();

            Dispatcher.Invoke(() =>
            {
                _xBtnBounds = new Rect(XBtn.PointToScreen(new Point(0, 0)), XBtn.RenderSize);
                _yBtnBounds = new Rect(YBtn.PointToScreen(new Point(0, 0)), YBtn.RenderSize);
                _bBtnBounds = new Rect(BBtn.PointToScreen(new Point(0, 0)), BBtn.RenderSize);
                _aBtnBounds = new Rect(ABtn.PointToScreen(new Point(0, 0)), ABtn.RenderSize);
            });

            // Also update bounds if the control's size or position changes after loading
            this.SizeChanged += (s, args) => UpdateButtonScreenBounds();
            this.LayoutUpdated += (s, args) => UpdateButtonScreenBounds();
        }

        private void FaceButtons_Unloaded(object sender, RoutedEventArgs e)
        {
            _mouseHook?.Dispose();
        }

        // Helper method para mag update button screen bounds (called on SizeChanged/LayoutUpdated)
        private void UpdateButtonScreenBounds()
        {
            Dispatcher.Invoke(() =>
            {
                if (!IsLoaded || PresentationSource.FromVisual(this) == null)
                    return;

                try
                {
                    _xBtnBounds = new Rect(XBtn.PointToScreen(new Point(0, 0)), XBtn.RenderSize);
                    _yBtnBounds = new Rect(YBtn.PointToScreen(new Point(0, 0)), YBtn.RenderSize);
                    _bBtnBounds = new Rect(BBtn.PointToScreen(new Point(0, 0)), BBtn.RenderSize);
                    _aBtnBounds = new Rect(ABtn.PointToScreen(new Point(0, 0)), ABtn.RenderSize);
                }
                catch (InvalidOperationException)
                {
                    // para guard pag unload ha content control diri mag error an visual tree chuchu
                }
            });
        }

        private void GlobalMouseDown(object? sender, GlobalMouseHook.MouseHookEventArgs e)
        {
            // Only allow one button to be "pressed" at a time to prevent conflicts
            if (_isAnyBtnPressed) return;

            // Check which button was clicked based on screen coordinates
            if (_xBtnBounds.Contains(e.X, e.Y))
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_I);
                _isAnyBtnPressed = true;
                Dispatcher.Invoke(() => XBtn.Fill = _pressedButtonFill);
            }
            else if (_yBtnBounds.Contains(e.X, e.Y))
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_J);
                _isAnyBtnPressed = true;
                Dispatcher.Invoke(() => YBtn.Fill = _pressedButtonFill);
            }
            else if (_bBtnBounds.Contains(e.X, e.Y))
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_K);
                _isAnyBtnPressed = true;
                Dispatcher.Invoke(() => BBtn.Fill = _pressedButtonFill);
            }
            else if (_aBtnBounds.Contains(e.X, e.Y))
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_L);
                _isAnyBtnPressed = true;
                Dispatcher.Invoke(() => ABtn.Fill = _pressedButtonFill);
            }
        }

        private void GlobalMouseUp(object? sender, GlobalMouseHook.MouseHookEventArgs e)
        {
            if (!_isAnyBtnPressed) return; // Only process if a button was previously pressed

            _isAnyBtnPressed = false; // Reset the flag

            // Release all keys that might have been pressed by this control
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_I);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_J);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_K);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_L);

            // Reset all button colors on the UI thread
            Dispatcher.Invoke(() =>
            {
                XBtn.Fill = _defaultButtonFill;
                YBtn.Fill = _defaultButtonFill;
                BBtn.Fill = _defaultButtonFill;
                ABtn.Fill = _defaultButtonFill;
            });
            Debug.WriteLine("FaceButtons: All Keys Up (via hook)");
        }
    }
}
