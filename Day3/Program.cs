using System.Collections;
using System.Linq;

namespace Rucksack
{
    class Program
    {
        static int GetPriority(char c)
        {
            // A in ASCII = 65
            // a in ASCII = 97
            var asciiVal = (int)c; 
            if (char.IsUpper(c))
            {
                return asciiVal - 38;
            }
            else
            {
                return asciiVal - 96;
            }
        }

        static void Main(string[] args)
        {
            var totalPriorities = 0;

            // Read the file and display it line by line.  
            foreach (var line in System.IO.File.ReadLines(@"rucksacks.txt"))
            {
                var numberOfItems = line.Length;
                var rucksackSize = numberOfItems / 2;
                var rucksack1 = line[0..rucksackSize];
                var rucksack2 = line[rucksackSize..];
                
                var commonItem = rucksack1.Intersect(rucksack2).Single();
                totalPriorities += GetPriority(commonItem);
            }

            Console.WriteLine($"Total priority = {totalPriorities}");
        }
    }
}