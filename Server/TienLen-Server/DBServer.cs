using System;
using System.Data;
using System.Data.SqlClient;

namespace TienLen_Server
{
    class DBServer
    {
        private SqlConnection Connection;
        public string ConnectionString;

        public DBServer(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public void ConnectToServer()
        {
            Console.WriteLine("Connecting to database...");
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            Console.WriteLine("Database connected");
        }

        /// <summary>
        /// find if user has matched username and password existed in database 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns>Player if login ok, otherwise null</returns>
        public Player FindUser(string Username, string Password)
        {
            string Querry = "select * from users where username=@username and password=@password";
            SqlCommand command = new SqlCommand(Querry, Connection);
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", Password);
            DataTable result = new DataTable();
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                try
                {
                    dataReader.Read();
                    if (dataReader.IsDBNull(0))
                        return null;
                    else return new Player(dataReader[0].ToString(), int.Parse(dataReader[2].ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Create a new user and add information to database 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns>New user if register ok, otherwise null</returns>
        public Player CreateNewUser(string Username, string Password)
        {
            string Querry = "select username from users where username=@username";
            SqlCommand command = new SqlCommand(Querry, Connection);
            command.Parameters.AddWithValue("@username", Username);

            if (command.ExecuteScalar() == null)
            {
                Querry = "insert into Users values(@username,@password,@money,@phongdangchoi)";
                command = new SqlCommand(Querry, Connection);
                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@money", 100000);
                command.Parameters.AddWithValue("@phongdangchoi", DBNull.Value);
                if (command.ExecuteNonQuery() == 1) return new Player(Username, 300);
            }
            return null;
        }
    }
}
