using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsSecure_V1._2
{
    class Client
    {
        private int uid;

        public int UID
        {
            get { return uid; }
            set { uid = value; }
        }

        private int sendID;

        public int SenderID
        {
            get { return sendID; }
            set { sendID = value; }
        }

        private int recpID;

        public int RecpID
        {
            get { return recpID; }
            set { recpID = value; }
        }

        private List<int> CompIDList = new List<int>();

        public List<int> MyProperty
        {
            get { return CompIDList; }
            set { CompIDList = value; }
        }



        public Client()
        {

        }

        public Client(int UserID, int SenderCompID)
        {
            UID = UserID;
            SenderID = SenderCompID;
        }

        public override string ToString()
        {
            return "Company UID: " + SenderID.ToString() + "\nEmployee UID: " + UID.ToString();
        }
    }
}
