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


        string tempString = "0";
        string whereBoardComeFrom = "0";
        string path = "0";

        while (true)
        {

            Console.WriteLine("choose c or f");
            whereBoardComeFrom = Console.ReadLine();

            
            if (whereBoardComeFrom == "c")
            {
                Console.WriteLine("please enter the a board\n");
                tempString = Console.ReadLine();
            }
            else if (whereBoardComeFrom == "f")
            {
                (tempString,path) = Reader();
            }


            if (tempString == "-1")
            {
                break;
            }

            try
            {
                isValid(tempString);

                int lng = (int)Math.Sqrt(tempString.Length);

                DLXSudokuBoard dlx = new DLXSudokuBoard(tempString, lng);

                String solvedBoard = dlx.SolvedBoard;

                if (whereBoardComeFrom == "f")
                {
                    if (solvedBoard == "")
                    {
                        solvedBoard = "cant solve";
                    }
                    Writer(path , solvedBoard);
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