using System.Collections;
using System.Linq;

namespace DayFour
{
    class Program
    {
        static (int, int) GetRanges(string range)
        {
            var bounds = range.Split('-');
            var lowerBound = int.Parse(bounds[0]);
            var upperBound = int.Parse(bounds[1]);

            return (lowerBound, upperBound);
        }

        static bool IsContained((int, int) one, (int, int) two)
        {
            // 1-100, 2-99
            if (one.Item1 <= two.Item1 && one.Item2 >= two.Item2)
            {
                return true;
            }
            // 2-99, 1-100
            if (two.Item1 <= one.Item1 && two.Item2 >= one.Item2)
            {
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            var overlapCount = 0;

            // Read the file and display it line by line.  
            foreach (var line in System.IO.File.ReadLines(@"cleaning.txt"))
            {
                // split the line
                var pairs = line.Split(',');
                var one = GetRanges(pairs[0]);
                var two = GetRanges(pairs[1]);

                var isFullyContained = IsContained(one, two);
                overlapCount += isFullyContained ? 1 : 0;
            }

            Console.WriteLine($"Number of overlaps = {overlapCount}");
        }
    }
}