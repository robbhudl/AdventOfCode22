var pairs = new List<List<string>> { new List<string>() };

foreach (var line in System.IO.File.ReadLines(@"signalstest.txt"))
{
    if (!string.IsNullOrWhiteSpace(line))
    {
        var newLine = line.Substring(1);
        pairs.Last().Add(newLine);
    }
    else
    {
        pairs.Add(new List<string>());
    }
}

foreach (var pair in pairs)
{
    var packetLeft = ParseSignal(pair[0]);
    var packetRight = ParseSignal(pair[1]);

    var isCorrectOrder = ComparePackets(packetLeft, packetRight);
}

var orderedCorrectly = new List<bool>();

Console.WriteLine($"Number of pairs = {pairs.Count}");

List<List<int>> ParseSignal(string signal)
{
    var numbers = new List<List<int>>();
    List<int>? currentList = null; // new List<int>();
    var previousChar = ' ';

    foreach (var character in signal)
    {
        switch (character)
        {
            case '[':
                var newList = new List<int>();
                currentList = newList;
                numbers.Add(currentList);
                break;

            case ',':
                continue;

            case ']':
                currentList = null;
                break;
            
            default:
                if (currentList == null)
                {
                    numbers.Add(new List<int>());
                    currentList = numbers.Last();
                }
                var val = (int)char.GetNumericValue(character);
                currentList.Add(val);
                break;
        }
        previousChar = character;
    }

    return numbers;
}

bool ComparePackets(List<List<int>> packetLeft, List<List<int>> packetRight)
{
    for (int i = 0; i < packetLeft.Count; i++)
    {
        var left = packetLeft[i];
        var right = packetRight[i];

        // if (left.Count < right.Count)
        // {
        //     return false;
        // }

        int index = 0;
        foreach (var valLeft in left)
        {
            if (index == right.Count)
            {
                return true;
            }
            var valRight = right[index];
            if (!CompareValues(valLeft, valRight))
            {
                return false;
            }
            index++;
        }
    }
    return true;
}

bool CompareValues(int left, int right)
{
    return left <= right;
}

class PacketData
{
    public List<int> Values { get; set; }
    bool IsSingleInt { get; set; }
}
