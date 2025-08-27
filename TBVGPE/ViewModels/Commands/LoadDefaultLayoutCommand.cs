namespace TBVGPE.ViewModels.Commands
{
    public class LoadDefaultLayoutCommand : CommandBase
    {
        private MainWindowViewModel _mainWindowViewModel;

        public LoadDefaultLayoutCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            if (!App.EditMode) return;

            _mainWindowViewModel.LoadDefaultLayout();
        }
    }
}
