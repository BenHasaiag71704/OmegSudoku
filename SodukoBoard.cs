using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using static Omega.Validator;


namespace Omega
{
    public class SodukoBoard
    {
        public int size { get; }

        public string inputedString { get; }

        public Cell[,] board { get; set; }

        public SodukoBoard()
        {
            //need to add validating
            Console.WriteLine("please enter the size\n");
            int tempInt = int.Parse(Console.ReadLine());
            this.size = tempInt;

            //need to add validating
            Console.WriteLine("please enter the board\n");
            string tempString = Console.ReadLine();
            this.inputedString = tempString;

            this.board = new Cell[tempInt, tempInt];

            boardFill(tempString);
        }

        public void boardFill(String str)
        {
            int count = 0;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j<this.size; j++)
                {
                    this.board[i, j] = new Cell(Convert.ToInt32(str[count].ToString()));
                    count++;
                }
            }
        }

        public void printBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    Console.Write(this.board[i, j].Value);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }


        public Boolean SolveBoard()
        {
            int[] temp = findFirstEmpty();
            if (temp == null)
            {
                return true;
            }
            //to fix , not /% but array (16,15 will return 17 at row)
            int row = temp[0];
            int col = temp[1];

            for (int i = 1; i <=size; i++)
            {
                if (cellValidator(this.board , row , col , this.size , i))
                {
                    board[row, col].Value = i;
                    if (SolveBoard())
                    {
                        return true;
                    }
                    board[row, col].Value = 0;
                }
            }
            return false;
        }

        public int[] findFirstEmpty()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (this.board[i,j].Value == 0)
                    {
                        return new int[] {i,j};

                        // lets say the first empty is 2,3
                        // 23 , 23/10 = 2 , and 23%10 = 3
                    }
                }
            }
            return null;
        }
        
    }
}
