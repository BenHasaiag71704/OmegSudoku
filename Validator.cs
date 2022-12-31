using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Omega
{
    public class Validator
    {

        //check if the cell which was just added is valid with the entire board
        // board is a copy of the board
        // row and col are the i and j of the selected number
        // size is the size of the board
        // value is the value which we trying to put inside of the cell
        public static Boolean cellValidator(Cell[,] board ,  int row , int col , int size , int value)
        {
            //check valid value
            if (value <0 || value > size)
            {
                return false;
            }

            //check value = 0 , we dont need to even check (should be 0 but still)
            if (value == 0)
            {
                return true;
            }

            //now we need to check that that the number isnt reapiting in row and col


            //row col check , if i=row , means we standing in the same col
            for (int i = 0; i < size;i++)
            {
                if (i != row)
                {
                    if (board[i,col].Value == value)
                    {
                        return false;
                    }
                }
            }


            //row col check , if i=col , means we standing in the same row

            for (int i = 0; i < size; i++)
            {
                if (i != col)
                {
                    if (board[row, i].Value == value)
                    {
                        return false;
                    }
                }
            }

            //now we need to validate the 3x3 box that the number is in


            //for example , 4,4 , we need to start at 3,3 until 5,5

            int lenght = (int)Math.Sqrt(size);

            int BoxStartingRow = row - row% lenght;

            int BoxStartingCol = col - col% lenght;

           
            for (int i = BoxStartingRow; i < BoxStartingRow + lenght; i++)
            {
                for (int j = BoxStartingCol; j < BoxStartingCol + lenght; j++)
                {
                    if (i != row && j != col)
                    {
                        if (board[i,j].Value == value)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        public static int cellHashValidatorRow(Cell[,] board, int row, int col, int size, int value)
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                HashSet<int> temp = board[i, col].possibilities;
                foreach (int v in temp)
                {
                    if (v == value)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static int cellHashValidatorCol(Cell[,] board, int row, int col, int size, int value)
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                HashSet<int> temp = board[row,i].possibilities;
                foreach (int v in temp)
                {
                    if (v == value)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static int cellHashValidatorBox(Cell[,] board, int row, int col, int size, int value)
        {


            //16*16 , lng = 4
            int count = 0;

            int lenght = (int)Math.Sqrt(size);

            int BoxStartingRow = row - row % lenght;

            int BoxStartingCol = col - col % lenght;


            HashSet<int> temp = new HashSet<int>();

            for (int i = BoxStartingRow; i < BoxStartingRow + lenght; i++)
            {
                for (int j = BoxStartingCol; j < BoxStartingCol + lenght; j++)
                {
                    temp = board[i, j].possibilities;
                    if (temp.Contains(value))
                    {
                            count++;
                    }
                }
            }
            return count;
        }

        public static int cellHashValidator(HashSet<int> temp, Cell[,] board, int row, int col, int size)
        {
            foreach (int v in temp) 
            {
                if (cellValidator(board, row, col, size, v))
                {
                    int Checkrow = cellHashValidatorRow(board, row, col, size, v);

                    int Checkcol = cellHashValidatorCol(board, row, col, size, v);

                    int CheckBox = cellHashValidatorBox(board, row, col, size, v);

                    if (Checkrow == 1 || Checkcol == 1 || CheckBox == 1)
                    {
                        return v;
                    }
                }
                else
                {
                    board[row,col].possibilities.Remove(v);
                }
            }
            return -1;
        }
    }
}
