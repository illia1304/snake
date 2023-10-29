using System.Drawing;

class SnakeGame
{
    private Snake snake;
    private bool isGameOver;
    private Apple apple;
    private AppleGenerator appleGenerator;

    public SnakeGame()
    {
        snake = new Snake(new Point(38, 15), 5);
        isGameOver = false;
        appleGenerator = new AppleGenerator();
        apple = appleGenerator.GenerateApple();
    }

    public void Run()
    {
        Console.CursorVisible = false;
        DrawGame();

        while (!isGameOver)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Q)
                isGameOver = true;
            else
                HandleKeyPress(key);

            if (!isGameOver)
            {
                MoveSnake();
                DrawGame();
            }
        }

        Console.CursorVisible = true;
    }

    private Direction snakeDirection = Direction.Right;

    private void HandleKeyPress(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                if (snakeDirection != Direction.Right)
                    snakeDirection = Direction.Left;
                break;
            case ConsoleKey.UpArrow:
                if (snakeDirection != Direction.Down)
                    snakeDirection = Direction.Up;
                break;
            case ConsoleKey.RightArrow:
                if (snakeDirection != Direction.Left)
                    snakeDirection = Direction.Right;
                break;
            case ConsoleKey.DownArrow:
                if (snakeDirection != Direction.Up)
                    snakeDirection = Direction.Down;
                break;
        }
    }

    private void MoveSnake()
    {
        snake.Move(snakeDirection);

        if (snake.IsAppleEaten(apple))
        {
            snake.IncreaseLength();
            apple = appleGenerator.GenerateApple();
        }
    }

    private void DrawGame()
    {
        Console.Clear();
        DrawSnake();
        DrawApple();
    }

    private void DrawSnake()
    {
        foreach (Point point in snake.Body)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write("*");
        }
    }

    private void DrawApple()
    {
        Console.SetCursorPosition(apple.Location.X, apple.Location.Y);
        Console.Write("0");
    }
}

enum Direction
{
    Left,
    Up,
    Right,
    Down
}