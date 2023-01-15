using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using static Omega.backTrackingValidator;


namespace Omega
{
    public class SodukoBoard
    {

        public static int[] EmptyArray { get; set; }

        public static int EmptyCountAndLocation;

        public int size { get; }

        public string inputedString { get; }

        public Cell[,] board { get; set; }

        public Boolean isValid { get; set; }


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

             this.isValid = true;


            //check that the size is perfect squre , if so , continue to next valid
            //4*4 board dont have 
            if (Math.Abs(Math.Sqrt(this.size) % 1) != 0)
            {
                this.isValid = false;
                Console.WriteLine("please enter a size which is a perfect square");
            }


            // check that the string lng mach the size
            if (this.size * this.size != tempString.Length)
            {
                this.isValid = false;
                Console.WriteLine("the size of the string must be the size^2");
            }

            // if the size and string size is valid , filling the board
            // if char is not valid for this board , swaping isValid to false , thus following funcs wont be used
            if (this.isValid)
            {

                this.board = new Cell[tempInt, tempInt];
                boardFill(tempString);
                if (!this.isValid)
                {
                    Console.WriteLine("a non valid char was inputed");
                }
            }

            // this func wont change isValid , but needs it to be valid to run
            if (this.isValid)
            {
                createEmptyArray();
            }
            // again , this function need isValid to be true in order to run , this is a board checker
            // use this to valid board before we every start
            if (this.isValid)
            {
                this.isValid = isSolveableBoard();
                // display proper message if needed
                if (!this.isValid)
                {
                    Console.WriteLine("Board is Not valid");
                }
            }
        }


