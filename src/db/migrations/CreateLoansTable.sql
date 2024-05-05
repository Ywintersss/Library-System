CREATE TABLE Loans(
    LoanID int NOT NULL AUTO_INCREMENT PRIMARY KEY,
    BookID int NOT NULL,
    LoanDate date NOT NULL,
    ReturnDate date,
    DueDate date NOT NULL,
    FOREIGN KEY (BookID) REFERENCES Books(BookID) ON DELETE CASCADE
)