using System.Collections.ObjectModel;
using System.Windows;
using TBVGPE.Views.Presets._3DS;
using TBVGPE.Models;
using TBVGPE.ViewModels;
using TBVGPE.Views;

namespace TBVGPE
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // gamepads list
            var virtualGamePadsCollection = new ObservableCollection<VirtualGamePads>
            {
                new VirtualGamePads { Id = -1, Name = "Select a GamePad..." },
                new VirtualGamePads { Id = 1, Name = "3DS" },
                new VirtualGamePads { Id = 2, Name = "Switch" }
            };

            // list of Controllers View Model Instances
            var controllerViewModels = new Dictionary<int, ViewModelBase>
            {
                [1] = new ViewModels.Controllers._3DS._3DSControllerViewModel(),
                [2] = new ViewModels.Controllers.Switch.SwitchControllerViewModel()
                // Add future controller ViewModels here
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
