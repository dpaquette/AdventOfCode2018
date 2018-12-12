using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var boxIds = new List<string>();
            var boxId = Console.ReadLine();
            while (!string.IsNullOrEmpty(boxId))
            {
                boxIds.Add(boxId);
                boxId = Console.ReadLine();
            }

            var containing2Letters = boxIds.Count(id =>
            {
                var byCharacter = id.ToCharArray().GroupBy(c => c);
                return byCharacter.Any(c => c.Count() == 2);
            });

            var containing3Letters = boxIds.Count(id =>
            {
                var byCharacter = id.ToCharArray().GroupBy(c => c);
                return byCharacter.Any(c => c.Count() == 3);
            });

            var checksum = containing3Letters * containing2Letters;

            Console.WriteLine("-----------------------");
            Console.WriteLine("Checksum: " + checksum);
            foreach (var id in boxIds)
            {
                var similarId = boxIds.FirstOrDefault(otherId => id != otherId && HasOnly1CharacterDifferent(id, otherId));
                if (similarId != null)
                {
                    Console.WriteLine("Similar IDs: {0} - {1}", id, similarId);
                    Console.WriteLine("Common Letters: {0}", GetCommonCharacters(id, similarId) );
                    break;
                }
            }
            Console.ReadLine();
        }

        static bool HasOnly1CharacterDifferent(string id1, string id2)
        {
            var id1Chars = id1.ToCharArray();
            var id2Chars = id2.ToCharArray();
            var count = 0;
            for (int i = 0; i < id1Chars.Length; i++)
            {
                if (id1Chars[i] != id2Chars[i])
                {
                    count++;
                    if (count > 1)
                    {
                        break;
                    }
                }
            }

            return count == 1;
        }

        static string GetCommonCharacters(string id1, string id2)
        {
            var id1Chars = id1.ToCharArray();
            var id2Chars = id2.ToCharArray();
            var count = 0;
            var commonCharacters = new StringBuilder();
            for (int i = 0; i < id1Chars.Length; i++)
            {
                if (id1Chars[i] == id2Chars[i])
                {
                    commonCharacters.Append(id1Chars[i]);
                }
            }

            return commonCharacters.ToString();
        }
    }
}
