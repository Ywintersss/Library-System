using Books = Book.Book;
using Loans = Loan.Loan;

using databaseConn = database.databaseConnection;

namespace LoanManager{
    class LoanManager{
        private databaseConn DBConn;
        public LoanManager(){
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

        //Add loan to database
        public void addLoan(Books book, int loanPeriod){
            string query = $"INSERT INTO loans (BookID, LoanDate, ReturnDate) VALUES ({book.getBookID()}, );";
        }
    }
}