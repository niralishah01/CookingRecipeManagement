using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountServiceReference.AccountServiceClient client = new AccountServiceReference.AccountServiceClient();
            RecipeServiceReference.RecipeServiceClient proxy = new RecipeServiceReference.RecipeServiceClient();
            RecipeServiceReference.Recipe[] recipeList = proxy.GetAllRecipes(-1);
            int r=0,c=0;
            HtmlGenericControl row = new HtmlGenericControl("div");
            row.ID = "row" + (++r).ToString();
            row.Attributes.Add("runat", "server");
            row.Attributes.Add("class", "row");
            row.Visible = true;
            if (recipeList != null)
            {
                int i;
                for (i = 0; i < recipeList.Length; i++)
                {
                    HtmlGenericControl col = new HtmlGenericControl("div");
                    col.ID = "col" + (++c).ToString();
                    col.Attributes.Add("runat", "server");
                    col.Attributes.Add("class", "col-xl-3 p-1");
                    col.Visible = true;

                    HtmlGenericControl card = new HtmlGenericControl("div");
                    card.ID = (recipeList[i].Id).ToString();
                    card.Attributes.Add("runat", "server");
                    card.Attributes.Add("class", "card");
                    card.Attributes.Add("style","width:300px");
                    card.Visible = true;

                    HtmlGenericControl card_h = new HtmlGenericControl("div");      //Card-header
                    card_h.Attributes.Add("class", "card-header bg-light");
                    card_h.InnerText = (recipeList[i].Title).ToString();

                    HtmlGenericControl card_b = new HtmlGenericControl("div");      //Card-body
                    card_b.Attributes.Add("class", "card-body");
                    Image img = new Image();
                    img.ImageUrl = "~/Images/" + (recipeList[i].Image).ToString();
                    img.Attributes.Add("class","card-img-top imgThumbnail");
                    img.Attributes.Add("alt","No Image Available");
                    card_b.Controls.Add(img);

                    HtmlGenericControl card_f = new HtmlGenericControl("div");      //Card-footer
                    card_f.Attributes.Add("class", "card-footer text-center");

                    card_h.Visible = true; card_b.Visible = true; card_f.Visible = true;

                    AccountServiceReference.Users user = client.GetUserDetail(Convert.ToInt32(recipeList[i].UserID));
                    card_f.InnerHtml = "By " + user.name;

                    card.Controls.Add(card_h);
                    card.Controls.Add(card_b);
                    card.Controls.Add(card_f);

                    col.Controls.Add(card);
                    row.Controls.Add(col);
                    if( (i+1) % 4 == 0 )
                    {
                        Panel1.Controls.Add(row);
                        row = new HtmlGenericControl("div");
                        row.ID = "row" + (++r).ToString();
                        row.Attributes.Add("runat", "server");
                        row.Attributes.Add("class", "row");
                        row.Visible = true;
                    }
                }
                if(i%4 != 0)
                    Panel1.Controls.Add(row);
            }
        }
        protected void getRecipe(object sender, EventArgs e,string ID)
        {
            Response.Redirect("GetRecipe.aspx?Id="+ID);
        }
    }
}
