using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace core.Runner
{
    interface IRunner
    {

        Task<bool> Run(ProcessStartInfo info, CancellationToken token);
    }

}
