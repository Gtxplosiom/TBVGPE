using System.ComponentModel;
using System.Text.Json;
using System.Windows.Controls;
using TBVGPE.Models;
using System.IO;

namespace TBVGPE.Views.Controller.Presets
{
    public partial class _3DSControllerLayout : UserControl, INotifyPropertyChanged
    {
        // properties values
        private double _layoutCanvasWidth;
        private double _shoulderButtonsX;
        private double _shoulderButtonsY;
        private double _directionalButtonsX;
        private double _directionalButtonsY;
        private double _leftAnalogStickX;
        private double _leftAnalogStickY;
        private double _faceButtonsX;
        private double _faceButtonsY;
        private double _utilityButtonsX;
        private double _utilityButtonsY;

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

        public event PropertyChangedEventHandler? PropertyChanged;

        // constuctor
        public _3DSControllerLayout()
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
                DirectionalButtonsX = DirectionalButtonsX,
                DirectionalButtonsY = DirectionalButtonsY,
                LeftAnalogStickX = LeftAnalogStickX,
                LeftAnalogStickY = LeftAnalogStickY,
                FaceButtonsX = FaceButtonsX,
                FaceButtonsY = FaceButtonsY,
                UtilityButtonsX = UtilityButtonsX,
                UtilityButtonsY = UtilityButtonsY,
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

            if ("3ds_config" != null)
            {
                // load position from config file and assign them in the properties
                // return;
            }

            // default positionings
            LayoutCanvasWidth = screenDimentions.Width;

            ShoulderButtonsX = 0;
            ShoulderButtonsY = 0;
            DirectionalButtonsX = 170;
            DirectionalButtonsY = 340;
            LeftAnalogStickX = 70;
            LeftAnalogStickY = 130;
            FaceButtonsX = LayoutCanvas.Width - NintendoNewFaceButtonsControl.Width - 20;
            FaceButtonsY = 220;
            UtilityButtonsX = LayoutCanvas.Width - _3DSUtilityButtonsControl.Width - 20;
            UtilityButtonsY = 470;
        }
    }
}

// TODO: Make support for both mouse and touch
