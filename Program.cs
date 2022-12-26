using Omega;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;



using static Omega.Validator;


public class HelloWorld
{
    public static void Main(string[] args)
    {
        var watch = new System.Diagnostics.Stopwatch();
        SodukoBoard cs = new SodukoBoard();
        Console.WriteLine("The Original Board : ");
        cs.printBoard();


        //TODO : adding simple elimination
        //TODO : adding naked singles
        //TODO : update board
        Console.WriteLine("\n\n");


        watch.Start();
        while (cs.simpleElimination())
        {

        }

        Console.WriteLine("board after simple : ");
        cs.printBoard();
        Console.WriteLine("\n\n");


        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine(" ");



        //int x = cellHashValidatorBox(cs.board, 0, 0 , 9, 4);

        //Console.WriteLine(x);

        //int count = 0;
        //for (int i = 0; i < 3; i++)
        //{
        //    for (int j = 0; j < 3; j++)
        //    {
        //        Console.Write("in cell " + "(" + i + "," + j + ") : ");
        //        HashSet<int> set = cs.board[i, j].possibilities;
        //        foreach (int p in set)
        //        {
        //            Console.Write(p + " ");
        //            if (p == 4)
        //            {
        //                count++;
        //            }
        //        }
        //        Console.WriteLine();

        //    }
        //    Console.WriteLine();
        //}


        //Console.WriteLine("the count : " + count);
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine(" ");





        Console.WriteLine(" ");

        while (cs.hiddenSingle())
        {

        }
        Console.WriteLine("board hidden single : ");
        cs.printBoard();









        Console.WriteLine(" ");

        
        Boolean b = cs.SolveBoard();
        watch.Stop();


        cs.printBoard();
        Console.WriteLine(" ");

        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

    }

}