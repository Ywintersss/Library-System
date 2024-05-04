using System;
using System.IO;

using databaseConn = database.databaseConnection;
using Utility = Utilities.Utilities;

namespace DBManager{
    public class DBManager{
        private databaseConn DBConn;
        private Utility utilities;

        public DBManager(){
            DBConn = new databaseConn();
            {
                DBConn.Host = "localhost";
                DBConn.User = "Librarian";
                DBConn.Password = "password123";
                DBConn.databaseName = "LibraryManagement";
            }
            utilities = new Utility();
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

            foreach (string file in Directory.GetFiles(migrationPath)){
                string query = File.ReadAllText(file);
                Console.WriteLine("Running migration for: " + file);
                try{
                    if (!DBConn.prepareQuery(query)){
                        Console.WriteLine("Error running migration for: " + file);
                        continue;
                    }
                    try{                            
                        DBConn.executeNonQuery();
                    } catch (Exception e){
                        Console.WriteLine(e);
                    }

                    Console.WriteLine("Migration successful.");
                } catch (Exception e){
                    Console.WriteLine(e);
                }        
            }
            DBConn.closeDB();
        }
    }
}
