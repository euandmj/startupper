using System;

namespace core.Exceptions
{
    public class ProcessNotExitedException
        : Exception
    {
        public string FullName { get; }
        public int ProcessID { get; }
        public int ExitCode { get; }

        public ProcessNotExitedException(string name, int pid, int code)
        {
            FullName = name;
            ProcessID = pid;
            ExitCode = code;
        }
    }
}
