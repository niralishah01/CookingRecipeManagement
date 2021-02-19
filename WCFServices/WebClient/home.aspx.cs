using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["userid"] != null)
            {
                myrecipes.HRef = "MyRecipes.aspx?userid=" + Request.QueryString["userid"];
                myhome.HRef = "home.aspx?userid=" + Request.QueryString["userid"];

                AccountServiceReference.AccountServiceClient account_proxy = new AccountServiceReference.AccountServiceClient();
                AccountServiceReference.Users current_user = account_proxy.GetUserDetail(Convert.ToInt32(Request.QueryString["userid"]));
                title.InnerText = "Welcome "+ current_user.name;
                account_proxy.Close();

                RecipeServiceReference.RecipeServiceClient proxy = new RecipeServiceReference.RecipeServiceClient();
                RecipeServiceReference.Recipe[] recipeList = proxy.GetAllRecipes();
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
                        card_h.Visible = true;

                        HtmlGenericControl card_b = new HtmlGenericControl("div");      //Card-body
                        card_b.Attributes.Add("class", "card-body");
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                        img.ImageUrl = "~/Images/" + (recipeList[i].Image).ToString();
                        img.Attributes.Add("class", "card-img-top imgThumbnail");
                        img.Attributes.Add("alt", "No Image Available");
                        card_b.Controls.Add(img);
                        card_b.Visible = true;

                        Label likeLabel = new Label();
                        likeLabel.ForeColor = Color.FromArgb(50, 205, 50);
                        likeLabel.Text = "Likes: " + recipeList[i].Likes.ToString() + "\t";
                        Label dislikeLabel = new Label();
                        dislikeLabel.Text = "Dislikes: " + recipeList[i].Dislikes.ToString();
                        dislikeLabel.ForeColor = Color.FromArgb(255, 0, 56);
                        card_b.Controls.Add(likeLabel);
                        card_b.Controls.Add(dislikeLabel);

                        //< div class="btn-group" role="group" aria-label="Button group example"

                        HtmlGenericControl card_f = new HtmlGenericControl("div");      //Card-footer
                        card_f.Attributes.Add("class", "card-footer text-center");
                        card_f.Visible = true;

                        HtmlGenericControl buttonGroup = new HtmlGenericControl("div");     //Button-Group
                        buttonGroup.Attributes.Add("class", "btn-group");
                        buttonGroup.Attributes.Add("role", "group");

                        Button viewDetails = new Button();                                          //viewDetails button
                        viewDetails.Attributes.Add("rid", (recipeList[i].Id).ToString());
                        viewDetails.Text = "View Details";
                        viewDetails.Attributes.Add("class", "btn btn-outline-primary btn-sm");
                        string s = ((recipeList[i].Id).ToString());
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        viewDetails.Click += new EventHandler((s1, e1) => getRecipe(sender, e, s));
                        viewDetails.Visible = true;

                        Button like = new Button();                                          //likes button
                        like.Attributes.Add("rid", (recipeList[i].Id).ToString());
                        like.Attributes.Add("class", "btn btn-outline-success btn-sm");
                        /*HtmlGenericControl itag = new HtmlGenericControl("i");
                        itag.Attributes.Add("class","fas fa-thumbs-up");
                        like.Controls.Add(itag);
                        itag.InnerHtml = "iLike ";*/
                        //like.Attributes.Add("class", "btn btn-primary align-self-center");
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        like.Click += new EventHandler((s1, e1) => addLike(sender, e, s));
                        like.Text = "Like";
                        like.Visible = true;

                        Button dislike = new Button();                                          //dislikes button
                        dislike.Attributes.Add("rid", (recipeList[i].Id).ToString());
                        dislike.Attributes.Add("class", "btn btn-outline-danger btn-sm");
                        /*HtmlGenericControl itag2 = new HtmlGenericControl("i");
                        itag2.Attributes.Add("class", "fas fa-thumbs-up");
                        itag2.InnerHtml = "iDislike ";
                        dislike.Controls.Add(itag);*/
                        //like.Attributes.Add("class", "btn btn-primary align-self-center");
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        dislike.Click += new EventHandler((s1, e1) => addDislike(sender, e, s));
                        dislike.Text = "Dislike";
                        dislike.Visible = true;


                        buttonGroup.Controls.Add(viewDetails);
                        buttonGroup.Controls.Add(like);
                        buttonGroup.Controls.Add(dislike);
                        card_f.Controls.Add(buttonGroup);

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
                proxy.Close();
            }
        }
        protected void getRecipe(object sender, EventArgs e, string ID)
        {
            Response.Redirect("GetRecipe.aspx?userid=" + ID + "&b=h");
        }
        protected void addLike(object sender, EventArgs e, string ID)
        {
            RecipeServiceReference.RecipeServiceClient proxy = new RecipeServiceReference.RecipeServiceClient();
            bool isadded = proxy.AddLike(Convert.ToInt32(ID));
            /*if(isadded == true)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('You like it!!!');", true);
            }*/
            Response.Redirect("home.aspx");
            proxy.Close();
        }
        protected void addDislike(object sender, EventArgs e, string ID)
        {
            RecipeServiceReference.RecipeServiceClient proxy = new RecipeServiceReference.RecipeServiceClient();
            bool isadded = proxy.AddDislike(Convert.ToInt32(ID));
            /*if (isadded == true)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('You dislike it!!!');", true);
            }*/
            Response.Redirect("home.aspx");
            proxy.Close();
        }
    }
}