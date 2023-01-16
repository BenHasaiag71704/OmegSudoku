
# Omega Sudoku Solver in C#


## Introdction
First of all , what is a Sudoku?
Sudoku is a placement puzzle dating from the late 19th century.
the goal of the puzzel is to fill the entirety of a giving board such that in each row colium and box will containe each number from 1 to S only once.

![rolcolbox](https://user-images.githubusercontent.com/92589632/212725952-a0d5925c-1d61-4a3d-a71d-399b544b70bc.PNG)

## Description 

This project is my attempt of creating an algorithm which is able to solve sudoku board by sizes 1x1 4x4 9x9 16x6 and 25x25 while in an incredible speed

## Requirements

.Net 6.0 instralled on your device

## Features

* Transformation of the sudoku puzzle into an exact cover problem.

* Implementation of Knuth's Algorithm X on mentioned problem.

* Support for sudoku boards in the sizes mentioned above (and more) along with input from both console and files.


## Instructions
To use the solver , you will need to call those two lines.

Board will represent the board the user input (as a string) , and length is the square root of the string size
``` 
DLXSudokuBoard dlx = new DLXSudokuBoard(Board, length);
dlx.FinalSolve();
``` 
For example
``` 
DLXSudokuBoard dlx = new DLXSudokuBoard(0000000000000000,4);
dlx.FinalSolve();
``` 
After that , you may call dlx.PrintBoard() to print the board into the console
or look into the string dlx.SolvedBoard which will hold the answer in a string form.

In the example showen above , dlx.SolvedBoard holds "1234341221434321"
and dlx.PrintBoard() will result
```
 ---------------
|01 02 |03 04 |
|03 04 |01 02 |
 ---------------
|02 01 |04 03 |
|04 03 |02 01 |
---------------
```

# Extras
for those who seeks more ways to solve a sudoku board , please have a look at commit 07783b3 which holds two of my older attempts of solving a sudoku board using the backtracking algorithm in numerus ways.  
for those who are interested in the full explanation about the dancing links could visit the articel : https://tinyurl.com/mtcybdsr
