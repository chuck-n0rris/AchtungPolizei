using System;
using System.Windows.Input;

namespace AchtungPolizei.Plugins.Impl
{
    public class PlainCommand : ICommand
    {
        private Action executeAction;

        public PlainCommand(Action executeAction)
        {
            this.executeAction = executeAction;
        }

        public void Execute(object parameter)
        {
            this.executeAction();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}