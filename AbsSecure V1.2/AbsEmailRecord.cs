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
        private string recipientEmail;
        private string subject;
        private string emailContent;
        private string dateTimeLog;
        private string absUID;
        private bool isAbsSecureVerified;


        public AbsEmailRecord(string se, string re, string s, string ec, string dtl, string a = "", bool isasv = false)
        {
            senderEmail = se;
            recipientEmail = re;
            subject = s;
            emailContent = ec;
            dateTimeLog = dtl;
            absUID = a;
            isAbsSecureVerified = isasv;
        }

        public string showFullContent()
        {
            return $"Sender: {senderEmail}\nSubject: {subject}\n\n\n{emailContent} \n\nRegards,\nFag";
        }

        public override string ToString()
        {
            return $"From: {senderEmail} \t Subject: {subject} \t {dateTimeLog}";
        }
    }
}
