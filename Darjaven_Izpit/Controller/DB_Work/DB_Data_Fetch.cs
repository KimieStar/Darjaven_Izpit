using System.Data;
using Npgsql;
namespace Darjaven_Izpit.Controller.DB_Work;

public class DB_Data_Fetch
{
   string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";
   string id = "";
   public string FetchId(string username)
   {
      using (NpgsqlConnection con = new NpgsqlConnection(conStr))
      {
         if (con.State == ConnectionState.Closed)
         {
            con.Open();
         }

         string query = "SELECT dizpit.user_acc.id FROM dizpit.user_acc where dizpit.user_acc.username Like @username";
         
         using (NpgsqlCommand command = new NpgsqlCommand(query,con))
         {
            command.Parameters.AddWithValue("username",username);
            try
            {
               id = command.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
               return "null";
            }
            
         }
      }

      return id;
   }
    
}