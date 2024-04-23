using System;
using System.IO;

namespace DBManager{
    public class DBManager{
        private database.databaseConnection DBConn;
        public Utilities.Utilities utilities;

        public DBManager(){
            DBConn = new database.databaseConnection();
            {
                DBConn.Host = "localhost";
                DBConn.User = "Librarian";
                DBConn.Password = "password123";
                DBConn.databaseName = "LibraryManagement";
            }
            utilities = new Utilities.Utilities();
        }

        public bool DBConnection(){
            return this.DBConn.DBConnection();
        }

        public void runMigration(){
            Console.WriteLine("Connecting to database...");
            if(this.DBConnection()){
                Console.WriteLine("Connection successful." );
            } else {
                Console.WriteLine("Connection failed.");
            }    
            string workingDirectory = utilities.getWorkingDirectory();
            string migrationPath = workingDirectory + "\\src\\db\\migrations\\";
            Console.WriteLine(migrationPath);
            foreach (string file in Directory.GetFiles(migrationPath)){
                string query = File.ReadAllText(file);
                Console.WriteLine("Running migration for: " + file);
                try{
                    if (!DBConn.prepareQuery(query)){
                        Console.WriteLine("Error running migration for: " + file);
                        continue;
                    }
                    try{                            
                        DBConn.executeQuery();
                    } catch (Exception e){
                        Console.WriteLine(e);
                    }

                    Console.WriteLine("Migration successful.");
                } catch (Exception e){
                    Console.WriteLine(e);
                }        
            }
        }
    }

    public class Program{
        static void Main(string[] args){
            DBManager dbManager = new DBManager();
            dbManager.runMigration();
        }
    }   
}
