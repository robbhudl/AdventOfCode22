using System.Collections;
using System.Linq;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxCalories = 0m;
            var currentCalories = 0m;
            var calories = new List<decimal>();

            // Read the file and display it line by line.  
            foreach (var line in System.IO.File.ReadLines(@"calories.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    maxCalories = currentCalories > maxCalories ? currentCalories : maxCalories;
                    calories.Add(currentCalories);
                    currentCalories = 0;
                    continue;
                }

                currentCalories += int.Parse(line);
            }

            calories = calories.OrderByDescending(c => c).ToList();
            calories.Take(3).Sum();
            var topThreeCalories = calories.Take(3).Sum();;

            Console.WriteLine($"Individual elf maxCalories = {maxCalories}");
            Console.WriteLine($"Top 3 elf maxCalories = {topThreeCalories}");
        }
    }
}