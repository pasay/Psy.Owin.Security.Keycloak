using System;
using System.Threading;

namespace Psy.Owin.Security.Keycloak.IdentityModel.Utilities.Synchronization
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