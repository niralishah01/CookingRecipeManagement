using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebClient.ServiceReference1;

namespace WebClient
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountServiceClient acc_client = new AccountServiceClient("BasicHttpBinding_IAccountService");
            RecipeServiceReference.RecipeServiceClient proxy = new RecipeServiceReference.RecipeServiceClient("BasicHttpBinding_IRecipeService");
            RecipeServiceReference.Recipe[] recipeList = proxy.GetAllRecipes(0);
            int r = 0, c = 0;
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
                    if (recipeList[i].Image != null)
                    {
                        //string[] imageParts = recipeList[i].Image.Split(delim);
                        recipeList[i].Image = "/Images/" + recipeList[i].Image; //extract only image filename
                    }
                    HtmlGenericControl col = new HtmlGenericControl("div");
                    col.ID = "col" + (++c).ToString();
                    col.Attributes.Add("runat", "server");
                    col.Attributes.Add("class", "col-xl-3 p-1");
                    col.Visible = true;

                    HtmlGenericControl card = new HtmlGenericControl("div");
                    card.ID = (recipeList[i].Id).ToString();
                    card.Attributes.Add("runat", "server");
                    card.Attributes.Add("class", "card");
                    card.Attributes.Add("style", "width:300px");
                    card.Visible = true;

                    HtmlGenericControl card_h = new HtmlGenericControl("div");      //Card-header
                    card_h.Attributes.Add("class", "card-header bg-light");
                    card_h.InnerText = (recipeList[i].Title).ToString();

                    HtmlGenericControl card_b = new HtmlGenericControl("div");      //Card-body
                    card_b.Attributes.Add("class", "card-body");
                    Image img = new Image();
                    img.ImageUrl = (recipeList[i].Image).ToString();
                    img.Attributes.Add("class", "card-img-top imgThumbnail");
                    img.Attributes.Add("alt", "No Image Available");
                    card_b.Controls.Add(img);

                    HtmlGenericControl card_f = new HtmlGenericControl("div");      //Card-footer
                    card_f.Attributes.Add("class", "card-footer text-center");

                    card_h.Visible = true; card_b.Visible = true; card_f.Visible = true;
                    
                    Users user = acc_client.GetUserDetail(recipeList[i].UserID); //get user details of comment writer
                    card_f.InnerText = "By " + user.name;
                    /*Button viewDetails = new Button();
                    viewDetails.Attributes.Add("rid", (recipeList[i].Id).ToString());
                    viewDetails.Text = "View Details";
                    viewDetails.Attributes.Add("class", "btn btn-primary align-self-center");
                    string s = ((recipeList[i].Id).ToString());
                    //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                    //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                    viewDetails.Click += new EventHandler((s1, e1) => getRecipe(sender, e, s));
                    viewDetails.Visible = true;

                    card_f.Controls.Add(viewDetails);*/
                    card.Controls.Add(card_h);
                    card.Controls.Add(card_b);
                    card.Controls.Add(card_f);

                    col.Controls.Add(card);
                    row.Controls.Add(col);
                    if ((i + 1) % 4 == 0)
                    {
                        Panel1.Controls.Add(row);
                        row = new HtmlGenericControl("div");
                        row.ID = "row" + (++r).ToString();
                        row.Attributes.Add("runat", "server");
                        row.Attributes.Add("class", "row");
                        row.Visible = true;
                    }
                }
                if (i % 4 != 0)
                    Panel1.Controls.Add(row);
            }
        }
        protected void getRecipe(object sender, EventArgs e, string ID)
        {
            Response.Redirect("GetRecipe.aspx?Id=" + ID);
        }
    }
}