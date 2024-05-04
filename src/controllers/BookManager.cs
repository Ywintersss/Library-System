using System;

using Books = Book.Book;
using Loans = Loan.Loan;

using databaseConn = database.databaseConnection;

namespace BookManager{
    class BookManager{
        private databaseConn DBConn;
        public BookManager(){
            DBConn = new databaseConn
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

        public void addBook(string title, string author){
            string query = $"INSERT INTO books(Title, Author, IsAvailable) VALUES('{title}', '{author}', true);";
            if(this.DBConnection()){
                Console.WriteLine("Connection successful." );
            } else {
                Console.WriteLine("Connection failed.");
            } 
            try{
                if (!DBConn.prepareQuery(query)){
                    Console.WriteLine("Error running query");
                }
                try{                            
                    DBConn.executeNonQuery();
                } catch (Exception e){
                    Console.WriteLine(e);
                }

                Console.WriteLine("Book added successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }
        }

        public void updateBook(int bookID, string? title = null, string? author = null){
            string query = "";
            if (!string.IsNullOrEmpty(author) && !string.IsNullOrEmpty(title)) {
                query = $"UPDATE books SET Title = '{title}', Author = '{author}' WHERE BookID = {bookID};";
            } else if (!string.IsNullOrEmpty(title)) {
                query = $"UPDATE books SET Title = '{title}' WHERE BookID = {bookID};";
            } else if (!string.IsNullOrEmpty(author)) {
                query = $"UPDATE books SET Author = '{author}' WHERE BookID = {bookID};";
            } else {
                Console.WriteLine("Nothing to update.");
            }
            if(this.DBConnection()){
                Console.WriteLine("Connection successful." );
            } else {
                Console.WriteLine("Connection failed.");
            } 
            try{
                if (!DBConn.prepareQuery(query)){
                    Console.WriteLine("Error running query");
                }
                try{                            
                    DBConn.executeNonQuery();
                } catch (Exception e){
                    Console.WriteLine(e);
                }    

                Console.WriteLine("Book updated successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }     
        }

        public void deleteBook(int bookID){
            string query = $"DELETE FROM books WHERE BookID = {bookID};";
            if(this.DBConnection()){
                Console.WriteLine("Connection successful." );
            } else {
                Console.WriteLine("Connection failed.");
            }
            try{
                if (!DBConn.prepareQuery(query)){
                    Console.WriteLine("Error running query");
                }
                try{                            
                    DBConn.executeNonQuery();
                } catch (Exception e){
                    Console.WriteLine(e);
                }    

                Console.WriteLine("Book Deleted successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            } 
        }

        public List<Books>? getBooks(){
            List<Books> books = new List<Books>();
            string query = "SELECT * FROM books;";
            if(this.DBConnection()){
                Console.WriteLine("Connection successful." );
            } else {
                Console.WriteLine("Connection failed.");
            }
            try{
                if (!DBConn.prepareQuery(query)){
                    Console.WriteLine("Error running query");
                }
                try{
                    if (!(DBConn.executeQuery() == null)){
                        foreach (var book in DBConn.executeQuery()){
                            books.Add(new Books(Convert.ToInt32(book[0]), book[1], book[2], Convert.ToBoolean(book[3])));
                        }
                    }
                    return books;
                } catch (Exception e){
                    Console.WriteLine(e);
                }
                
                Console.WriteLine("Books queried successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }
            return null;
        }
    }
}