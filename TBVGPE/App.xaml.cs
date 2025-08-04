using System.Collections.ObjectModel;
using System.Windows;
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
                new VirtualGamePads { Id = 1, Name = "3DS" }
            };

            // View Model Instances
            var menuBarViewModel = new MenuBarViewModel(virtualGamePadsCollection);
            var mainWindowViewModel = new MainWindowViewModel(menuBarViewModel);

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
