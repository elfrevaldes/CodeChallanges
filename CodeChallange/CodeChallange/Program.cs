using System;

public class Program
{
    public static int GetNthFibonacci(int n)
    {
        if (n <= 0)
            throw new ArgumentException("Input must be a positive integer.", nameof(n));

        else if (n == 1 || n == 2)
            return 1;

        else
            return GetNthFibonacci(n - 1) + GetNthFibonacci(n - 2);
    }

    public static void Main(string[] args)
    {
        int n = 10;
        int result = GetNthFibonacci(n);
        Console.WriteLine($"The {n}th Fibonacci number is: {result}");
    }
}