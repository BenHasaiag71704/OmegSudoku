using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Omega.Exceptions.Exceptions;

namespace Omega.BoardValidator
{
    public class dlxBoardValidator
    {
        public static Boolean isValid(String inputedString)
        {
            if (Math.Sqrt(Math.Sqrt(inputedString.Length)) % 1 != 0)
            {
                //raise size not supported exception
                throw new sizeNotSupportedException();
            }
            int size = (int)Math.Sqrt(inputedString.Length);

            byte[,] tempMat = boardfillvalid(inputedString, size);


            canSolve(tempMat, size);

            return true;
        }

        // valid char exception
        public static byte[,] boardfillvalid(String str, int size)
        {
            // convert the string into matrix of workable numbers
            byte[,] matrix = new byte[size, size];
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = (byte)(str[count] - 48);

                    if (matrix[i, j] < 0 || matrix[i, j] > size)
                    {

                        throw new charNotSupportedException();
                    }

                    count++;
                }
            }
            return matrix;
        }


        public static void canSolve(byte[,] board, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] != 0)
                    {
                        cellValidator(board, i, j, size, board[i, j]);
                    }
                }
            }
        }
        

        // add excpetions row col box
        public static void cellValidator(byte[,] board, int row, int col, int size, int value)
        {
            //check valid value
            if (value < 0 || value > size)
            {
                throw new charNotSupportedException();

            }

            //check value = 0 , we dont need to even check (should be 0 but still)


            //now we need to check that that the number isnt reapiting in row and col


            //row col check , if i=col , means we standing in the same row

            
            for (int i = 0; i < size; i++)
            {
                if (i != col)
                {
                    if (board[row, i] == value)
                    {
                        throw new rowNotSupportedException();
                    }
                }
            }

            //row col check , if i=row , means we standing in the same col
            for (int i = 0; i < size; i++)
            {
                if (i != row)
                {
                    if (board[i, col] == value)
                    {
                        throw new colNotSupportedException();
                    }
                }
            }
            //now we need to validate the 3x3 box that the number is in


            //for example , 4,4 , we need to start at 3,3 until 5,5

            int lenght = (int)Math.Sqrt(size);

            int BoxStartingRow = row - row % lenght;

            int BoxStartingCol = col - col % lenght;


            for (int i = BoxStartingRow; i < BoxStartingRow + lenght; i++)
            {
                for (int j = BoxStartingCol; j < BoxStartingCol + lenght; j++)
                {
                    if (i != row && j != col)
                    {
                        if (board[i, j] == value)
                        {
                            throw new boxNotSupportedException();

                        }
                    }
                }
            }
        }
    }
}
