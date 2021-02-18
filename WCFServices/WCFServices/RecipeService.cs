using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RecipeService" in both code and config file together.
    public class RecipeService : IRecipeService
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public bool AddDislike(int id)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: " + conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from recipes where ID=@Id";
            SqlParameter para = new SqlParameter("@ID", id);
            cmd.Parameters.Add(para);
            try
            {
                using(conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr!= null)
                    {
                        rdr.Read();
                        cmd.CommandText = "update recipes set dislikes=@Dislikes where id = @Rid";
                        SqlParameter para1 = new SqlParameter("@Dislikes", Convert.ToInt32(rdr["dislikes"])+1);
                        rdr.Close();
                        SqlParameter para2 = new SqlParameter("@Rid", id);
                        cmd.Parameters.Add(para1);
                        cmd.Parameters.Add(para2);
                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                string Msg = "ERROR: " + err.ToString();
                Console.WriteLine(Msg);
                throw new Exception(err.Message);
            }
            Console.WriteLine("Connection disconnected, Connection State: " + conn.State);
            return false;
        }

        public bool AddLike(int id)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: " + conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from recipes where ID=@Id";
            SqlParameter para = new SqlParameter("@ID", id);
            cmd.Parameters.Add(para);
            try
            {
                using (conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                    {
                        rdr.Read();
                        cmd.CommandText = "update recipes set likes=@Likes where id = @Rid";
                        SqlParameter para1 = new SqlParameter("@Likes", Convert.ToInt32(rdr["likes"]) + 1);
                        rdr.Close();
                        SqlParameter para2 = new SqlParameter("@Rid", id);
                        cmd.Parameters.Add(para1);
                        cmd.Parameters.Add(para2);
                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                string Msg = "ERROR: " + err.ToString();
                Console.WriteLine(Msg);
                throw new Exception(err.Message);
            }
            Console.WriteLine("Connection disconnected, Connection State: " + conn.State);
            return false;
        }

        public List<Recipe> GetAllRecipes()
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: "+ conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from recipes";
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    {
                        Console.WriteLine("The database contains some rows.");
                        Recipe recipe;
                        while (rdr.Read())
                        {
                            recipe = new Recipe();
                            recipe.Id = Convert.ToInt32(rdr["ID"]);
                            recipe.Title = rdr["title"].ToString();
                            recipe.Ingredients = rdr["ingredients"].ToString();
                            recipe.Method = rdr["method"].ToString();
                            recipe.Image = rdr["image"].ToString();
                            recipe.Category = rdr["category"].ToString();
                            recipe.Otherdetails = rdr["otherdetails"].ToString();
                            recipe.UserID = Convert.ToInt32(rdr["userID"]);
                            recipe.Likes = Convert.ToInt32(rdr["likes"]);
                            recipe.Dislikes = Convert.ToInt32(rdr["dislikes"]);
                            recipes.Add(recipe);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The database does not contain rows.");
                    }
                }
            }
            catch (Exception err)
            {
                string Msg = "ERROR: " + err.ToString();
                Console.WriteLine(Msg);
                throw new Exception(err.Message);
            }
            Console.WriteLine("Connection disconnected, Connection State: "+conn.State);
            return recipes;
        }

        public Recipe GetRecipe(int id)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: " + conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from recipes where ID=@Id";
            SqlParameter para = new SqlParameter("@ID", id);
            cmd.Parameters.Add(para);
            Recipe recipe = null;
            try
            {
                using (conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    recipe = new Recipe();
                    while (rdr.Read())
                    {
                        recipe.Id = Convert.ToInt32(rdr["ID"]);
                        recipe.Title = rdr["title"].ToString();
                        recipe.Ingredients = rdr["ingredients"].ToString();
                        recipe.Method = rdr["method"].ToString();
                        recipe.Image = rdr["image"].ToString();
                        recipe.Category = rdr["category"].ToString();
                        recipe.Otherdetails = rdr["otherdetails"].ToString();
                        recipe.UserID = Convert.ToInt32(rdr["userID"]);
                        recipe.Likes = Convert.ToInt32(rdr["likes"]);
                        recipe.Dislikes = Convert.ToInt32(rdr["dislikes"]);
                    }
                    rdr.Close();
                }
            }
            catch (Exception err)
            {
                string Msg = "ERROR: " + err.ToString();
                Console.WriteLine(Msg);
                throw new Exception(err.Message);
            }
            Console.WriteLine("Connection disconnected, Connection State: " + conn.State);
            return recipe;
        }
    }
}
