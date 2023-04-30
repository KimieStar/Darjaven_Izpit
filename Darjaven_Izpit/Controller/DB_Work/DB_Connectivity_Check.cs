using Npgsql;

namespace Darjaven_Izpit.Controller.DB_Work;

public class DB_Connectivity_Check
{
    string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";
    public bool isDbUp()
    {

        using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }

            }
            catch (Npgsql.NpgsqlException)
            {
                return false;
            }
        }
        return false;
    }

    public bool isConnectionOpen()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}