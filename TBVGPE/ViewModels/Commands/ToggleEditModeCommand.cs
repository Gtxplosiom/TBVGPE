using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBVGPE.Views;

namespace TBVGPE.ViewModels.Commands
{
    public class ToggleEditModeCommand : CommandBase
    {
        private MainWindowViewModel _mainWindowViewModel;

        public ToggleEditModeCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (App.EditMode == true)
            {
                App.EditMode = false;

                _mainWindowViewModel.ToggleEditModeText = "Edit Layout";
            }
            else
            {
                App.EditMode = true;

                _mainWindowViewModel.ToggleEditModeText = "Save Layout";

                // Implement actual saving
            }
        }
    }
}
