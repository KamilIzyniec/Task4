using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Task4.Models;

namespace Task4.Services
{
    public class UsersDAO
    {
        bool success = false;
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Main; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public bool FindUserByNameAndPassword(UserModel user)
        {
            string sqlStatement = "SELECT active_status FROM dbo.Users WHERE username = @username AND password= @password";
            string sqlUpdateStatement = "UPDATE dbo.Users SET last_login_time = CURRENT_TIMESTAMP WHERE username = @username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                bool b = false;
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                SqlCommand updateLoginTime = new SqlCommand(sqlUpdateStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;
                updateLoginTime.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        b = (bool)reader["active_status"];
                    }
                    if (reader.HasRows && b == true)
                    {
                        success = true;
                        reader.Close();
                        updateLoginTime.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
        public bool AddNewUser(UserModel user)
        {
            bool success = false;
            string sqlUsernameCheckStatement = "SELECT * FROM dbo.Users WHERE username = @username OR email = @email";
            string sqlStatement = "INSERT INTO dbo.Users (username, password, email, registration_time, active_status) VALUES (@username, @password, @email, CURRENT_TIMESTAMP, 1)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                SqlCommand usernameCheckCommand = new SqlCommand(sqlUsernameCheckStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 40).Value = user.Email;
                usernameCheckCommand.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                usernameCheckCommand.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 40).Value = user.Email;
                try
                {
                    connection.Open();
                    SqlDataReader reader = usernameCheckCommand.ExecuteReader();
                    if (reader.HasRows)
                    {

                    }
                    else
                    {
                        reader.Close();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            success = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
        public List<UserModel> ListUsers()
        {
            List<UserModel> userModels = new List<UserModel>();
            string sqlStatement = "select * from dbo.Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["id"] != null)
                        {
                            userModels.Add(new UserModel()
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                UserName = reader["username"].ToString(),
                                Email = reader["email"].ToString(),
                                LastLoginTime = reader["last_login_time"].ToString(),
                                RegistrationTime = reader["registration_time"].ToString(),
                                ActiveStatus = Convert.ToBoolean(Convert.ToInt32(reader["active_status"]))
                            });
                        }

                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return userModels;
            }

        }
        public bool BlockUsers(int user)
        {
            bool success = false;
            string sqlStatement = "update dbo.Users set active_status = 0 where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = user;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    success = true;
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            return success;
        }
        public bool UnblockUsers(int user)
        {
            bool success = false;
            string sqlStatement = "update dbo.Users set active_status = 1 where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = user;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    success = true;
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return success;
        }
        public bool DeleteUsers(int user)
        {
            bool success = false;
            string sqlStatement = "delete from dbo.Users where id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = user;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    success = true;
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return success;
        }
        public int GetId(string user)
        {
           int id = 0;
            string sqlStatement = "select id from dbo.Users where username = @username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        id = (int)reader["id"];
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return id;
        }
    }
}
