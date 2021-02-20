using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AccountServiceReference.AccountServiceClient proxy = new AccountServiceReference.AccountServiceClient("BasicHttpBinding_IAccountService");
                string email = Request.QueryString["emailid"];
                string ucode = Request.QueryString["ucode"];
                string result = proxy.RedirectToResetPassword(ucode, email);
                if (result.Equals("success"))
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Label3.Text = result;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string pwd = TextBox1.Text;
                AccountServiceReference.AccountServiceClient proxy = new AccountServiceReference.AccountServiceClient("BasicHttpBinding_IAccountService");
                string emailid = Request.QueryString["emailid"];
                string ucode = Request.QueryString["ucode"];
                string result = proxy.ResetPassword(ucode, emailid, pwd);
                if (result == "success")
                {
                    Label2.Text = "Your password has been updated successfully.";
                }
                else
                {
                    Label3.Text = result;
                }
                HyperLink1.Visible = true;
            }
        }
    }
}