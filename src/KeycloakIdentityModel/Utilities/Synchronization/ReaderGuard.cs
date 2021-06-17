using System;
using System.Threading;

namespace Psy.KeycloakIdentityModel.Utilities.Synchronization
{
    public class ReaderGuard : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;

        public ReaderGuard(ReaderWriterLockSlim readerWriterLock)
        {
            _readerWriterLock = readerWriterLock;
            _readerWriterLock.EnterReadLock();
        }

        public void Dispose()
        {
            _readerWriterLock.ExitReadLock();
        }
    }
}