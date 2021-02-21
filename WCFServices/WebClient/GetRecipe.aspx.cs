using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class GetRecipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["userid"] != null)
            {
                if (Request.QueryString["b"] != null)
                {
                    if (Request.QueryString["b"] == "h")
                        back.HRef = "home.aspx?userid=" + Request.QueryString["userid"];
                    if(Request.QueryString["b"] == "m")
                        back.HRef = "MyRecipes.aspx?userid=" + Request.QueryString["userid"];
                }
                RecipeServiceReference.RecipeServiceClient client = new RecipeServiceReference.RecipeServiceClient("BasicHttpBinding_IRecipeService");
                TableCell c; TableRow r;
                string madeby = "";
                try
                {
                    RecipeServiceReference.Recipe recipe = client.GetRecipe(Convert.ToInt32(Request.QueryString["rid"]));
                    card_h.InnerText = recipe.Title;

                    AccountServiceReference.AccountServiceClient account_proxy = new AccountServiceReference.AccountServiceClient();
                    AccountServiceReference.Users current_user = account_proxy.GetUserDetail(Convert.ToInt32(recipe.UserID));
                    madeby = current_user.name;
                    account_proxy.Close();

                    Image img = new Image();
                    img.ImageUrl = "~/Images/" + recipe.Image;
                    img.Attributes.Add("class", "imgThumbnail");
                    card_b.Controls.Add(img);
                    Table t = new Table();
                    t.Attributes.Add("class", "table table-striped table-hover");

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Made by";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = madeby;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Ingredients";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.Ingredients;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Method";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.Method;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Category";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.Category;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Other details";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.Otherdetails;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    card_b.Controls.Add(t);
                }
                catch (Exception ex)
                {
                    Response.Write("<b>An unhandled exception occured: " + ex.Message + "</b>");
                }
            }
        }
        protected void button2_Click(object sender, EventArgs e) // to Add Comment
        {
            Panel1.Visible = false;
            CommentServiceReference.Comments comment = new CommentServiceReference.Comments();
            string userId = Request.QueryString["userid"].ToString();
            string recipeId = Request.QueryString["rid"].ToString();
            string back = Request.QueryString["b"].ToString();
            comment.RecipeId = Convert.ToInt32(recipeId);
            comment.UserId = Convert.ToInt32(userId);
            comment.CommentText = TextArea1.Text;
            comment.Datetime = DateTime.Now;

            CommentServiceReference.CommentServiceClient client = new CommentServiceReference.CommentServiceClient();
            try
            {
                string msg = client.AddComment(comment);
                if (msg == "Error")
                {
                    Label1.Text = "<h5>Comment Not Added..ERROR!!</h5>";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                client.Close();
                Response.Redirect("GetRecipe.aspx?rid=" + recipeId + "&b=" + back + "&userid=" + userId);
            }
            catch (Exception ex)
            {
                Response.Write("<b>An unhandled exception occured: " + ex.Message + "</b>");
                client.Abort();
            }
        }

        protected void button3_Click(object sender, EventArgs e) //to Display Comments
        {
            Console.WriteLine("Inside Button3_click");
            string userId = Request.QueryString["userid"].ToString();
            string recipeId = Request.QueryString["rid"].ToString();
            string back = Request.QueryString["b"].ToString();


            if (button3.Text == "Show Comments")
            {
                Panel1.Visible = true;
                button3.Text = "Hide Comments";
                CommentServiceReference.CommentServiceClient client = new CommentServiceReference.CommentServiceClient("BasicHttpBinding_ICommentService");
                AccountServiceReference.AccountServiceClient acc_client = new AccountServiceReference.AccountServiceClient("BasicHttpBinding_IAccountService");
                try
                {
                    CommentServiceReference.Comments[] comments = client.ViewComments(Convert.ToInt32(recipeId));
                    if (comments != null)
                    {
                        foreach (CommentServiceReference.Comments comment in comments)
                        {
                            Console.WriteLine("Comment Found: (Id:{0})", comment.Id);
                            AccountServiceReference.Users user = acc_client.GetUserDetail(comment.UserId); //get user details of comment writer

                            HtmlGenericControl media = new HtmlGenericControl("div");
                            media.ID = comment.Id.ToString();
                            media.Attributes.Add("runat", "server");
                            media.Attributes.Add("class", "media");
                            media.Visible = true;

                            HtmlGenericControl mediaBody = new HtmlGenericControl("div");
                            mediaBody.ID = "comment" + comment.Id.ToString();
                            mediaBody.Attributes.Add("runat", "server");
                            mediaBody.Attributes.Add("class", "media-body");

                            HtmlGenericControl h4 = new HtmlGenericControl("h5");
                            h4.Attributes.Add("class", "media-heading");
                            h4.InnerText = user.name + "\t";
                            h4.Visible = true;

                            HtmlGenericControl smallText = new HtmlGenericControl("small");
                            smallText.Visible = true;
                            h4.Controls.Add(smallText);

                            HtmlGenericControl i_datetime = new HtmlGenericControl("i");
                            i_datetime.InnerText = comment.Datetime.ToString();
                            i_datetime.Visible = true;
                            smallText.Controls.Add(i_datetime);

                            HtmlGenericControl para = new HtmlGenericControl("p");
                            para.InnerText = comment.CommentText;
                            para.Visible = true;

                            mediaBody.Controls.Add(h4);
                            mediaBody.Controls.Add(para);
                            mediaBody.Visible = true;

                            HtmlGenericControl hr = new HtmlGenericControl("hr");
                            hr.Visible = true;
                            Panel1.Controls.Add(hr);

                            media.Controls.Add(mediaBody);
                            Panel1.Controls.Add(media);

                        }
                        acc_client.Close();
                    }
                    else
                    {
                        Label2.Text = "<h5>No Comments Available..!!</h5>";
                        Label2.ForeColor = System.Drawing.Color.Red;
                    }
                    client.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("<b>An unhandled exception occured: " + ex.Message +"<br>"+ex.StackTrace+ "</b>");
                    acc_client.Abort();
                    client.Abort();
                }
            }
            else if (button3.Text == "Hide Comments")
            {
                Panel1.Visible = false;
                button3.Text = "Show Comments";
            }

        }
    }
}