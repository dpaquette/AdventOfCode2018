using System;
using System.Linq;
using System.Text;

namespace Day12
{
    public class Rule
    {
        public string Pattern;
        public char Result;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var initialState = "...#..######..#....#####..###.##..#######.####...####.##..#....#.##.....########.#...#.####........#.#...";
            var rulesStrings = @"#...# => .
##.## => .
###.. => .
.#### => .
#.#.# => .
##..# => #
..#.# => #
.##.. => #
##... => #
#..## => #
#..#. => .
.###. => #
#.##. => .
..### => .
.##.# => #
....# => .
##### => .
#.### => .
.#..# => .
#.... => .
...## => .
.#.## => .
##.#. => #
#.#.. => #
..... => .
.#... => #
...#. => #
..#.. => .
..##. => .
###.# => .
####. => .
.#.#. => .".Split(Environment.NewLine);
            var rules = rulesStrings.Select(s =>
                new Rule()
                {
                    Pattern = s.Substring(0, 5),
                    Result = s[9]
                }).Where(r => r.Result == '#').ToArray();

            long firstIndex = -3;
            var currentState = initialState;
            Console.WriteLine(" 0: {0}", currentState);
            for (long generation = 1; generation <= 50000000000; generation++)
            {
                var previousState = currentState;
                var currentStateBuilder = new StringBuilder();
                for (int pot = 0; pot < currentState.Length; pot++)
                {
                    bool ruleMatched = false;
                    string previousStateForPot;

                    if (pot < 2)
                    {
                        previousStateForPot = previousState.Substring(0, 3 + pot).PadLeft(5, '.');
                    }
                    else if (pot + 2 >= currentState.Length)
                    {
                        previousStateForPot = previousState.Substring(pot - 2).PadRight(5, '.');
                    }
                    else
                    {
                        previousStateForPot = previousState.Substring(pot - 2, 5);
                    }


                    foreach (var rule in rules)
                    {
                        if (previousStateForPot == rule.Pattern)
                        {
                            currentStateBuilder.Append(rule.Result);
                            ruleMatched = true;
                            break;
                        }
                    }

                    if (!ruleMatched)
                    {
                        currentStateBuilder.Append(".");
                    }
                }

                if (currentStateBuilder[0] == '#' || currentStateBuilder[1] == '#' || currentStateBuilder[2] == '#')
                {
                    currentStateBuilder.Insert(0, "...");
                    firstIndex -= 3;
                }
                else if (currentStateBuilder[0] == '.' && currentStateBuilder[1] == '.' && currentStateBuilder[2] == '.'
                         && currentStateBuilder[3] == '.' && currentStateBuilder[4] == '.' &&
                         currentStateBuilder[5] == '.')
                {
                    currentStateBuilder.Remove(0, 3);
                    firstIndex += 3;
                }

                var currentStateLength = currentStateBuilder.Length;
                if (currentStateBuilder[currentStateLength - 3] == '#' ||
                    currentStateBuilder[currentStateLength - 2] == '#' ||
                    currentStateBuilder[currentStateLength - 1] == '#')

                {
                    currentStateBuilder.Append("...");
                }


                currentState = currentStateBuilder.ToString();
                if (generation % 1000000 == 0)
                {
                    Console.WriteLine("{0:D2}: {1}", generation, currentState);

                    long countOfPlants = 0;
                    for (int i = 0; i < currentState.Length; i++)
                    {
                        if (currentState[i] == '#')
                        {
                            countOfPlants += (i + firstIndex);
                        }
                    }
                    Console.WriteLine("Count of plants: {0}", countOfPlants);
                }
                
            }

            long finalCountOfPlants = 0;
            for (int i = 0; i < currentState.Length; i++)
            {
                if (currentState[i] == '#')
                {
                    finalCountOfPlants += (i + firstIndex);
                }
            }
            Console.WriteLine("Count of plants: {0}", finalCountOfPlants);
            Console.ReadLine();
        }
    }
}
