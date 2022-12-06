using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Crates
{
    class Program
    {
        static List<int> ParseMove(string move)
        {
            var rx = new Regex(@"\d+");
            var matches = rx.Matches(move);

            var moves = matches.Select(m => int.Parse(m.Value)).ToList();

            return moves;
        }

        static List<List<char>> ParseCrates()
        {
            var crates = new List<List<char>>(9);
            for (int i = 0; i < 9; i++)
            {
                crates.Add(new List<char>());
            }

            foreach (var line in System.IO.File.ReadLines(@"crates.txt"))
            {
                for (int i = 1, j = 0; i < line.Length; i+=4, j++)
                {
                    var crateLetter = line[i];
                    if(!Char.IsWhiteSpace(crateLetter))
                    {
                        crates[j].Add(crateLetter);
                    }
                }
            }

            foreach (var crate in crates)
            {
                crate.Reverse();
            }

            return crates;
        }

        static void Main(string[] args)
        {
            var crates = ParseCrates();

            foreach (var line in System.IO.File.ReadLines(@"moves.txt"))
            {
                var moves = ParseMove(line);
                var cratesToMove = moves[0];
                var colFrom = moves[1] - 1;
                var colTo = moves[2] - 1;

                // Part 1
                // for (int i = 0; i < cratesToMove; i++)
                // {
                //     var crateStack = crates[colFrom];
                //     var crate = crateStack.Last();
                //     crateStack.RemoveAt(crateStack.Count - 1);
                //     crates[colTo].Add(crate);
                // }

                // Part 2
                var crateStack = crates[colFrom];
                var startIndex = (crateStack.Count - cratesToMove);
                var crateRange = crateStack.GetRange(startIndex, cratesToMove);
                crateStack.RemoveRange(startIndex, cratesToMove);
                crates[colTo].AddRange(crateRange);
            }

            foreach (var crate in crates)
            {
                Console.Write(crate.Last());
            }
        }
    }
}