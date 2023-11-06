using System;
using System.Drawing;

class Apple
{
    public Point Location { get; private set; }

    public Apple(Point location)
    {
        Location = location;
    }
}
class AppleGenerator
{
    private Random random = new Random();

    public Apple GenerateApple()
    {
        int x = random.Next(0, Console.WindowWidth);
        int y = random.Next(0, Console.WindowHeight);

        return new Apple(new Point(x, y));
    }
}