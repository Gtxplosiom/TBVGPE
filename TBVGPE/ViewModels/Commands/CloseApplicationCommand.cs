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
            MessageBoxResult option = MessageBox.Show("Do you want to close TBVGPE?", "Confirmation",
                                                      MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (option != MessageBoxResult.Yes)
            {
                return;
            }
            else
            {
                Application.Current.Shutdown();
            }   
        }
    }
}
