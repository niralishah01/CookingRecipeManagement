using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in both code and config file together.
    public class AccountService : IAccountService
    {
        public string Register(Users u)
        {
            string result = "";
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string command = "insert into Users(name,email,password) values(@name,@email,@password)";
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@name", u.name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                using (conn)
                {
                    cmd.ExecuteNonQuery();
                    result = "Registered Successfully..";
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }
        /*Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False*/
        public Users Login(string email,string password)
        {
            Users fetchu = new Users();
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string command = "select * from Users where email=@email and password=@password";
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    fetchu.ID = rdr.GetInt32(0);
                    fetchu.name = rdr.GetString(1);
                    fetchu.email = rdr.GetString(2);
                    fetchu.password = rdr.GetString(3);
                }
                rdr.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return fetchu;
        }
        public void DeleteUser(int ID)
        {
            
        }
        public Users GetUserDetail(int ID)
        {
            Users user = new Users();
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string command = "select * from Users where ID=@id";
                SqlCommand cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                using (conn)
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        user.name = rdr.GetString(1);
                        user.email = rdr.GetString(2);
                        user.password = rdr.GetString(3);
                    }
                    rdr.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return user;
        }
        public DataSet GetUsers()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                string command = "select * from Users ";
                //SqlCommand cmd = new SqlCommand(command, conn);
                SqlDataAdapter sda = new SqlDataAdapter(command, conn);
                sda.Fill(ds, "Users");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return ds;
        }
        public void UpdateUserDetails(Users u)
        {
            
        }
    }
}
