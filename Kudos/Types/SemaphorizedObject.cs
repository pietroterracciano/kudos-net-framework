using System;
using System.Threading;
using System.Threading.Tasks;
using Kudos.Utils;

namespace Kudos.Types
{
    public class SemaphorizedObject
    {
        private readonly SemaphoreSlim _ss;

        public SemaphorizedObject() { _ss = new SemaphoreSlim(1); }

        protected void _WaitSemaphore() { SemaphoreUtils.WaitSemaphore(_ss); }
        protected async Task _WaitSemaphoreAsync() { await SemaphoreUtils.WaitSemaphoreAsync(_ss); }
        protected void _ReleaseSemaphore() { SemaphoreUtils.ReleaseSemaphore(_ss); }
    }
}