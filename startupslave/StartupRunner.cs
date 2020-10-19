using core.Runner;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace core
{
    internal class StartupRunner
        : IDisposable
    {
        private bool _disposed;

        private CancellationTokenSource _tokenSource;

        public StartupRunner(CancellationTokenSource token)
        {
            _tokenSource = token;
        }

        internal async void RunParameter(StartupParameter param)
        {
            if (!File.Exists(param.FullPath))
                throw new ArgumentException("so such file - " + param.FullPath, nameof(param));

            var info = GetInfo(param.FullPath, param.Arguments);

            var runner = GetRunner(param);

            await runner.Run(info, _tokenSource.Token);
        }

        private ProcessStartInfo GetInfo(string filename, params string[] args)
        {
            return new ProcessStartInfo(filename, string.Join(" ", args));
        }

        private IRunner GetRunner(StartupParameter param)
        {
            switch(param.Method)
            {
                case StartupMethod.Exit:
                    return new ExitRunner(param.Timeout);
                case StartupMethod.Maximised:
                    return new MaximisedRunner();
                default:
                    throw new NotSupportedException();
            }
        }           

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _tokenSource = null;
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
