using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TBVGPE.Models;
using TBVGPE.ViewModels.Commands;

namespace TBVGPE.ViewModels
{
    public class MenuBarViewModel : ViewModelBase
    {
        public ObservableCollection<VirtualGamePads>? VirtualGamePadsCollection { get; set; }

        private VirtualGamePads? _selectedVirtualGamepad;
        private Visibility _visibilityState = Visibility.Collapsed;

        public ICommand OpenUpdaterCommand { get; set; }

        public MenuBarViewModel(ObservableCollection<VirtualGamePads>? virtualGamePadsCollection)
        {
            if (virtualGamePadsCollection == null) return;

            VirtualGamePadsCollection = virtualGamePadsCollection;

            SelectedVirtualGamePad = VirtualGamePadsCollection[0];

            OpenUpdaterCommand = new OpenUpdaterCommand();
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
