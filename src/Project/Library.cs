using dbManager = DBManager.DBManager;
using bkManager = BookManager.BookManager;
using lnManager = LoanManager.LoanManager;
using Books = Book.Book;
using Loans = Loan.Loan;
using System.Collections;

namespace LibrarySystem{
    class Library{
        private List<Books>? books;
        private List<Loans>? loans;
        private string? command;
        private dbManager dbManager = new dbManager();
        private bkManager bkManager = new bkManager();
        private lnManager lnManager = new lnManager();

        public void Init(){
            Console.WriteLine("Initializing library...");
            dbManager.runMigration();
            Console.WriteLine("Library initialized.");
        }

        public void Home(){
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Console.WriteLine("Welcome to Hero library."); 
            while(true){
                Console.WriteLine("Hero Library\n_________________________________");
                Console.WriteLine("What would you like to do? (1/2, 0 to exit)");            
                Console.WriteLine("1. View Books \n2. View Loans");
                command = Console.ReadLine();
                switch (command){
                    case "1":
                        booksPage();
                        break;
                    case "2":
                        loansPage();
                        break;
                    case "0":
                        Console.WriteLine("Have a nice day! Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }

        public void booksPage(){
            while(true){
                books = bkManager.getBooks();
                if (books == null){ 
                    Console.WriteLine("No books found.");
                    return; 
                }

                //displays all books
                Console.WriteLine("\nBOOK LIST\n_________________________________");    
                for (int i = 0; i < books.Count; i++){
                    Console.WriteLine(Convert.ToString(i + 1) + ". " + books[i].getTitle() + " by " + books[i].getAuthor() + "." + " Available: " + books[i].getIsAvailable());
                }

                Console.WriteLine("_________________________________\n0. Back \n1. Add Book \n2. Update Book \n3. Delete Book \n4. Loan Book ");
                command = Console.ReadLine();
                switch (command){
                    case "0":
                        return;
                    case "1":
                        addBookPage();
                        break;
                    case "2":
                        updateBookPage(books);
                        break;
                    case "3":
                        deleteBookPage(books);
                        break;
                    case "4":
                        loanBookPage(books);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }

        public void loansPage(){
            while(true){
                books = bkManager.getBooks();
                loans = lnManager.GetLoans(bkManager.getBooks());
                List<Loans>? activeLoans = lnManager.GetActiveLoans(bkManager.getBooks());
                if (loans == null){ 
                    Console.WriteLine("No loans found.");
                    return; 
                }
                //displays all loans
                Console.WriteLine("\nLOAN LIST\n_________________________________");
                displayLoans(loans);
                
                Console.WriteLine("_________________________________\n0. Back \n1. View Active Loans \n2. Return Book");
                command = Console.ReadLine();
                switch (command){
                    case "0":
                        return;
                    case "1":
                        activeLoansPage(activeLoans);
                        break;
                    case "2":
                        returnBookPage(activeLoans);
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }

        public void activeLoansPage(List<Loans> activeLoans){
            if (activeLoans.Count == 0){
                Console.WriteLine("No active loans found. Please try again.");
                return;
            }
            Console.WriteLine(activeLoans);
            while(true){
                Console.WriteLine("\nACTIVE LOAN LIST\n_________________________________");
                displayLoans(activeLoans);

                Console.WriteLine("0. Back");
                command = Console.ReadLine();
                if (command == "0"){
                    return;
                } else {
                    Console.WriteLine("\nInvalid command. Please try again.");
                    continue;
                }
            }
        }

        public void addBookPage(){
            string title = "", author = "";
            int state = 0;
            while(true){
                switch(state){
                    case 0:
                        Console.WriteLine("\nADD BOOK\n_________________________________");
                        Console.WriteLine("Enter book title or 0 to exit: \n---------------");
                        title = Console.ReadLine();

                        if (string.IsNullOrEmpty(title)){
                            Console.WriteLine("Invalid title. Please try again.");
                            continue;
                        }; 
                        if (title == "0"){
                            return;
                        }
                        state++;
                        break;
                    case 1:
                        Console.WriteLine("Enter book author or 0 to go back: \n---------------");
                        author = Console.ReadLine();
                        if(string.IsNullOrEmpty(author)){
                            Console.WriteLine("Invalid author. Please try again.");
                            continue;
                        }
                        if (author == "0"){
                            state--;
                            continue;
                        }
                        state++;
                        break;
                    case 2:
                        Console.WriteLine("Book to add: \n---------------");
                        Console.WriteLine("Title: " + title + "\nAuthor: " + author);
                        if(confirmation()){
                            try{
                                bkManager.addBook(title, author);
                            } catch (Exception e){
                                Console.WriteLine(e);
                            }
                            return;
                        } else {
                            state--;
                        }
                        break;
                }    
            }
        }

        public void updateBookPage(List<Books>? books = null){
            if (books == null){
                Console.WriteLine("No books found. Please try again.");
                return;
            }
            string title = "", author = "", bookNum, newTitle = "", newAuthor = "";
            Books? bk = books[0];
            int state = 0;
            while(true){
                switch(state){
                    case 0:
                        Console.WriteLine("\nUPDATE BOOK DETAILS\n_________________________________");
                        displayBooks(books);
                        bookNum = Console.ReadLine();
                        if(string.IsNullOrEmpty(bookNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        // checks if input is a number
                        } else if (!checkChars(bookNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        }
                        if (bookNum == "0"){
                            return;
                        }
                        bk = books[Convert.ToInt32(bookNum) - 1];
                        title = bk.getTitle();
                        author = bk.getAuthor();
                        state++;
                        break;
                    case 1:
                        Console.WriteLine("Current title: " + title + "\n---------------");
                        Console.WriteLine("Leave blank to keep current title \nEnter new book title or 0 to go back: ");
                        newTitle = Console.ReadLine();
                        if(string.IsNullOrEmpty(newTitle)){
                            newTitle = title;
                        }; 
                        if (newTitle == "0"){
                            state--;
                            continue;
                        }
                        state++;
                        break;
                    case 2:
                        Console.WriteLine("Current author: " + author + "\n---------------");
                        Console.WriteLine("Leave blank to keep current author \nEnter new book author or 0 to go back: ");
                        newAuthor = Console.ReadLine();
                        if(string.IsNullOrEmpty(newAuthor)){
                            newAuthor = author;
                        }
                        if (newAuthor == "0"){
                            state--;
                            continue;
                        }
                        state++;
                        break;
                    case 3:
                        Console.WriteLine("New Title: " + newTitle + "\nNew Author: " + newAuthor);
                        if(confirmation()){
                            try{
                                bkManager.updateBook(bk.getBookID(), newTitle, newAuthor);
                            } catch (Exception e){
                                Console.WriteLine(e);
                            }
                            return;
                        } else {
                            state--;
                        }
                        break;
                }
            }
        }

        public void deleteBookPage(List<Books>? books = null){
            if (books == null){
                Console.WriteLine("No books found. Please try again.");
                return;
            }
            string bookNum;
            Books? bk = books[0];
            int state = 0;
            while(true){
                switch(state){
                    case 0:
                        Console.WriteLine("\nDELETE BOOK\n_________________________________");
                        displayBooks(books);
                        bookNum = Console.ReadLine();
                        if(string.IsNullOrEmpty(bookNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        // checks if input is a number
                        } else if (!checkChars(bookNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        }
                        if (bookNum == "0"){
                            return;
                        }

                        bk = books[Convert.ToInt32(bookNum) - 1];
                        state++;
                        break;
                    case 1:
                        Console.WriteLine("Book to delete: ");
                        Console.WriteLine("Title: " + bk.getTitle() + "\nAuthor: " + bk.getAuthor());
                        if(confirmation()){
                            try{
                                bkManager.deleteBook(bk.getBookID());
                            } catch (Exception e){
                                Console.WriteLine(e);
                            }
                            return;
                        } else {
                            state--;
                        }
                        break;
                }
            }
        }

        public void loanBookPage(List<Books>? books = null){
            if (books == null){
                Console.WriteLine("No books found. Please try again.");
                return;
            }
            string bookNum, title = "", author = "";
            int loanPeriod = 0;
            Books? bk = books[0];
            int state = 0;
            while(true){
                switch(state){
                    case 0:
                        Console.WriteLine("\nBOOK LOANING\n_________________________________");
                        displayBooks(books);
                        bookNum = Console.ReadLine();
                        if(string.IsNullOrEmpty(bookNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        } else if (!checkChars(bookNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        }
                        if (bookNum == "0"){
                            return;
                        }
                        bk = books[Convert.ToInt32(bookNum) - 1];
                        if (!bk.getIsAvailable()){
                            Console.WriteLine("Book not available. Please try again.");
                        }
                        title = bk.getTitle();
                        author = bk.getAuthor();
                        state++;
                        break;
                    case 1:
                        Console.WriteLine("For how long (in days)? or 0 to go back: ");
                        string command = Console.ReadLine();
                        if(string.IsNullOrEmpty(command)){
                            Console.WriteLine("Invalid loan period. Please try again.");
                            continue;
                        } else if (command == "0"){
                            state--;
                            continue;
                        } else if (!checkChars(command)){
                            Console.WriteLine("Invalid loan period. Please try again.");
                            continue;
                        }
                        loanPeriod = Convert.ToInt32(command);
                        if (loanPeriod < 1){
                            Console.WriteLine("Invalid loan period. Please try again.");
                            continue;
                        } 
                        state++;
                        break;
                    case 2:
                        Console.WriteLine("Title: " + title + "\nAuthor: " + author + "\nLoan period: " + loanPeriod + " days");
                        if(confirmation()){
                            try{
                                lnManager.addLoan(bk, loanPeriod);
                            } catch (Exception e){
                                Console.WriteLine(e);
                            }
                            return;
                        } else {
                            state--;
                        }
                        break;
                }
            }
        }

        public void returnBookPage(List<Loans> activeLoans){
            if (activeLoans.Count == 0){
                Console.WriteLine("No active loans found. Please try again.");
                return;  
            } 
            string loanNum;
            Loans? ln = activeLoans[0];
            int state = 0;
            while(true){
                switch(state){
                    case 0:
                        Console.WriteLine("\nRETURN BOOK\n_________________________________");
                        displayLoans(activeLoans);
                        loanNum = Console.ReadLine();
                        if(string.IsNullOrEmpty(loanNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        } else if (!checkChars(loanNum)){
                            Console.WriteLine("Invalid Book Number. Please try again.");
                            continue;
                        }
                        if (loanNum == "0"){
                            return;
                        }
                        ln = activeLoans[Convert.ToInt32(loanNum) - 1];
                        state++;
                        break;
                    case 1:
                        Console.WriteLine("Book to return: ");
                        Console.WriteLine("Title: " + ln.getBook().getTitle() + "\nAuthor: " + ln.getBook().getAuthor());
                        if(confirmation()){
                            try{
                                lnManager.returnLoan(ln.getBook());
                            } catch (Exception e){
                                Console.WriteLine(e);
                            }
                            return;
                        } else {
                            state--;
                        }
                        break;
                }
            }
        }

        public bool confirmation(){
            while(true){
                Console.WriteLine("Are you sure? (y/n)");
                command = Console.ReadLine();
                if (command == "y"){
                    return true;
                } else if (command == "n"){
                    return false;
                } else {
                    Console.WriteLine("Invalid command. Please try again.");
                }
            }
        }

        public void displayBooks(List<Books> books ){
            for (int i = 0; i < books.Count; i++){
                Console.WriteLine(i + 1 + ". " + books[i].getTitle() + " by " + books[i].getAuthor());
            }
            Console.WriteLine("Enter Number. (1 - " + books.Count + ") or 0 to exit: ");
        }

        public void displayLoans(List<Loans> loans, int mode = 0){
            for (int i = 0; i < loans.Count; i++){
                Console.WriteLine(i + 1 + ". " + loans[i].getBook().getTitle() + " by " + loans[i].getBook().getAuthor() + "\n\tLoaned out: " + loans[i].getLoanDate() + "\n\tDue: " + loans[i].getDueDate() + "\n\tReturned Date: " + loans[i].getReturnDate());
            }
            if (mode == 0){ 
                Console.WriteLine("Enter Number. (1 - " + loans.Count + ") or 0 to exit: ");
            } else if (mode == 1){
                return;
            }
        }

        //used to check if user input is a number, not a character
        public bool checkChars(string str){
            char character;
            if (str.Length != 1){
                return false;
            } else{
                str = str.ToLower();
                character = Convert.ToChar(str);
            }
            for(char c = 'a'; c <= 'z'; c++){
                if(character == c){
                    return false;
                }
            }
            return true;
        }
    }

    public class Program{
        static void Main(string[] args){
            Library library = new Library();
            library.Init();
            library.Home();
            
        }
    }
}