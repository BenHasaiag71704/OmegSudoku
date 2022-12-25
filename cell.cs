using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega
{
    public class Cell
    {
        public int Value { get; set; }
        public Boolean hasValue { get; set; }


        //will be worked on in the future when adding eliminations
        public HashSet<int> allPossibilities { get; set; }

        public Cell(int value)
        {
            if (value == 0)
            {
                this.hasValue = false;
                this.Value = 0;
            }
            else
            {
                this.hasValue = true;
                this.Value = value;
            }
        }




    }
}
