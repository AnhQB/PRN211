using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMEmployee.Commands
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<T> DoWork;
        public RelayCommand(Action<T> work)
        {
            DoWork = work;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoWork((T) parameter);
        }
    }

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action DoWork;
        public RelayCommand(Action work)
        {
            DoWork = work;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoWork();
        }
    }
}
