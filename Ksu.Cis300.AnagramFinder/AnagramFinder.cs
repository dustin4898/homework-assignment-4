using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Ksu.Cis300.AnagramFinder;
using System.IO;

namespace Ksu.Cis300.TrieLibrary
{

    public static class AnagramFinderClass
    {

        private static IList anagramsList;

        private static ITrie _words;

        public static object Next { get; private set; }

        public static void GetAnagrams()
        {
            ITrie allowableCompletions = _words;
            IList anagramsList = null;
            StringBuilder sb = new StringBuilder();
            if (allowableCompletions.ToString() == "")
            {
                if (sb.ToString().Contains(""))
                {
                    anagramsList.Add(sb.Append(""));
                }
                else
                {
                    sb.Append(" ");
                    GetAnagrams();
                    sb.Remove(0, 0);
                }
                string letters = "";
                foreach (char c in letters) {
                    _words.GetCompletions(letters);
                    if (allowableCompletions != null)
                    {
                        sb.Append(c);
                        sb.Remove(0,0);
                    } }
            }
        }

        public static List<LetterCounter1> GetLetterCounters(string letters)
        {
            List<char> chars = GetLetters(letters);
            List<LetterCounter1> letterCount = new List<LetterCounter1>();
            LetterCounter1 letterCounter = new LetterCounter1(letterCount, letters, Next);
            foreach (char c in letters)
            {
                LetterCounter1 lastLetter1 = letterCount[letterCount.Count - 1];
                if (letterCounter.Equals(lastLetter1))
                {
                    int lastLetter = (letterCount.Count - 1);
                    lastLetter++;
                }
                else
                {
                    letterCount.Add(letterCounter);
                }
            }
            return letterCount;
        }

        private static List<char> GetLetters(string letters)
        {
            List<char> chars = new List<char>();
            OpenFileDialog ofd = new OpenFileDialog();
            StringBuilder sb = new StringBuilder();
            letters.ToLower();
            foreach (char c in letters)
            {
                if (c >= 'a' && c <= 'z')
                {
                    sb.Append(c);
                    chars.Add(c);
                    chars.Sort();
                }
            }
            return chars;
        }

        public static ITrie GetWordList(string file)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                file = ofd.FileName;
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                       _words = _words.Add(line);
                    }
                }
            }
            catch
            {
                    throw new IOException("The word list contains the empty string.");
            }
            return _words;
        }

        private static void WriteAnagrams (IList anagrams, string file)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            StreamWriter sw = new StreamWriter(ofd.FileName);
            while (anagrams != null)
            {
                sw.WriteLine(file);
            }
        }
    }
}

