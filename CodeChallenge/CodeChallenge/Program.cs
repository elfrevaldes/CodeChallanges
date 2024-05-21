using System;
using System.Collections.Generic;
using System.Reflection;

class Result
{

    /*
     * Data type that can search in O(1) by value and index (it uses more space)
     */
    public class Mapping
    {
        private List<int> indexes;
        //                 id   value
        private Dictionary<int, int> values; // Renamed to avoid confusion
       
        public int Count { get => indexes.Count; }

        public bool CanHaveDuplicates { get; private set; }

        public Mapping(bool canHaveDuplicates = false)
        {
            values = new Dictionary<int, int>();
            indexes = new List<int>();
            CanHaveDuplicates = canHaveDuplicates;
        }

        public void Add(int key)
        {
            if (CanHaveDuplicates || !values.ContainsKey(key))
            {
                int lastItem = indexes.Count;
                values.Add(key, lastItem);
                indexes.Add(key);
            }
        }

        public int ValueByIndex(int index)
        {
            if (index <= indexes.Count)
            {
                return indexes[index];
            }
            else
            {
                throw new KeyNotFoundException("The index or key was not found.");
            }
        }

        public int IndexByValue(int value)
        {
            return values[value];
        }

        public bool Contains(int key)
        {
            return values.ContainsKey(key);
        }

        private void UpdateIndex(int index)
        {
            int lastItem = indexes.Count;
            while (index < lastItem)
            {
                int key = indexes[index];
                values[key] = index++;
            }
        }

        public void DeleteByIndex(int index)
        {
            if (index <= indexes.Count)
            {
                int key = indexes[index];
                indexes.RemoveAt(index);
                values.Remove(key);
                UpdateIndex(index);
            }
            else
            {
                throw new KeyNotFoundException("The index was not found.");
            }
        }

        public void DeleteByValue(int value)
        {
            if (values.ContainsKey(value))
            {
                indexes.Remove(value);
                int index = values[value];
                values.Remove(value);
                UpdateIndex(index);
            }
            else
            {
                throw new KeyNotFoundException("The index was not found.");
            }
        }

    }

    private static int FindRankByScore(Mapping scoreRanks, int score)
    {
        // I could use the IndexOf scoreRanks + 1 but I want to save the time
        // to search for it again
        int left = 0;
        int right = scoreRanks.Count - 1;

        while (left <= right)
        {
            // find the middle
            int middle = left + (right - left) / 2;
            int middleValue = scoreRanks.ValueByIndex(middle); // Key is the score

            if (middleValue == score)
            {
                return middle + 1; // value is the rank
            }
            else if (middleValue < score)
            {
                // to the right
                right = middle - 1;
            }
            else
            {
                left = middle + 1;
            }
        }
        // If Higher bound
        if (right == -1 && left == 0)
            return 1;
        
        return left + 1;
    }

    public static List<int> climbingLeaderboard(List<int> ranked, List<int> player)
    {
        Mapping scoreRanks = new();
        
        foreach (int score in ranked)
        {
            scoreRanks.Add(score);
        }

        List<int> results = new List<int>();
        // for each of the plays you want to check
        foreach (int plScore in player)
        {
            if (scoreRanks.Contains(plScore))
            {
                results.Add(scoreRanks.IndexByValue(plScore)+1);
            }
            else
            {
                results.Add(Result.FindRankByScore(scoreRanks, plScore));
            }
        }
        
        return results;
    }
}

    public class Program
{
    public static void Main()
    {
        List<int> test = new() { 100, 100, 50, 40, 40, 20, 10 };

        // 0,100 1,50 2,40 3,20 4,10
        // 5,6 25,4 50,2 100,1 120,1

        List<int> player = new() { 5, 25, 50, 100, 120 };

        List<int> results = Result.climbingLeaderboard(test, player);

        //Console.WriteLine("Hello World");
    }
}