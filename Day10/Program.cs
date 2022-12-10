var start = 0;
var during = 1;
var end = 2;

var registerValues = new List<int[]>
{
    new int[] {1, 1, 1},
};

foreach (var line in System.IO.File.ReadLines(@"clock.txt"))
{
    var currentRegisterValue = registerValues.Last();

    if (line == "noop")
    {
        registerValues.Add(currentRegisterValue);
    }
    else
    {
        var value = int.Parse(line.Split(" ")[1]);
        var previousCycleVal = currentRegisterValue[end];
        var updatedCycle = new int[] {previousCycleVal, previousCycleVal, previousCycleVal}; 
        registerValues.Add(updatedCycle);

        currentRegisterValue = registerValues.Last();
        previousCycleVal = currentRegisterValue[end];
        var updatedValue = (previousCycleVal + value);
        var cycleStartVal = previousCycleVal;
        var cycleDuringVal = previousCycleVal;
        var cycleEndVal = updatedValue;
        var cycle = new int[] {cycleStartVal, cycleDuringVal, cycleEndVal};

        registerValues.Add(cycle);
    }

    // Part 1
    // MoveHeadShort(direction, numberOfSteps);
}

Console.WriteLine($"Total Strength: {CalculateTotalStrength()}");

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