using System;
using System.Collections.Generic;
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
        SqlCommand cmd = null;
        string command;
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public string Register(Users u)
        {
            string result = "";
            try
            {
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
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }
        /*Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RecipeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False*/
        public Users Login(string email, string password)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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

        public void Logout()
        {
            //throw new NotImplementedException();
        }

        public string SendMail(string emailid)
        {
            //throw new NotImplementedException();
            string ucode = string.Empty;
            string result = string.Empty;
            try
            {
                string command = "select * from Users where email=@email";
                cmd = new SqlCommand(command, conn);
                cmd.Parameters.AddWithValue("@email", emailid);
                using (conn)
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        ucode = Convert.ToString(System.Guid.NewGuid());
                        cmd = new SqlCommand("update Users set uniquecode=@uniquecode where email=@email", conn);
                        cmd.Parameters.AddWithValue("@uniquecode", ucode);
                        cmd.Parameters.AddWithValue("@email", emailid);
                        StringBuilder strBody = new StringBuilder();
                        strBody.Append("<a href=http://localhost:44373/ResetPassword.aspx?emailid=" + emailid + "&ucode=" + ucode + ">Click here to change the password</a>");
                        MailMessage mail = new MailMessage("henapatel2000.data@gmail.com", emailid, "Reset your Password", strBody.ToString());
                        System.Net.NetworkCredential mailAuth = new System.Net.NetworkCredential("henapatel2000.data@gmail.com", "48he30na");
                        SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);
                        mailclient.EnableSsl = true;
                        mailclient.DeliveryMethod=SmtpDeliveryMethod.Network;
                        mailclient.UseDefaultCredentials = false;
                        mailclient.Credentials = mailAuth;
                        mail.IsBodyHtml = true;
                        mailclient.Send(mail);
                        rdr.Close();
                        cmd.ExecuteNonQuery();
                        result = "Reset Password link has been sent to your email address";
                    }
                    else
                    {
                        result = "Please enter valid email address.";
                    }
                }
            }
            catch(Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        public string RedirectToResetPassword(string ucode, string emailid)
        {
            //throw new NotImplementedException();
            string result = string.Empty;
            try
            {
                cmd = new SqlCommand("select email,uniquecode from Users where uniquecode=@uniquecode and email=@email", conn);
                cmd.Parameters.AddWithValue("@uniquecode", ucode);
                cmd.Parameters.AddWithValue("@email", emailid);
                using (conn)
                {
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    {
                        result = "success";
                    }
                    else
                    {
                        result = "Reset Passwordlink has expired.it was for one time use only.";
                    }
                    rdr.Close();
                }
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }

        public string ResetPassword(string ucode, string emailid, string password)
        {
            //throw new NotImplementedException();
            string result = string.Empty;
            try
            {
                cmd = new SqlCommand("update Users set uniquecode='',password=@password where uniquecode=@uniquecode and email=@email",conn);
                cmd.Parameters.AddWithValue("@uniquecode", ucode);
                cmd.Parameters.AddWithValue("@email", emailid);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = "success";
            }
            catch(Exception ex)
            {
                result = ex.Message.ToString();
            }
            return result;
        }
    }
}
