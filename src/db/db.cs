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

        public void executeNonQuery(){
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
                    default:
                        Console.WriteLine(e);
                        break;
                }
            }
        }

        public List<string[]>? executeQuery(){
            List<string[]> dataList = new List<string[]>();
            if (Command == null){
                Console.WriteLine("Error: No query is prepared. Command is null");
                return null;
            } 
            try{
                MySqlDataReader reader = Command.ExecuteReader();
                while (reader.Read()){
                    //initialize values with array size of reader.FieldCount(number of columns)
                    string[] values = new string[reader.FieldCount];
                    //iterate through each column for each row
                    for (int i = 0; i < reader.FieldCount; i++){
                        //add values to array
                        if(reader[i] != DBNull.Value){
                            #pragma warning disable CS8601 // Possible null reference assignment.
                            values[i] = reader[i].ToString();
                            #pragma warning restore CS8601 // Possible null reference assignment.
                        } else {
                            //handle the null value case
                            values[i] = null; // Or any other default value
                        }
                    }
                    dataList.Add(values);
                }
                reader.Close();
                return dataList;
            } catch (Exception e){
                Console.WriteLine(e);
                return null;
            }
        }
        public void closeDB(){
            if (Connection != null){
                Connection.Close();
            }
        }
    }
}
