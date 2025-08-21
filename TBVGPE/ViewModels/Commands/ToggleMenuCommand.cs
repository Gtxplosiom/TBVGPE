using System.Windows;

namespace TBVGPE.ViewModels.Commands
{
    public class ToggleMenuCommand : CommandBase
    {
        private readonly MenuBarViewModel _menuBarViewModel;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ToggleMenuCommand(MenuBarViewModel menuBarViewModel, MainWindowViewModel mainWindowViewModel)
        {
            _menuBarViewModel = menuBarViewModel;
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            var menuBar = _menuBarViewModel;
            if (menuBar == null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (menuBar.VisibilityState == Visibility.Visible)
                {
                    menuBar.VisibilityState = Visibility.Collapsed;
                    _mainWindowViewModel.UtilityMenuVisibility = Visibility.Collapsed;
                    _mainWindowViewModel.ToggleMenuText = "Show";
                }
                else
                {
                    menuBar.VisibilityState = Visibility.Visible;
                    _mainWindowViewModel.UtilityMenuVisibility = Visibility.Visible;
                    _mainWindowViewModel.ToggleMenuText = "Hide Menu";
                }
            });
        }
    }
}

// Consider an idea na kun naka hide aadto anay ha system tray, ngan kailangan la ig click an icon didto para mag appear utro an menu
// pero an problem what if kun may controller nga active, ngan an menu la an na ha hide hassle kun an menu kailangan na mag appear 
// magkikinadto pa taskbar
// or magconsider pa mga idea basta diri la makaksalipod an toggle button, bawal ada an nagbabalhin balhin ngan na transparent kay di macliclick hahhahaha
