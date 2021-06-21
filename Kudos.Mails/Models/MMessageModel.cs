using Kudos.Types;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Kudos.Mails.Models
{
    public class MMessageModel
    {
        private MUserModel
            _mSender;

        private Dictionary<String, Int32> 
            _dBCCReceiversMails2BCCReiceversIndexes;
        private List<MUserModel>
            _lBCCReceivers;

        private Dictionary<String, Int32> 
            _dCCReceiversMails2CCReiceversIndexes;
        private List<MUserModel>
            _lCCReceivers;

        private Dictionary<String, Int32> 
            _dReceiversMails2ReiceversIndexes;
        private List<MUserModel>
            _lReceivers;

        private Dictionary<String, Int32>
            _dReplies2Mails2Replies2Indexes;
        private List<MUserModel>
            _lReplies2;

        private Dictionary<String, Int32>
            _dAttachmentsFiles2AttachmentsIndexes;
        private List<Attachment>
            _lAttachments;

        public Text Message { get; set; }
        public Text Object { get; set; }

        public MMessageModel()
        {
            FlushReceivers();
            FlushBCCReceivers();
            FlushCCReceivers();
            FlushAttachments();
            FlushReplies2();
        }

        #region Sender

        public Boolean SetSender(String sSenderMail, String sSenderName = null)
        {
            return SetSender(new MUserModel() { Mail = sSenderMail, Name = sSenderName });
        }

        private Boolean SetSender(MUserModel mSender)
        {
            if (mSender != null && mSender.IsMailValid())
            { 
                _mSender = mSender;
                return true;
            }

            _mSender = null;
            return false;
        }

        public MUserModel GetSender()
        {
            return _mSender;
        }

        #endregion

        #region Attachments

        public void FlushAttachments()
        {
            if(_lAttachments != null)
                foreach(Attachment oAttachment in _lAttachments)
                {
                    if (oAttachment == null) continue;
                    try { oAttachment.Dispose(); } catch { }
                }
            
            _lAttachments = new List<Attachment>();
            _dAttachmentsFiles2AttachmentsIndexes = new Dictionary<String, Int32>();
        }

        public Boolean AddAttachment(String sFile, String sFMimeType = null)
        {
            if (File.Exists(sFile))
            {
                String
                    sCleanedFile = sFile.ToLower().Trim();
                Int32 iIndex;
                if (_dAttachmentsFiles2AttachmentsIndexes.TryGetValue(sCleanedFile, out iIndex) || iIndex > -1)
                    return false;

                try
                {
                    _lAttachments.Add(new Attachment(sFile, sFMimeType));
                }
                catch
                {
                    return false;
                }

                _dAttachmentsFiles2AttachmentsIndexes[sCleanedFile] = _lAttachments.Count - 1;

                return true;
            }
            return false;
        }

        public Boolean RemoveAttachment(String sFile)
        {
            if (!String.IsNullOrWhiteSpace(sFile))
            {
                Int32 i32Index;
                if (!_dAttachmentsFiles2AttachmentsIndexes.TryGetValue(sFile.ToLower().Trim(), out i32Index) || !ListUtils.IsValidIndex(_lAttachments, i32Index) )
                    return false;

                _lAttachments.RemoveAt(i32Index);
                _dAttachmentsFiles2AttachmentsIndexes.Remove(sFile);

                return true;
            }

            return false;
        }

        public Attachment[] GetAttachments()
        {
            return _lAttachments.ToArray();
        }

        #endregion

        #region BCCReceivers

        public void FlushBCCReceivers()
        {
            FlushUsers(ref _lBCCReceivers, ref _dBCCReceiversMails2BCCReiceversIndexes);
        }

        #region public Boolean AddBCCReceiver

        public Boolean AddBCCReceiver(String sMail, String sName = null)
        {
            return AddUser(ref _lBCCReceivers, ref _dBCCReceiversMails2BCCReiceversIndexes, sMail, sName);
        }
        private Boolean AddBCCReceiver(MUserModel mUser)
        {
            return AddUser(ref _lBCCReceivers, ref _dBCCReceiversMails2BCCReiceversIndexes, mUser);
        }

        #endregion

        #region public Boolean RemoveBCCReceiver()

        public Boolean RemoveBCCReceiver(String sMail)
        {
            return RemoveUser(ref _lBCCReceivers, ref _dBCCReceiversMails2BCCReiceversIndexes, sMail);
        }

        public Boolean RemoveBCCReceiver(MUserModel mUser)
        {
            return RemoveUser(ref _lBCCReceivers, ref _dBCCReceiversMails2BCCReiceversIndexes, mUser);
        }

        #endregion

        public MUserModel[] GetBCCReceivers()
        {
            return GetUsers(_lBCCReceivers);
        }

        #endregion

        #region CCReceivers

        public void FlushCCReceivers()
        {
            FlushUsers(ref _lCCReceivers, ref _dCCReceiversMails2CCReiceversIndexes);
        }

        #region public Boolean AddCCReceiver

        public Boolean AddCCReceiver(String sMail, String sName = null)
        {
            return AddUser(ref _lCCReceivers, ref _dCCReceiversMails2CCReiceversIndexes, sMail, sName);
        }
        private Boolean AddCCReceiver(MUserModel mUser)
        {
            return AddUser(ref _lCCReceivers, ref _dCCReceiversMails2CCReiceversIndexes, mUser);
        }

        #endregion

        #region public Boolean RemoveCCReceiver()

        public Boolean RemoveCCReceiver(String sMail)
        {
            return RemoveUser(ref _lCCReceivers, ref _dCCReceiversMails2CCReiceversIndexes, sMail);
        }

        public Boolean RemoveCCReceiver(MUserModel mUser)
        {
            return RemoveUser(ref _lCCReceivers, ref _dCCReceiversMails2CCReiceversIndexes, mUser);
        }

        #endregion

        public MUserModel[] GetCCReceivers()
        {
            return GetUsers(_lCCReceivers);
        }

        #endregion

        #region Receivers

        public void FlushReceivers()
        {
            FlushUsers(ref _lReceivers, ref _dReceiversMails2ReiceversIndexes);
        }

        #region public Boolean AddReceiver

        public Boolean AddReceiver(String sMail, String sName = null)
        {
            return AddUser(ref _lReceivers, ref _dReceiversMails2ReiceversIndexes, sMail, sName);
        }
        private Boolean AddReceiver(MUserModel mUser)
        {
            return AddUser(ref _lReceivers, ref _dReceiversMails2ReiceversIndexes, mUser);
        }

        #endregion

        #region public Boolean RemoveReceiver()

        public Boolean RemoveReceiver(String sMail)
        {
            return RemoveUser(ref _lReceivers, ref _dReceiversMails2ReiceversIndexes, sMail);
        }

        public Boolean RemoveReceiver(MUserModel mUser)
        {
            return RemoveUser(ref _lReceivers, ref _dReceiversMails2ReiceversIndexes, mUser);
        }

        #endregion

        public MUserModel[] GetReceivers()
        {
            return GetUsers(_lReceivers);
        }

        #endregion

        #region Replies2

        public void FlushReplies2()
        {
            FlushUsers(ref _lReplies2, ref _dReplies2Mails2Replies2Indexes);
        }

        #region public Boolean AddReceiver

        public Boolean AddReply2(String sMail, String sName = null)
        {
            return AddUser(ref _lReplies2, ref _dReplies2Mails2Replies2Indexes, sMail, sName);
        }
        private Boolean AddReply2(MUserModel mUser)
        {
            return AddUser(ref _lReplies2, ref _dReplies2Mails2Replies2Indexes, mUser);
        }

        #endregion

        #region public Boolean RemoveReply2()

        public Boolean RemoveReply2(String sMail)
        {
            return RemoveUser(ref _lReplies2, ref _dReplies2Mails2Replies2Indexes, sMail);
        }

        public Boolean RemoveReply2(MUserModel mUser)
        {
            return RemoveUser(ref _lReplies2, ref _dReplies2Mails2Replies2Indexes, mUser);
        }

        #endregion

        public MUserModel[] GetReplies2()
        {
            return GetUsers(_lReplies2);
        }

        #endregion

        #region Users

        private void FlushUsers(ref List<MUserModel> lUsers, ref Dictionary<String, Int32> dUMails2UIndexes)
        {
            lUsers = new List<MUserModel>();
            dUMails2UIndexes = new Dictionary<String, Int32>();
        }

        #region private Boolean AddUser

        private Boolean AddUser(ref List<MUserModel> lUsers, ref Dictionary<String, Int32> dUMails2UIndexes, String sReceiverMail, String sReceiverName = null)
        {
            return AddUser(ref lUsers, ref dUMails2UIndexes, new MUserModel() { Mail = sReceiverMail, Name = sReceiverName });
        }
        private Boolean AddUser(ref List<MUserModel> lUsers, ref Dictionary<String, Int32> dUMails2UIndexes, MUserModel mUser)
        {
            if (lUsers != null && dUMails2UIndexes != null && mUser != null && mUser.IsMailValid())
            {
                String
                    sUMail = mUser.Mail.ToLower().Trim();

                Int32 iIndex;
                if (dUMails2UIndexes.TryGetValue(sUMail, out iIndex) || ListUtils.IsValidIndex(lUsers, iIndex))
                    return false;

                lUsers.Add(mUser);
                dUMails2UIndexes[sUMail] = lUsers.Count - 1;

                return true;
            }

            return false;
        }

        #endregion

        #region private Boolean RemoveUser()

        private Boolean RemoveUser(ref List<MUserModel> lUsers, ref Dictionary<String, Int32> dUMails2UIndexes, String sUMail)
        {
            return RemoveUser(ref lUsers, ref dUMails2UIndexes, new MUserModel() { Mail = sUMail });
        }

        private Boolean RemoveUser(ref List<MUserModel> lUsers, ref Dictionary<String, Int32> dUMails2UIndexes, MUserModel mUser)
        {
            if (lUsers != null && dUMails2UIndexes != null && mUser != null && mUser.IsMailValid())
            {
                String sUMail = mUser.Mail.ToLower().Trim();
                Int32 i32Index;
                if (!dUMails2UIndexes.TryGetValue(sUMail, out i32Index) || !ListUtils.IsValidIndex(lUsers, i32Index))
                    return false;

                lUsers.RemoveAt(i32Index);
                dUMails2UIndexes.Remove(sUMail);

                return true;
            }

            return false;
        }


        #endregion

        private MUserModel[] GetUsers(List<MUserModel> lUsers)
        {
            return lUsers != null ? lUsers.ToArray() : null;
        }

        #endregion

        public MailMessage ToMailMessage()
        {
            if (
                _mSender == null
                || _lReceivers.Count < 1
            )
                return null;

            MailAddress
                oMASender = _mSender.ToMailAddress();

            if (oMASender == null)
                return null;

            MailMessage
                oMailMessage = new MailMessage()
                {
                    IsBodyHtml = true,

                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    HeadersEncoding = Encoding.UTF8,

                    Body = Message,
                    Subject = Object,

                    Sender = oMASender,
                    From = oMASender
                };

            if (_lReplies2.Count > 0)
                for(int i=0; i<_lReplies2.Count; i++)
                {
                    MailAddress
                        oMailAddress = _lReplies2[i].ToMailAddress();

                    if (oMailAddress == null)
                        continue;

                    oMailMessage.ReplyToList.Add(oMailAddress);
                }
            else
                oMailMessage.ReplyToList.Add(oMailMessage.From);

            for (int i = 0; i < _lReceivers.Count; i++)
            {
                MailAddress
                    oMailAddress = _lReceivers[i].ToMailAddress();

                if (oMailAddress == null)
                    continue;

                oMailMessage.To.Add(oMailAddress);
            }

            if (oMailMessage.To.Count < 1)
                return null;

            for (int i=0; i<_lBCCReceivers.Count; i++)
            {
                MailAddress
                    oMailAddress = _lBCCReceivers[i].ToMailAddress();

                if (oMailAddress == null)
                    continue;

                oMailMessage.Bcc.Add(oMailAddress);
            }

            for (int i = 0; i < _lCCReceivers.Count; i++)
            {
                MailAddress
                    oMailAddress = _lCCReceivers[i].ToMailAddress();

                if (oMailAddress == null)
                    continue;

                oMailMessage.CC.Add(oMailAddress);
            }

            for(int i=0; i<_lAttachments.Count; i++)
                oMailMessage.Attachments.Add(_lAttachments[i]);

            return oMailMessage;
        }
    }
}