using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SignalR.Maui.Client
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {
            SimpleCommandManager.AssignOnPropertyChanged(ref PropertyChanged);
            _commandsList = new List<RelayCommand>();
        }

        private List<RelayCommand> _commandsList;
        private bool disposedValue;

        protected RelayCommand CreateCommand(Action execute)
        {
            return CreateCommand(execute, null);
        }

        protected RelayCommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            var tempCmd = new RelayCommand(execute, canExecute);
            if (_commandsList.Contains(tempCmd))
            {
                return _commandsList[_commandsList.IndexOf(tempCmd)];
            }
            else
            {
                _commandsList.Add(tempCmd);
                return tempCmd;
            }
        }

        public void RemoveCommands()
        {
            for (var i = 0; i < _commandsList.Count; i++)
            {
                _commandsList[i].RemoveCommand();
            }
        }

        public virtual IDispatcher DispatcherObject
        {
            get
            {
                if(Application.Current != null)
                {
                    return Application.Current.Dispatcher;
                }
                else
                {
                    return Dispatcher.GetForCurrentThread() ?? throw new NullReferenceException("Could not get a valid dispatcher.");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void ChangeProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value))
            {
                return;
            }
            else
            {
                property = value;

                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChangedEmpty()
        {
            OnPropertyChanged(String.Empty);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    RemoveCommands();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
