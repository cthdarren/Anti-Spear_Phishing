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
        private string attachment;


        public AbsEmailRecord(string se, string re, string s, string ec, string dtl, string a = "")
        {
            senderEmail = se;
            recipientEmail = re;
            subject = s;
            emailContent = ec;
            dateTimeLog = dtl;
            attachment = a;
        }

        public override string ToString()
        {
            return $"From: {senderEmail} \t Subject: {subject} \t {dateTimeLog}";
        }
    }
}
