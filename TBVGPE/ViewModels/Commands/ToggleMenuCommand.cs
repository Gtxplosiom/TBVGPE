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
                    _mainWindowViewModel.CloseAppToggleControllerSliderBtnVisibility = Visibility.Collapsed;
                    _mainWindowViewModel.ToggleMenuText = "Show Menu";
                }
                else
                {
                    menuBar.VisibilityState = Visibility.Visible;
                    _mainWindowViewModel.CloseAppToggleControllerSliderBtnVisibility = Visibility.Visible;
                    _mainWindowViewModel.ToggleMenuText = "Hide Menu";
                }
            });
        }
    }
}
