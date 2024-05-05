using System;

using Books = Book.Book;
using Loans = Loan.Loan;
using Utility = Utilities.Utilities;
using bkManager = BookManager.BookManager;

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

        //opens connection to database
        public bool DBConnection(){
            return this.DBConn.DBConnection();
        }

        public void updateBook(Books book){
            string query = $"UPDATE books SET IsAvailable = {!book.getIsAvailable()} WHERE BookID = {book.getBookID()};";
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

        //Add loan to database
        public void addLoan(Books book, int loanPeriod){
            string query = $"INSERT INTO loans(BookID, LoanDate, ReturnDate, DueDate) VALUES({book.getBookID()}, NOW(), null, NOW() + INTERVAL {loanPeriod} DAY);";
            
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
                    Console.WriteLine("Loan added successfully");
                    try{
                        updateBook(book);
                    } catch (Exception e){
                        Console.WriteLine(e);
                    } 
                } catch (Exception e){
                    Console.WriteLine(e);
                }
            } catch (Exception e){
                Console.WriteLine(e);
            }
        }

        //Return loan from database
        public void returnLoan(Books book){
            string query = $"UPDATE loans SET ReturnDate = NOW() WHERE BookID = {book.getBookID()};";
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
                    Console.WriteLine("Loan returned successfully");
                    try{
                        updateBook(book);
                    } catch (Exception e){
                        Console.WriteLine(e);
                    }
                } catch (Exception e){
                    Console.WriteLine(e);
                }

            } catch (Exception e){
                Console.WriteLine(e);
            }
        }

        public List<Loans>? GetLoans(List<Books> books){
            List<Loans> loans = new List<Loans>();
            List<Books> cache = new List<Books>();
            string query = "SELECT * FROM loans";
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
                    //get all loans and iterate through them
                    foreach (var loan in DBConn.executeQuery()){
                        //loops through all books
                        foreach (var book in books){
                            //checks if book is already in cache
                            if (cache.Contains(book)){ continue; }
                            //checks if book is present in the loan
                            if (book.getBookID() == Convert.ToInt32(loan[1])){
                                //adds loan to list
                                loans.Add(new Loans(Convert.ToInt32(loan[0]), book, Convert.ToDateTime(loan[2]).ToString("yyyy-MM-dd"), loan[3] == null ? "null" : Convert.ToDateTime(loan[3]).ToString("yyyy-MM-dd"), Convert.ToDateTime(loan[4]).ToString("yyyy-MM-dd")));
                                //adds book to cache
                                cache.Add(book);
                            }
                        }
                    };
                    return loans;
                } catch (Exception e){
                    Console.WriteLine(e);
                }

                Console.WriteLine("Loans queried successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }         
            return null;
        }

        public List<Loans>? GetActiveLoans(List<Books> books){
            List<Loans> loans = new List<Loans>();
            List<Books> cache = new List<Books>();
            string query = "SELECT * FROM loans WHERE DueDate > NOW() AND ReturnDate IS NULL";
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
                    foreach (var loan in DBConn.executeQuery()){
                        foreach (var book in books){
                            if (cache.Contains(book)){ continue; }
                            if (book.getBookID() == Convert.ToInt32(loan[1])){
                                loans.Add(new Loans(Convert.ToInt32(loan[0]), book, Convert.ToDateTime(loan[2]).ToString("yyyy-MM-dd"), loan[3] == null ? "null" : Convert.ToDateTime(loan[3]).ToString("yyyy-MM-dd"), Convert.ToDateTime(loan[4]).ToString("yyyy-MM-dd")));
                                cache.Add(book);
                            }
                        }
                    };
                    return loans;
                } catch (Exception e){
                    Console.WriteLine(e);
                }

                Console.WriteLine("Active Loans queried successfully");
            } catch (Exception e){
                Console.WriteLine(e);
            }
            return null;
        }
    }
}