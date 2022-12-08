using System.Collections;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = 1;
            var y = 1;
            var treeGrid = new List<List<int>>();
            var scoreGrid = new List<List<int>>();
            var rowLength = 0;
            var colHeight = 0;
            var viewScore = 0;

            foreach (var line in System.IO.File.ReadLines(@"trees.txt"))
            {
                var treeHeights = line.Select(c => (int)char.GetNumericValue(c)).ToList();
                treeGrid.Add(treeHeights);
            }

            rowLength = treeGrid[0].Count;
            colHeight = treeGrid.Count;

            var perimeterTrees = (rowLength * 2) + ((colHeight - 2) * 2);
            var internalTrees = 0;

            for (int colIndex = y; colIndex < colHeight - 1; colIndex++)
            {
                var row = treeGrid[colIndex];

                for (int rowIndex = x; rowIndex < rowLength - 1; rowIndex++)
                {
                    var height = treeGrid[colIndex][rowIndex];
                    var isVisible = IsVisibleLeft(rowIndex, height, row) 
                        || IsVisibleRight(rowIndex, height, row) 
                        || IsVisibleUp(colIndex, rowIndex, height, treeGrid) 
                        || IsVisibleDown(colIndex, rowIndex, height, treeGrid);

                    internalTrees += isVisible ? 1 : 0;

                    var scoreL = ScoreLeft(rowIndex, height, row);
                    var scoreR = ScoreRight(rowIndex, height, row);
                    var scoreU = ScoreUp(colIndex, rowIndex, height, treeGrid);
                    var scoreD = ScoreDown(colIndex, rowIndex, height, treeGrid);

                    var score = scoreL * scoreR * scoreU * scoreD;

                    viewScore = score > viewScore ? score : viewScore;
                }
            }

            Console.WriteLine($"Tree count: {internalTrees + perimeterTrees}");
            Console.WriteLine($"Highest view score: {viewScore}");
        }

        static bool IsVisibleLeft(int rowIndex, int height, List<int> row)
        {
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (row[i] >= height)
                {
                    return false;
                }
            }

            return true;
        }

        static bool IsVisibleRight(int rowIndex, int height, List<int> row)
        {
            for (int i = rowIndex + 1; i <= row.Count - 1; i++)
            {
                if (row[i] >= height)
                {
                    return false;
                }
            }

            return true;
        }

        static bool IsVisibleUp(int colIndex, int rowIndex, int height, List<List<int>> grid)
        {
            for (int i = colIndex - 1; i >= 0; i--)
            {
                if (grid[i][rowIndex] >= height)
                {
                    return false;
                }
            }

            return true;
        }

        static bool IsVisibleDown(int colIndex, int rowIndex, int height, List<List<int>> grid)
        {
            for (int i = colIndex + 1; i <= grid.Count - 1; i++)
            {
                if (grid[i][rowIndex] >= height)
                {
                    return false;
                }
            }

            return true;
        }

        // Part 2
        static int ScoreLeft(int rowIndex, int height, List<int> row)
        {
            var score = 0;
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                score++;
                if (i == 0 || row[i] >= height)
                {
                    return score;
                }
            }

            return score;
        }

        static int ScoreRight(int rowIndex, int height, List<int> row)
        {
            var score = 0;
            for (int i = rowIndex + 1; i <= row.Count - 1; i++)
            {
                score++;
                if (i == 0 || row[i] >= height)
                {
                    return score;
                }
            }

            return score;
        }

        static int ScoreUp(int colIndex, int rowIndex, int height, List<List<int>> grid)
        {
            var score = 0;
            for (int i = colIndex - 1; i >= 0; i--)
            {
                score++;
                if (i == 0 || grid[i][rowIndex] >= height)
                {
                    return score;
                }
            }

            return score;
        }

        static int ScoreDown(int colIndex, int rowIndex, int height, List<List<int>> grid)
        {
            var score = 0;
            for (int i = colIndex + 1; i <= grid.Count - 1; i++)
            {
                score++;
                var newHeight = grid[i][rowIndex];
                if (i == 0 || newHeight >= height)
                {
                    return score;
                }
            }

            return score;
        }
    }
}