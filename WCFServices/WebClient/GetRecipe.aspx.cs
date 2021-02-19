using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class GetRecipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["Id"];
            RecipeServiceReference.RecipeServiceClient client = new RecipeServiceReference.RecipeServiceClient("BasicHttpBinding_IRecipeService");
            TableCell c; TableRow r;
            try
            {
                RecipeServiceReference.Recipe recipe = client.GetRecipe(Convert.ToInt32(ID));
                card_h.InnerText = recipe.Title;
                Image img = new Image();
                img.ImageUrl = "~/Images/" + recipe.Image;
                card_b.Controls.Add(img);
                Table t = new Table();
                t.Attributes.Add("class", "table table-striped table-hover");

                r = new TableRow();
                c = new TableCell(); c.Text = "Made by";
                r.Cells.Add(c);
                c = new TableCell(); c.Text = recipe.UserID.ToString();
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
}