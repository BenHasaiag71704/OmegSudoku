using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.Exceptions
{
    public class Exceptions
    {
        public class sizeNotSupportedException : Exception
        { 
            
            public sizeNotSupportedException() : base("the board size is invalid")
            {
                
            }
        }

        public class charNotSupportedException : Exception
        {

            public charNotSupportedException() : base("this char is invalid for this board size")
            {

            }
        }


        public class rowNotSupportedException : Exception
        {

            public rowNotSupportedException() : base("there is invalid row")
            {

            }
        }


        public class colNotSupportedException : Exception
        {

            public colNotSupportedException() : base("there is invalid col")
            {

            }
        }

        public class boxNotSupportedException : Exception
        {
            public boxNotSupportedException() : base("there is invalid box")
            {

            }
        }
    }
}
