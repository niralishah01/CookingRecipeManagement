using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CommentService" in both code and config file together.
    public class CommentService : ICommentService
    {
        string cs = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public string AddComment(Comments comment)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Insert into Comments (comment,userID,recipeID,datetime) values (@comment,@userID,@recipeID,@datetime)";

                    cmd.Parameters.AddWithValue("@comment", comment.CommentText); //add all parameters
                    cmd.Parameters.AddWithValue("@userId", comment.UserId); //add all parameters
                    cmd.Parameters.AddWithValue("@recipeId", comment.RecipeId); //add all parameters
                    cmd.Parameters.AddWithValue("@datetime", comment.Datetime); //add all parameters
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

        public IEnumerable<Comments> ViewComments(int recipeId)
        {
            List<Comments> comments = new List<Comments>();
            Comments comment = null;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from Comments where recipeID = @ID order by datetime desc"; //Get All Comments As per Latest ordering
                    cmd.Parameters.AddWithValue("@ID", recipeId);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            comment = new Comments();
                            comment.Id = Convert.ToInt32(rd["Id"]);
                            comment.CommentText = rd["comment"].ToString();
                            comment.UserId = Convert.ToInt32(rd["userID"]);
                            comment.RecipeId = recipeId;
                            comment.Datetime = Convert.ToDateTime(rd["datetime"]);
                            comments.Add(comment);
                        }
                    }
                    else
                    {
                        comments = null;
                    }
                    rd.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return comments;
        }
    }
}
