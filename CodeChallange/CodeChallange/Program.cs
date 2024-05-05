using System;

public class BubbleSort
{
    public static void BubbleSortArray(int[] arr)
    {
        int n = arr.Length - 1;

        for (int i = 0; i < n; i++)
        {
            int nMinOne = n - 1;
            for (int j = 0; j < nMinOne - i; j++)
            {
                int jPlusOne = j + 1;
                if (arr[j] > arr[jPlusOne])
                {
                    // Swap arr[j] and arr[j+1]
                    int temp = arr[j];
                    arr[j] = arr[jPlusOne];
                    arr[jPlusOne] = temp;
                }
            }
        }
        // Times it run 21, computations: 64
        // Times it run 21, computations: 22
    }

    public static void Main(string[] args)
    {
        int[] array = { 64, 34, 25, 12, 22, 11, 90 };
        Console.WriteLine("Original array:");
        PrintArray(array);

        BubbleSortArray(array);

        Console.WriteLine("Array after Bubble Sort:");
        PrintArray(array);
    }

    public static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}