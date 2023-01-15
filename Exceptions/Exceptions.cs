using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.Exceptions
{
    public class Exceptions
    {
        //the size is not valid
        public class SizeNotSupportedException : Exception
        { 
            
            public SizeNotSupportedException() : base("the board size is invalid")
            {
                
            }
        }
        //when an unvalid char is used for a board , for example , E cant be placed in 9x9 board
        public class CharNotSupportedException : Exception
        {

            public CharNotSupportedException() : base("this char is invalid for this board size")
            {

            }
        }

        //in the case there are 2 of the same number in the same row
        public class RowNotSupportedException : Exception
        {

            public RowNotSupportedException() : base("there is invalid row")
            {

            }
        }

        //in the case there are 2 of the same number in the same col
        public class ColNotSupportedException : Exception
        {

            public ColNotSupportedException() : base("there is invalid col")
            {

            }
        }

        //in the case there are 2 of the same number in the same box
        public class BoxNotSupportedException : Exception
        {
            public BoxNotSupportedException() : base("there is invalid box")
            {

            }
        }
    }
}
