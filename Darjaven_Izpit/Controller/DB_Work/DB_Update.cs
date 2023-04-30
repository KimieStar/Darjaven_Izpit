using System.Data;
using Npgsql;

namespace Darjaven_Izpit.Controller.DB_Work;

public class DB_Update
{
    string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";

    public void UpdateUsername(string newUsername, string oldUsername)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(conStr))
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "Update dizpit.u_acc set username = @newUname where username = @oldUname";
            using (NpgsqlCommand command = new NpgsqlCommand(query,con))
            {
                command.Parameters.AddWithValue("newUname", newUsername);
                command.Parameters.AddWithValue("oldUname", oldUsername);

                command.ExecuteNonQuery();
            }
        }
    }
}