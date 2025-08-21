using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
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

        // current controller id
        private int _selectedId;

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
            _selectedId = _menuBarViewModel.SelectedVirtualGamePad?.Id ?? -1;

            // Look up the corresponding ViewModel in our dictionary
            if (_controllerViewModels.TryGetValue(_selectedId, out var controllerVM))
            {
                // an layout na magiging visible ay an layout nga naka attach or connected to the viewmodel
                // na naka attach via datatemplate, which is set ha ubos na line, ha currentcontrollerviewmodel property
                CurrentControllerViewModel = controllerVM;

                // "connect" the controller, kun may na select ha virtualgamepad list
                if (_selectedId == 5)
                {
                    App.Vigem.ConnectController("DualShock4");
                }
                else
                {
                    App.Vigem.ConnectController("Xbox360");
                }
            }
            else
            {
                // If nothing is selected or found, show nothing
                CurrentControllerViewModel = null;

                App.Vigem.DisconnectController();
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
