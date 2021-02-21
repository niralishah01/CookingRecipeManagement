using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.ServiceReference1;
using WebClient.ServiceReference2;

namespace WebClient
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                string name = TextBox1.Text;
                string email = TextBox2.Text;
                string password = TextBox3.Text;
                ServiceReference2.AccountServiceClient proxy = new ServiceReference2.AccountServiceClient("BasicHttpBinding_IAccountService");
                Users newuser = new Users();
                newuser.name = name;
                //u. = name;
                newuser.email = email;
                newuser.password = password;
                //new Users(name, email, password)
                string msg = proxy.Register(newuser);
                if (msg == "Registered Successfully..")
                    Response.Redirect("~/Login.aspx");
                else
                    Label4.Text = "*" + msg;
            }
        }
            
    }
}