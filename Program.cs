using Omega;
using Omega.DLX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;


using static Omega.BoardValidator.dlxBoardValidator;
using static Omega.Exceptions.Exceptions;
using static Omega.FileReader.sudokuFileReader;
using static Omega.FileReader.sudokuFileWriter;


//10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<


// Desktop.9x9BoardTest.txt

public class HelloWorld
{
    public static void Main(string[] args)
    {


        Console.WriteLine("\r\n░██╗░░░░░░░██╗███████╗██╗░░░░░░█████╗░░█████╗░███╗░░░███╗███████╗  ████████╗░█████╗░  ████████╗██╗░░██╗███████╗\r\n░██║░░██╗░░██║██╔════╝██║░░░░░██╔══██╗██╔══██╗████╗░████║██╔════╝  ╚══██╔══╝██╔══██╗  ╚══██╔══╝██║░░██║██╔════╝\r\n░╚██╗████╗██╔╝█████╗░░██║░░░░░██║░░╚═╝██║░░██║██╔████╔██║█████╗░░  ░░░██║░░░██║░░██║  ░░░██║░░░███████║█████╗░░\r\n░░████╔═████║░██╔══╝░░██║░░░░░██║░░██╗██║░░██║██║╚██╔╝██║██╔══╝░░  ░░░██║░░░██║░░██║  ░░░██║░░░██╔══██║██╔══╝░░\r\n░░╚██╔╝░╚██╔╝░███████╗███████╗╚█████╔╝╚█████╔╝██║░╚═╝░██║███████╗  ░░░██║░░░╚█████╔╝  ░░░██║░░░██║░░██║███████╗\r\n░░░╚═╝░░░╚═╝░░╚══════╝╚══════╝░╚════╝░░╚════╝░╚═╝░░░░░╚═╝╚══════╝  ░░░╚═╝░░░░╚════╝░  ░░░╚═╝░░░╚═╝░░╚═╝╚══════╝\r\n\r\n░█████╗░███╗░░░███╗░█████╗░███████╗██╗███╗░░██╗░██████╗░  ░█████╗░███╗░░░███╗███████╗░██████╗░░█████╗░\r\n██╔══██╗████╗░████║██╔══██╗╚════██║██║████╗░██║██╔════╝░  ██╔══██╗████╗░████║██╔════╝██╔════╝░██╔══██╗\r\n███████║██╔████╔██║███████║░░███╔═╝██║██╔██╗██║██║░░██╗░  ██║░░██║██╔████╔██║█████╗░░██║░░██╗░███████║\r\n██╔══██║██║╚██╔╝██║██╔══██║██╔══╝░░██║██║╚████║██║░░╚██╗  ██║░░██║██║╚██╔╝██║██╔══╝░░██║░░╚██╗██╔══██║\r\n██║░░██║██║░╚═╝░██║██║░░██║███████╗██║██║░╚███║╚██████╔╝  ╚█████╔╝██║░╚═╝░██║███████╗╚██████╔╝██║░░██║\r\n╚═╝░░╚═╝╚═╝░░░░░╚═╝╚═╝░░╚═╝╚══════╝╚═╝╚═╝░░╚══╝░╚═════╝░  ░╚════╝░╚═╝░░░░░╚═╝╚══════╝░╚═════╝░╚═╝░░╚═╝\r\n\r\n░██████╗██╗░░░██╗██████╗░░█████╗░██╗░░██╗██╗░░░██╗  ░██████╗░█████╗░██╗░░░░░██╗░░░██╗███████╗██████╗░\r\n██╔════╝██║░░░██║██╔══██╗██╔══██╗██║░██╔╝██║░░░██║  ██╔════╝██╔══██╗██║░░░░░██║░░░██║██╔════╝██╔══██╗\r\n╚█████╗░██║░░░██║██║░░██║██║░░██║█████═╝░██║░░░██║  ╚█████╗░██║░░██║██║░░░░░╚██╗░██╔╝█████╗░░██████╔╝\r\n░╚═══██╗██║░░░██║██║░░██║██║░░██║██╔═██╗░██║░░░██║  ░╚═══██╗██║░░██║██║░░░░░░╚████╔╝░██╔══╝░░██╔══██╗\r\n██████╔╝╚██████╔╝██████╔╝╚█████╔╝██║░╚██╗╚██████╔╝  ██████╔╝╚█████╔╝███████╗░░╚██╔╝░░███████╗██║░░██║\r\n╚═════╝░░╚═════╝░╚═════╝░░╚════╝░╚═╝░░╚═╝░╚═════╝░  ╚═════╝░░╚════╝░╚══════╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝");

        Console.WriteLine();
        Console.WriteLine();

        
        Console.WriteLine("enter -1 at any time to stop the program");
        Console.WriteLine("press c to write a board or press f to read a board from file");

        Console.WriteLine();

        // the string of the board , from text or from file
        string tempString = "0";

        //to know what to do , are we reading from a file , text , we need to stop?
        string whereBoardComeFrom = "0";

        //in the case they chose f , store the path
        string path = "0";

        //in case that something went wrong , skip the calc part
        Boolean toJoin = false;


        while (true)
        {
            toJoin = false;
            Console.WriteLine("choose c or f");
            whereBoardComeFrom = Console.ReadLine();

            
            if (whereBoardComeFrom == "c" || whereBoardComeFrom == "C")
            {
                 
                Console.WriteLine("please enter the a board\n");
                tempString = Console.ReadLine();
                toJoin = true;
            }
            else if (whereBoardComeFrom == "f" || whereBoardComeFrom == "F")
            {
                try
                {
                   (tempString, path) = Reader();
                    toJoin = true;
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            if (whereBoardComeFrom == "-1")
            {
                break;
            }

            if (toJoin == false)
            {
                Console.WriteLine("invalid choise , please try again");
            }

            else if (tempString == "")
            {
                Console.WriteLine("you cannot enter an empty board");
            }
            else 
            {
                try
                {
                    isValid(tempString);

                    int lng = (int)Math.Sqrt(tempString.Length);


                    DLXSudokuBoard dlx = new DLXSudokuBoard(tempString, lng);

                    dlx.printBoard();

                    dlx.finalSolve();

                    dlx.printBoard();


                    String solvedBoard = dlx.SolvedBoard;

                    if (whereBoardComeFrom == "f")
                    {
                        if (solvedBoard == "")
                        {
                            solvedBoard = "cant solve";
                        }
                        Writer(path, solvedBoard);
                    }
                    if (solvedBoard != "")
                    {
                        Console.WriteLine(solvedBoard);
                    }
                }
                catch (sizeNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (charNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (rowNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (colNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (boxNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            



        }
    }

}