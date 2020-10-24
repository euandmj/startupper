using System;

namespace core.Exceptions
{
    public class RunnerException
        : Exception
    {
        public string FullName { get; }
        public int ProcessID { get; }
        public int ExitCode { get; }

        public RunnerException(string name, int pid, int code)
        {
            FullName = name;
            ProcessID = pid;
            ExitCode = code;
        }
    }
}
