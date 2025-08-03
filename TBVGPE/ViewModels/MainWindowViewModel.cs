using TBVGPE.ViewModels;
using System.Windows;
using System.Windows.Input;
using TBVGPE.ViewModels.Commands;
using System.Windows.Media.Animation;

namespace TBVGPE.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MenuBarViewModel? _menuBarViewModel;
        private string _toggleButtontext = "Hide";

        // implement this
        public ICommand ToggleButtonCommand { get; set; }

        public MainWindowViewModel(MenuBarViewModel menuBarViewModel)
        {
            _menuBarViewModel = menuBarViewModel;

            ToggleButtonCommand = new ToggleMenuCommand(_menuBarViewModel, this);

            ShowMenuBar();
        }

        // Properties
        public string ToggleButtonText
        {
            get => _toggleButtontext;
            set
            {
                if (_toggleButtontext != value)
                {
                    _toggleButtontext = value;
                    OnPropertyChanged(nameof(ToggleButtonText));
                }
            }
        }

        private void ShowMenuBar()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _menuBarViewModel.VisibilityState = Visibility.Visible;
            });
        }
    }
}

// TODO: make toggle button
