using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.FileReader
{
    public class sudokuFileReader
    {
        public static (String,String) Reader()
        {
            Console.WriteLine("please enter file path");
            string path = Console.ReadLine();

            string board = "";

            try {
                board = File.ReadAllText(path);
                }
            catch (FileNotFoundException ex) {
                throw new FileNotFoundException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return (board,path);
        }
    }
}
