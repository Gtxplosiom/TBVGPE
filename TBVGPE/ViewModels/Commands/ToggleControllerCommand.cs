using System.Windows;

namespace TBVGPE.ViewModels.Commands
{
    class ToggleControllerCommand : CommandBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;

        public ToggleControllerCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_mainWindowViewModel.ControllerVisibility == Visibility.Visible)
                {
                    _mainWindowViewModel.ControllerVisibility = Visibility.Collapsed;
                    _mainWindowViewModel.ToggleControllerText = "Show Controller";
                }
                else
                {
                    _mainWindowViewModel.ControllerVisibility = Visibility.Visible;
                    _mainWindowViewModel.ToggleControllerText = "Hide Controller";
                }
            });
        }
    }
}
