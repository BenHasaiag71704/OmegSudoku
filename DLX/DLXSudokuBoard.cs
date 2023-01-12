﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Omega.DLX
{
    public class DLXSudokuBoard
    {

        //  size of the board
        public int size;


        //  size of the board^0.5 (so we wont calc it over and over)
        public int sqrtSize;


        // the board itself
        public byte[,] matrix;


        //size n^3 * (n^2*4)
        // the matrix which holds all the 0 and 1
        // will be used to create the linked list which the algorithm utilize 
        public byte[,] coverMatrix;

        // stack of all the nodes which will make out the answer to the board
        public Stack<BaseDLXNode> dlxStack;


        //use it as a link to the giant linked list we create.
        //used to point at all the fathers 
        public AdvanceDLXNode fatherOfAll;


        public DLXSudokuBoard()
        {
            //creating the board , will be improved soon

            var watch = new System.Diagnostics.Stopwatch();
            Console.WriteLine("please enter the size\n");
            int tempInt = int.Parse(Console.ReadLine());
            this.size = tempInt; 

            this.sqrtSize = (int) Math.Sqrt(tempInt);


            Console.WriteLine("please enter the boardString");
            string tempString = Console.ReadLine();

            watch.Start();
            boardFill(tempString);

            initCoverMatrix();


            ConvertMatrixIntoNodeMatrix();


            this.dlxStack= new Stack<BaseDLXNode>();


            // start the solve fucntion , which is search
            //watch.Start();
            Boolean b = search();
            watch.Stop();
            if (b)
            {
                ConverBackToBoard();
                printBoard();
                Console.WriteLine($"Execution Time: {watch.Elapsed.TotalMilliseconds} ms");

            }
            else
            {
                Console.WriteLine("cant solve");
            }



        }
        public void boardFill(String str)
        {
            // convert the string into matrix of workable numbers
            this.matrix = new byte[size, size];
            int count = 0;
            for (int i = 0; i < this.size; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    this.matrix[i, j] = (byte) (str[count] - 48);
                    count++;
                }
            }
        }



        // this function is used to generate the coverMatrix
        // it will create a sudoku representation as a binary matrix
        // the cover matrix is at size : size^5 * 4
        // size*size*4 - for each cell-row-col-box
        // size*size*size for the row col and all posibilities we can put inside it

        public void initCoverMatrix()
        {
            // initialize the cover matrix
            coverMatrix = new byte[size * size * size, size * size * 4];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                { 
                    for (int value = 0; value < size; value++)
                    {
                        // if you are a posible value (mat[row,col] = 0)
                        // or if you are the only option
                        if (matrix[row, col] == 0 || matrix[row, col] == value+1)
                        {
                            // the row in the cover matrix we need to insert to [X,]
                            int rowPlace = firstDimensionPlacement(row, col, value);

                            // setting the mat in the calculated spots to 1
                            coverMatrix[rowPlace, cellPlacement(row, col)] = 1;


                            coverMatrix[rowPlace, this.rowPlacement(row, value)] = 1;

                            
                            coverMatrix[rowPlace, colPlacement(col, value)] = 1;

                            
                            coverMatrix[rowPlace, boxPlacement(row, col, value)] = 1;
                        }
                    }
                }
            }
        }

        private int firstDimensionPlacement(int row, int col, int value)
        {
            return row * size * size + col * size + value;
        }

        private int cellPlacement(int row, int col)
        {
            return row * size + col;
        }

        private int rowPlacement(int row, int value)
        {
            return size * size + row * size + value;
        }

        private int colPlacement(int col, int value)
        {
            return size * size * 2 + col * size + value;
        }

        private int boxPlacement(int row, int col, int value)
        {
            return size * size * 3 +
                   (row / sqrtSize * sqrtSize + col / sqrtSize) * size + value;
        }




        // converting the cover matrix into doubly linked node
        // to use in search func
        public void ConvertMatrixIntoNodeMatrix()
        {
            //init the father of all - the "holder" of the linked list
            this.fatherOfAll = new AdvanceDLXNode(-1);


            // creating the father list - size*size*(cell+row+col+box , or 4)
            AdvanceDLXNode[] fatherArray = new AdvanceDLXNode[size * size * 4];


            for (int index = 0; index < size * size * 4; index++)
            {
                

                //fatherArray[index] = new AdvanceDLXNode(index);
                //this.fatherOfAll.linkRight(fatherArray[index]);


                //init the father list
                // first time we connect to the father of all
                // after that we can connec to the fathers , they all hold each
                // others from right and left thus they all connected
                if (index == 0)
                {
                    fatherArray[index] = new AdvanceDLXNode(index);
                    this.fatherOfAll.linkRight(fatherArray[index]);
                }
                else
                {
                    fatherArray[index] = new AdvanceDLXNode(index);
                    fatherArray[index - 1].linkRight(fatherArray[index]);
                }


            }

            int value = 0;


            // the father of all the nodes we inserted
            AdvanceDLXNode fatherNode;

            // node to insert
            BaseDLXNode insertNode;

            BaseDLXNode lastNode;


            for (int row = 0; row < size * size * size  ; row++)
            {
                // save the last insertned node to connect to new ones , init to null every loop
                lastNode = null;

                for (int col = 0; col < size * size * 4; col++)
                {
                    // creating a node only when the value isnt 0 
                    value = this.coverMatrix[row, col];
                    if (value == 0)
                    {

                    }
                    else
                    {
                        // gettint the correct father for each insert
                        fatherNode = fatherArray[col];
                        insertNode = new BaseDLXNode(fatherNode);
                        // linking the new node to the father
                        fatherNode.Up.linkDown(insertNode);

                        // inserting the new nodes
                        if (lastNode != null)
                        {
                            lastNode.linkRight(insertNode);
                            lastNode = lastNode.Right;
                        }
                        // the case of lastNode = null 
                        // make the current node into the node to insert
                        else
                        {
                            lastNode = insertNode;
                        }

                        //update the size so we know how many nodes the father is linking to
                        // will be used later in search
                        fatherNode.Size++;
                    }
                }
            }
        }



        // going over all the fathers and returning the father which hold the least
        // amont of nodes - most efective way of selectiong for fast calculating
        public AdvanceDLXNode chooseColumnObject()
        {
            AdvanceDLXNode n = (AdvanceDLXNode) this.fatherOfAll.Right;

            AdvanceDLXNode saveNode= null;

            // max size of nodes the father can hold
            int TheSize = this.size * this.size * this.size;

            while (n != this.fatherOfAll)
            {
                // saving the min
                if (n.Size < TheSize)
                {
                    TheSize = n.Size;
                    saveNode = n;
                }
                n = (AdvanceDLXNode) n.Right;
            }
            // returning the father
            return saveNode;
        }


        public Boolean search()
        {
            // if the father.right = father , means there are no move fathers linked 
            // in the list , means we have all the solutions we need
            if (fatherOfAll.Right == fatherOfAll)
            {
                return true;
            }


            // choosing the colunm to cover
            AdvanceDLXNode chosenNode = chooseColumnObject();

            // covering it
            chosenNode.Cover();

            BaseDLXNode RowNode = chosenNode.Down;
            BaseDLXNode temp;


            // going ove all the nodes in the corrent col , and covering all the
            // noded in the cols that are connected

            //stop when the row is empty (we covers all the cols and all the cols which were connected)
            while (RowNode != chosenNode)
            {
                //pushing the corrent row , which will be used to calc the answer
                this.dlxStack.Push(RowNode);

                temp = RowNode.Right;
                // covering all the cols in the same column
                while (temp != RowNode)
                {
                    temp.father.Cover();
                    temp = temp.Right;
                }

                // calling the function again
                if (search())
                {
                    return true;
                }


                // if we pass this , search havent found a solution
                this.dlxStack.Pop();

                // we now need to uncover everything we covers sofar to
                // restore the fucntion to its original state
                chosenNode = RowNode.father;

                temp = RowNode.Left;

                while (temp != RowNode)
                {
                    temp.father.unCover();
                    temp = temp.Left;
                }

                RowNode = RowNode.Down;
            }
            //uncover the node we chose to cover at the start
            // ending the restoration of the linked list 
            chosenNode.unCover();

            //  return false (so the if knows to get into this point , and reset what is needed)
            return false;
        }



        // last func to finish it all
        // if the seached worked , the stack will hold the all the answers as base nodes
        // we will translate them into sudoku board
        public void ConverBackToBoard()
        {

            //foreach elemnt in the stack , which we need to translate
            foreach (BaseDLXNode n in dlxStack)
            {

                // copy so we can work with it (wont let me work with n , each node)
                BaseDLXNode copyN = n;

                // getting the value of the father (its index)
                int minimum = copyN.father.IndexName;

                // go over all the fathers which have childrens that are connected into out node
                // pick the smallest index 
                for (BaseDLXNode temp = copyN.Right; temp != copyN; temp = temp.Right)
                {
                    int v = temp.father.IndexName;
                    if (v < minimum)
                    {
                        minimum = v;
                        copyN = temp;
                    }
                }

                // calculate where and what to put in the matrix

                // name index represents the index in the array we need to put the value in
                int rowcol = copyN.father.IndexName;


                // /size for row and % size for col (same as it was with backtracking)
                int row = rowcol / this.size;
                int col = rowcol % this.size;


                // the value to place is calculated by father to the right of the node
                

                //out board dont have any zeros , so we incress by 1
                int value = copyN.Right.father.IndexName % this.size + 1;

                this.matrix[row, col] = (byte) value;


            }
        }


        public void printBoard()
        {
            Console.Write(" ");
            for (int j = 0; j < this.size + 1; j++)
            {
                Console.Write("---");
            }
            Console.WriteLine();

            for (int i = 0; i < this.size; i++)
            {
                int modo = (int)Math.Sqrt(size);
                for (int j = 0; j < this.size; j++)
                {
                    if (this.matrix[i, j] >= 10)
                    {
                        if (j == 0)
                        {
                            Console.Write("|" + this.matrix[i, j]);
                        }
                        else
                        {
                            Console.Write(this.matrix[i, j]);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            Console.Write("|0" + this.matrix[i, j]);
                        }
                        else
                        {
                            Console.Write("0" + this.matrix[i, j]);
                        }

                    }
                    Console.Write(" ");
                    if (j % modo == modo - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.Write("\n");
                if (i % modo == modo - 1)
                {
                    Console.Write(" ");
                    for (int j = 0; j < this.size + 1 ; j++) {
                        Console.Write("---");
                    }
                    Console.WriteLine();
                }
            }
        }



    }
}
