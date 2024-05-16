using System;
using System.Collections.Generic;

class Result
{

    /*
     * Complete the 'climbingLeaderboard' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY ranked
     *  2. INTEGER_ARRAY player
     */
    public class Mapping
    {
        private List<int> indexesList;
        private Dictionary<int, int> values; // Renamed to avoid confusion
        public int Count { get => indexesList.Count private set; }

        public Mapping()
        {
            values = new Dictionary<int, int>();
            indexesList = new List<int>();
        }

        // Example methods to manipulate the dictionaries could be added here
        public void Add(int key)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, indexesList.Count + 1);
                indexesList.Add(key);
            }
        }

        public int IndexOf(int index)
        {
            if (index <= indexesList.Count)
            {
                return values[indexesList[index]];
            }
            throw new KeyNotFoundException("The index or key was not found.");
        }

        public bool ContainsKey(int key)
        {
            return values.ContainsKey(key);
        }

        public void DeleteByIndex(int index)
        {
            if(index <= Count)
            {
                int key = indexesList[index];
                indexesList.RemoveAt(index);
            }
            throw new KeyNotFoundException("The index was not found.");
        }

        public void DeleteByKey(int key)
        {
            if (values.ContainsKey(key))
            {
                indexesList.Remove(key);
                values.Remove(key);
                return;
            }
            throw new KeyNotFoundException("The index was not found.");
        }

    }

    private static int findPlace(Mapping scoreRanks, int score)
    {
        // I could use the IndexOf scoreRanks + 1 but I want to save the time
        // to search for it again
        int index = 1;
        int left = 0;
        int right = scoreRanks.Count - 1;

        while (left <= right)
        {
            // find the middle
            int middle = left + (right - left) / 2;

            if (scoreRanks.IndexOf(middle) == score)
            {
                return middle + 1;
            }
            else if (scoreRanks.IndexOf(middle) < score)
            {
                // to the right
                right = middle - 1;
            }
            else
            {
                left = middle + 1;
            }
        }
        return left + 1;
        //foreach (KeyValuePair<int, int> kvp in scoreRanks)
        //{
        //    if (score > kvp.Key)
        //        return index;
        //    index++;
        //}
        // return index;
    }

    public static List<int> climbingLeaderboard(List<int> ranked, List<int> player)
    {
        // process the ranks to give them their place
        // List<int> scoreRanks = new List<int>();
        Mapping scoreRanks = new Mapping();
        
        foreach (int score in ranked)
        {
            scoreRanks.Add(score);
        }

        scoreRanks.DeleteByKey(70);
        List<int> results = new List<int>();
        // for each of the plays you want to check
        foreach (int plScore in player)
        {
            if (scoreRanks.ContainsKey(plScore))
            {
                results.Add(scoreRanks.IndexOf(plScore));
            }
            else
            {
                results.Add(Result.findPlace(scoreRanks, plScore));
            }
        }

        // if the score is in the scoredList assign score
        // if not loop through scores and find the spot
        return results;
    }
    //public static List<int> climbingLeaderboard(List<int> ranked, List<int> player)
    //{
    //    // process the ranks to give them their place
    //    // List<int> scoreRanks = new List<int>();
    //    Dictionary<int, int> scoreRanks = new Dictionary<int, int>();
    //    int place = 1;
    //    foreach (int score in ranked)
    //    {
    //        if (!scoreRanks.ContainsKey(score))
    //            scoreRanks.Add(score, place++);
    //    }

    //    List<int> results = new List<int>();
    //    // for each of the plays you want to check
    //    foreach (int plScore in player)
    //    {
    //        if (scoreRanks.ContainsKey(plScore))
    //        {
    //            results.Add(scoreRanks[plScore]);
    //        }
    //        else
    //        {
    //            results.Add(Result.findPlace(scoreRanks, plScore));
    //        }
    //    }

    //    // if the score is in the scoredList assign score
    //    // if not loop through scores and find the spot
    //    return results;
    //}
}

    public class Program
{
    public static void Main()
    {
        List<int> test = new() { 100, 100, 70, 50, 40, 40, 20, 10 };

        List<int> player = new() { 5, 25, 50, 120 };

        List<int> results = Result.climbingLeaderboard(test, player);

        //Console.WriteLine("Hello World");
    }
}