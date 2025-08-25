using TBVGPE.Views;

namespace TBVGPE.ViewModels.Commands
{
    public class OpenUpdaterCommand : CommandBase
    {
        private UpdaterWindow? _updaterWindow;

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            _updaterWindow = new UpdaterWindow(App.UpdateVersion, App.UpdateLink);
            _updaterWindow.Show();
        }
    }
}
