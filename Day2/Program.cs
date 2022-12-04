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

        static Dictionary<char, int> results = new Dictionary<char, int> 
        {
            {'X', 1}, // Lose
            {'Y', 2}, // Draw
            {'Z', 3}, // Win
        };

        static int win = 6;
        static int draw = 3;
        static int lose = 0;

        static int CalculateShape(char oppo, char me)
        {
            switch (oppo)
            {
                case 'A': // Rock
                    if (me == 'X') { return lose + scores['Z']; }
                    if (me == 'Y') { return draw + scores['X']; }
                    if (me == 'Z') { return win + scores['Y']; }

                break;
                
                case 'B': // Paper
                    if (me == 'X') { return lose + scores['X']; }
                    if (me == 'Y') { return draw + scores['Y']; }
                    if (me == 'Z') { return win + scores['Z']; }
                    
                break;

                case 'C': // Scissors
                    if (me == 'X') { return lose + scores['Y']; }
                    if (me == 'Y') { return draw + scores['Z']; }
                    if (me == 'Z') { return win + scores['X']; }
                    
                break;

                default:
                    return 0;
            }

            return 0;
        }

        static int CalculateResult(char oppo, char result)
        {
            var baseScore = scores[result];
            switch (oppo)
            {
                case 'A': // Rock
                    if (result == 'X') { return draw + baseScore; }
                    if (result == 'Y') { return win + baseScore; }
                    if (result == 'Z') { return lose + baseScore; }

                break;
                
                case 'B': // Paper
                    if (result == 'X') { return lose + baseScore; }
                    if (result == 'Y') { return draw + baseScore; }
                    if (result == 'Z') { return win + baseScore; }
                    
                break;

                case 'C': // Scissors
                    if (result == 'X') { return win + baseScore; }
                    if (result == 'Y') { return lose + baseScore; }
                    if (result == 'Z') { return draw + baseScore; }
                    
                break;

                default:
                    return 0;
            }

            return 0;
        }

        static void Main(string[] args)
        {
            var totalScore = 0;
            var totalResult = 0;

            foreach (var line in System.IO.File.ReadLines(@"strategy.txt"))
            {
                var chars = line.ToCharArray();
                // Console.WriteLine(chars.Length);
                var oppo = chars[0];
                var me = chars[2];

                totalScore += CalculateResult(oppo, me);
                totalResult += CalculateShape(oppo, me);
            }

            Console.WriteLine($"Total score = {totalScore}");
            Console.WriteLine($"Total score = {totalResult}");
        }
    }
}