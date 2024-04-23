namespace UserDataManager{
    public class UserDataManager {
        private database.databaseConnection DBConn;
        public UserDataManager(){
            DBConn = new database.databaseConnection
            {
                Host = "localhost",
                User = "Librarian",
                Password = "password123",
                databaseName = "LibraryManagement"
            };
        }
        public bool DBConnection(){
            return this.DBConn.DBConnection();
        }
    }
}