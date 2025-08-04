using System.Windows;

namespace TBVGPE.ViewModels.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
