using core.Exceptions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace core.Runner
{
    class ExitRunner
        : IRunner
    {
        private readonly TimeSpan _timeout;


        public ExitRunner(TimeSpan t)
        {
            _timeout = t;
        }

        public async Task<bool> Run(ProcessStartInfo info, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    var p = Process.Start(info);

                    await Task.Delay(_timeout, token);

                    p.Kill();

                    if(!p.HasExited)
                        throw new RunnerException(p.ProcessName, p.Id, p.ExitCode);
                }
                catch (Exception ex)
                {
                    await Task.FromException(ex);
                }
                return true;
            }, token);
        }
    }
}
