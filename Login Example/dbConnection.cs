using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Example
{
    public class dbConnection
    {
        private string Connectionstr;
        public dbConnection()
        {
            SqlConnection con = new SqlConnection(Connectionstr);

            this.Connectionstr = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
        }

        public object LoginUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(this.Connectionstr))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from users where Username=@username AND Password=@password", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return true;
                    
                }
                else
                {
                    return false;
                }


            }

        }

        public object checkAdmin(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(this.Connectionstr))
            { 

                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from users WHERE Username=@username AND Password=@password AND userIsAdmin=1", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }

        }

        public bool checkUsername(string username)
        {
            using (SqlConnection con = new SqlConnection(this.Connectionstr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from users where Username=@username", con);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }

        }


        public bool AddUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(this.Connectionstr))
            {
                try {

                    con.Open();
                    //SAVE FOR LATER
                    //String query = "INSERT INTO users (Username,Password,Salt,UserIsAdmin) VALUES (@username, @password, @salt, @adminValue)";


                    String query = "INSERT INTO users (Username,Password,UserIsAdmin) VALUES (@username, @password, @adminValue)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    //cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@adminValue", 0);

                    cmd.ExecuteNonQuery();
                    return true;
                    con.Close();
                    

                } catch
                {
                    return false;
                }
            }
    }}
}
