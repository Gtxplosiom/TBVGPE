using System.Collections.ObjectModel;
using System.Windows;
using TBVGPE.Models;

namespace TBVGPE.ViewModels
{
    public class MenuBarViewModel : ViewModelBase
    {
        public ObservableCollection<VirtualGamePads>? VirtualGamePadsCollection { get; set; }

        private VirtualGamePads? _selectedVirtualGamepad;
        private Visibility _visibilityState = Visibility.Collapsed;

        public MenuBarViewModel()
        {
            // Dummy data
            VirtualGamePadsCollection = new ObservableCollection<VirtualGamePads>
            {
                new VirtualGamePads { Id = -1, Name = "Select a GamePad..." },
                new VirtualGamePads { Id = 1, Name = "3DS" }
            };

            SelectedVirtualGamePad = VirtualGamePadsCollection[0];
        }

        public VirtualGamePads SelectedVirtualGamePad
        {
            get => _selectedVirtualGamepad;
            set
            {
                if (_selectedVirtualGamepad != value)
                {
                    _selectedVirtualGamepad = value;
                    OnPropertyChanged(nameof(SelectedVirtualGamePad));
                }
            }
        }

        public Visibility VisibilityState
        {
            get => _visibilityState;
            set
            {
                if (_visibilityState != value)
                {
                    _visibilityState = value;
                    OnPropertyChanged(nameof(VisibilityState));
                }
            }
        }
    }
}
