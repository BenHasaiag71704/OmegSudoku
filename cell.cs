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
        public HashSet<int> possibilities { get; set; }

        public Cell(int value , int size)
        {
            if (value == 0)
            {
                this.hasValue = false;
                this.Value = 0;
                this.possibilities = new HashSet<int>();
                for (int i = 1; i <= size; i++)
                {
                    this.possibilities.Add(i);
                }
            }
            else
            {
                this.hasValue = true;
                this.Value = value;
                this.possibilities = new HashSet<int>();
            }
        }

    }
}
