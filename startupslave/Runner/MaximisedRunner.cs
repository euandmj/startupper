using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace core.Runner
{
    class MaximisedRunner
        : IRunner
    {
        public async Task<bool> Run(ProcessStartInfo info, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                Process p = null;
                try
                {
                    p = Process.Start(info);
                    await Task.Delay(1000, token); // delay really needed?
                    FFI.ShowWindow(p.Handle, FFI.SW_COMMANDS.SW_MAXIMIZE); // doesnt work??
                }
                catch(Exception ex)
                {
                    await Task.FromException(ex);
                }
                return true;
            }, token);
        }
    }
}
