using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class UpdateRecipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int index = 0;
            char[] delim = { '\\', '/' };
            if (!Page.IsPostBack)
            {
                string userId = Request.QueryString["userid"];
                if(userId==null)
                {
                    Response.Redirect("Login.aspx");
                }
                back.HRef = "MyRecipes.aspx?userid=" + userId;
                ServiceReference1.RecipeServiceClient client = new ServiceReference1.RecipeServiceClient("BasicHttpBinding_IRecipeService");
                int Id= Convert.ToInt32(Request.QueryString["rid"]);
                try
                {
                    ServiceReference1.Recipe recipe = client.EditRecipe(Id);

                    if(recipe.Id!=0)
                    {
                        if (recipe.Image != null)
                        {
                            recipe.Image = "/Images/" + recipe.Image; //extract only image filename
                        }
                        recipeId.Value = recipe.Id.ToString();
                        title.Value = recipe.Title;
                        ingredients.Value = recipe.Ingredients;
                        method.Value = recipe.Method;
                        other.Value = recipe.Otherdetails;
                        photo.Src = recipe.Image;

                        foreach (string name in Enum.GetNames(typeof(Recipe_Category.Category)))
                        {
                            ListItem item = new ListItem(name, (index++).ToString());
                            if (recipe.Category == name)
                            {
                                item.Selected = true;
                            }
                            category.Items.Add(item);
                        }
                    }
                    else
                    {
                        Label1.Text = "<h5>Recipe Not Found..!!</h5>";
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                     
                }
                catch(Exception ex)
                {
                    Response.Write("<b>An unhandled exception occured: " + ex.Message + "</b>");
                }
            }
            
        }

        protected void button_Click(object sender, EventArgs e)
        {
            ServiceReference1.Recipe recipe = new ServiceReference1.Recipe();
            recipe.Id = Convert.ToInt32(recipeId.Value);
            recipe.Title = title.Value;
            recipe.Ingredients = ingredients.Value;
            recipe.Method = method.Value;
            recipe.Category = category.SelectedItem.Text;
            recipe.Otherdetails = other.Value;

            ServiceReference1.RecipeServiceClient client = new ServiceReference1.RecipeServiceClient("BasicHttpBinding_IRecipeService");

            try
            {
                string status = client.UpdateRecipe(recipe);
                if (status == "Done")
                {
                    Label1.Text = "<h5>Recipe updated successfully..</h5>";
                    Label1.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Label1.Text = "<h5>Recipe not updated..ERROR!!</h5>";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                client.Close();
                Response.Redirect("GetRecipe.aspx?rid="+recipe.Id+"&b=m&userid=" + Request.QueryString["userid"]);
            }
            catch (Exception ex)
            {
                Label1.Text+= "<b>An unhandled exception occured: " + ex.Message + "</b>";
                client.Abort();
            }

        }
    }
}