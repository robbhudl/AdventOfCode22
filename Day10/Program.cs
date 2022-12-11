var registerValues = new List<int[]>
{
    new int[] {1, 1, 1},
};

var CRT = new List<string> 
{ 
    "",
    "",
    "",
    "",
    "",
    "", 
};

int CRTrow = 0;
int pixelPosition = 0;
int cycleCount = 0;
var start = 0;
var during = 1;
var end = 2;

foreach (var line in System.IO.File.ReadLines(@"clock.txt"))
{
    var currentRegisterValue = registerValues.Last();

    if (line == "noop")
    {
        var previousCycleVal = currentRegisterValue[end];
        NoOp(previousCycleVal);
        
        Process();
    }
    else
    {
        var value = int.Parse(line.Split(" ")[1]);
        var previousCycleVal = currentRegisterValue[end];
        NoOp(previousCycleVal);

        currentRegisterValue = registerValues.Last();

        Process();

        previousCycleVal = currentRegisterValue[end];
        var updatedValue = (previousCycleVal + value);
        var cycle = new int[] {previousCycleVal, previousCycleVal, updatedValue};

        registerValues.Add(cycle);
        cycleCount++;

        Process();
    }
}

// Part 1
Console.WriteLine($"Total Strength: {CalculateTotalStrength()}");

// Part 2
foreach (var row in CRT)
{
    Console.WriteLine(row);
}

void NoOp(int previousCycleVal)
{
    var updatedCycle = new int[] {previousCycleVal, previousCycleVal, previousCycleVal}; 
    registerValues.Add(updatedCycle);
    cycleCount++;
}

void Process()
{
    var xPos = registerValues[cycleCount][during];
    DrawPixel(xPos);
    pixelPosition += 1;
    ChangeRow();
}

void DrawPixel(int centerX)
{
    var isOverlap = (centerX - 1) <= pixelPosition && pixelPosition <= centerX + 1;
    var pixelVal = isOverlap ? "#" : ".";
    CRT[CRTrow] += pixelVal;
}

void ChangeRow()
{
    if (pixelPosition == 40)
    {
        pixelPosition = 0;
        CRTrow += 1;
    }
}

int CalculateTotalStrength()
{
    var totalStrength = 0;

    for (int i = 20; i <= 220 ; i+=40)
    {
        var strength = i * registerValues[i][during];
        Console.WriteLine($"Cycle: {i} X: {registerValues[i][start]} {registerValues[i][during]} {registerValues[i][end]} Strength: {strength}");
        totalStrength += strength;
    }

    return totalStrength;
}