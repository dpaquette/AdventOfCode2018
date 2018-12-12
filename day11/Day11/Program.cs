using System;

namespace Day11
{
    class Program
    {
        static int[,] PowerGrid;
        const int SerialNumber = 1723;

        static void Main(string[] args)
        {

            PowerGrid = new int[300, 300];

            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    PowerGrid[x, y] = CalcPowerLevel(x, y);
                }
            }

            var maxPower = 0;
            (int, int, int) maxCoordinates = (0, 0, 1);

            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    for (int size = 1; size <= Math.Min(300-x, 300-y); size++)
                    {
                        var currentValue = GetPowerLevel(x, y, size);
                        if (currentValue > maxPower)
                        {
                            maxPower = currentValue;
                            maxCoordinates = (x, y, size);
                        }
                    }
                    
                }
            }

            Console.WriteLine("Max Total Power: {0}", maxPower);
            Console.WriteLine("Max Total Power Coordinates: {0},{1},{2}", maxCoordinates.Item1, maxCoordinates.Item2, maxCoordinates.Item3);
            Console.ReadLine();
        }

        private static int GetPowerLevel(int x, int y, int squareSize)
        {
            var total = 0;
            for (int x2 = 0; x2 < squareSize; x2++)
            {
                for (int y2 = 0; y2 < squareSize; y2++)
                {
                    total += PowerGrid[x + x2, y + y2];
                }
            }

            return total;
        }


        static int CalcPowerLevel(int x, int y)
        {
            int rackId = x + 10;
            int powerLevelStart = ((rackId * y) + SerialNumber) * rackId;
            int hundredthDigit = Math.Abs(powerLevelStart / 100 % 10);

            return hundredthDigit - 5;
        }
    }
}
