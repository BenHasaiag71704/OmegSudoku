using Omega;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;



using static Omega.Validator;


//10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<


public class HelloWorld
{
    public static void Main(string[] args)
    {

        Console.WriteLine("Welcome to the sudoku solver!");


        var watch = new System.Diagnostics.Stopwatch();
        SodukoBoard cs = new SodukoBoard();

        
        if (cs.isValid)
        {
            Console.WriteLine("The Original Board : ");
            cs.printBoard();
            watch.Start();

            while (cs.simpleElimination())
            {

            }
            Console.WriteLine(" ");

            while (cs.hiddenSingle())
            {

            }
            //Boolean b = cs.SolveBoardHashAndBySize();

            //Boolean b = cs.SolveBoardBackwards();

            //Boolean b = cs.SolveBoard();


            //problematic on full empty board;

            //Boolean b = cs.SolveBoardBackwardsAndBySize();

            //Boolean b = cs.SolveBoardHash();

            //Boolean b = cs.SolveBoardEmptyArray();


            watch.Stop();


            cs.printBoard();
            Console.WriteLine(" ");

            Console.WriteLine($"Execution Time: {watch.Elapsed.TotalMilliseconds} ms");
        }
        else
        {
            Console.WriteLine("Board Is Not Valid");
        }


        Console.ReadLine();

    }

}