using System.Text.RegularExpressions;

var monkeys = new List<Monkey>();
var itemsString = "Starting items:";
var operationString = "Operation: new =";
var testString = "Test:";

foreach (var line in System.IO.File.ReadLines(@"monkeys.txt"))
{
    if (line.StartsWith("Monkey"))
    {
        monkeys.Add(new Monkey());
    }
    else
    {
        var monkey = monkeys.Last();
        if (line.Contains(itemsString))
        {
            var items = line.Substring(line.IndexOf(": ") + 1);
            var startingItems = items.Split(", ").Select(i => int.Parse(i)).ToList();
            monkey.Items = startingItems;
        }
        else if (line.Contains(operationString))
        {
            var ops = line.Substring(line.IndexOf("old ") + 4).Split(" ");
            monkey.Operator = ops[0];

            var isInt = int.TryParse(ops[1], out var rhs);
            if (isInt)
            {
                monkey.RHS = rhs;
            }
        }
        else if (line.Contains(testString))
        {
            var rgx = new Regex(@"\d+");
            var match = rgx.Match(line).Value;
            monkey.Divisor = int.Parse(match);
        }
        else if (line.Contains("true"))
        {
            var rgx = new Regex(@"\d+");
            var match = rgx.Match(line).Value;
            monkey.NextTrue = int.Parse(match);
        }
        else if (line.Contains("false"))
        {
            var rgx = new Regex(@"\d+");
            var match = rgx.Match(line).Value;
            monkey.NextFalse = int.Parse(match);
        }
    }
}

for (int i = 0; i < 20; i++)
{
    foreach (var monkey in monkeys)
    {
        foreach (var item in monkey.Items)
        {
            var worry = monkey.PerformCalculation(item);
            worry /= 3;

            var isDivisible = (worry % monkey.Divisor) == 0;
            var nextMonkey = isDivisible ? monkey.NextTrue : monkey.NextFalse;
            monkeys[nextMonkey].Items.Add(worry);
            monkey.Inspections++;
        }
        monkey.Items.Clear();
    }
    
}

var inspections = monkeys.Select(m => m.Inspections).OrderByDescending(i => i).ToList();

Console.WriteLine($"Monkey business = {inspections[0] * inspections[1]}");

class Monkey
{
    public List<int> Items { get; set; } = new List<int>();
    public int Divisor { get; set; }
    public int NextTrue { get; set; }
    public int NextFalse { get; set; }
    public int Inspections { get; set; }

    public string Operator { get; set; } = "";
    public int RHS { get; set; } = -1;

    public int PerformCalculation(int number1)
    {
        var number2 = RHS == -1 ? number1 : RHS;
        switch (Operator)
        {
            case "+":
                return number1 + number2;

            case "-":
                return number1 - number2;

            case "/":
                return number1 / number2;

            case "*":
                return number1 * number2;

            default:
                return -1;
        }
    }
}