using System;

using Books = Book.Book;
using Loans = Loan.Loan;
using Utility = Utilities.Utilities;

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
            string LoanDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string ReturnDate = DateTime.Now.Date.AddDays(loanPeriod).ToString("yyyy-MM-dd");

            Console.WriteLine(book.getBookID()); 
            Console.WriteLine(LoanDate);
            Console.WriteLine(ReturnDate);

            string query = $"INSERT INTO loans(BookID, LoanDate, ReturnDate) VALUES({book.getBookID()}, '{LoanDate}', '{ReturnDate}');";
            Console.WriteLine(query);

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
                    DBConn.executeQuery();
                } catch (Exception e){
                    Console.WriteLine(e);
                }

                Console.WriteLine("Loan added successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }
            
        }

        public List<Loans> GetActiveLoans(){
            List<Loans> loans = new List<Loans>();
            string query = "SELECT * FROM loans WHERE ReturnDate > NOW()";
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
                    DBConn.executeQuery();
                } catch (Exception e){
                    Console.WriteLine(e);
                }

                Console.WriteLine("Loan added successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }
            return loans;
        }
    }

    class Program{
        static void Main(string[] args){
            LoanManager lm = new LoanManager();
            lm.addLoan(new Books(001, "The Great Gatsby", "F. Scott Fitzgerald", true), 7);
        }
    }
}