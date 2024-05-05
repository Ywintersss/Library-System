# Library-System <br>
DBMS used is MySQL <br>
Using MySQL server 83 <br>

Assuming the console app is only used by the librarian <br>
Assuming that there is a root@localhost user <br>

# C# setup <br>
install C# dotnet software development kit version 8.0.204 with x64 architecture <br>

# Database setup<br>
Open any powershell/terminal, login to root user:<br>
mysql -u root -p <br>
Enter root user password <br>
MySQL server command line queries: <br>
CREATE USER 'Librarian'@'localhost' IDENTIFIED BY 'password123'; <br>
grant all privileges on *.* to 'Librarian'@'localhost'; <br>
Database librarymanagement was manually created in MySQL Workbench<br>

# Libraries/Packages used <br>
MySql.Data for MySql Connection between MySQL Server and C# application <br>

# Project Overview <br>
## Project Structure: <br>
- src (source folder)
    - controllers (folder for **Application to Database interactions** files)
        - BookManager.cs
        - DBManager.cs
        - LoanManager.cs
        - README.md (list of project areas that i think could be improved)
    - db (folder for **database configuration** files)
        - migrations (folder for database table creations/table configurations)
            - *migration-files.sql...*
        - seeds (folder for database table data)
            - *seed-files.sql*
        - db.cs 
    - Project (main folder for **application logic and project object models** files)
        - Books.cs (books model)
        - Library.cs (application logic)
        - Loan.cs (loans model)
    - Program.cs (entry point for program)
    - utilities.cs
- .gitignore
- Library-System.csproj
- Library-System.sln
- README.md (currently here)

## Design choice <br>
The program is heavily influenced by my Web Development experience, and somewhat resembles the Model-View-Controller(MVC) architecture. <br>
A *controllers* folder that handles application and database interaction logic. <br>
handles **CRUD** operations to the database for different aspects of the application. <br>
Example: <br>

    '''
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
    '''
This code snippet shows the logic of adding a book to the database. <br>

*Views* and *Models* of the application fall under the Project directory. <br>
View Example: <br>

    '''
    private void loanBookPage(List<Books>? books = null){
            if (books == null){
                Console.WriteLine("No books found. Please try again.");
                return;
            }
            string bookNum, title = "", author = "";
            int loanPeriod = 0;
            Books? bk = books[0];
            int state = 0, bookCount = books.Count;
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
                        } else if (Convert.ToInt32(bookNum) > bookCount){
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
    '''
The Code Snippet above demonstrates the display of the Book Loaning page for user <br>
Users can enter in values, validations are in place and user have the ease of navigating between different states during loaning with a confirmation prompt at the end<br>
Each *view* or page displayed to the user is written in the Library.cs source file. <br>

*Models* are written in Books.cs and Loan.cs source file which are classes that models the attributes of the object. <br>
Example: <br>

    '''
    class Book(int bookID, string title, string author, bool isAvailable)
    {
        private int bookID = bookID;
        private string title = title;
        private string author = author;
        private bool isAvailable = isAvailable;


        //Getters and Setters
        public int getBookID(){
            return bookID;
        }

        public string getTitle(){
            return title;
        }

        public string getAuthor(){
            return author;
        }

        public bool getIsAvailable(){
            return isAvailable;
        }

        public void setTitle(string title){
            this.title = title;
        }

        public void setAuthor(string author){
            this.author = author;
        }
        public void setIsAvailable(bool isAvailable){
            this.isAvailable = isAvailable;
        }
    }
    '''
The Code Snippet above displays the attributes of a book object and methods in accessing/modifying the data.<br>
Mainly to be used by data retreived from the database. <br>

The Project was structured in a way that promotes **separation of concerns**. <br>

*Migrations* were used to help keep track of databse modifications and help simplify the initializaiton process. <br>

### Testing was done to a minimal
### Mostly Unit Tests for contorller methods