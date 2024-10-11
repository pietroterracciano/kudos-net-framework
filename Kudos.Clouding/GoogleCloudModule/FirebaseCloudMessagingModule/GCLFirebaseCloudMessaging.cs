using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;

namespace Kudos.Clouding.GoogleCloudModule.FirebaseCloudMessagingModule
{
    public sealed class GCLFirebaseCloudMessaging
    {
        private readonly FirebaseMessaging? _fm;

        internal GCLFirebaseCloudMessaging(ref FirebaseMessaging? fm)
        {
            _fm = fm;
        }

        public async Task<String> SendAsync(Message? m)
        {
            if (_fm != null && m != null) try { return await _fm.SendAsync(m); } catch { }
            return String.Empty;
        }

        public async Task<BatchResponse?> SendEachAsync(IEnumerable<Message>? ms)
        {
            if (_fm != null && ms != null) try { return await _fm.SendEachAsync(ms); } catch { }
            return null;
        }

        public async Task<BatchResponse?> SendEachForMulticastAsync(MulticastMessage? mm)
        {
            if (_fm != null && mm != null) try { return await _fm.SendEachForMulticastAsync(mm); } catch { }
            return null;
        }
    }
}

