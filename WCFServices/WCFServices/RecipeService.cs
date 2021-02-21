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

        public bool DeleteRecipe(int id)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: " + conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "delete from recipes where ID=@Id";
            SqlParameter para = new SqlParameter("@Id", id);
            cmd.Parameters.Add(para);
            try
            {
                using (conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    int rowaffected = cmd.ExecuteNonQuery();
                    if (rowaffected == 1)
                    {
                        Console.WriteLine("The database contains some rows.");
                        return true;
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
            Console.WriteLine("Connection disconnected, Connection State: " + conn.State);
            return false;
    }

        public List<Recipe> GetAllRecipes(int userid)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: "+ conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from recipes where userID != @uId";
            cmd.Parameters.AddWithValue("@uID", userid);
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

        public List<Recipe> GetUserSpecificRecipes(int id)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            Console.WriteLine("Connection established, Connecction state: " + conn.State);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from recipes where userid=@Id";
            SqlParameter para = new SqlParameter("@Id",id);
            cmd.Parameters.Add(para);
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using(conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
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
            Console.WriteLine("Connection disconnected, Connection State: " + conn.State);
            return recipes;
        }

        public List<Recipe> Search(string searchText)
        {
            string newconnectionstring = connectionstring + ";MultipleActiveResultSets=true;";
            Console.WriteLine(newconnectionstring);
            List<Recipe> recipes = new List<Recipe>();
            SqlConnection conn = new SqlConnection(newconnectionstring);
            Console.WriteLine("Connection established, Connecction state: " + conn.State);

            //Search recipes based on username
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "select * from users where name=@Name";
            SqlParameter para1 = new SqlParameter("@Name", searchText);
            cmd1.Parameters.Add(para1);

            //Search recipes based on title
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select * from recipes where title=@Title";
            SqlParameter para2 = new SqlParameter("@Title", searchText);
            cmd2.Parameters.Add(para2);

            //Search recipes based on Category
            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = "select * from recipes where category=@Category";
            SqlParameter para3 = new SqlParameter("@Category", searchText);
            cmd3.Parameters.Add(para3);
            int count = 0;
            try
            {
                using (conn)
                {
                    conn.Open();
                    Console.WriteLine("Connection is opened, Connection state: " + conn.State);
                    SqlDataReader rdr1 = cmd1.ExecuteReader();
                    if (rdr1.HasRows)
                    {
                        Console.WriteLine("The database contains some rows based on user");
                        Recipe recipe;
                        while (rdr1.Read())
                        {
                            SqlCommand cmd4 = new SqlCommand();
                            cmd4.Connection = conn;
                            cmd4.CommandText = "select * from recipes where userid=@Userid";
                            SqlParameter para4 = new SqlParameter("@Userid", rdr1["ID"]);
                            cmd4.Parameters.Add(para4);
                            SqlDataReader rdr4 = cmd4.ExecuteReader();
                            while (rdr4.Read())
                            {
                                recipe = new Recipe();
                                recipe.Id = Convert.ToInt32(rdr4["ID"]);
                                recipe.Title = rdr4["title"].ToString();
                                recipe.Ingredients = rdr4["ingredients"].ToString();
                                recipe.Method = rdr4["method"].ToString();
                                recipe.Image = rdr4["image"].ToString();
                                recipe.Category = rdr4["category"].ToString();
                                recipe.Otherdetails = rdr4["otherdetails"].ToString();
                                recipe.UserID = Convert.ToInt32(rdr4["userID"]);
                                recipe.Likes = Convert.ToInt32(rdr4["likes"]);
                                recipe.Dislikes = Convert.ToInt32(rdr4["dislikes"]);
                                recipes.Add(recipe);
                                count++;
                            }
                            rdr4.Close();
                        }
                        rdr1.Close();
                    }
                    SqlDataReader rdr2 = cmd2.ExecuteReader();
                    if(rdr2.HasRows)
                    {
                        Console.WriteLine("The database does not contain rows based on title");
                        Recipe recipe;
                        while (rdr2.Read())
                        {
                            recipe = new Recipe();
                            recipe.Id = Convert.ToInt32(rdr2["ID"]);
                            recipe.Title = rdr2["title"].ToString();
                            recipe.Ingredients = rdr2["ingredients"].ToString();
                            recipe.Method = rdr2["method"].ToString();
                            recipe.Image = rdr2["image"].ToString();
                            recipe.Category = rdr2["category"].ToString();
                            recipe.Otherdetails = rdr2["otherdetails"].ToString();
                            recipe.UserID = Convert.ToInt32(rdr2["userID"]);
                            recipe.Likes = Convert.ToInt32(rdr2["likes"]);
                            recipe.Dislikes = Convert.ToInt32(rdr2["dislikes"]);
                            recipes.Add(recipe);
                            count++;
                        }
                        rdr2.Close();
                    }
                    SqlDataReader rdr3 = cmd3.ExecuteReader();
                    if(rdr3.HasRows)
                    {
                        Console.WriteLine("The database does not contain rows based on category");
                        Recipe recipe;
                        while (rdr3.Read())
                        {
                            recipe = new Recipe();
                            recipe.Id = Convert.ToInt32(rdr3["ID"]);
                            recipe.Title = rdr3["title"].ToString();
                            recipe.Ingredients = rdr3["ingredients"].ToString();
                            recipe.Method = rdr3["method"].ToString();
                            recipe.Image = rdr3["image"].ToString();
                            recipe.Category = rdr3["category"].ToString();
                            recipe.Otherdetails = rdr3["otherdetails"].ToString();
                            recipe.UserID = Convert.ToInt32(rdr3["userID"]);
                            recipe.Likes = Convert.ToInt32(rdr3["likes"]);
                            recipe.Dislikes = Convert.ToInt32(rdr3["dislikes"]);
                            recipes.Add(recipe);
                            count++;
                        }
                        rdr3.Close();
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
            foreach(Recipe r in recipes)
            {
                Console.WriteLine(r.Title);
            }
            Console.WriteLine("Total count:"+Convert.ToInt32(count));
            return recipes;
        }

        public string AddRecipe(Recipe Recipe)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Insert into Recipes ( title, ingredients, method, category, image, otherdetails, userID ) values (@title, @ingredients, @method, @category, @image, @otherdetails, @userID) ";

                    cmd.Parameters.AddWithValue("@title", Recipe.Title); //add all parameters
                    cmd.Parameters.AddWithValue("@ingredients", Recipe.Ingredients); //add all parameters
                    cmd.Parameters.AddWithValue("@method", Recipe.Method); //add all parameters
                    cmd.Parameters.AddWithValue("@category", Recipe.Category); //add all parameters
                    cmd.Parameters.AddWithValue("@otherdetails", Recipe.Otherdetails); //add all parameters
                    cmd.Parameters.AddWithValue("@userID", Recipe.UserID); //add all parameters
                    cmd.Parameters.AddWithValue("@image", Recipe.Image); //add all parameters
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows != 0)
                        return "Done";
                    else
                        return "Error";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Recipe EditRecipe(int Id)
        {
            Recipe recipe = new Recipe();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from Recipes where Id= @Id"; //Get All recipes
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        recipe.Id = Convert.ToInt32(rd["ID"]);
                        recipe.Title = rd["title"].ToString();
                        recipe.Ingredients = rd["ingredients"].ToString();
                        recipe.Method = rd["method"].ToString();
                        recipe.Otherdetails = rd["otherdetails"].ToString();
                        recipe.Category = rd["category"].ToString();
                        recipe.Image = rd["image"].ToString();
                        recipe.UserID = Convert.ToInt32(rd["userID"]);
                    }
                    rd.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return recipe;
        }


        public string UpdateRecipe(Recipe Recipe)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Update Recipes set title= @title, ingredients= @ingredients, method= @method, category= @category, otherdetails= @otherdetails, image= @image where Id= @Id";
                    cmd.Parameters.AddWithValue("@Id", Recipe.Id); //add all parameters
                    cmd.Parameters.AddWithValue("@title", Recipe.Title); //add all parameters
                    cmd.Parameters.AddWithValue("@ingredients", Recipe.Ingredients); //add all parameters
                    cmd.Parameters.AddWithValue("@method", Recipe.Method); //add all parameters
                    cmd.Parameters.AddWithValue("@category", Recipe.Category); //add all parameters
                    cmd.Parameters.AddWithValue("@otherdetails", Recipe.Otherdetails); //add all parameters
                    cmd.Parameters.AddWithValue("@image", Recipe.Image); //add all parameters

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows != 0)
                        return "Done";
                    else
                        return "Error";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
