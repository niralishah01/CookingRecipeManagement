using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.ServiceReference1;

namespace WebClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email=TextBox1.Text;
            string password = TextBox2.Text;
            WebClient.ServiceReference1.AccountServiceClient proxy = new ServiceReference1.AccountServiceClient("BasicHttpBinding_IAccountService");
            Users loguser=proxy.Login(email,password);
            if (loguser == null)
                Label3.Text = "Invalid Email or password, please enter your registerd email and password only..";
            else
                //Label3.Text = loguser.name;
                Response.Redirect("~/Index.aspx?user="+loguser);
        }
    }
}