using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TBVGPE.ViewModels;
using TBVGPE.ViewModels.Commands;

namespace TBVGPE.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MenuBarViewModel? _menuBarViewModel;
        private readonly Dictionary<int, ViewModelBase>? _controllerViewModels;

        // viewmodelbase as a type??
        private ViewModelBase? _currentControllerViewModel;

        private string _toggleButtontext = "Hide";

        // implement this
        public ICommand ToggleButtonCommand { get; set; }

        public MainWindowViewModel(MenuBarViewModel menuBarViewModel, Dictionary<int, ViewModelBase> controllerViewModels)
        {
            _menuBarViewModel = menuBarViewModel;
            _controllerViewModels = controllerViewModels;

            _menuBarViewModel.PropertyChanged += MenuBarViewModel_PropertyChanged;

            ToggleButtonCommand = new ToggleMenuCommand(_menuBarViewModel, this);

            ShowMenuBar();
            _controllerViewModels = controllerViewModels;

            UpdateCurrentController();
        }

        // Properties
        public ViewModelBase CurrentControllerViewModel
        {
            get => _currentControllerViewModel;
            set
            {
                _currentControllerViewModel = value;
                OnPropertyChanged(nameof(CurrentControllerViewModel));
            }
        }

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

        private void MenuBarViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // We only care if the SelectedVirtualGamePad property changed
            if (e.PropertyName == nameof(MenuBarViewModel.SelectedVirtualGamePad))
            {
                UpdateCurrentController();
            }
        }

        private void UpdateCurrentController()
        {
            // Get the ID of the selected gamepad
            var selectedId = _menuBarViewModel.SelectedVirtualGamePad?.Id ?? -1;

            // Look up the corresponding ViewModel in our dictionary
            if (_controllerViewModels.TryGetValue(selectedId, out var controllerVM))
            {
                CurrentControllerViewModel = controllerVM;
            }
            else
            {
                // If nothing is selected or found, show nothing
                CurrentControllerViewModel = null;
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
