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


    private static void displayBoardDebug(bool[,] boardToDisplay, int ROW_COL, int qr, int qc, List<List<int>> obstacles)
    {
        Console.WriteLine("Queen's color: Red");
        Console.WriteLine("Obstacles color: Blue");
        Console.WriteLine("DEBUG Board Contents:");

        Console.WriteLine();
        for (int r = ROW_COL; r >= 0; r--)
        {
            for (int c = 0; c <= ROW_COL; c++)
            {
                if (c == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(r + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                // check for queen
                if (r == qr && c == qc)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((boardToDisplay[r, c] ? "1" : "0") + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                // check for obstacles
                foreach (var obstacle in obstacles)
                {
                    if (obstacle[0] == r && obstacle[1] == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break; // no need to check other for more obsticle
                    }
                }
                
                Console.Write((boardToDisplay[r, c] ? "1" : "0") + " ");
                Console.ForegroundColor = ConsoleColor.White;

            }
            Console.WriteLine();
        }

        Console.Write("  ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        for (int c = 0; c <= ROW_COL; c++)
        {
            Console.Write(c + " ");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine();
    }

    public static void displayBoard(bool[,] boardToDisplay, int ROW_COL, int qr, int qc, List<List<int>> obstacles, bool debug = false)
    {
        // Calculate the number of digits in ROW_COL
        int numDigits = (int)Math.Floor(Math.Log10(ROW_COL)) + 1;
        int maxDigits = ROW_COL.ToString().Length + 1;

        if (debug)
            Console.WriteLine("DEBUG Contents:");
        else
            Console.WriteLine("Board Contents:");

        Console.WriteLine("Queen's color: Red");
        Console.WriteLine("Obstacles's color: Blue");
        Console.WriteLine();

        int min = debug ? 0 : 1;

        for (int r = ROW_COL; r >= min; r--)
        {
            for (int c = min; c <= ROW_COL; c++)
            {
                bool obstacleEncountered = false;
                if (c == min)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(r.ToString().PadLeft(numDigits) + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                // check for queen
                if (r == qr && c == qc)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (debug)
                        Console.Write((boardToDisplay[r, c] ? "1" : "0").PadRight(maxDigits).PadLeft(numDigits));
                    else
                        Console.Write("Q".PadRight(maxDigits).PadLeft(numDigits));

                    Console.ForegroundColor = ConsoleColor.White; 
                    continue;
                }

                // check for obstacles
                foreach (var obstacle in obstacles)
                {
                    if (obstacle[0] == r && obstacle[1] == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        obstacleEncountered = !debug;
                        break; // no need to check other for more obsticle
                    }
                }
                if (debug)
                    Console.Write((boardToDisplay[r, c] ? "1" : "0").PadRight(maxDigits).PadLeft(numDigits));
                else
                    Console.Write((obstacleEncountered ? "#" : (boardToDisplay[r, c] ? "1" : "0")).PadRight(maxDigits).PadLeft(numDigits));

                Console.ForegroundColor = ConsoleColor.White;

            }
            Console.WriteLine();
        }

        Console.Write(" ".PadLeft(numDigits) + " ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        for (int c = min; c <= ROW_COL; c++)
        {
            Console.Write(c.ToString().PadRight(numDigits + 1));
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine();
    }

    private static void setAsInvalid(bool[,] toSet, int rc)
    {
        for (int r = 1; r < rc; r++)
            for (int c = 1; c < rc; c++)
                toSet[r, c] = false;
    }

    /// <summary>
    /// Counts possible number of movements for queen
    /// </summary>
    /// <param name="board">Must countain the queen</param>
    /// <param name="rc">Board size (board is simetrical)</param>
    private static int countPossibleMovements(bool[,] board, int rc)
    {
        int counter = -1;
        for (int r = 1; r <= rc; r++)
            for (int c = 1; c <= rc; c++)
                counter += board[r, c] ? 1 : 0;
        return counter;
    }

    public static int queensAttackImproved(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    { // I do not need K because the list privides me with the count
        // Initialize board and other variables
        int ROW_COL = n;
        int rowQueenPos = r_q;
        int colQueenPos = c_q;
        bool[,] board = new bool[ROW_COL + 1, ROW_COL + 1];
        // this method sets the all position in the board as false
        setAsInvalid(board, ROW_COL);

        // Set Queen position
        board[rowQueenPos, colQueenPos] = true;

        const int ROW = 0;
        const int COL = 1;
        // Define directions to go from queen
        int[][] directions = {
            new int[] {  1,  0 }, // Vertical up
            new int[] { -1,  0 }, // Vertical Down
            new int[] {  0,  1 }, // Horizontal Right
            new int[] {  0, -1 }, // Horizontal Left
            new int[] {  1,  1 }, // Diagonal UpRight
            new int[] {  1, -1 }, // Diagonal UpLeft
            new int[] { -1,  1 }, // Diagonal DownRight
            new int[] { -1, -1 }  // Diagonal DownLeft
        };

        int movements = 0;

        // Check each direction
        for (int i = 0; i < directions.Length; i++)
        {
            int r = rowQueenPos + directions[i][ROW];
            int c = colQueenPos + directions[i][COL];

            // Check for obstacles and board boundaries
            while (r >= 1 && r <= ROW_COL && c >= 1 && c <= ROW_COL)
            {
                // Check if obstacle encountered
                if (obstacles.Any(o => o[ROW] == r && o[COL] == c))
                {
                    break; // stop marking possible
                }

                // Count movement and update position
                board[r, c] = true;
                movements++;
                r += directions[i][ROW];
                c += directions[i][COL];
            }
        }

        Console.WriteLine("Improved");
        Result.displayBoard(board, ROW_COL, rowQueenPos, colQueenPos, obstacles);
        Console.WriteLine("Improved");
        Result.displayBoard(board, ROW_COL, rowQueenPos, colQueenPos, obstacles, true);

        // Return total movements
        Console.WriteLine($"Number of possible movements for queen: {movements}");
        return movements;
    }

    public static int queensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        int ROW_COL = n; //because I do not want to deal with 0 index
        // int numOfObtacles = k; Do not need K cause the list provides me with the count
        int rowQueenPos = r_q;
        int colQueenPos = c_q;
        bool[,] board = new bool[ROW_COL + 1, ROW_COL + 1];
        // this method sets the all position in the board as false
        setAsInvalid(board, ROW_COL);

        // Set Queen position
        board[rowQueenPos, colQueenPos] = true;

        int rUp = rowQueenPos + 1;
        int rDown = rowQueenPos - 1;

        int cUp = colQueenPos + 1;
        int cDown = colQueenPos - 1;

        bool upRComplete = false;
        bool downRComplete = false;
        bool horiRComplete = false;

        bool vertUComplete = false;
        bool vertDComplete = false;

        bool upLComplete = false;
        bool downLComplete = false;
        bool horiLComplete = false;


        while (!upRComplete || !downRComplete || !horiRComplete || !vertUComplete || 
            !vertDComplete || !upLComplete || !downLComplete || !horiLComplete)
        {
            if (rUp > ROW_COL)
            {
                upLComplete = true;
                vertUComplete = true;
                upRComplete = true;
            }

            if (cUp > ROW_COL)
            {
                upRComplete = true;
                horiRComplete = true;
                downRComplete = true;
            }

            if (rDown < 1)
            {
                downRComplete = true;
                vertDComplete = true;
                downLComplete = true;
            }

            if (cDown < 1)
            {
                upLComplete = true;
                horiLComplete = true;
                downLComplete = true;
            }

            if (upRComplete && downRComplete && horiRComplete && vertUComplete &&
            vertDComplete && upLComplete && downLComplete && horiLComplete)
            { break; }

            foreach (var obstacle in obstacles)
            {
                // diagonal left up 
                if (obstacle[0] == rUp && obstacle[1] == cDown)
                {
                    upLComplete = true;
                }
                // diagonal left down
                if (obstacle[0] == rDown && obstacle[1] == cDown)
                {
                    downLComplete = true;
                }
                // horizontal left
                if (obstacle[0] == rowQueenPos && obstacle[1] == cDown)
                {
                    horiLComplete = true;
                }

                // vertical up
                if (obstacle[0] == rUp && obstacle[1] == colQueenPos)
                {
                    vertUComplete = true;
                }
                // vertical down
                if (obstacle[0] == rDown && obstacle[1] == colQueenPos)
                {
                    vertDComplete = true;
                }

                // diagonal right up 
                if (obstacle[0] == rUp && obstacle[1] == cUp)
                {
                    upRComplete = true;
                }
                // diagonal right down
                if (obstacle[0] == rDown && obstacle[1] == cUp)
                {
                    downRComplete = true;
                }
                // horizontal right
                if (obstacle[0] == rowQueenPos && obstacle[1] == cUp)
                {
                    horiRComplete = true;
                }
            }

            if (!upLComplete)
            {
                // mark diagonal left up
                board[rUp, cDown] = true;
            }
            if (!downLComplete)
            {
                // mark diagonal left down
                board[rDown, cDown] = true;
            }
            
            if (!vertUComplete)
            {
                // mark vertical up
                board[rUp, colQueenPos] = true;
            }
            if (!vertDComplete)
            {
                // mark vertical down
                board[rDown, colQueenPos] = true;
            }

            if (!upRComplete)
            {
                // mark diagonal right up
                board[rUp, cUp] = true;
            }
            if (!downRComplete)
            {
                // mark diagonal right down
                board[rDown, cUp] = true;
            }

            if (!horiLComplete)
            {
                // mark horizontal left
                board[rowQueenPos, cDown] = true;
            }
            if (!horiRComplete)
            {
                // mark horizontal right
                board[rowQueenPos, cUp] = true;
            }

            rUp++;
            cUp++;
            rDown--;
            cDown--;
        }


        Result.displayBoard(board, ROW_COL, rowQueenPos, colQueenPos, obstacles);
        Result.displayBoard(board, ROW_COL, rowQueenPos, colQueenPos, obstacles, true);

        int movements = Result.countPossibleMovements(board, ROW_COL);
        Console.WriteLine($"Number of possible movements for queen: {movements}");
        return movements;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        //string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = 4;// Convert.ToInt32(firstMultipleInput[0]);

        int k = 3; // Convert.ToInt32(firstMultipleInput[1]);

        //string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r_q = 4; //Convert.ToInt32(secondMultipleInput[0]);

        int c_q = 4; // Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();

        //obstacles.Add(new List<int>() { 5, 5 });
        //obstacles.Add(new List<int>() { 4, 2 });
        //obstacles.Add(new List<int>() { 2, 3 });

        //// Testing obstacles right side
        //obstacles.Add(new List<int>() { 7, 7 });
        //// obstacles.Add(new List<int>() { 4, 7 });
        //obstacles.Add(new List<int>() { 2, 6 });

        //// Testing obstacles left side
        //obstacles.Add(new List<int>() { 1, 1 });
        //// obstacles.Add(new List<int>() { 4, 2 });
        //obstacles.Add(new List<int>() { 6, 2 });

        // Testing obstacles vertically
        //obstacles.Add(new List<int>() { r_q, ((n - c_q) / 2) });
        //obstacles.Add(new List<int>() { r_q, ((n - c_q) / 2) + c_q });

        //// Testing obstacles horizontally
        //obstacles.Add(new List<int>() { ((n - r_q) / 2),  c_q });
        //obstacles.Add(new List<int>() { ((n - r_q) / 2) + r_q, c_q });

        //for (int i = 0; i < k; i++)
        //{
        //    obstacles.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp)).ToList());
        //}

        Result.queensAttack(n, k, r_q, c_q, obstacles);
        Console.WriteLine();
 
        Console.WriteLine();
        Result.queensAttackImproved(n, k, r_q, c_q, obstacles);
        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
