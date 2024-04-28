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
    }
}