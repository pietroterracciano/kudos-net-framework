using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kudos.Utils
{
	public static class SemaphoreUtils
	{
        public static void WaitSemaphore(SemaphoreSlim? ss) { if(ss != null) try { ss.Wait(); } catch { } }
        public static async Task WaitSemaphoreAsync(SemaphoreSlim? ss) { if (ss != null) try { await ss.WaitAsync(); } catch { } }
        public static void ReleaseSemaphore(SemaphoreSlim? ss) { if(ss != null) try { ss.Release(); } catch { } }
	}
}