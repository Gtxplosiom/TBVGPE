using System.ComponentModel;
using System.Diagnostics;
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

        private string _toggleMenuText = "Hide Menu";
        private string _toggleControllerText = "Hide Controls";
        private float _controllerTransparency = 1.0f;

        private Visibility _closeAppBtnVisibility = Visibility.Visible;
        private Visibility _controllerVisibility = Visibility.Visible;

        // implement this
        public ICommand ToggleMenuCommand { get; set; }
        public ICommand ToggleControllerCommand { get; set; }
        public ICommand CloseApplicationCommand { get; set; }

        public MainWindowViewModel(MenuBarViewModel menuBarViewModel, Dictionary<int, ViewModelBase> controllerViewModels)
        {
            _menuBarViewModel = menuBarViewModel;
            _controllerViewModels = controllerViewModels;

            _menuBarViewModel.PropertyChanged += MenuBarViewModel_PropertyChanged;

            ToggleMenuCommand = new ToggleMenuCommand(_menuBarViewModel, this);
            ToggleControllerCommand = new ToggleControllerCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand();

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

        public string ToggleMenuText
        {
            get => _toggleMenuText;
            set
            {
                if (_toggleMenuText != value)
                {
                    _toggleMenuText = value;
                    OnPropertyChanged(nameof(ToggleMenuText));
                }
            }
        }

        public string ToggleControllerText
        {
            get => _toggleControllerText;
            set
            {
                if (_toggleControllerText != value)
                {
                    _toggleControllerText = value;
                    OnPropertyChanged(nameof(ToggleControllerText));
                }
            }
        }

        public float ControllerTransparency
        {
            get => _controllerTransparency;
            set
            {
                if (_controllerTransparency != value)
                {
                    _controllerTransparency = value;

                    OnPropertyChanged(nameof(ControllerTransparency));
                }
            }
        }

        public Visibility CloseAppToggleControllerSliderBtnVisibility
        {
            get => _closeAppBtnVisibility;
            set
            {
                if (_closeAppBtnVisibility != value)
                {
                    _closeAppBtnVisibility = value;
                    OnPropertyChanged(nameof(CloseAppToggleControllerSliderBtnVisibility));
                }
            }
        }

        public Visibility ControllerVisibility
        {
            get => _controllerVisibility;
            set
            {
                if (value != _controllerVisibility)
                {
                    _controllerVisibility = value;
                    OnPropertyChanged(nameof(ControllerVisibility));
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
