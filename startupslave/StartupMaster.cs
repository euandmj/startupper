using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public sealed class StartupMaster
    {
        //private const string CONFIG_PATH = "config.json";
        private readonly string _path = StartupDirectory.ConfigPath;


        public StartupMaster()
        {
            Parameters = new StartupCollection();
        }

        public StartupCollection Parameters { get; set; }
        

        private string ReadAllText(string path)
        {
            using (var sr = new StreamReader(path, encoding: Encoding.ASCII))
            {
                return sr.ReadToEnd();
            }
        }

        private void WriteConfig(string json)
        {
            using (var fs = new FileStream(StartupDirectory.ConfigPath, FileMode.Truncate, FileAccess.Write, FileShare.Write))
            {
                var bytes = Encoding.ASCII.GetBytes(json);

                fs.Write(bytes, 0, bytes.Length);
            }
        }

        private void Deserialize(string json)
        {
            Parameters = JsonConvert.DeserializeObject<StartupCollection>(json);
        }

        private string Serialize()
        {
            return JsonConvert.SerializeObject(Parameters, Formatting.Indented);
        }

        public void SaveConfig()
        {
            //Parameters = new StartupCollection()
            //{
            //    new StartupParameter()  { FullPath = @"D:\Documents\VS Code\WindowsMedialCtrl\Server\Windows\bin\Release\netcoreapp3.1\winmediactrl Server.exe", Filename = "winmediactrl Sever.exe" },
            //    new StartupParameter() { FullPath = "Path2", Filename = "Path2.exe" }
            //};
            string json = Serialize();

            WriteConfig(json);
        }

        public void LoadConfig()
        {
            if (string.IsNullOrWhiteSpace(_path)) throw new ArgumentNullException(nameof(_path));
            if (!File.Exists(_path)) throw new ArgumentException("file does not exist", nameof(_path));

            var config = ReadAllText(_path);

            if (string.IsNullOrWhiteSpace(config))
            {
                Debug.WriteLine("empty config provided...");
                return;
            }

            Deserialize(config);
        }

        public void AddParameter(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) throw new ArgumentException("file does not exist", nameof(path));

            var di = new DirectoryInfo(path);

            Parameters.Add(new StartupParameter(di));
        }

        public void RemoveParameter(StartupParameter param)
        {
            if (!Parameters.Contains(param)) throw new ArgumentException("collection does not contain this parameter", nameof(param));

            Parameters.Remove(param);
        }

        public void Execute()
        {
            using (var runner = new StartupRunner(new System.Threading.CancellationTokenSource()))
            {
                Parallel.ForEach(Parameters, (p) =>
                {
                    try
                    {
                        runner.RunParameter(p);
                    }
                    catch (Win32Exception ex) { Debug.Write($"Win32 Except: {ex.Message}"); }
                });
            }
        }
    }
}