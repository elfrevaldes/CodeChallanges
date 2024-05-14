public class Result
{
    public static string winner(List<int> andrea, List<int> maria, string s)
    {
        int andreaScore = 0;
        int mariaScore = 0;

        bool isEven = s == "Even";

        for (int i = 0; i < maria.Count; i++)
        {
            if (isEven)
            {
                andreaScore += andrea[i] - maria[i];
                mariaScore += maria[i] - andrea[i];
            }
            isEven = !isEven;
        }


        if (andreaScore > mariaScore)
        {
            return "Andrea";
        }
        else if (mariaScore > andreaScore)
        {
            return "Maria";
        }
        else
        {
            return "Tie";
        }
    }
}


class Solution
{
    public static void Main(string[] args)
    {
        // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        // int andreaCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> andrea = new List<int>() { 4, 5, 7 };
        List<int> maria = new List<int>() { 3, 5, 6 };

        //for (int i = 0; i < andreaCount; i++)
        //{
        //    int andreaItem = Convert.ToInt32(Console.ReadLine().Trim());
        //    andrea.Add(andreaItem);
        //}

        //int mariaCount = Convert.ToInt32(Console.ReadLine().Trim());

        //List<int> maria = new List<int>();

        //for (int i = 0; i < mariaCount; i++)
        //{
        //    int mariaItem = Convert.ToInt32(Console.ReadLine().Trim());
        //    maria.Add(mariaItem);
        //}

        string s = "Odd"; //Console.ReadLine();

        string result = Result.winner(andrea, maria, s);

        Console.WriteLine($"The winner is: {result}");
        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}