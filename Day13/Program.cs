var packetPairs = new List<List<string>> { new List<string>() };

foreach (var line in System.IO.File.ReadLines(@"signalstest.txt"))
{
    if (!string.IsNullOrWhiteSpace(line))
    {
        packetPairs.Last().Add(line);
    }
    else
    {
        packetPairs.Add(new List<string>());
    }
}

var signal = ParseSignal(packetPairs[7][0]);

var orderedCorrectly = new List<bool>();

Console.WriteLine($"Number of pairs = {packetPairs.Count}");

List<List<Object>> ParseSignal(string signal)
{
    var referenceStack = new Stack<List<Object>>();
    var currentList = new List<Object>();
    var packetData = new List<List<Object>> { currentList };
    foreach (var character in signal)
    {
        switch (character)
        {
            case '[':
                var newList = new List<Object>();
                referenceStack.Push(newList);
                currentList?.Add(newList);
                currentList = newList;
                break;

            case ',':
                continue;

            case ']':
                referenceStack.Pop();
                referenceStack.TryPeek(out currentList);
                break;
            
            default:
                var val = (int)char.GetNumericValue(character);
                currentList?.Add(val);
                break;
        }
    }

    return packetData;
}