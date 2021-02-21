using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in both code and config file together.
    public class AccountService : IAccountService
    {
        static string cs = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
        SqlCommand cmd=null;
        string command;
        SqlConnection conn=new SqlConnection(cs);
        public string Register(Users u)
        {
            string result = "";
            try
            {
                command = "select * from Users where email=@email";
                cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@email", u.email);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = "User is already registerd..";
                    rdr.Close();

                    return result;
                }
                else
                {
                    conn.Close();
                    command = "insert into Users(name,email,password) values(@name,@email,@password)";
                    cmd = new SqlCommand(command, conn);
                    cmd.Parameters.AddWithValue("@name", u.name);
                    cmd.Parameters.AddWithValue("@email", u.email);
                    cmd.Parameters.AddWithValue("@password", u.password);
                    using (conn)
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        result = "Registered Successfully..";
                    }
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
                command = "select * from Users where email=@email and password=@password";
                cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        fetchu.ID = rdr.GetInt32(0);
                        fetchu.name = rdr.GetString(1);
                        fetchu.email = rdr.GetString(2);
                        fetchu.password = rdr.GetString(3);
                    }
                }
                else
                    fetchu = null;
                rdr.Close();
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                //fetchu = null;
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
                command = "select * from Users where ID=@id";
                cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                using (conn)
                {
                    conn.Open();
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
                command = "select * from Users ";
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
            Users user = new Users();
            try
            {
                command = "update Users set name=@name,email=@email where ID=@id";
                cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@id", u.ID);
                cmd.Parameters.AddWithValue("@name", u.name);
                cmd.Parameters.AddWithValue("@email", u.email);
                using (conn)
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //return user;
        }

        public  void Logout()
        {
            //throw new NotImplementedException();
        }

    }
}
