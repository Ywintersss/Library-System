using System;

using Books = Book.Book;

namespace Loan{
    class Loan{
        private int LoanID;
        private Books Book;
        private string LoanDate, ReturnDate, DueDate;

        public Loan(int LoanID, Books Book, string LoanDate, string ReturnDate, string DueDate){
            this.LoanID = LoanID;
            this.Book = Book;
            this.LoanDate = LoanDate;
            this.ReturnDate = ReturnDate;
            this.DueDate = DueDate;
        }

        //Getters and Setters
        public int getLoanID(){
            return LoanID;
        }

        public Books getBook(){
            return Book;
        }

        public string getLoanDate(){
            return LoanDate;
        }

        public string getReturnDate(){
            return ReturnDate;
        }

        public string getDueDate(){
            return DueDate;
        }

        public void setBook(Books Book){
            this.Book = Book;
        }

        public void setLoanDate(string LoanDate){
            this.LoanDate = LoanDate;
        }

        public void setReturnDate(string ReturnDate){
            this.ReturnDate = ReturnDate;
        }

        public void setDueDate(string DueDate){
            this.DueDate = DueDate;
        }
    }
}