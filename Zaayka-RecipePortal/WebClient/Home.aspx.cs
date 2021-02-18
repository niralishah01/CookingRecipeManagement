using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.ServiceReference1;

namespace WebClient
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.QueryString["name"];
            Label1.Text = "welcome " + name;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebClient.ServiceReference1.AccountServiceClient proxy = new ServiceReference1.AccountServiceClient("BasicHttpBinding_IAccountService");
            int id=Int32.Parse( Request.QueryString["userid"]);
            Users fetchu=proxy.GetUserDetail(id);

        }
    }
}