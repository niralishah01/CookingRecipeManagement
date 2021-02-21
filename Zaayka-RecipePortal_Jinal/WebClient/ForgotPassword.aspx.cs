using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ServiceReference2.AccountServiceClient proxy = new ServiceReference2.AccountServiceClient("BasicHttpBinding_IAccountService");
            string result = proxy.SendMail(TextBox1.Text);
            Label3.Text = result;
        }
    }
}