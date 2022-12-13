var grid = new List<List<int>>();
var graphNodes = new List<List<GraphNode>>();

var startPoint = new Point(0, 0);
var endPoint = new Point(0, 0);

var visited = new Dictionary<GraphNode, GraphNode>();

var x = 0;
var y = 0;

foreach (var line in System.IO.File.ReadLines(@"elevation.txt"))
{
    var row = new List<int>();
    foreach (var c in line)
    {
        switch (c)
        {
            case 'S':
                startPoint.X = x;
                startPoint.Y = y;
                row.Add(0);
                break;

            case 'E':
                endPoint.X = x;
                endPoint.Y = y;
                row.Add(25);
                break;

            default:
                row.Add(ConvertToHeight(c));
                break;
        }
        x++;
    }
    x = 0;
    y++;
    grid.Add(row);
}

for (int y1 = 0; y1 < grid.Count; y1++)
{
    var row = grid[y1];
    graphNodes.Add(new List<GraphNode>());

    for (int x1 = 0; x1 < row.Count; x1++)
    {
        var height = row[x1];
        var position = new Point(x1, y1);

        var graph = new GraphNode
        {
            Position = position,
            Height = height,
        };
        
        graphNodes[y1].Add(graph);
    }
}

foreach (var row in graphNodes)
{
    foreach (var node in row)
    {
        AddEdges(node);
    }
}

List<Point> visitedPoints = new List<Point>();

DFSSearch(graphNodes[0][0]);

var queue = new Queue<GraphNode>();
queue.Enqueue(graphNodes[0][0]);
visited.Add(graphNodes[0][0], graphNodes[0][0]);

while (queue.Any())
{
    var node = queue.Dequeue();

    if (node.Position.X == endPoint.X && node.Position.Y == endPoint.Y)
    {
        break;
    }

    var neighbours = node.Edges;
    neighbours.ForEach(n => 
    {
        if (visited.Keys.Contains(n))
        {
            return;
        }
        visited.Add(n, node);
        queue.Enqueue(n);
    });
}

var endNode = graphNodes[endPoint.Y][endPoint.X];
var previousLocation = visited[endNode];
var steps = 1;

while (previousLocation != graphNodes[0][0])
{
    previousLocation = visited[previousLocation];
    steps++;
}

// create graph
// add all edges
// only add an edge if adjcent height - height <= 1 

Console.WriteLine($"Minimum number of moves: {steps}");

void DFSSearch(GraphNode node)
{
    visitedPoints.Add(node.Position);
    //visited.Add();
    var neighbours = node.Edges;

    foreach (var n in neighbours)
    {
        if (!visitedPoints.Contains(n.Position))
        {
            DFSSearch(n);
        }
    }
}

int ConvertToHeight(char elevation)
{
    var asciiAdjust = 97;
    var asciiValue = (int)elevation;
    return elevation - asciiAdjust;
}

void AddEdges(GraphNode graph)
{
    var currentPoint = graph.Position;
    var above = currentPoint.Y - 1 >= 0 ? graphNodes[currentPoint.Y - 1][currentPoint.X] : null;
    var below = currentPoint.Y + 1 < grid.Count ? graphNodes[currentPoint.Y + 1][currentPoint.X] : null;
    var left = currentPoint.X - 1 >= 0 ? graphNodes[currentPoint.Y][currentPoint.X - 1] : null;
    var right = currentPoint.X + 1 < grid[0].Count ? graphNodes[currentPoint.Y][currentPoint.X + 1] : null;

    if (above != null && grid[above.Position.Y][above.Position.X] - graph.Height <= 1)
    {
        graph.Edges.Add(above);
    }

    if (below != null && grid[below.Position.Y][below.Position.X] - graph.Height <= 1)
    {
        graph.Edges.Add(below);
    }

    if (left != null && grid[left.Position.Y][left.Position.X] - graph.Height <= 1)
    {
        graph.Edges.Add(left);
    }

    if (right != null && grid[right.Position.Y][right.Position.X] - graph.Height <= 1)
    {
        graph.Edges.Add(right);
    }
}

class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X;
    public int Y;
}

class GraphNode
{
    public Point Position;
    public int Height { get; set; }
    public List<GraphNode> Edges { get; set; } = new List<GraphNode>();
}