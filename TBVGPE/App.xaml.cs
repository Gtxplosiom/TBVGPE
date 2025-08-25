using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using TBVGPE.Models;
using TBVGPE.Services;
using TBVGPE.ViewModels;
using TBVGPE.Views;

namespace TBVGPE
{
    public partial class App : Application
    {
        // para pag prevent multiple instances of the app to be opened
        private static Mutex? _mutex;
        private const string AppGuid = "TBVGPE";    // pwede pa balyuon kung gusto unique gud an identifier, pero yana oks na gad ada ini

        // static property to para ig-hold an vigemservice class instance
        // para kun ma hold na, ma tatawag ini hiya via App.Vigem.... chuchu
        // aparrently kailangan maging static
        public static VigemService? Vigem { get; private set; }

        // expose ko lat ini na duha para ma easily accessed by other classes espacially the updates shits classe
        public static string? PackageJson { get; set; }
        public static string? CurrentVersion { get; set; }

        public static string? UpdateVersion { get; set; }
        public static string? UpdateLink { get; set; }

        private readonly string _jsonPath = Path.Combine(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)!,"package.json");

        protected override void OnStartup(StartupEventArgs e)
        {
            CheckDuplicateAppInstance();

            base.OnStartup(e);

            // xinput emulator instance
            // TODO: consider switching between input types like dualshock, or directinput
            // if xinput do something about the double inputs, kun magkaada problema which i'm positive magkakaada
            Vigem = new VigemService();

            // set this shits as well
            PackageJson = _jsonPath;
            CurrentVersion = GetVersion();
            UpdateVersion = "";
            UpdateLink = "";

            // gamepads list
            var virtualGamePadsCollection = new ObservableCollection<VirtualGamePads>
            {
                new VirtualGamePads { Id = -1, Name = "Select a GamePad..." },
                new VirtualGamePads { Id = 1, Name = "GBA" },
                new VirtualGamePads { Id = 2, Name = "DS" },
                new VirtualGamePads { Id = 3, Name = "Xbox360" },
                new VirtualGamePads { Id = 4, Name = "3DS" },
                new VirtualGamePads { Id = 5, Name = "PS4" },
                new VirtualGamePads { Id = 6, Name = "Switch" }
            };

            // list of Controllers View Model Instances
            var controllerViewModels = new Dictionary<int, ViewModelBase>
            {
                [1] = new ViewModels.Controllers.GBAControllerViewModel(),
                [2] = new ViewModels.Controllers.DSControllerViewModel(),
                [3] = new ViewModels.Controllers.Xbox360ControllerViewModel(),
                [4] = new ViewModels.Controllers._3DSControllerViewModel(),
                [5] = new ViewModels.Controllers.PS4ControllerViewModel(),
                [6] = new ViewModels.Controllers.SwitchControllerViewModel()
                // Add an mga future controller ViewModels dinhi
            };

            // View Model Instances
            var menuBarViewModel = new MenuBarViewModel(virtualGamePadsCollection);
            var mainWindowViewModel = new MainWindowViewModel(menuBarViewModel, controllerViewModels);   

            // View Instances
            var menuBar = new MenuBar()
            {
                DataContext = menuBarViewModel
            };

            var mainWindow = new MainWindow()
            {
                DataContext = mainWindowViewModel
            };
            mainWindow.Show();
        }

        private void CheckDuplicateAppInstance()
        {
            _mutex = new Mutex(true, "Global\\" + AppGuid, out bool newInstance);

            if (!newInstance)
            {
                MessageBox.Show("Another instance of TBVGPE is already running.", "Cannot Open TBVGPE", MessageBoxButton.OK, MessageBoxImage.Warning);
                Current.Shutdown();
                return;
            }
        }

        public string? GetVersion()
        {
            if (!File.Exists(PackageJson))
            {
                Console.WriteLine($"Error: The file at path '{PackageJson}' was not found.");
                return null;
            }

            try
            {
                string jsonString = File.ReadAllText(PackageJson);

                var packageInfo = JsonSerializer.Deserialize<PackageInfo>(jsonString);

                return packageInfo?.CurrentVersion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the JSON file: {ex.Message}");
                return null;
            }
        }

        public class PackageInfo
        {
            [JsonPropertyName("appversion")]
            public string? CurrentVersion { get; set; }
        }
    }

}

// TODO: implement config for custom user control positions and sizes
// TODO: Add completely custom layouts
