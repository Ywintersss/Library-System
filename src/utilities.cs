namespace Utilities{
    public class Utilities{
        public string getWorkingDirectory(){
            string workingDirectory = Directory.GetCurrentDirectory();
            string endDirectory = "Library-System";

            int index = workingDirectory.LastIndexOf(endDirectory);
            if (index == -1){
                Console.WriteLine("Error: Library-System not found in working directory");
            }

            return workingDirectory.Substring(0, index + endDirectory.Length);
        }
    }

    class Program{
        static void notMain(string[] args){
            Utilities utilities = new Utilities();
            
        }
    }
}
    