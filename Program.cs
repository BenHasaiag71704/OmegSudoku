using Omega;
using System;
using System.Diagnostics;

public class HelloWorld
{
    public static void Main(string[] args)
    {

        SodukoBoard cs = new SodukoBoard();

        cs.printBoard();

        Boolean b = cs.SolveBoard();

        Console.WriteLine(" ");

        cs.printBoard();

    }

}