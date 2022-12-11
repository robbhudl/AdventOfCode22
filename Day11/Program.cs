using System.Text.RegularExpressions;

var monkeys = new List<Monkey>();
var itemsString = "Starting items:";
var operationString = "Operation: new =";
var testString = "Test:";

var rounds = 10000;
var worryLevelReduction = 3;

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
            var startingItems = items.Split(", ").Select(i => ulong.Parse(i)).ToList();
            monkey.Items = startingItems;
        }
        else if (line.Contains(operationString))
        {
            var ops = line.Substring(line.IndexOf("old ") + 4).Split(" ");
            monkey.Operator = ops[0];

            var isInt = ulong.TryParse(ops[1], out var rhs);
            if (isInt)
            {
                monkey.RHS = rhs;
            }
        }
        else if (line.Contains(testString))
        {
            var rgx = new Regex(@"\d+");
            var match = rgx.Match(line).Value;
            monkey.Divisor = ulong.Parse(match);
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

var divisors = monkeys.Select(m => m.Divisor).ToArray();

static ulong LCM(ulong[] ofNumbers) // Find LCM using GCD (source: https://iq.opengenus.org/lcm-of-array-of-numbers/ )
{
    ulong ans = ofNumbers[0];
    for (int i = 1; i < ofNumbers.Length; i++)
    {
        ans = ofNumbers[i] * ans / GCD(ofNumbers[i], ans);
    }
    return ans;
}

static ulong GCD(ulong a, ulong b) { return b == 0 ? a : GCD(b, a % b); }

var mod = LCM(divisors); // divisors.Aggregate((i, i1) => i * i1);

for (int i = 0; i < rounds; i++)
{
    foreach (var monkey in monkeys)
    {
        foreach (var item in monkey.Items)
        {
            var worry = monkey.PerformCalculation(item);
            // Part 1
            // worry /= worryLevelReduction;
            
            // Part 2
            worry = worry % mod;

            var isDivisible = (worry % monkey.Divisor) == 0;
            var nextMonkey = isDivisible ? monkey.NextTrue : monkey.NextFalse;
            monkeys[nextMonkey].Items.Add(worry);
            monkey.Inspections++;
        }
        monkey.Items.Clear();
    }
}

var inspections = monkeys.Select(m => m.Inspections).ToList();
var orderedInspections = inspections.OrderByDescending(i => i).ToList();

Console.WriteLine($"Monkey business = {inspections[0] * inspections[1]}");

class Monkey
{
    public List<ulong> Items { get; set; } = new List<ulong>();
    public ulong Divisor { get; set; }
    public int NextTrue { get; set; }
    public int NextFalse { get; set; }
    public ulong Inspections { get; set; }

    public string Operator { get; set; } = "";
    public ulong RHS { get; set; } = 0;

    public ulong PerformCalculation(ulong number1)
    {
        var number2 = RHS == 0 ? number1 : RHS;
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
                return 0;
        }
    }
}