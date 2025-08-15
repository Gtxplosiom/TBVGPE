using System.Collections.ObjectModel;
using System.Windows;
using TBVGPE.Models;
using TBVGPE.ViewModels;
using TBVGPE.Views;

namespace TBVGPE
{
    public partial class App : Application
    {
        // static property to para ig-hold an vigemservice class instance
        // para kun ma hold na, ma tatawag ini hiya via App.Vigem.... chuchu
        public static VigemService Vigem { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // xinput emulator instance
            // TODO: consider switching between input types like dualshock, or directinput
            // if xinput do something about the double inputs, kun magkaada problema which i'm positive magkakaada
            Vigem = new VigemService();

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
    }

}
