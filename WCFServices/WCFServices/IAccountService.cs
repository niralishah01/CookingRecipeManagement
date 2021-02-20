using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountService" in both code and config file together.
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        string Register(Users u);
        [OperationContract]
        Users Login(string email, string password);

        [OperationContract]
        void UpdateUserDetails(Users u);
        [OperationContract]
        Users GetUserDetail(int ID);
        [OperationContract]
        DataSet GetUsers();
        [OperationContract]
        void DeleteUser(int ID);
        [OperationContract]
        void Logout();
        [OperationContract]
        string SendMail(string emailid);
        [OperationContract]
        string RedirectToResetPassword(string ucode, string emailid);
        [OperationContract]
        string ResetPassword(string ucode, string emailid, string password);
    }
}
