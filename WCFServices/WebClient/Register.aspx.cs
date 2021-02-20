using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string name = TextBox1.Text;
                string email = TextBox2.Text;
                string password = TextBox3.Text;
                AccountServiceReference.AccountServiceClient proxy = new AccountServiceReference.AccountServiceClient("BasicHttpBinding_IAccountService");
                AccountServiceReference.Users newuser = new AccountServiceReference.Users();
                newuser.name = name;
                //u. = name;
                newuser.email = email;
                newuser.password = password;
                //new Users(name, email, password)
                string msg = proxy.Register(newuser);
                if (msg == "Registered Successfully..")
                    Response.Redirect("~/Index.aspx");
                else
                    Label4.Text = "*" + msg;
            }
        }
    }
}