        public void boardFill(String str)
        {
            int count = 0;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j<this.size; j++)
                {
                    this.board[i, j] = new Cell(Convert.ToInt32((int)str[count] - 48), this.size);

                    if (Convert.ToInt32((int)str[count] - 48) < 0 || Convert.ToInt32((int)str[count] - 48) > this.size)
                    {
                        this.isValid = false;
                        break;
                    }
                    count++;
                }
            }
            EmptyCountAndLocation = count;
        }

        public void createEmptyArray()
        {
            int tempCounter = 0;
            EmptyArray = new int[EmptyCountAndLocation];
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j<this.size; j++)
                {
                    if (this.board[i, j].Value == 0)
                    {
                        EmptyArray[tempCounter] = i * this.size + j;
                        tempCounter++;
                    }
                }
            }
            EmptyCountAndLocation = 0;
        }

        public void printBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                int modo = (int) Math.Sqrt(size);
                for (int j = 0; j < this.size; j++)
                {
                    if (this.board[i, j].Value >= 10)
                    {
                        Console.Write(this.board[i, j].Value);
                    }
                    else
                    {
                        Console.Write("0" + this.board[i, j].Value);

                    }
                    Console.Write(" ");
                    if (j% modo == modo-1)
                    {
                        Console.Write("|");
                    }
                }
                Console.Write("\n");
                // hardcoded for 9x9
                if (i% modo == modo-1)
                {
                    Console.WriteLine("-----------------");
                }
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
                    }
                }
            }
            return null;
        }


        public Boolean isSolveableBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (this.board[i, j].Value != 0)
                    {
                        if (!cellValidator(this.board, i, j, this.size, this.board[i, j].Value))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        public int[] findFirstEmptyByHashSize()
        {

            //
            //
            for (int count = 2; count <= this.size; count++)
            {
                for (int i = 0; i < this.size; i++)
                {
                    for (int j = 0; j < this.size; j++)
                    {
                        if (this.board[i, j].Value == 0)
                        {
                            if (this.board[i, j].possibilities.Count == count)
                            {
                                return new int[] { i, j };

                            }
                        }
                    }
                }
            }
            return null;
        }


        public Boolean updateValue()
        {
            Boolean b = false;

            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    HashSet<int> temp = this.board[i, j].possibilities;
                    if (temp.Count == 1)
                    {
                        this.board[i, j].Value = temp.First();
                        this.board[i, j].possibilities.Remove(temp.First());
                        b = true;
                    }
                }
            }
            return b;
        }

        private void updatePosibilities(int row, int col)
        {
            int value = board[row, col].Value;

            for (int i = 0; i < size; i++)
            {
                if (i != col)
                {
                    board[row, i].possibilities.Remove(value);
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (i != row)
                {
                    board[i, col].possibilities.Remove(value);
                }
            }

            int lenght = (int)Math.Sqrt(size);


            int BoxStartingRow = row - row % lenght;
            int BoxStartingCol = col - col % lenght;


            for (int i = BoxStartingRow; i < BoxStartingRow + lenght; i++)
            {
                for (int j = BoxStartingCol; j < BoxStartingCol + lenght; j++)
                {
                    if (i != row && j != col)
                    {
                        board[i, j].possibilities.Remove(value);
                    }
                }
            }
        }




















        public Boolean simpleElimination()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    HashSet<int> temp = this.board[i, j].possibilities;
                    if (temp.Count > 0)
                    {
                        foreach (int value in temp)
                        {
                            if (!(cellValidator(this.board , i , j , this.size , value)))
                            { 
                                temp.Remove(value);
                            }
                        }
                    }
                }
            }
            Boolean needContinue = updateValue();
            return needContinue;
        }




        public Boolean hiddenSingle()
        {
            Boolean changed = false;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    HashSet<int> temp = this.board[i, j].possibilities;
                    if (temp.Count > 0)
                    {
                        int needDelete = cellHashValidator(temp, this.board, i, j, this.size);
                        if (needDelete != -1)
                        {
                            this.board[i, j].possibilities.Clear();
                            this.board[i, j].Value = needDelete;
                            updatePosibilities(i, j);
                            changed = true;
                        }
                    }
                }
            }
            //if (changed)
            //{
            //    Boolean b = updateValue();
            //    return b;
            //}
            return changed;
        }



        public int[] findFirstEmptyBackwards()
        {
            for (int i = this.size - 1 ; i >= 0 ; i--)
            {
                for (int j = this.size - 1; j >= 0 ; j--)
                {
                    if (this.board[i, j].Value == 0)
                    {
                        return new int[] { i, j };

                        // lets say the first empty is 2,3
                        // 23 , 23/10 = 2 , and 23%10 = 3
                    }
                }
            }
            return null;
        }

        public Boolean SolveBoardBackwards()
        {

            int[] temp = findFirstEmptyBackwards();
            if (temp == null)
            {
                return true;
            }
            //to fix , not /% but array (16,15 will return 17 at row)
            int row = temp[0];
            int col = temp[1];

            for (int i = 1; i <= size; i++)
            {
                if (cellValidator(this.board, row, col, this.size, i))
                {
                    board[row, col].Value = i;
                    if (SolveBoardBackwards())
                    {
                        return true;
                    }
                    board[row, col].Value = 0;
                }
            }
            return false;
        }
        


        public Boolean SolveBoardHash()
        {
            int[] temp = findFirstEmpty();
            if (temp == null)
            {
                return true;
            }
            //to fix , not /% but array (16,15 will return 17 at row)
            int row = temp[0];
            int col = temp[1];

            HashSet<int> visited = this.board[row, col].possibilities;
            foreach (int i in visited)
            {
                if (cellValidator(this.board, row, col, this.size, i))
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



        public Boolean SolveBoardHashAndBySize()
        {
            int[] temp = findFirstEmptyByHashSize();
            if (temp == null)
            {
                return true;
            }
            //to fix , not /% but array (16,15 will return 17 at row)
            int row = temp[0];
            int col = temp[1];

            HashSet<int> visited = this.board[row, col].possibilities;
            foreach (int i in visited)
            {
                if (cellValidator(this.board, row, col, this.size, i))
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

        public Boolean SolveBoardBackwardsAndBySize()
        {

            int[] temp = findFirstEmptyByHashSize();
            if (temp == null)
            {
                return true;
            }
            //to fix , not /% but array (16,15 will return 17 at row)
            int row = temp[0];
            int col = temp[1];

            for (int i = 1; i <= size; i++)
            {
                if (cellValidator(this.board, row, col, this.size, i))
                {
                    board[row, col].Value = i;
                    if (SolveBoardBackwards())
                    {
                        return true;
                    }
                    board[row, col].Value = 0;
                }
            }
            return false;
        }





        public Boolean SolveBoardEmptyArray()
        {
            if (EmptyCountAndLocation < 0 || EmptyCountAndLocation >= EmptyArray.Length)
            {
                return true;
            }


            int row = EmptyArray[EmptyCountAndLocation] / this.size;
            int col = EmptyArray[EmptyCountAndLocation] % this.size;

            for (int i = 1; i <= size; i++)
            {
                if (cellValidator(this.board, row, col, this.size, i))
                {
                    EmptyCountAndLocation++;
                    board[row, col].Value = i;
                    if (SolveBoard())
                    {
                        return true;
                    }
                    EmptyCountAndLocation--;
                    board[row, col].Value = 0;
                }
            }
            return false;
        }


    }
}
