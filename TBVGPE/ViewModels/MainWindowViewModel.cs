using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TBVGPE.Services;
using TBVGPE.ViewModels.Commands;
using TBVGPE.Views;
using TBVGPE.Views.Controller.Presets;
using System.IO;

namespace TBVGPE.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MenuBarViewModel? _menuBarViewModel;
        private readonly Dictionary<int, ViewModelBase>? _controllerViewModels;

        // viewmodelbase as a type??
        private ViewModelBase? _currentControllerViewModel;

        private string _toggleMenuText = "Hide Menu";
        private string _toggleControllerText = "Hide Controller";
        private float _controllerTransparency = 1.0f;

        private string _toggleEditModeText = "Edit Layout";

        private Visibility _closeAppBtnVisibility = Visibility.Visible;
        private Visibility _controllerVisibility = Visibility.Visible;

        // implement this
        public ICommand ToggleMenuCommand { get; set; }
        public ICommand ToggleControllerCommand { get; set; }
        public ICommand ToggleEditModeCommand { get; set; }
        public ICommand CloseCurrentAppCommand { get; set; }
        public ICommand CloseApplicationCommand { get; set; }
        public ICommand LoadDefaultLayoutCommand { get; set; }

        // For layout saving
        private readonly Dictionary<int, string> _idNameMap;

        // current controller id
        private int _selectedId;

        public MainWindowViewModel(MenuBarViewModel menuBarViewModel, Dictionary<int, ViewModelBase> controllerViewModels)
        {
            _menuBarViewModel = menuBarViewModel;
            _controllerViewModels = controllerViewModels;

            _menuBarViewModel.PropertyChanged += MenuBarViewModel_PropertyChanged;

            ToggleMenuCommand = new ToggleMenuCommand(_menuBarViewModel, this);
            ToggleControllerCommand = new ToggleControllerCommand(this);
            ToggleEditModeCommand = new ToggleEditModeCommand(this);
            LoadDefaultLayoutCommand = new LoadDefaultLayoutCommand(this);
            CloseCurrentAppCommand = new CloseCurrentAppCommand();
            CloseApplicationCommand = new CloseApplicationCommand();

            ShowMenuBar();
            _controllerViewModels = controllerViewModels;

            _idNameMap = new Dictionary<int, string>
            {
                [1] = "gba",
                [2] = "ds",
                [3] = "xbox360",
                [4] = "3ds",
                [5] = "ps4",
                [6] = "switch"
            };

            UpdateCurrentController();
        }

        public async Task RunUpdateChecker()
        {
            // run updater
            var updateService = new UpdateService();
            var result = await updateService.CheckForUpdateAsync();
            var updaterWindow = new UpdaterWindow(result.version.ToString(), result.downloadLink.ToString());

            if (result != (null, null))
            {
                // kun may result ig set para ma pasa ha open updater window, kay an updater window kailangan
                // ito na arguments apra contructor, para pag click han update button mapakita kun may available na update
                App.UpdateVersion = result.version.ToString();
                App.UpdateLink = result.downloadLink.ToString();

                // check kun updated
                if (App.UpdateVersion.CompareTo(App.CurrentVersion) > 0)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        updaterWindow.Show();
                    });
                }
            }
        }

        // Properties
        public ViewModelBase CurrentControllerViewModel
        {
            get => _currentControllerViewModel;
            set
            {
                if (_currentControllerViewModel != value)
                {
                    _currentControllerViewModel = value;
                    OnPropertyChanged(nameof(CurrentControllerViewModel));
                }
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

        public string ToggleEditModeText
        {
            get => _toggleEditModeText;
            set
            {
                if (_toggleEditModeText != value)
                {
                    _toggleEditModeText = value;
                    OnPropertyChanged(nameof(ToggleEditModeText));
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

        public Visibility UtilityMenuVisibility
        {
            get => _closeAppBtnVisibility;
            set
            {
                if (_closeAppBtnVisibility != value)
                {
                    _closeAppBtnVisibility = value;
                    OnPropertyChanged(nameof(UtilityMenuVisibility));
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

                Debug.WriteLine($"Raw: {CurrentControllerViewModel}");
                Debug.WriteLine($"Casted: {nameof(CurrentControllerViewModel)}");

                // so every switching of controller layout and the edit mode is active, ma au-auto save
                if (App.EditMode)
                {
                    ToggleEditModeCommand.Execute(null);
                }

                // "connect" the controller, kun may na select ha virtualgamepad list
                if (_selectedId == 5)
                {
                    //App.Vigem.ConnectController("DualShock4");
                    // updating connection logic
                    App.Vigem.ConnectController("Xbox360");
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

        public void SaveCurrentLayout()
        {
            if (CurrentControllerViewModel is IControllerLayoutManagerInterface layout && _idNameMap.TryGetValue(_selectedId, out var name))
            {
                string jsonFile = $"{name}_config.json";

                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appFolder = Path.Combine(appData, "TBVGPE", "Layouts");

                if(!Directory.Exists(appFolder))
                    Directory.CreateDirectory(appFolder);

                string configPath = Path.Combine(appFolder, jsonFile);

                layout.SaveLayout(configPath);

                Debug.WriteLine("Saving layout...");
            }
        }

        public void LoadDefaultLayout()
        {
            if (CurrentControllerViewModel is IControllerLayoutManagerInterface layout)
            {
                layout.LoadDefault();

                Debug.WriteLine("Loading default...");
            }
        }
    }
}
