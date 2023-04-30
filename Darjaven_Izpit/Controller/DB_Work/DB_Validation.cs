using Npgsql;

namespace Darjaven_Izpit.Controller.DB_Work
{

    public class DB_Validation
    {
        string conStr = "Server=129.152.7.145;Port=5432;Database=it_kariera;User Id=it_kariera;Password=Kariera;";
        bool userExist;
        bool passCorrect;
        
        public bool ValidateUserAcc(string username, string password)
        {
            

            using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT EXISTS(SELECT * FROM \"dizpit\".\"user_acc\" WHERE \"user_acc\".username Like @username)", connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    userExist = (bool)command.ExecuteScalar();


                }
                if (userExist == true)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT EXISTS(SELECT * FROM \"dizpit\".\"user_acc\" WHERE \"user_acc\".password Like @password AND \"user_acc\".username Like @username)", connection))
                    {
                        command.Parameters.AddWithValue("password", password);
                        command.Parameters.AddWithValue("username", username);
                        passCorrect = (bool)command.ExecuteScalar();

                    }
                    if (passCorrect == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }
        
        
        public bool ValidateAdminAcc(string username, string password)
        {
            

            using (NpgsqlConnection connection = new NpgsqlConnection(conStr))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT EXISTS(SELECT * FROM \"dizpit\".\"admin_account\" WHERE \"admin_account\".username Like @username)", connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    userExist = (bool)command.ExecuteScalar();


                }
                if (userExist == true)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT EXISTS(SELECT * FROM \"dizpit\".\"admin_account\" WHERE \"admin_account\".password Like @password AND \"admin_account\".username Like @username)", connection))
                    {
                        command.Parameters.AddWithValue("password", password);
                        command.Parameters.AddWithValue("username", username);
                        passCorrect = (bool)command.ExecuteScalar();

                    }
                    if (passCorrect == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }
        
    }
}