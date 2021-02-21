using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class UpdateRecipe : System.Web.UI.Page
    {
        //static string oldImage;
        char[] delim = { '\\', '/' };
        protected void Page_Load(object sender, EventArgs e)
        {
            int index = 0;
            if (!Page.IsPostBack)
            {
                string userId = Request.QueryString["userid"];
                if (userId == null)
                {
                    Response.Redirect("Login.aspx");
                }
                back.HRef = "MyRecipes.aspx?userid=" + userId;
                RecipeServiceReference.RecipeServiceClient client = new RecipeServiceReference.RecipeServiceClient("BasicHttpBinding_IRecipeService");
                int Id = Convert.ToInt32(Request.QueryString["rid"]);
                try
                {
                    RecipeServiceReference.Recipe recipe = client.EditRecipe(Id);

                    if (recipe.Id != 0)
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
                catch (Exception ex)
                {
                    Response.Write("<b>An unhandled exception occured: " + ex.Message + "</b>");
                }
            }

        }

        protected void button_Click(object sender, EventArgs e)
        {
            RecipeServiceReference.Recipe recipe = new RecipeServiceReference.Recipe();
            recipe.Id = Convert.ToInt32(recipeId.Value);
            recipe.Title = title.Value;
            recipe.Ingredients = ingredients.Value;
            recipe.Method = method.Value;
            recipe.Category = category.SelectedItem.Text;
            recipe.Otherdetails = other.Value;
            if (FileUpload1.HasFile)
            {
                string fileName = FileUpload1.FileName;
                string folderPath = Server.MapPath("~\\Images\\");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                folderPath += fileName;
                FileUpload1.SaveAs(folderPath);
                recipe.Image = fileName; //store imagePath to image field
            }
            else
            {
                string[] imgParts = photo.Src.Split(delim);
                recipe.Image = imgParts.Last();
            }
            RecipeServiceReference.RecipeServiceClient client = new RecipeServiceReference.RecipeServiceClient("BasicHttpBinding_IRecipeService");

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
                Response.Redirect("GetRecipe.aspx?rid=" + recipe.Id + "&b=m&userid=" + Request.QueryString["userid"]);
            }
            catch (Exception ex)
            {
                Label1.Text += "<b>An unhandled exception occured: " + ex.Message + "</b>";
                client.Abort();
            }

        }
    }
}