using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsSecure_V1._2
{
    class AbsEmailRecord
    {
        private string senderName;
        private string recipientEmail;
        private string subject;
        private string dateTimeLog;


        private string senderEmail;

        public string SenderEmail
        {
            get { return senderEmail; }
            set { senderEmail = value; }
        }



        private string absUID;

        public string AbsUID
        {
            get { return absUID; }
            set { absUID = value; }
        }


        private bool isAbsSecureVerified;

        public bool IsAbsSecureVerified
        {
            get { return isAbsSecureVerified; }
            set { isAbsSecureVerified = value; }
        }

        private string emailContent;

        public string EmailContent
        {
            get { return emailContent; }
            set { emailContent = value; }
        }




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
