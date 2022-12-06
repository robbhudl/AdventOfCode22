using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Radio
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var line in System.IO.File.ReadLines(@"datastream.txt"))
            {
                var isDistinct = false;
                var markerLength = 4;
                var charsFromStart = markerLength;
                var skip = 0;

                while(!isDistinct)
                //while(!isDistinct || skip < 60)
                {
                    Console.WriteLine(charsFromStart);
                    var chars = line.Skip(skip).Take(markerLength);
                    isDistinct = chars.Distinct().Count() >= markerLength;
                    charsFromStart += 1;
                    foreach (var c in chars)
                    {
                        Console.Write(c);
                    }
                    Console.WriteLine(chars.Distinct().Count());
                    Console.WriteLine(skip + markerLength);

                    skip++;
                }
            }
        }
    }
}