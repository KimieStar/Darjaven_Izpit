using System.Data;
using Npgsql;

namespace Darjaven_Izpit.Controller.DB_Work;

public class DB_Create
{
    string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";
    
    public void CreateUser(string username, string password, string firstName, string lastName)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conStr))
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
            string query = "INSERT INTO dizpit.user_acc (username, password, firstName, lastName, role) VALUES (@username, @password, @firstname, @lastname, 'user');";
            using (NpgsqlCommand command = new NpgsqlCommand(query, con))
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("firstname", firstName);
                command.Parameters.AddWithValue("lastname", lastName);
                command.Parameters.AddWithValue("role", "user");

                command.ExecuteNonQuery();
            }
        }
    }
    
    public void CreateEvent(string username, string password, string firstName, string lastName)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conStr))
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
            string query = "INSERT INTO dizpit.user_acc (username, password, firstName, lastName, role) VALUES (@username, @password, @firstname, @lastname, 'user');";
            using (NpgsqlCommand command = new NpgsqlCommand(query, con))
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("firstname", firstName);
                command.Parameters.AddWithValue("lastname", lastName);
                command.Parameters.AddWithValue("role", "user");

                command.ExecuteNonQuery();
            }
        }
    }
}