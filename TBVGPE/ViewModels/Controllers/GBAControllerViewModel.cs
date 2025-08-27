using System.IO;
using System.Text.Json;
using System.Windows.Media.Media3D;
using TBVGPE.Models;

namespace TBVGPE.ViewModels.Controllers
{
    public class GBAControllerViewModel : ViewModelBase, IControllerLayoutManagerInterface
    {
        // Child elements
        public LayoutElementViewModel ShoulderButtons { get; set; } = new();
        public LayoutElementViewModel DirectionalButtons { get; set; } = new();
        public LayoutElementViewModel FaceButtons { get; set; } = new();
        public LayoutElementViewModel UtilityButtons { get; set; } = new();

        // Sizes (keep as they are now)
        public int LayoutCanvasWidth { get; set; }
        public int ShoulderButtonsWidth { get; set; }
        public int ShoulderButtonsHeight { get; set; }
        public int DirectionalButtonsWidth { get; set; }
        public int DirectionalButtonsHeight { get; set; }
        public int FaceButtonsWidth { get; set; }
        public int FaceButtonsHeight { get; set; }
        public int UtilityButtonsWidth { get; set; }
        public int UtilityButtonsHeight { get; set; }

        public GBAControllerViewModel()
        {
            InitializeElementSizes();

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appData, "TBVGPE", "Layouts");
            Directory.CreateDirectory(appFolder);

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            string configPath = Path.Combine(appFolder, "gba_config.json");

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
            DirectionalButtons.X = controllerLayout.DirectionalButtonsX;
            DirectionalButtons.Y = controllerLayout.DirectionalButtonsY;
            FaceButtons.X = controllerLayout.FaceButtonsX;
            FaceButtons.Y = controllerLayout.FaceButtonsY;
            UtilityButtons.X = controllerLayout.UtilityButtonsX;
            UtilityButtons.Y = controllerLayout.UtilityButtonsY;
        }

        public void SaveLayout(string path)
        {
            var controllerLayout = new ControllerLayoutModel
            {
                ShoulderButtonsX = ShoulderButtons.X,
                ShoulderButtonsY = ShoulderButtons.Y,
                DirectionalButtonsX = DirectionalButtons.X,
                DirectionalButtonsY = DirectionalButtons.Y,
                FaceButtonsX = FaceButtons.X,
                FaceButtonsY = FaceButtons.Y,
                UtilityButtonsX = UtilityButtons.X,
                UtilityButtonsY = UtilityButtons.Y
            };

            File.WriteAllText(path, JsonSerializer.Serialize(controllerLayout, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void LoadDefault()
        {
            ShoulderButtons.X = 0;
            ShoulderButtons.Y = 0;
            DirectionalButtons.X = 70;
            DirectionalButtons.Y = 100;
            FaceButtons.X = LayoutCanvasWidth - FaceButtonsWidth - 80;
            FaceButtons.Y = 150;
            UtilityButtons.X = 125;
            UtilityButtons.Y = 320;
        }

        private void InitializeElementSizes()
        {
            LayoutCanvasWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            ShoulderButtonsWidth = LayoutCanvasWidth;
            ShoulderButtonsHeight = 70;
            DirectionalButtonsWidth = 180;
            DirectionalButtonsHeight = 180;
            UtilityButtonsWidth = 70;
            UtilityButtonsHeight = 130;
            FaceButtonsWidth = 135;
            FaceButtonsHeight = 135;
        }
    }
}
