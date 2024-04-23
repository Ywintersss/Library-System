using MySql.Data;
using MySql.Data.MySqlClient;

namespace database{
    public class databaseConnection{
        public string? Host, User, Password;
        public string? databaseName;

        public MySqlConnection? Connection;
        public MySqlCommand? Command;


        public bool DBConnection(){
            if (Connection == null){
                if (String.IsNullOrEmpty(databaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Host, databaseName, User, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }
            return true;
        }

        public bool prepareQuery(string query){
            if (query == null){
                return false;
            }
            Command = new MySqlCommand(query, Connection);
            return true;
        }

        public void executeQuery(){
            if (Command == null){
                Console.WriteLine("Error: No query is prepared. Command is null");
                return;
            }
            try{
                Command.ExecuteNonQuery();
                Console.WriteLine("Query executed successfully");
            } catch (MySqlException e){
                switch(e.Number){
                    case 1045:
                        Console.WriteLine("Error: Invalid username or password");
                        break;
                    case 1049:
                        Console.WriteLine("Error: Database does not exist");
                        break;
                    case 1050:
                        Console.WriteLine("Error: Table already exists");
                        break;
                    
                }
            }
        }
        public void closeDB(){
            if (Connection != null){
                Connection.Close();
            }
        }
    }
}
