using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using TBVGPE.Models;

namespace TBVGPE.Views.Controller.Presets
{
    public partial class GBAControllerLayout : UserControl, INotifyPropertyChanged
    {
        // properties values
        private double _layoutCanvasWidth;
        private double _shoulderButtonsX;
        private double _shoulderButtonsY;
        private double _directionalButtonsX;
        private double _directionalButtonsY;
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
        public GBAControllerLayout()
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

            if ("gba_config" != null)
            {
                // load position from config file and assign them in the properties
                // return;
            }

            // default positionings
            LayoutCanvasWidth = screenDimentions.Width;

            ShoulderButtonsX = 0;
            ShoulderButtonsY = 0;
            DirectionalButtonsX = 70;
            DirectionalButtonsY = 100;
            FaceButtonsX = LayoutCanvas.Width - GBAFaceButtonsControl.Width - 80;
            FaceButtonsY = 150;
            UtilityButtonsX = 125;
            UtilityButtonsY = 320;
        }
    }
}
