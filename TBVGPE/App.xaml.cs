using System.Windows;
using TBVGPE.ViewModels;
using TBVGPE.Views;

namespace TBVGPE
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // View Model Instances
            var menuBarViewModel = new MenuBarViewModel();
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
