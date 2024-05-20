// [ Powers of Two ]

// Write a func that takes numbers and return if these numbers are powers of 2

// [5, 3, 1234, 8, 4, 0, -54, 33] -> [0, 0, 0, 1, 1, 0]


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
class Solution
{
    private static List<int> powersOfTwo = new List<int>();
    private static Dictionary<int, int> PowOfTwo = new Dictionary<int, int>() {
            { 1, 1 }, { 2, 1 }, { 4, 1 }, { 8, 1 },
            { 16, 1 }, { 32, 1 }, { 64, 1 }, { 128, 1 }, { 256, 1 }, { 512, 1 },
            { 1024, 1 }, { 2048, 1 }, { 4096, 1 }, { 8192, 1 }, { 16384, 1 },
            { 32768, 1 }, { 65536, 1 }, { 131072, 1 }, { 262144, 1 }, { 524288, 1 },
            { 1048576, 1 }, { 2097152, 1 }, { 4194304, 1 }, { 8388608, 1 },
            { 16777216, 1 }, { 33554432, 1 }, { 67108864, 1 }, { 134217728, 1 },
            { 268435456, 1 }, { 536870912, 1 }, { 1073741824, 1 }
        };

    public List<bool> isAPowerOfTwoImproved(List<int> numbers)
    {
        List<bool> results = new List<bool>();
        foreach (int number in numbers)
        {
            results.Add(PowOfTwo.ContainsKey(number));
        }
        return results;
    }

    public List<bool> isAPowerOfTwo(List<int> numbers)
    {
        List<bool> results = new List<bool>();
        foreach (int num in numbers)
        {
            if (num <= 1 || num > 1073741824) // 1073741824 is the biggest power of 2 that you can have
            {
                results.Add(false);
                continue;
            }
            if (powersOfTwo.Contains(num))
            {
                results.Add(true);
                continue;
            }
            
            if (powersOfTwo.Count == 0 || powersOfTwo[powersOfTwo.Count - 1] < num)
            {
                int powTwo = 1;
                if (powersOfTwo.Count > 0)
                    powTwo = powersOfTwo[powersOfTwo.Count - 1];
                 
                while (powTwo < num)
                {
                    powTwo *= 2;
                    powersOfTwo.Add(powTwo);
                    if (powTwo == num)
                    {
                        results.Add(true);
                        break;
                    }
                }
                results.Add(false);
            }
            else
            {
                results.Add(false);
            }
        }
        return results;
    }

    static void Main(String[] args)
    {

        Solution sol = new Solution();
        List<int> test = new List<int>() { 5, 3, 1234, 8, 4, 0, -54, 33, 536870912 };


        List<bool> results = sol.isAPowerOfTwo(test);
        List<bool> improvedResults = sol.isAPowerOfTwoImproved(test);
        Console.WriteLine("Regular:");
        foreach (bool r in results)
        {
            Console.WriteLine(r ? 1 : 0);
        }
        Console.WriteLine("Improved:");
        foreach (bool r in improvedResults)
        {
            Console.WriteLine(r ? 1 : 0);
        }
    }
}