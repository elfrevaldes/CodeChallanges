using System;

public class Solution
{
    Dictionary<int, int> Solved;

    public Solution()
    {
        Solved = new Dictionary<int, int>() { {0, 0}, {1, 1} };
    }

    public int GetNthFibonacci(int n)
    { // O(2n)
        if (n < 0)
            throw new ArgumentException("Input must be a positive integer.", nameof(n));
        if (n == 0) 
            return 0;

        else if (n == 1 || n == 2)
            return 1;

        else
            return GetNthFibonacci(n - 1) + GetNthFibonacci(n - 2);
    }

    public int GetNthFibonacciCashed(int n)
    {
        if (Solved.ContainsKey(n))
        {
             return Solved[n];
        }
        else
        {
            for (int i = Solved.Count; i <= n; i++) 
            {
                Solved.Add(i, Solved[i - 1] + Solved[i - 2]);
            }
            return Solved[n];
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        int n = 10;
        Solution solver = new Solution();
        // With recursion
        int result = solver.GetNthFibonacci(n);
        Console.WriteLine($"The {n}th Fibonacci number is: {result}");
        // Linial
        result = solver.GetNthFibonacciCashed(n);
        Console.WriteLine($"The {n}th Fibonacci number is: {result}");
        // cashed
        result = solver.GetNthFibonacciCashed(n-1);
        Console.WriteLine($"The {n}th Fibonacci number is: {result}");
    }
}