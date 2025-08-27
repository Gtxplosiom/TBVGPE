using System.IO;
using System.Text.Json;
using TBVGPE.Models;
using TBVGPE.Views.Controller.Components.AnalogSticks;

namespace TBVGPE.ViewModels.Controllers
{
    public class Xbox360ControllerViewModel : ViewModelBase, IControllerLayoutManagerInterface
    {
        // Child elements
        public LayoutElementViewModel ShoulderButtons { get; set; } = new();
        public LayoutElementViewModel AnalogStickButtons { get; set; } = new();
        public LayoutElementViewModel LeftAnalogStick { get; set; } = new();
        public LayoutElementViewModel DirectionalButtons { get; set; } = new();
        public LayoutElementViewModel FaceButtons { get; set; } = new();
        public LayoutElementViewModel UtilityButtons { get; set; } = new();
        public LayoutElementViewModel RightAnalogStick { get; set; } = new();

        // Sizes (keep as they are now)
        public int LayoutCanvasWidth { get; set; }
        public int ShoulderButtonsWidth { get; set; }
        public int ShoulderButtonsHeight { get; set; }
        public int AnalogStickButtonsWidth { get; set; }
        public int AnalogStickButtonsHeight { get; set; }
        public int LeftAnalogStickWidth { get; set; }
        public int LeftAnalogStickHeight { get; set; }
        public int DirectionalButtonsWidth { get; set; }
        public int DirectionalButtonsHeight { get; set; }
        public int FaceButtonsWidth { get; set; }
        public int FaceButtonsHeight { get; set; }
        public int UtilityButtonsWidth { get; set; }
        public int UtilityButtonsHeight { get; set; }
        public int RightAnalogStickWidth { get; set; }
        public int RightAnalogStickHeight { get; set; }

        public Xbox360ControllerViewModel()
        {
            InitializeElementSizes();

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appData, "TBVGPE", "Layouts");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            string configPath = Path.Combine(appFolder, "xbox360_config.json");

            LoadLayout(configPath);
        }

        public void LoadLayout(string path)
        {
            if (!File.Exists(path))
            {
                LoadDefault();
                SaveLayout(path);
                return;
            }

            var json = File.ReadAllText(path);
            var controllerLayout = JsonSerializer.Deserialize<ControllerLayoutModel>(json);
            if (controllerLayout is null) return;

            ShoulderButtons.X = controllerLayout.ShoulderButtonsX;
            ShoulderButtons.Y = controllerLayout.ShoulderButtonsY;
            AnalogStickButtons.X = controllerLayout.AnalogStickButtonsX;
            AnalogStickButtons.Y = controllerLayout.AnalogStickButtonsY;
            LeftAnalogStick.X = controllerLayout.LeftAnalogStickX;
            LeftAnalogStick.Y = controllerLayout.LeftAnalogStickY;
            DirectionalButtons.X = controllerLayout.DirectionalButtonsX;
            DirectionalButtons.Y = controllerLayout.DirectionalButtonsY;
            FaceButtons.X = controllerLayout.FaceButtonsX;
            FaceButtons.Y = controllerLayout.FaceButtonsY;
            UtilityButtons.X = controllerLayout.UtilityButtonsX;
            UtilityButtons.Y = controllerLayout.UtilityButtonsY;
            RightAnalogStick.X = controllerLayout.RightAnalogStickX;
            RightAnalogStick.Y = controllerLayout.RightAnalogStickY;
        }

        public void SaveLayout(string path)
        {
            var controllerLayout = new ControllerLayoutModel
            {
                ShoulderButtonsX = ShoulderButtons.X,
                ShoulderButtonsY = ShoulderButtons.Y,
                AnalogStickButtonsX = AnalogStickButtons.X,
                AnalogStickButtonsY = AnalogStickButtons.Y,
                LeftAnalogStickX = LeftAnalogStick.X,
                LeftAnalogStickY = LeftAnalogStick.Y,
                DirectionalButtonsX = DirectionalButtons.X,
                DirectionalButtonsY = DirectionalButtons.Y,
                FaceButtonsX = FaceButtons.X,
                FaceButtonsY = FaceButtons.Y,
                UtilityButtonsX = UtilityButtons.X,
                UtilityButtonsY = UtilityButtons.Y,
                RightAnalogStickX = RightAnalogStick.X,
                RightAnalogStickY = RightAnalogStick.Y
            };

            File.WriteAllText(path, JsonSerializer.Serialize(controllerLayout, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void LoadDefault()
        {
            ShoulderButtons.X = 0;
            ShoulderButtons.Y = 0;
            AnalogStickButtons.X = 0;
            AnalogStickButtons.Y = ShoulderButtonsHeight;
            DirectionalButtons.X = 170;
            DirectionalButtons.Y = 400;
            LeftAnalogStick.X = 70;
            LeftAnalogStick.Y = 200;
            FaceButtons.X = LayoutCanvasWidth - FaceButtonsWidth - 50;
            FaceButtons.Y = 220;
            UtilityButtons.X = LayoutCanvasWidth - UtilityButtonsWidth - 20;
            UtilityButtons.Y = 470;
            RightAnalogStick.X = LayoutCanvasWidth - RightAnalogStickWidth - 250;
            RightAnalogStick.Y = 400;
        }

        private void InitializeElementSizes()
        {
            LayoutCanvasWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            ShoulderButtonsWidth = LayoutCanvasWidth;
            ShoulderButtonsHeight = 120;
            AnalogStickButtonsHeight = 70;
            LeftAnalogStickWidth = 180;
            LeftAnalogStickHeight = 180;
            DirectionalButtonsWidth = 180;
            DirectionalButtonsHeight = 180;
            FaceButtonsWidth = 200;
            FaceButtonsHeight = 200;
            UtilityButtonsWidth = 200;
            UtilityButtonsHeight = 50;
            RightAnalogStickWidth = 180;
            RightAnalogStickHeight = 180;
        }
    }
}
