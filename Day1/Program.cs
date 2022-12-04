namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxCalories = 0;
            var currentCalories = 0;

            // Read the file and display it line by line.  
            foreach (var line in System.IO.File.ReadLines(@"calories.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    maxCalories = currentCalories > maxCalories ? currentCalories : maxCalories;
                    currentCalories = 0;
                    continue;
                }

                currentCalories += int.Parse(line);
                // System.Console.WriteLine(currentCalories);  
            }  

            Console.WriteLine(maxCalories);
        }
    }
}