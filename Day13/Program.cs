var pairs = new List<List<string>> { new List<string>() };

foreach (var line in System.IO.File.ReadLines(@"signals.txt"))
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

var orderedCorrectly = new Dictionary<int,bool>();

int packetPair = 1;
foreach (var pair in pairs)
{
    var packetLeft = ParseSignal(pair[0]);
    var packetRight = ParseSignal(pair[1]);

    var isCorrectOrder = ComparePackets(packetLeft, packetRight);
    orderedCorrectly.Add(packetPair, isCorrectOrder);
    packetPair++;
}

var sum = orderedCorrectly.Where(o => o.Value).Sum(o => o.Key);

Console.WriteLine($"Number of pairs: {pairs.Count}, sum of correct: {sum}");

List<PacketData> ParseSignal(string signal)
{
    var packets = new List<PacketData>();
    PacketData? currentPacketData = null;
    var previousChar = ' ';

    foreach (var character in signal)
    {
        switch (character)
        {
            case '[':
                var newList = new PacketData();
                currentPacketData = newList;
                packets.Add(currentPacketData);
                break;

            case ',':
                continue;

            case ']':
                currentPacketData = null;
                break;
            
            default:
                if (currentPacketData == null)
                {
                    packets.Add(new PacketData());
                    currentPacketData = packets.Last();
                    currentPacketData.IsSingleInt = true;
                }
                else
                {
                    currentPacketData.IsSingleInt = false;
                }
                var val = (int)char.GetNumericValue(character);
                currentPacketData.Values.Add(val);
                break;
        }
    }

    return packets;
}

bool ComparePackets(List<PacketData> packetLeft, List<PacketData> packetRight)
{
    for (int i = 0; i < packetLeft.Count; i++)
    {
        var left = packetLeft[i].Values;

        if (i >= packetRight.Count)
        {
            // right ran out of items
            return false;
        }
        var right = packetRight[i].Values;
        var rightIsSingleInt = packetRight[i].IsSingleInt;

        // if (left.Count < right.Count)
        // {
        //     return false;
        // }

        int index = 0;
        foreach (var valLeft in left)
        {
            // If right was a single integer
            if (index == right.Count && rightIsSingleInt)
            {
                return true;
            }

            // If right was not a single integer
            if (index == right.Count)
            {
                // right has run out before left
                return false;
            }
            var valRight = right[index];

            var diff = CompareValues(valLeft, valRight);
            if (diff < 0)
            {
                return true;
            }
            if (diff > 0)
            {
                return false;
            }
            index++;
        }
    }
    return true;
}

int CompareValues(int left, int right)
{
    return left - right;
}

class PacketData
{
    public List<int> Values { get; set; } = new List<int>();
    public bool IsSingleInt { get; set; }
}
