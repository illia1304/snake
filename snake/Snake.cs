using System.Drawing;

class Snake
{
    private List<Point> body;
    private int length;

    public Snake(Point initialPosition, int initialLength)
    {
        body = new List<Point> { initialPosition };
        length = initialLength;
    }

    public List<Point> Body => body;

    public void Move(Direction direction)
    {
        Point head = GetHead();
        Point newHead = CalculateNewHeadPosition(head, direction);
        body.Add(newHead);

        if (body.Count > length)
        {
            body.RemoveAt(0);
        }
    }

    private Point GetHead()
    {
        return body.Last();
    }

    private Point CalculateNewHeadPosition(Point currentHead, Direction direction)
    {
        int newX = currentHead.X;
        int newY = currentHead.Y;

        switch (direction)
        {
            case Direction.Left:
                newX--;
                break;
            case Direction.Up:
                newY--;
                break;
            case Direction.Right:
                newX++;
                break;
            case Direction.Down:
                newY++;
                break;
        }

        return new Point(newX, newY);
    }

    public void IncreaseLength()
    {
        length++;
    }

    public bool IsAppleEaten(Apple apple)
    {
        Point head = GetHead();
        return head == apple.Location;
    }
}
