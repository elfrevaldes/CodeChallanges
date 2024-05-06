using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    /*
     * Complete the 'queensAttack' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER r_q
     *  4. INTEGER c_q
     *  5. 2D_INTEGER_ARRAY obstacles
     */

    public static void displayBoardDebug(bool[,] boardToDisplay, int rc)
    {
        Console.WriteLine("Board Contents:");

        Console.WriteLine();
        for (int r = rc; r >= 1; r--)
        {
            for (int c = 1; c <= rc; c++)
            {
                if (c == 1)
                {
                    Console.Write(r + " ");
                }
                Console.Write((boardToDisplay[r, c] ? "1" : "0") + " ");
            }
            Console.WriteLine();
        }
        Console.Write("  ");
        for (int c = 1; c <= rc; c++)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }
    public static void setAsInvalid(bool[,] toSet, int rc)
    {
        for (int r = 1; r < rc; r++)
            for (int c = 1; c < rc; c++)
                toSet[r, c] = false;
    }

    public static int queensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        int ROW_COL = n; //because I do not want to deal with 0 index
        int numOfObtacles = k;
        int rowQueenPos = r_q;
        int colQueenPos = c_q;
        // return the number of squares the queen can attack

        bool[,] board = new bool[ROW_COL + 1, ROW_COL + 1];
        setAsInvalid(board, ROW_COL);
        //{   //        1      2      3      4      5      6      7      8
        //    {false, false, false, false, false, false, false, false, false}, // for 0 index
        //    {false, false, false, false, false, false, false, false, false}, // 8
        //    {false, false, false, false, false, false, false, false, false}, 
        //    {false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false}, // 5
        //    {false,  true, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false},
        //    {false, false, false, false, false, false, false, false, false},  // 1
        //    {false, false, false, false, false, false, false, false, false},  // 0
        //}; // ^ same here
        //    r  c
        board[rowQueenPos, colQueenPos] = true;

        Result.displayBoardDebug(board, ROW_COL);

        // mark 
        for (int c = 1; c <= ROW_COL; c++)
        {
            board[rowQueenPos, c] = true;
        }
        Result.displayBoardDebug(board, ROW_COL);
        for (int r = 1; r <= ROW_COL; r++)
        {
            board[r, colQueenPos] = true;
        }

        Result.displayBoardDebug(board, ROW_COL);

        // based on the col compute the starting row
        // R 4 C 4
        int rowDown = ROW_COL - rowQueenPos;
        int colInit = 1;
        for (int r = rowDown; r >= 1; r--)
        {
            board[r, colInit++] = true;
        }

        Result.displayBoardDebug(board, ROW_COL);

        int rowUp = Math.Abs(colQueenPos - rowQueenPos) + 1;
        colInit = ROW_COL - rowQueenPos + 1;
        for (int r = rowDown; r <= ROW_COL; r++)
        {
            board[r, colInit++] = true;
        }

        Result.displayBoardDebug(board, ROW_COL);

        //Console.WriteLine("after marking row");
        //Result.displayBoardDebug(board);

        return 0;
    }


}

class Solution
{
    public static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        //string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = 8;// Convert.ToInt32(firstMultipleInput[0]);

        int k = 3; // Convert.ToInt32(firstMultipleInput[1]);

        //string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r_q = 7; //Convert.ToInt32(secondMultipleInput[0]);

        int c_q = 5; // Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();
        obstacles.Add(new List<int>() { 3, 7 });
        obstacles.Add(new List<int>() { 3, 7 });

        //for (int i = 0; i < k; i++)
        //{
        //    obstacles.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp)).ToList());
        //}

        int result = Result.queensAttack(n, k, r_q, c_q, obstacles);

        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
