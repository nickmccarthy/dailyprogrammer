using Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _380__Smooshed_Morse_Code
{
    public class Easy
    {
        private const string morseAlphabet = ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --..";

        private static string[] Words => File.ReadAllLines("enable1.txt");

        public static string SMorse(string str)
        {
            var dict = Enumerable.Range('a', 26)
                .Zip(morseAlphabet.Split(' '))
                .ToDictionary(z => z.First, z => z.Second);

            return str
                .Select(c => dict[c])
                .Concat();
        }

        public static string Test()
        {
            return new List<string>
            {
                {"sos"},
                {"daily"},
                {"programmer"},
                {"bits"},
                {"three"},
            }
            .Select(w => PrintSMorse(w))
            .Join('\n');
        }

        // The sequence -...-....-.--. is the code for four different words (needing, nervate, niding, tiling).
        // Find the only sequence that's the code for 13 different words.
        public static string C1()
        {
            return Words
                .GroupBy(w => SMorse(w))
                .Where(g => g.Count() == 13)
                .Single()
                .Select(w => PrintSMorse(w))
                .Join('\n');
        }

        // autotomous encodes to .-..--------------..-..., which has 14 dashes in a row.
        // Find the only word that has 15 dashes in a row.
        public static string C2()
        {
            var word = Words
                .Where(w => SMorse(w).Contains("---------------"))
                .Single();

            return PrintSMorse(word);
        }

        // Call a word perfectly balanced if its code has the same number of dots as dashes.
        // counterdemonstrations is one of two 21-letter words that's perfectly balanced. 
        // Find the other one.
        public static string C3()
        {
            return Words
                .Where(w => w.Length == 21)
                .Where(w => SMorse(w).Count(c => c == '.') == SMorse(w).Count(c => c == '-'))
                .Select(w => PrintSMorse(w))
                .Join('\n');           
        }

        // protectorate is 12 letters long and encodes to .--..-.----.-.-.----.-..--.,
        // which is a palindrome (i.e. the string is the same when reversed).
        // Find the only 13-letter word that encodes to a palindrome
        public static string C4()
        {
            var word = Words
                .Where(w => w.Length == 13)
                .Where(w => SMorse(w) == new string(SMorse(w).Reverse().ToArray()))
                .Single();

            return PrintSMorse(word);
        }

        // --.---.---.-- is one of five 13-character sequences that does not appear in the encoding of any word.
        // Find the other four.
        public static string C5()
        {
            var sequences = Enumerable.Range(0, 8192) // 2^13
                .Select(n => Regex.Replace($"{ Convert.ToString(n, 2),13}", "[ 0]", ".").Replace('1', '-'))
                .ToList();

            var encodings = Words
                .Select(w => SMorse(w))
                .Where(s => s.Length >= 13);

            foreach (var e in encodings)
            {
                sequences.RemoveAll(s => e.Contains(s));
            }

            return sequences.Join('\n');
        }


        private static string PrintSMorse(string word)
        {
            return $"word: {word}, smorse: {SMorse(word)}";
        }
    }
}
