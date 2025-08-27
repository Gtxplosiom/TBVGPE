using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using TBVGPE.Models;

namespace TBVGPE.Views.Controller.Presets
{
    public partial class SwitchControllerLayout : UserControl, INotifyPropertyChanged
    {
        // properties values
        private double _layoutCanvasWidth;
        private double _shoulderButtonsX;
        private double _shoulderButtonsY;
        private double _analogStickButtonsX;
        private double _analogStickButtonsY;
        private double _directionalButtonsX;
        private double _directionalButtonsY;
        private double _leftAnalogStickX;
        private double _leftAnalogStickY;
        private double _faceButtonsX;
        private double _faceButtonsY;
        private double _utilityButtonsX;
        private double _utilityButtonsY;
        private double _rightAnalogStickX;
        private double _rightAnalogStickY;

        // properties
        public double LayoutCanvasWidth
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

        public double ShoulderButtonsX
        {
            get => _shoulderButtonsX;
            set
            {
                if (_shoulderButtonsX != value)
                {
                    _shoulderButtonsX = value;
                    OnPropertyChanged(nameof(ShoulderButtonsX));
                }
            }
        }

        public double ShoulderButtonsY
        {
            get => _shoulderButtonsY;
            set
            {
                if (_shoulderButtonsY != value)
                {
                    _shoulderButtonsY = value;
                    OnPropertyChanged(nameof(ShoulderButtonsY));
                }
            }
        }

        public double AnalogStickButtonsX
        {
            get => _analogStickButtonsX;
            set
            {
                if (_analogStickButtonsX != value)
                {
                    _analogStickButtonsX = value;
                    OnPropertyChanged(nameof(AnalogStickButtonsX));
                }
            }
        }

        public double AnalogStickButtonsY
        {
            get => _analogStickButtonsY;
            set
            {
                if (_analogStickButtonsY != value)
                {
                    _analogStickButtonsY = value;
                    OnPropertyChanged(nameof(AnalogStickButtonsY));
                }
            }
        }

        public double DirectionalButtonsX
        {
            get => _directionalButtonsX;
            set
            {
                if (_directionalButtonsX != value)
                {
                    _directionalButtonsX = value;
                    OnPropertyChanged(nameof(DirectionalButtonsX));
                }
            }
        }

        public double DirectionalButtonsY
        {
            get => _directionalButtonsY;
            set
            {
                if (_directionalButtonsY != value)
                {
                    _directionalButtonsY = value;
                    OnPropertyChanged(nameof(DirectionalButtonsY));
                }
            }
        }

        public double LeftAnalogStickX
        {
            get => _leftAnalogStickX;
            set
            {
                if (_leftAnalogStickX != value)
                {
                    _leftAnalogStickX = value;
                    OnPropertyChanged(nameof(LeftAnalogStickX));
                }
            }
        }

        public double LeftAnalogStickY
        {
            get => _leftAnalogStickY;
            set
            {
                if (_leftAnalogStickY != value)
                {
                    _leftAnalogStickY = value;
                    OnPropertyChanged(nameof(LeftAnalogStickY));
                }
            }
        }

        public double FaceButtonsX
        {
            get => _faceButtonsX;
            set
            {
                if (_faceButtonsX != value)
                {
                    _faceButtonsX = value;
                    OnPropertyChanged(nameof(FaceButtonsX));
                }
            }
        }

        public double FaceButtonsY
        {
            get => _faceButtonsY;
            set
            {
                if (_faceButtonsY != value)
                {
                    _faceButtonsY = value;
                    OnPropertyChanged(nameof(FaceButtonsY));
                }
            }
        }

        public double UtilityButtonsX
        {
            get => _utilityButtonsX;
            set
            {
                if (_utilityButtonsX != value)
                {
                    _utilityButtonsX = value;
                    OnPropertyChanged(nameof(UtilityButtonsX));
                }
            }
        }

        public double UtilityButtonsY
        {
            get => _utilityButtonsY;
            set
            {
                if (_utilityButtonsY != value)
                {
                    _utilityButtonsY = value;
                    OnPropertyChanged(nameof(UtilityButtonsY));
                }
            }
        }

        public double RightAnalogStickX
        {
            get => _rightAnalogStickX;
            set
            {
                if (_rightAnalogStickX != value)
                {
                    _rightAnalogStickX = value;
                    OnPropertyChanged(nameof(RightAnalogStickX));
                }
            }
        }

        public double RightAnalogStickY
        {
            get => _rightAnalogStickY;
            set
            {
                if (_rightAnalogStickY != value)
                {
                    _rightAnalogStickY = value;
                    OnPropertyChanged(nameof(RightAnalogStickY));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // constuctor
        public SwitchControllerLayout()
        {
            InitializeComponent();

            DataContext = this;

            Loaded += InitializeControllerLayout;
        }

        // methods
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveLayout(string path)
        {
            var controllerLayout = new ControllerLayoutModel
            {
                ShoulderButtonsX = ShoulderButtonsX,
                ShoulderButtonsY = ShoulderButtonsY,
                AnalogStickButtonsX = AnalogStickButtonsX,
                AnalogStickButtonsY = AnalogStickButtonsY,
                DirectionalButtonsX = DirectionalButtonsX,
                DirectionalButtonsY = DirectionalButtonsY,
                LeftAnalogStickX = LeftAnalogStickX,
                LeftAnalogStickY = LeftAnalogStickY,
                FaceButtonsX = FaceButtonsX,
                FaceButtonsY = FaceButtonsY,
                UtilityButtonsX = UtilityButtonsX,
                UtilityButtonsY = UtilityButtonsY,
                RightAnalogStickX = RightAnalogStickX,
                RightAnalogStickY = RightAnalogStickY
            };

            File.WriteAllText(path, JsonSerializer.Serialize(controllerLayout, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void InitializeControllerLayout(object sender, EventArgs e)
        {
            (double Width, double Height) screenDimentions = (
                System.Windows.SystemParameters.PrimaryScreenWidth,
                System.Windows.SystemParameters.PrimaryScreenHeight
                );

            // default positionings
            LayoutCanvasWidth = screenDimentions.Width;

            if ("switch_config" != null)
            {
                // load position from config file and assign them in the properties
                // return;
            }

            // default positionings
            LayoutCanvasWidth = screenDimentions.Width;

            ShoulderButtonsX = 0;
            ShoulderButtonsY = 0;
            AnalogStickButtonsX = 0;
            AnalogStickButtonsY = NintendoShoulderButtonsControl.Height;
            DirectionalButtonsX = 170;
            DirectionalButtonsY = 400;
            LeftAnalogStickX = 70;
            LeftAnalogStickY = 200;
            FaceButtonsX = LayoutCanvas.Width - NintendoNewFaceButtonsControl.Width - 20;
            FaceButtonsY = 220;
            UtilityButtonsX = LayoutCanvas.Width - SwitchUtilityButtonsControl.Width - 20;
            UtilityButtonsY = 470;
            RightAnalogStickX = LayoutCanvas.Width - RightAnalogStickControl.Width - 250;
            RightAnalogStickY = 400;
        }
    }
}
