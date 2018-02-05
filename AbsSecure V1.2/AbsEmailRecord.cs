using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsSecure_V1._2
{
    class AbsEmailRecord
    {
        private string senderEmail;
        private string senderName;
        private string recipientEmail;
        private string subject;
        private string emailContent;
        private string dateTimeLog;
        private string absUID;
        private bool isAbsSecureVerified = false;


        public AbsEmailRecord(string se, string sn, string re, string s, string ec, string dtl, bool isasv, string a = "")
        {
            senderEmail = se;
            senderName = sn;
            recipientEmail = re;
            subject = s;
            emailContent = ec;
            dateTimeLog = dtl;
            absUID = a;
            isAbsSecureVerified = isasv;
        }

        public string showFullContent()
        {
            return $"Sender: {senderEmail}\nSubject: {subject}\n\n\n{emailContent} \n\nRegards,\n{senderName}";
        }

        public override string ToString()
        {
            return $"From: {senderEmail} \t isAbsSecureMail? = {isAbsSecureVerified.ToString()}\t Subject: {subject} \t {dateTimeLog}";
        }
    }
}
