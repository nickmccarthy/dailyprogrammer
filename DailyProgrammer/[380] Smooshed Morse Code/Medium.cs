using Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _380__Smooshed_Morse_Code
{
    public class Medium
    {
        private const string morseAlphabet = ".- -... -.-. -.. . ..-. --. .... .. .--- -.- .-.. -- -. --- .--. --.- .-. ... - ..- ...- .-- -..- -.-- --..";

        private static string[] Words => File.ReadAllLines("enable1.txt");

        private static Dictionary<int, string> MorseDict = new Dictionary<int, string> { { 'a', ".-" }, { 'b', "-..." }, { 'c', "-.-." }, { 'd', "-.." }, { 'e', "." }, { 'f', "..-." }, { 'g', "--." }, { 'h', "...." }, { 'i', ".." }, { 'j', ".---" }, { 'k', "-.-" }, { 'l', ".-.." }, { 'm', "--" }, { 'n', "-." }, { 'o', "---" }, { 'p', ".--." }, { 'q', "--.-" }, { 'r', ".-." }, { 's', "..." }, { 't', "-" }, { 'u', "..-" }, { 'v', "...-" }, { 'w', ".--" }, { 'x', "-..-" }, { 'y', "-.--" }, { 'z', "--.." } };

        public static string SMAplha(string str)
        {
            var (match, result) = _SMAlpha(str);
            if (match)
            {
                return result;
            }

            return "Failed to find match";
        }

        private static (bool match, string result) _SMAlpha(string encoding, string alphabet = "abcdefghijklmnopqrstuvwxyz")
        {
            if (encoding.Length == 0 && alphabet.Length == 0)
            {
                return (true, string.Empty);
            }

            if (encoding.Length == 0 ^ alphabet.Length == 0)
            {
                throw new ArgumentException("encoding and alphabet must be non-empty");
            }

            for (var i = 0; i < alphabet.Length; i++)
            {
                var c = alphabet[i];
                var morseC = MorseDict[c];
                if (!encoding.StartsWith(morseC))
                {
                    continue;
                }

                var (match, result) = _SMAlpha(encoding.Remove(0, morseC.Length), alphabet.Remove(i, 1));
                if (match)
                {
                    return (true, $"{c}{result}");
                }
            }

            return (false, string.Empty);
        }

        public static string Test()
        {
            return new List<string>
            {
                {".--...-.-.-.....-.--........----.-.-..---.---.--.--.-.-....-..-...-.---..--.----.."},
                {".----...---.-....--.-........-----....--.-..-.-..--.--...--..-.---.--..-.-...--..-"},
                {"..-...-..-....--.---.---.---..-..--....-.....-..-.--.-.-.--.-..--.--..--.----..-.."},
            }
            .Select(w => PrintSMAlpha(w))
            .Join('\n');
        }

        public static string C1()
        {
            var morse = File.ReadAllLines("smorse2-bonus1.in.txt");
            var watch = new Stopwatch();
            watch.Start();
            morse.AsParallel().Select(m => SMAplha(m)).ToList();
            watch.Stop();

            return $"Finished in {watch.ElapsedMilliseconds.ToString()}ms";
        }

        private static string PrintSMAlpha(string word)
        {
            return $"morse: {word}\nalphabet: {SMAplha(word)}\n";
        }
    }
}
