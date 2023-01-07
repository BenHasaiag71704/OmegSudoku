using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public int size;

        public int sqrtSize;

        public byte[,] matrix;


        //size n^3 * (n^2*4)
        public byte[,] coverMatrix;

        public Stack<BaseDLXNode> dlxStack;

        public AdvanceDLXNode fatherOfAll;


        public DLXSudokuBoard()
        {
            Console.WriteLine("please enter the size\n");
            int tempInt = int.Parse(Console.ReadLine());
            this.size = tempInt; 

            this.sqrtSize = (int) Math.Sqrt(tempInt);


            Console.WriteLine("please enter the boardString");
            string tempString = Console.ReadLine();

            
            boardFill(tempString);

            initCoverMatrix();

            ConvertMatrixIntoNodeMatrix();



            this.dlxStack= new Stack<BaseDLXNode>();

            Boolean b = search();

            if (b)
            {
                ConverBackToBoard();
                printBoard();
            }
            else
            {
                Console.WriteLine("cant solve");
            }



        }
        public void boardFill(String str)
        {
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


        public void initCoverMatrix()
        {
            coverMatrix = new byte[size * size * size, size * size * 4];

            int Placement = 0;
            int matrixValue = 0;
            int cellShifter = 0;
            int colShifter = size * size;
            int rowShifter = size * size * 2;
            int boxShifter = size * size * 3;
            int rowAsymmetric = 0;
            int boxStrarting = 0;

            for (int row = 0; row < size; row++)
            {
                colShifter = size * size;

                for (int col = 0; col < size; col++)
                {
                    matrixValue = matrix[row, col];

                    boxStrarting = (row / sqrtSize) * sqrtSize + col / sqrtSize;

                    for (int CurrentCandidate = 1; CurrentCandidate <= size; CurrentCandidate++)
                    {

                        if (matrixValue == 0 || matrixValue == CurrentCandidate)
                        {
                            coverMatrix[Placement, cellShifter] = 1;

                            coverMatrix[Placement, colShifter] = 1;

                            rowAsymmetric = CurrentCandidate - 1;
                            coverMatrix[Placement, rowShifter + rowAsymmetric] = 1;

                            coverMatrix[Placement, boxShifter + boxStrarting * size + rowAsymmetric] = 1;
                        }

                        Placement++;
                        colShifter++;
                    }

                    cellShifter++;
                }

                rowShifter += size;
            }

            return;
        }

        public void ConvertMatrixIntoNodeMatrix()
        {

            this.fatherOfAll = new AdvanceDLXNode(-1);

            AdvanceDLXNode[] fatherArray = new AdvanceDLXNode[size * size * 4];


            for (int index = 0; index < size * size * 4; index++)
            {
                fatherArray[index] = new AdvanceDLXNode(index);
                this.fatherOfAll.linkRight(fatherArray[index]);
            }

            int value = 0;

            AdvanceDLXNode fatherNode;

            BaseDLXNode insertNode;
            BaseDLXNode lastNode;


            for (int row = 0; row < size * size * size; row++)
            {

                lastNode = null;

                for (int col = 0; col < size * size * 4; col++)
                {
                    value = this.coverMatrix[row, col];
                    if (value == 0)
                    {
                        
                    }
                    else
                    {
                        fatherNode = fatherArray[col];
                        insertNode = new BaseDLXNode(fatherNode);
                        fatherNode.Up.linkDown(insertNode);

                        if (lastNode != null)
                        {
                            lastNode.linkRight(insertNode);
                            lastNode = lastNode.Right;
                        }
                        else
                        {
                            lastNode = insertNode;
                        }
                        fatherNode.Size++;
                    }
                }
            }
        }

        public AdvanceDLXNode chooseColumnObject()
        {
            AdvanceDLXNode n = (AdvanceDLXNode) this.fatherOfAll.Right;

            AdvanceDLXNode saveNode= null;

            int size = this.size * this.size * this.size;

            while (n != this.fatherOfAll)
            {
                if (n.Size < size)
                {
                    size = n.Size;
                    saveNode = n;
                }
                n = (AdvanceDLXNode) n.Right;
            }

            return saveNode;
        }


        public Boolean search()
        {
            if (fatherOfAll.Right == fatherOfAll)
            {
                return true;
            }

            AdvanceDLXNode chosenNode = chooseColumnObject();
            chosenNode.Cover();

            BaseDLXNode RowNode = chosenNode.Down;
            BaseDLXNode temp;

            while (RowNode != chosenNode)
            {

                this.dlxStack.Push(RowNode);

                temp = RowNode.Right;

                while (temp != RowNode)
                {
                    temp.father.Cover();
                    temp = temp.Right;
                }

                if (search())
                {
                    return true;
                }

                this.dlxStack.Pop();


                chosenNode = RowNode.father;

                temp = RowNode.Left;

                while (temp != RowNode)
                {
                    temp.father.unCover();
                    temp = temp.Left;
                }

                RowNode = RowNode.Down;
            }

            chosenNode.unCover();


            return false;
        }

        public void ConverBackToBoard()
        {
            foreach (BaseDLXNode n in dlxStack)
            {

                BaseDLXNode copyN = n;


                int minimum = copyN.father.IndexName;

                for (BaseDLXNode temp = copyN.Right; temp != copyN; temp = temp.Right)
                {
                    int v = temp.father.IndexName;
                    if (v < minimum)
                    {
                        minimum = v;
                        copyN = temp;
                    }
                }
                int rowcol = copyN.father.IndexName;

                int row = rowcol / this.size;
                int col = rowcol % this.size;

                int value = copyN.Right.father.IndexName % this.size + 1;

                this.matrix[row, col] = (byte) value;


            }
        }



        public void printBoard()
        {
            for (int i = 0; i < this.size; i++)
            {
                int modo = (int)Math.Sqrt(size);
                for (int j = 0; j < this.size; j++)
                {
                    if (this.matrix[i, j] >= 10)
                    {
                        Console.Write(this.matrix[i, j]);
                    }
                    else
                    {
                        Console.Write("0" + this.matrix[i, j]);

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

    }

}
