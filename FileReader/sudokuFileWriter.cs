using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.FileReader
{
    public class sudokuFileWriter
    {
        //writer which get a path and write into if the solved string
        public static void Writer(String path , String board)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            File.WriteAllText(path, board);
        }
    }
}
