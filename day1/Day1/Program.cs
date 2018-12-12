using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var result = 0;
            var firstRepeated = 0;
            var firstRepeatedFound = false;
            var allInputs = new List<int>();
            HashSet<Int32> seenFrequencies = new HashSet<int>();
            seenFrequencies.Add(0);
            while (Int32.TryParse(input, out int value))
            {
                allInputs.Add(value);
                result += value;
                if (!firstRepeatedFound && seenFrequencies.Contains(result))
                {
                    firstRepeatedFound = true;
                    firstRepeated = result;
                }
                else
                {
                    seenFrequencies.Add(result);
                }
                input = Console.ReadLine();
            }

            var loopingResult = result;
            while (!firstRepeatedFound)
            {
                foreach (var value in allInputs)
                {
                    loopingResult += value;
                    if (seenFrequencies.Contains(loopingResult))
                    {
                        firstRepeatedFound = true;
                        firstRepeated = loopingResult;
                        break;
                    }
                    else
                    {
                        seenFrequencies.Add(loopingResult);
                    }
                }
            }
            
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Result: " + result);
            Console.WriteLine("First frequency reached twice: " + firstRepeated);
            Console.ReadLine();
        }
    }
}
