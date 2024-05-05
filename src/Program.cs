using System;
using Library_System = LibrarySystem.Library;

namespace LibraryProgram{
    public class Program{
        static void Main(string[] args){
            Library_System library = new Library_System();
            
            library.Init();
            library.Home();
        }
    }   
}