using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] fabric = new int[1000, 1000];

            var startingPointToIds = new Dictionary<string, List<string>>();

            var claimString = Console.ReadLine();
            var claims = new List<Claim>();
            while (!string.IsNullOrEmpty(claimString))
            {
                //claim format: #1 @ 1,3: 4x4
                var claimParts = claimString.Split(new[] { ' ', ',', '#', '@', ':', 'x' }, StringSplitOptions.RemoveEmptyEntries);
                var claim = new Claim();
                claim.Id = claimParts[0];
                claim.StartX = Int32.Parse(claimParts[1]);
                claim.StartY = Int32.Parse(claimParts[2]);
                claim.EndX = claim.StartX + Int32.Parse(claimParts[3]) - 1;
                claim.EndY = claim.StartY + Int32.Parse(claimParts[4]) - 1;
                claims.Add(claim);

                for (int x = claim.StartX; x <= claim.EndX; x++)
                {
                    for (int y = claim.StartY; y <= claim.EndY; y++)
                    {
                        fabric[x, y]++;
                    }
                }
                
                claimString = Console.ReadLine();
            }

            var overlapCount = 0;
            foreach (var i in fabric)
            {
                if (i > 1)
                {
                    overlapCount++;
                }
            }
            Console.WriteLine("--------------------------");
            Console.WriteLine("Square inches of fabric within two or more claims: {0}", overlapCount);

            foreach (var claim in claims)
            {
                var hasOverlap = false;
                for (int x = claim.StartX; x <= claim.EndX; x++)
                {
                    for (int y = claim.StartY; y <= claim.EndY; y++)
                    {
                        if (fabric[x, y] > 1)
                        {
                            hasOverlap = true;
                            break;
                        }
                    }

                    if (hasOverlap)
                    {
                        break;
                    }
                }

                if (!hasOverlap)
                {
                    Console.WriteLine("ID of the only claim that doesn't overlap: {0}", claim.Id);
                    break;
                }
            }
            
            Console.ReadLine();
        }
    }
}
