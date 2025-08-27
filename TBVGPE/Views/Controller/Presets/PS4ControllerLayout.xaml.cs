using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using TBVGPE.ViewModels.Controllers;
using TBVGPE.Views.Controller;
using TBVGPE.Views.Controller.Components.ShoulderButtons;
using TBVGPE.Views.Controller.Components.UtilityButtons;
using TBVGPE.Views.Controller.Presets;

namespace TBVGPE.Views.Presets
{
    public partial class PS4ControllerLayout : UserControl, INotifyPropertyChanged
    {
        // properties values
        private int _layoutCanvasWidth;
        private int _playStationShoulderButtonsLeft;
        private int _playStationShoulderButtonsTop;
        private int _standardAnalogStickButtonsLeft;
        private int _standardAnalogStickButtonsTop;
        private int _standardDirectionalButtonsLeft;
        private int _leftAnalogStickLeft;
        private int _standardDirectionalButtonsTop;
        private int _leftAnalogStickTop;
        private int _playStationFaceButtonsLeft;
        private int _ps4UtilityButtonsLeft;
        private int _rightAnalogStickLeft;
        private int _playStationFaceButtonsTop;
        private int _ps4UtilityButtonsTop;
        private int _rightAnalogStickTop;

        // properties
        public int LayoutCanvasWidth
        {
            get => _layoutCanvasWidth;
            set
            {
                if (_layoutCanvasWidth != value)
                {
                    _layoutCanvasWidth = value;
                    OnPropertyChanged(nameof(LayoutCanvasWidth));
                }
            }
        }

        public int PlayStationShoulderButtonsLeft
        {
            get => _playStationShoulderButtonsLeft;
            set
            {
                if (_playStationShoulderButtonsLeft != value)
                {
                    _playStationShoulderButtonsLeft = value;
                    OnPropertyChanged(nameof(PlayStationShoulderButtonsLeft));
                }
            }
        }

        public int PlayStationShoulderButtonsTop
        {
            get => _playStationShoulderButtonsTop;
            set
            {
                if (_playStationShoulderButtonsTop != value)
                {
                    _playStationShoulderButtonsTop = value;
                    OnPropertyChanged(nameof(PlayStationShoulderButtonsTop));
                }
            }
        }

        public int StandardAnalogStickButtonsLeft
        {
            get => _standardAnalogStickButtonsLeft;
            set
            {
                if (_standardAnalogStickButtonsLeft != value)
                {
                    _standardAnalogStickButtonsLeft = value;
                    OnPropertyChanged(nameof(StandardAnalogStickButtonsLeft));
                }
            }
        }

        public int StandardAnalogStickButtonsTop
        {
            get => _standardAnalogStickButtonsTop;
            set
            {
                if (_standardAnalogStickButtonsTop != value)
                {
                    _standardAnalogStickButtonsTop = value;
                    OnPropertyChanged(nameof(StandardAnalogStickButtonsTop));
                }
            }
        }

        public int StandardDirectionalButtonsLeft
        {
            get => _standardDirectionalButtonsLeft;
            set
            {
                if (_standardDirectionalButtonsLeft != value)
                {
                    _standardDirectionalButtonsLeft = value;
                    OnPropertyChanged(nameof(StandardDirectionalButtonsLeft));
                }
            }
        }

        public int StandardDirectionalButtonsTop
        {
            get => _standardDirectionalButtonsTop;
            set
            {
                if (_standardDirectionalButtonsTop != value)
                {
                    _standardDirectionalButtonsTop = value;
                    OnPropertyChanged(nameof(StandardDirectionalButtonsTop));
                }
            }
        }

        public int LeftAnalogStickLeft
        {
            get => _leftAnalogStickLeft;
            set
            {
                if (_leftAnalogStickLeft != value)
                {
                    _leftAnalogStickLeft = value;
                    OnPropertyChanged(nameof(LeftAnalogStickLeft));
                }
            }
        }

        public int LeftAnalogStickTop
        {
            get => _leftAnalogStickTop;
            set
            {
                if (_leftAnalogStickTop != value)
                {
                    _leftAnalogStickTop = value;
                    OnPropertyChanged(nameof(LeftAnalogStickTop));
                }
            }
        }

