namespace Book{
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
}