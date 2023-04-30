using Darjaven_Izpit.Model;
using Npgsql;

namespace Darjaven_Izpit.Controller.DB_Work;

public class Collections
{
    string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";

    public List<User> GetUsersList()
        {
            List<User> result = new List<User>();
            using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
            {
                string query = "SELECT * FROM dizpit.user_acc";
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User employee = new User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                LastName = reader.GetString(3),
                            };
                            result.Add(employee);
                    }
                    reader.Close();
                }
                return result;
            }



        }
    
    public List<Events> GetEventList()
    {
        List<Events> result = new List<Events>();
        using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
        {
            string query = "SELECT * FROM dizpit.event";
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                        Events employee = new Events()
                        {
                            Name = reader.GetString(0),
                            Description = reader.GetString(1),
                            Date = reader.GetDateTime(3),
                        };
                        result.Add(employee);
                }
                reader.Close();
            }
            return result;
        }



    }
    
    
    
    public List<Tickets> GetTicketList()
    {
        List<Tickets> result = new List<Tickets>();
        using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
        {
            string query = "SELECT * FROM dizpit.tickets";
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tickets employee = new Tickets()
                    {
                        Id = reader.GetInt32(0),
                        Eventname = reader.GetString(1),
                        Userown = reader.GetString(2),
                    };
                    result.Add(employee);
                }
                reader.Close();
            }
            return result;
        }



    }
    
    
}