        public int PlayStationFaceButtonsLeft
        {
            get => _playStationFaceButtonsLeft;
            set
            {
                if (_playStationFaceButtonsLeft != value)
                {
                    _playStationFaceButtonsLeft = value;
                    OnPropertyChanged(nameof(PlayStationFaceButtonsLeft));
                }
            }
        }

        public int PS4UtilityButtonsLeft
        {
            get => _ps4UtilityButtonsLeft;
            set
            {
                if (_ps4UtilityButtonsLeft != value)
                {
                    _ps4UtilityButtonsLeft = value;
                    OnPropertyChanged(nameof(PS4UtilityButtonsLeft));
                }
            }
        }

        public int RightAnalogStickLeft
        {
            get => _rightAnalogStickLeft;
            set
            {
                if (_rightAnalogStickLeft != value)
                {
                    _rightAnalogStickLeft = value;
                    OnPropertyChanged(nameof(RightAnalogStickLeft));
                }
            }
        }

        public int PlayStationFaceButtonsTop
        {
            get => _playStationFaceButtonsTop;
            set
            {
                if (_playStationFaceButtonsTop != value)
                {
                    _playStationFaceButtonsTop = value;
                    OnPropertyChanged(nameof(PlayStationFaceButtonsTop));
                }
            }
        }

        public int PS4UtilityButtonsTop
        {
            get => _ps4UtilityButtonsTop;
            set
            {
                if (_ps4UtilityButtonsTop != value)
                {
                    _ps4UtilityButtonsTop = value;
                    OnPropertyChanged(nameof(PS4UtilityButtonsTop));
                }
            }
        }

        public int RightAnalogStickTop
        {
            get => _rightAnalogStickTop;
            set
            {
                if (_rightAnalogStickTop != value)
                {
                    _rightAnalogStickTop = value;
                    OnPropertyChanged(nameof(RightAnalogStickTop));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // constuctor
        public PS4ControllerLayout()
        {
            InitializeComponent();

            DataContext = this;

            Loaded += PositionControllerElements;
        }

        // methods
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveLayout()
        {
            var elements = new List<object>();

            foreach (var child in LayoutCanvas.Children.OfType<ControllerWrapperViewModel>())
            {
                elements.Add(new
                {
                    Column = 1,
                    X = Canvas.GetLeft(child),
                    Y = Canvas.GetTop(child),
                    Width = child.ActualWidth,
                    Height = child.ActualHeight,
                    Type = child.Content.GetType().Name
                });
            }

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory,"Assets\\Config\\Layouts\\ps4layout.json"), JsonSerializer.Serialize(elements));
        }

        private void PositionControllerElements(object sender, EventArgs e)
        {
            if ("ps4_config" != null)
            {
                // load position from config file and assign them in the properties
                // return;
            }

            (int Width, int Height) screenDimentions = (
                (int)System.Windows.SystemParameters.PrimaryScreenWidth,
                (int)System.Windows.SystemParameters.PrimaryScreenHeight
                );

            // default positionings
            LayoutCanvasWidth = screenDimentions.Width;

            PlayStationShoulderButtonsLeft = 0;
            StandardAnalogStickButtonsLeft = 0;
            StandardDirectionalButtonsLeft = 20;
            LeftAnalogStickLeft = 170;
            PlayStationFaceButtonsLeft = (int)LayoutCanvas.Width - (int)PlayStationFaceButtonsControl.Width - 20;
            PS4UtilityButtonsLeft = (int)LayoutCanvas.Width - (int)PS4UtilityButtonsControl.Width - 20;
            RightAnalogStickLeft = (int)LayoutCanvas.Width - (int)RightAnalogStickControl.Width - 250;

            PlayStationShoulderButtonsTop = 0;
            StandardAnalogStickButtonsTop = (int)PlayStationShoulderButtonsControl.Height;
            StandardDirectionalButtonsTop = 220;
            LeftAnalogStickTop = 400;
            PlayStationFaceButtonsTop = 220;
            PS4UtilityButtonsTop = 450;
            RightAnalogStickTop = 400;
        }
    }
}

// TODO: migrate to a viewmodel
