using dbManager = DBManager.DBManager;
using Books = Book.Book;

namespace LibrarySystem{
    class Library{
        private List<Books>? availableBooks;

        public static void Init(){
            var dbManager = new dbManager();
            Console.WriteLine("Initializing library...");
            dbManager.runMigration();
            Console.WriteLine("Library initialized.");
        }
    }

    public class Program{
        static void Main(string[] args){
            Library.Init();
        }
    }
}