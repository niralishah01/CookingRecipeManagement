using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class AddRecipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int index = 0;
            string userId = Request.QueryString["userid"];
            if (userId == null)
            {
                Response.Redirect("Login.aspx");
            }

            myrecipes.HRef = "MyRecipes.aspx?userid=" + userId;
            myhome.HRef = "home.aspx?userid=" + userId;
            if (!Page.IsPostBack)
            {
                ServiceReference1.Recipe recipe = new ServiceReference1.Recipe();
                foreach(string name in Enum.GetNames(typeof(Recipe_Category.Category)))
                {
                    ListItem item = new ListItem(name,(index++).ToString());
                    category.Items.Add(item);
                }
            }
        }

        protected void button_Click(object sender, EventArgs e)
        {
            string userId = Request.QueryString["userid"];
            if (category.SelectedItem.Value!= "-1")
            {
                ServiceReference1.Recipe recipe = new ServiceReference1.Recipe();
                recipe.Title = title.Value;
                recipe.Ingredients = ingredients.Value;
                recipe.Method = method.Value;
                recipe.Category = category.SelectedItem.Text;
                recipe.Otherdetails = other.Value;
                recipe.UserID = Convert.ToInt32(userId); //current logged-in user's Id
                recipe.Image = null;
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

                ServiceReference1.RecipeServiceClient client = new ServiceReference1.RecipeServiceClient("BasicHttpBinding_IRecipeService");

                try
                {
                    string status = client.AddRecipe(recipe);
                    if (status == "Done")
                    {
                        Label1.Text = "<h5>Recipe added successfully..</h5>";
                        Label1.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        Label1.Text = "<h5>Recipe not added..ERROR!!</h5>";
                        Label1.ForeColor = System.Drawing.Color.Red;
                    }
                    client.Close();
                    Response.Redirect("MyRecipes.aspx?userid="+userId);
                }
                catch (Exception ex)
                {
                    Label1.Text += "<b>An unhandled exception occured: " + ex.Message + "</b>";
                    client.Abort();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('Please Select Valid Category');", true);
            }
        }
    }
}