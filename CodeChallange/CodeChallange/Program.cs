using System;

public class Program
{
    public class ProhibitWord
    {
        int _index { get; set; }
        bool _isConsecutive;
        public string Word { get; private set; }
        public ProhibitWord(string word)
        {
            _isConsecutive = false;
            Word = word;
            _index = 0;
        }

        public bool IsMatch(char letter)
        {

            if (char.ToUpper(Word[_index]) == char.ToUpper(letter))
            {
                _index++;

                if (_index == Word.Length && _isConsecutive)
                    return true;

                _isConsecutive = true;
            }
            else
            {
                // we were keeping track of a word but no longer match
                // however, we need to keep track of this new letter
                if (_index > 0 && _isConsecutive)
                {
                    ResetMatch();
                    IsMatch(letter);
                }
                else
                {
                    ResetMatch();
                }
                return false;
            }
            // We do not have a match yet
            return false;
        }

        // When done with one word we do not want to keep the preveous find
        public void ResetMatch()
        {
            _isConsecutive = false;
            _index = 0;
        }
    }

    // The method assumes that you do not want to look for intermideate words
    // Something like ProDucks oDuc would be a math when looking at 2 words combine
    static int GetMaxValue(string strWord, List<ProhibitWord> pWords)
    {
        int index = 0;
        int startingIndex = 0;
        int letterCounter = 0;
        int lastLetter = strWord.Length - 1;
        string finalWord = "";
        bool wordBeingFlagged = false;

        Console.WriteLine("Checking: " + strWord);
        foreach (char letter in strWord)
        {
            // no need to keep track of letter until flag is removed
            if (!wordBeingFlagged)
            {
                foreach (ProhibitWord pw in pWords)
                {
                    if (pw.IsMatch(letter))
                    {
                        wordBeingFlagged = true;
                    }
                }
            }

            // we might want to skip this word completly
            if (wordBeingFlagged)
            {
                if (char.IsUpper(letter))
                {
                    var wrd = strWord.Substring(startingIndex, letterCounter);
                    Console.WriteLine("Word avoided: " + wrd + " count: " + wrd.Length);
                    startingIndex = index;
                    letterCounter = 0;
                    wordBeingFlagged = false;
                    // reset all pw
                    foreach (ProhibitWord pw in pWords)
                    {
                        pw.ResetMatch();
                        // We can't have a single letter word
                        pw.IsMatch(letter);
                    }
                }
                else
                {
                    // Ovoiding to do extra checks, just incrementing
                    index++;
                    letterCounter++;
                    continue;
                }
            }

            // we found the end of a word or the end of the string
            if (char.IsUpper(letter) && letterCounter > 1 || index == lastLetter)
            {
                if (index == lastLetter)
                    letterCounter++;

                var tempW = strWord.Substring(startingIndex, letterCounter);
                // check if any pW is flagged

                finalWord += tempW;
                Console.WriteLine("Found this word: " + tempW + " count: " + tempW.Length);
                startingIndex = index;
                letterCounter = 0;
            }
            letterCounter++;
            index++;
        }
        return finalWord.Length;
    }

    public static void Main()
    {
        List<ProhibitWord> pWords = new List<ProhibitWord>()
        {
            new ProhibitWord("crap"),
            new ProhibitWord("oduc"),
        };

        // Test
        string strWord = "GoodProductButScrapAfterWash";
        // string strWord = "AfterWash";
        // string strWord = "AfterWashCrrccrapCrcrackTom";  

        Console.WriteLine("Your highest score is: " + GetMaxValue(strWord, pWords));
    }
}