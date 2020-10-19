using System;
using System.Windows.Input;

namespace WibuTubeConverter.ViewModels.Commands
{
    public class CommandEventHandler<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<T> action;
        private Func<bool> canExecute;

        public CommandEventHandler(Action<T> action)
        {
            this.action = action;
        }

        public CommandEventHandler(Action<T> action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public void Execute(object parameter)
        {
            this.action((T)parameter);
        }
    }
}