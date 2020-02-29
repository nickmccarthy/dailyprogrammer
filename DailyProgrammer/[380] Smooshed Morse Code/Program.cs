using System;

namespace _380__Smooshed_Morse_Code
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"------------- EASY -------------");

            Run(Easy.Test);
            Run(Easy.C1);
            Run(Easy.C2);
            Run(Easy.C3);
            Run(Easy.C4);
            Run(Easy.C5);

            Console.WriteLine($"------------- MEDIUM -------------");

            Run(Medium.Test);
            Run(Medium.C1);
        }

        public static void Run(Func<string> func)
        {
            Console.WriteLine($"------------- Running {func.Method.Name} -------------");
            Console.WriteLine(func());
            Console.WriteLine();
        }
    }
}
