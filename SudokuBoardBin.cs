using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega
{
    internal class SudokuBoardBin
    {
        // in this class i will try use the backtracking
        // this time will be using 

        public int size;

        public int sqrtSize;

        public int[,] sudokuBoardBin;

        public int[] rowPossibilities;

        public int[] colPossibilities;

        public int[] boxPossibilities;

        public int[] bitValues;

        
        public SudokuBoardBin()
        {

            Console.WriteLine("please enter the size\n");
            int tempInt = int.Parse(Console.ReadLine());

            //need to add validating
            Console.WriteLine("please enter the board\n");
            string tempString = Console.ReadLine();

            this.sqrtSize= (int) Math.Sqrt(tempInt);

            this.sudokuBoardBin = new int[tempInt, tempInt];

            int count = 0;
            for (int i = 0; i < tempInt; i++)
            {
                for (int j = 0; j < tempInt; j++)
                {
                    this.sudokuBoardBin[i, j] = (tempString[count] - 48);
                    count++;
                }
            }

            this.size = tempInt;

            this.rowPossibilities= new int[tempInt];
            this.colPossibilities = new int[tempInt];
            this.boxPossibilities = new int[tempInt];
            this.bitValues =  new int[tempInt];
            for (int i = 0; i < tempInt; i++)
            {
                this.rowPossibilities[i] = 0;
                this.colPossibilities[i] = 0;
                this.boxPossibilities[i] = 0;
                this.bitValues[i] = 0;
            }

            creatBitValueArray();


            initializeAllPossibilities();


            //printBoard();

            //Boolean b = backTracking();
            //Console.WriteLine(b);

            //printBoard();
        }
        public void Solve()
        {
            printBoard();

            Boolean b = backTracking();
            Console.WriteLine(b);

            printBoard();
        }


        // print the board , nothing to explain :)
        public void printBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                int modo = (int)Math.Sqrt(size);
                for (int j = 0; j < this.size; j++)
                {
                    if (this.sudokuBoardBin[i, j] >= 10)
                    {
                        Console.Write(this.sudokuBoardBin[i, j]);
                    }
                    else
                    {
                        Console.Write("0" + this.sudokuBoardBin[i, j]);

                    }
                    Console.Write(" ");
                    if (j % modo == modo - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.Write("\n");
                // hardcoded for 9x9
                if (i % modo == modo - 1)
                {
                    Console.WriteLine("-----------------");
                }
            }
        }


        // will happend once
        public void initializeAllPossibilities()
        {
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (sudokuBoardBin[i,j] != 0)
                    {
                        int v = sudokuBoardBin[i, j];
                        updatePosibilities(i,j, v);
                    }
                }
            }
        }

        public void creatBitValueArray()
        {
            for (int i = 1; i <= this.size; i++)
            {
                bitValues[i - 1] = 1 << (i-1);
            }
        }



        public void updatePosibilities(int row , int col , int value)
        {
            rowPossibilities[row] |= bitValues[value-1];
            colPossibilities[col] |= bitValues[value - 1];
            int boxNumber = getBoxNumber(row, col);
            boxPossibilities[boxNumber] |= bitValues[value - 1];
        }

        public Boolean isCellValid(int posibilitie , int value)
        {
            return (posibilitie & bitValues[value - 1]) == 0;
        }


        public int getBoxNumber(int row , int col)
        {

            return (row / this.sqrtSize) * this.sqrtSize + (col / this.sqrtSize);
        }


        //return value int which represent bin number which represent Posibilitie
        public int getPosibilitie(int row , int col)
        {
            int boxNumber = getBoxNumber(row , col);
            return rowPossibilities[row] | colPossibilities[col] | boxPossibilities[boxNumber];
        }

        public int countAllPosibilities(int row, int col)
        {
            int posibilities = getPosibilitie(row, col);
            int count = 0;
            for (int i = 0; i< this.size; i++)
            {
                if ((posibilities & (1<<i)) == 0)
                {
                    count++;
                }
            }
            return count;
        }

        // we will do solve by lowest posibilities



        public int[,] copyBoard(int[,] boardToCopy)
        {
            int [,] temp = new int[this.size, this.size];
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    temp[i,j] = boardToCopy[i, j];
                }
            }
            return temp;
        }
        public Boolean backTracking()
        {
            int[,] saveBoard = copyBoard(this.sudokuBoardBin);
            int[] saveRowPossibilities =  new int[this.size];
            int[] saveColPossibilities = new int[this.size];
            int[] saveBoxPossibilities = new int[this.size];
            Array.Copy(this.rowPossibilities, saveRowPossibilities, this.size);
            Array.Copy(this.colPossibilities, saveColPossibilities, this.size);
            Array.Copy(this.boxPossibilities, saveBoxPossibilities, this.size);

            Boolean b = hiddenSingle();
            

            int index = getLowestPosibilities();
            if (index == -1)
            {
                return true;
            }

            int row = index / this.size;
            int col = index % this.size; 



            for (int i = 1 ; i <= this.size; i++)
            {
                if (isCellValid(getPosibilitie(row, col), i))
                {
                    sudokuBoardBin[row, col] = i;
                    updatePosibilities(row, col, i);

                    if (backTracking())
                    {
                        return true;
                    }

                    //sudokuBoardBin[row, col] = 0;

                    this.rowPossibilities = saveRowPossibilities;
                    this.colPossibilities = saveColPossibilities;
                    this.boxPossibilities = saveBoxPossibilities;
                    this.sudokuBoardBin = saveBoard;
                }
            }
            return false;
        }


        // will be implemented inside the backTracking
        public Boolean hiddenSingle()
        {
            Boolean changed = false;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (sudokuBoardBin[i,j] == 0)
                    {
                        if (countAllPosibilities(i, j) == 1)
                        {
                            for (int v = 1; v <= this.size; v++)
                            {
                                if (isCellValid(getPosibilitie(i, j), v))
                                {
                                    this.sudokuBoardBin[i, j] = v;
                                    updatePosibilities(i, j, v);
                                    changed = true;
                                }
                            }
                        }
                    }
                    
                }
            }
            return changed;
        }

        public int getLowestPosibilities()
        {
            int min = this.size + 1; ;
            int index = -1;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (sudokuBoardBin[i,j] == 0)
                    {
                        int count = countAllPosibilities(i,j);
                        if (count < min)
                        {
                            min = count;
                            index = i * this.size + j;
                        }
                    }

                }
            }
           return index;
        }

    }

}
