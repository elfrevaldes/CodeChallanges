public class Solution
{
    public static List<(int, int)> MergeIntervals(List<(int, int)> intervals)
    {
        int start = intervals[0].Item1;
        int end = intervals[0].Item2;
        List <(int, int)> points = new List <(int, int)> ();
        
        for (int i = 1; i < intervals.Count; i++)
        {
            if (end >= intervals[i].Item1)
            {
                end = intervals[i].Item2;
            }
            else
            {
                // save that one and began a new one
                points.Add((start, end));
                start = intervals[i].Item1;
                end = intervals[i].Item2;
            }
            if (i + 1 == intervals.Count)
                points.Add((start, end));
        };
        
        return points;
    }

}

public class Program
{
    public static void Main()
    {
        List<(int, int)> stringList = new List<(int, int)>{ (1,3), (2,6), (6,9), (10,13), (15,18), (16, 24) };

        List<(int, int)> solution = Solution.MergeIntervals(stringList);
        Console.WriteLine(string.Join(", ", solution.Select(x => $"[{x.Item1}, {x.Item2}]")));
    }
}