using System.Drawing;

var uniquePositions = new HashSet<Point>();

var head = new Point(0, 0);
var tail = new Point(0, 0);
uniquePositions.Add(tail);

foreach (var line in System.IO.File.ReadLines(@"ropemoves.txt"))
{
    var chars = line.Split(" ");
    var direction = chars[0];
    var numberOfSteps = int.Parse(chars[1]);

    // Console.WriteLine($"Head X:{head.X} Y:{head.Y}, Tail X:{tail.X} Y:{tail.Y}");
    MoveHead(direction, numberOfSteps);
}

Console.WriteLine($"Tail positions: {uniquePositions.Count}");

void MoveHead(string direction, int spaces)
{
    switch (direction)
    {
        case "R":
            for (int i = 0; i < spaces; i++)
            {
                head.X += 1;
                // Console.WriteLine($"Head X:{head.X} Y:{head.Y}, Tail X:{tail.X} Y:{tail.Y}");

                MoveTail();
            }
            break;
        
        case "L":
            for (int i = 0; i < spaces; i++)
            {
                head.X -= 1;
                // Console.WriteLine($"Head X:{head.X} Y:{head.Y}, Tail X:{tail.X} Y:{tail.Y}");

                MoveTail();
            }
            break;

        case "U":
            for (int i = 0; i < spaces; i++)
            {
                head.Y += 1;
                // Console.WriteLine($"Head X:{head.X} Y:{head.Y}, Tail X:{tail.X} Y:{tail.Y}");

                MoveTail();
            }
            break;
        
        case "D":
            for (int i = 0; i < spaces; i++)
            {
                head.Y -= 1;
                // Console.WriteLine($"Head X:{head.X} Y:{head.Y}, Tail X:{tail.X} Y:{tail.Y}");

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
    // Console.WriteLine($"Head X:{head.X} Y:{head.Y}, Tail X:{tail.X} Y:{tail.Y}");
}