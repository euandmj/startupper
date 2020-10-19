using core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace daemon
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new StartupMaster();



            startup.LoadConfig();

            try
            {
                startup.Execute();

            }
            catch(Win32Exception)
            {
                Console.WriteLine("error occurred whilst executing one parameter");
            }


            Console.ReadKey();
        }
    }
}
