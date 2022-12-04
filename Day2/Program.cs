using System.Collections;
using System.Linq;

namespace DayTwo
{
    class Program
    {
        static Dictionary<char, int> scores = new Dictionary<char, int> 
        {
            {'X', 1}, // Rock
            {'Y', 2}, // Paper
            {'Z', 3}, // Scissors
        };

        static int win = 6;
        static int draw = 3;
        static int lose = 0;

        static int CalculateResult(char oppo, char me)
        {
            var baseScore = scores[me];
            switch (oppo)
            {
                case 'A': // Rock
                    if (me == 'X') { return draw + baseScore; }
                    if (me == 'Y') { return win + baseScore; }
                    if (me == 'Z') { return lose + baseScore; }

                break;
                
                case 'B': // Paper
                    if (me == 'X') { return lose + baseScore; }
                    if (me == 'Y') { return draw + baseScore; }
                    if (me == 'Z') { return win + baseScore; }
                    
                break;

                case 'C': // Scissors
                    if (me == 'X') { return win + baseScore; }
                    if (me == 'Y') { return lose + baseScore; }
                    if (me == 'Z') { return draw + baseScore; }
                    
                break;

                default:
                    return 0;
            }

            return 0;
        }

        static void Main(string[] args)
        {
            var totalScore = 0;

            foreach (var line in System.IO.File.ReadLines(@"strategy.txt"))
            {
                var chars = line.ToCharArray();
                // Console.WriteLine(chars.Length);
                var oppo = chars[0];
                var me = chars[2];

                totalScore += CalculateResult(oppo, me);
            }

            Console.WriteLine($"Total score = {totalScore}");
        }
    }
}