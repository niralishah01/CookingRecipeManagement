using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFServices
{
    [DataContract]
    public class Users
    {
        private int userID;
        private string uname;
        private string emailid;
        private string passwd;
        [DataMember]
        public int ID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string name
        {
            get { return uname; }
            set { uname = value; }
        }
        [DataMember]
        public string email
        {
            get { return emailid; }
            set { emailid = value; }
        }
        [DataMember]
        public string password
        {
            get { return passwd; }
            set { passwd = value; }
        }
    }
}
