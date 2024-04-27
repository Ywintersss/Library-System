using System;

using Books = Book.Book;

namespace Loan{
    class Loan{
        private int LoanID;
        private Books Book;
        private DateTime LoanDate;
        private DateTime ReturnDate;

        public Loan(int LoanID, Books Book, DateTime LoanDate, DateTime ReturnDate){
            this.LoanID = LoanID;
            this.Book = Book;
            this.LoanDate = LoanDate;
            this.ReturnDate = ReturnDate;
        }

        //Getters and Setters
        public int getLoanID(){
            return LoanID;
        }

        public Books getBook(){
            return Book;
        }

        public DateTime getLoanDate(){
            return LoanDate;
        }

        public DateTime getReturnDate(){
            return ReturnDate;
        }

        public void setBook(Books Book){
            this.Book = Book;
        }

        public void setLoanDate(DateTime LoanDate){
            this.LoanDate = LoanDate;
        }

        public void setReturnDate(DateTime ReturnDate){
            this.ReturnDate = ReturnDate;
        }
    }
}