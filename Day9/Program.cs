using System.Drawing;

var uniquePositions = new HashSet<Point>();

// var head = new Point(0, 0);
// var tail = new Point(0, 0);

var knots = new List<Point>();
for (int i = 0; i < 10; i++)
{
    knots.Add(new Point(0, 0));
}

var head = knots[0];
var tail = knots[9];
uniquePositions.Add(tail);


foreach (var line in System.IO.File.ReadLines(@"ropemoves.txt"))
{
    var chars = line.Split(" ");
    var direction = chars[0];
    var numberOfSteps = int.Parse(chars[1]);

    // Part 1
    // MoveHeadShort(direction, numberOfSteps);
    
    // Part 2
    MoveHeadLong(direction, numberOfSteps);
}

Console.WriteLine($"Tail positions: {uniquePositions.Count}");

void MoveHeadShort(string direction, int spaces)
{
    switch (direction)
    {
        case "R":
            for (int i = 0; i < spaces; i++)
            {
                head.X += 1;
                MoveTail();
            }
            break;
        
        case "L":
            for (int i = 0; i < spaces; i++)
            {
                head.X -= 1;
                MoveTail();
            }
            break;

        case "U":
            for (int i = 0; i < spaces; i++)
            {
                head.Y += 1;
                MoveTail();
            }
            break;
        
        case "D":
            for (int i = 0; i < spaces; i++)
            {
                head.Y -= 1;
                MoveTail();
            }
            break;
    }
}

void MoveTail()
{
    var horizontalOffset = head.X - tail.X;
    var horizontalMove = Math.Abs(horizontalOffset) > 1;

    var verticalOffset = head.Y - tail.Y;
    var verticalMove = Math.Abs(verticalOffset) > 1;

    var shouldMove = horizontalMove || verticalMove;

    if (!shouldMove)
    {
        return;
    }

    if (horizontalMove)
    {
        tail.Y = head.Y;
        tail.X += horizontalOffset > 0 ? 1 : -1;
    }

    if (verticalMove)
    {
        tail.X = head.X;
        tail.Y += verticalOffset > 0 ? 1 : -1;
    }
    
    uniquePositions.Add(tail);
}

void MoveHeadLong(string direction, int spaces)
{
    switch (direction)
    {
        case "R":
            for (int i = 0; i < spaces; i++)
            {
                var head = knots[0];
                head.X += 1;
                knots[0] = head;
                MoveKnots();
            }
            break;
        
        case "L":
            for (int i = 0; i < spaces; i++)
            {
                var head = knots[0];
                head.X -= 1;
                knots[0] = head;
                MoveKnots();
            }
            break;

        case "U":
            for (int i = 0; i < spaces; i++)
            {
                var head = knots[0];
                head.Y += 1;
                knots[0] = head;
                MoveKnots();
            }
            break;
        
        case "D":
            for (int i = 0; i < spaces; i++)
            {
                var head = knots[0];
                head.Y -= 1;
                knots[0] = head;
                MoveKnots();
            }
            break;
    }
}

void MoveKnots()
{
    for (int i = 1; i < knots.Count; i++)
    {
        MoveKnot(i);
    }
}

void MoveKnot(int knotPosition)
{
    var frontKnot = knots[knotPosition - 1];
    var backKnot = knots[knotPosition];

    var horizontalOffset = frontKnot.X - backKnot.X;
    var horizontalMove = Math.Abs(horizontalOffset) > 1;

    var verticalOffset = frontKnot.Y - backKnot.Y;
    var verticalMove = Math.Abs(verticalOffset) > 1;

    var shouldMove = horizontalMove || verticalMove;
    var diagonalMove = horizontalMove && verticalMove;

    if (!shouldMove)
    {
        return;
    }

    if (diagonalMove)
    {
        backKnot.Y += verticalOffset > 0 ? 1 : -1;
        backKnot.X += horizontalOffset > 0 ? 1 : -1;
    }
    else
    {
        if (horizontalMove)
        {
            backKnot.Y = frontKnot.Y;
            backKnot.X += horizontalOffset > 0 ? 1 : -1;
        }

        if (verticalMove)
        {
            backKnot.X = frontKnot.X;
            backKnot.Y += verticalOffset > 0 ? 1 : -1;
        }
    }
    
    knots[knotPosition] = backKnot;
    if (knotPosition == 9)
    {
        uniquePositions.Add(knots[knotPosition]);
    } 
}