using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace core
{
    [DebuggerDisplay("{" + nameof(FullPath) + "}")]
    [JsonObject]
    public class StartupParameter
        : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _timeout = 5;
        private string _arguments;
        private StartupMethod _method;

        [JsonIgnore]
        internal TimeSpan Timeout => TimeSpan.FromSeconds(_timeout);

        [JsonProperty]
        public string FullPath { get; set; }

        [JsonProperty]
        public string Filename { get; set; }

        [JsonProperty]
        public StartupMethod Method
        {
            get => _method;
            set
            {
                _method = value;
                OnChanged(nameof(Method));
            }
        }

        [JsonProperty]
        public string Arguments
        {
            get => _arguments;
            set
            {
                _arguments = value;
                OnChanged(nameof(Arguments));
            }
        }

        [JsonProperty]
        public int TimeoutSeconds
        {
            get => _timeout;
            set
            {
                _timeout = value;
                OnChanged(nameof(TimeoutSeconds));
            }
        }

        public StartupParameter(DirectoryInfo info, string args = null)
        {
            if (info is null) return;
            FullPath = info.FullName;
            Filename = info.Name;
            Method = 0;
            Arguments = args;
        }

        private void OnChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }

    public enum StartupMethod
    {
        Exit = 0,
        Maximised,
        Minimised,
        MinimisedToTray,
        None
    }
}
