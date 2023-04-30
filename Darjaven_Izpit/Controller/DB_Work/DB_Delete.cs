using System.Data;
using Npgsql;
namespace Darjaven_Izpit.Controller.DB_Work;

public class DB_Delete
{
    string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";
    
    public void DeleteUser(string username)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conStr))
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "DELETE FROM dizpit.user_acc WHERE username = @username;";
            using (NpgsqlCommand command = new NpgsqlCommand(query, con))
            {
                command.Parameters.AddWithValue("username", username);
                

                command.ExecuteNonQuery();
            }
        }
    }
    
    public void DeleteEvent(string name)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conStr))
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "DELETE FROM dizpit.event WHERE name = @name;";
            using (NpgsqlCommand command = new NpgsqlCommand(query, con))
            {
                command.Parameters.AddWithValue("name", name);
                

                command.ExecuteNonQuery();
            }
        }
    }
    
    
    
    
    public void DeleteTicket(string eventname)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conStr))
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "DELETE FROM dizpit.tickets WHERE eventname = @eventname;";
            using (NpgsqlCommand command = new NpgsqlCommand(query, con))
            {
                command.Parameters.AddWithValue("eventname", eventname);
                

                command.ExecuteNonQuery();
            }
        }
    }
}