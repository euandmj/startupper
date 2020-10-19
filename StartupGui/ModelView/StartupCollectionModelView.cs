using core;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace gui.ModelView
{
    class StartupCollectionModelView
        : INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly StartupMaster _master;


        public StartupCollectionModelView()
        {
            _master = new StartupMaster();
            _master.LoadConfig();
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action) => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, nameof(Collection)));
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<StartupParameter> Collection
        {
            get => new ObservableCollection<StartupParameter>(_master.Parameters);
        }

        public void AddFile(string path)
        {
            _master.AddParameter(path);
            OnCollectionChanged(NotifyCollectionChangedAction.Add);
            OnPropertyChanged(nameof(Collection));
        }

        public void RemoveFile(StartupParameter param)
        {
            _master.RemoveParameter(param);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove);
            OnPropertyChanged(nameof(Collection));
        }

        public void UpdateParameterMethod(StartupParameter param, StartupMethod method)
        {
            _master.Parameters[param].Method = method;
        }

        public void SaveConfig()
        {
            _master.SaveConfig();
        }


    }
}
