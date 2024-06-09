using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Builders;
using Kudos.Clouds.AmazonWebServiceModule.PinpointModule.Descriptors;
using Kudos.Reflection.Utils;
using Kudos.Utils;

namespace Kudos.Clouds.AmazonWebServiceModule.PinpointModule
{
	public class AWSPinpoint
	{
        public static class AddressConfiguration
        {
            public static readonly Amazon.Pinpoint.Model.AddressConfiguration
                SMS,
                Mail;

            static AddressConfiguration()
            {
                SMS = new Amazon.Pinpoint.Model.AddressConfiguration() { ChannelType = ChannelType.SMS };
                Mail = new Amazon.Pinpoint.Model.AddressConfiguration() { ChannelType = ChannelType.EMAIL };
            }
        }

        private readonly AmazonPinpointClient? _appc;
        private readonly SendMessagesRequest? _smr;
        private readonly HashSet<ChannelType> _hs;

        internal AWSPinpoint(ref AWSPinpointDescriptor awsppd)
		{
            _appc = awsppd.Client;

            if
            (
                String.IsNullOrWhiteSpace(awsppd.ApplicationID)
                || _appc == null
            )
                return;

            try
            {
                _smr = new SendMessagesRequest();
                _smr.ApplicationId = awsppd.ApplicationID;
                _smr.MessageRequest = new MessageRequest();
                _smr.MessageRequest.MessageConfiguration = new DirectMessageConfiguration();
                _smr.MessageRequest.Addresses = new Dictionary<string, Amazon.Pinpoint.Model.AddressConfiguration>();
            }
            catch
            {
                _smr = null;
            }

            _hs = new HashSet<ChannelType>();
        }

        public Boolean PutSMSMessage(SMSMessage? smss)
        {
            if (_smr != null)
                try { _smr.MessageRequest.MessageConfiguration.SMSMessage = smss; return true; } catch { }
            return false;
        }

        public Boolean PutMailMessage(EmailMessage? em)
        {
            if (_smr != null)
                try { _smr.MessageRequest.MessageConfiguration.EmailMessage = em; return true; } catch { }
            return false;
        }

        public Boolean PutSMSAddress(String? s)
        {
            Amazon.Pinpoint.Model.AddressConfiguration ac = AddressConfiguration.SMS;
            Boolean b;
            _PutAddress(ref s, ref ac, out b);
            return b;
        }
        public Boolean PutMailAddress(String? s)
        {
            Amazon.Pinpoint.Model.AddressConfiguration ac = AddressConfiguration.Mail;
            Boolean b;
            _PutAddress(ref s, ref ac, out b);
            return b;
        }
        public Boolean PutAddress(String? s, Amazon.Pinpoint.Model.AddressConfiguration? ac) { Boolean b; _PutAddress(ref s, ref ac, out b); return b; }
        private void _PutAddress(ref String? s, ref Amazon.Pinpoint.Model.AddressConfiguration? ac, out Boolean b)
        {
            if
            (
                _smr != null
                && !String.IsNullOrWhiteSpace(s)
                && ac != null
                && ac.ChannelType != null
            )
                try
                {
                    _smr.MessageRequest.Addresses[s] = ac;
                    b = true;
                    _hs.Add(ac.ChannelType);
                    return;
                }
                catch
                {
                }

            b = false;
        }

        public Task<SendMessagesResponse?> SendMessagesAsync() { return Task.Run(SendMessages); }
        public SendMessagesResponse? SendMessages()
		{
            if (
                _smr == null
                || _hs.Count < 1
                || 
                (
                    _hs.Contains(ChannelType.SMS)
                    && _smr.MessageRequest.MessageConfiguration.SMSMessage == null
                )
                ||
                (
                    _hs.Contains(ChannelType.EMAIL)
                    && _smr.MessageRequest.MessageConfiguration.EmailMessage == null
                )
            )
                return null;

            try
            {
                Task<SendMessagesResponse>? tsmr = _appc.SendMessagesAsync(_smr);
                tsmr.Wait();
                return tsmr.Result;
            }
            catch
            {
                return null;
            }
        }

        public static AWSPinpointBuilder RequestBuilder()
		{
			AWSPinpointDescriptor awsppd = new AWSPinpointDescriptor();
			return new AWSPinpointBuilder(ref awsppd);
		}
	}
}

