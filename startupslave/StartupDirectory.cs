using System;
using System.IO;

namespace core
{
    public static class StartupDirectory
    {
        private static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string FullDirectory = $"{AppDataPath}/startupdaemon";


        public static string ConfigPath
        {
            get
            {
                if (!Directory.Exists(FullDirectory))
                {
                    // directory doesnt yet exist. make it.
                    Directory.CreateDirectory(FullDirectory);
                }
                return $"{FullDirectory}/config.json";
            }
        }

        public static string AppDataFolder
        {
            get
            {
                if (!Directory.Exists(FullDirectory))
                {
                    // directory doesnt yet exist. make it.
                    Directory.CreateDirectory(FullDirectory);
                }
                return FullDirectory;
            }
        }
    }
}
