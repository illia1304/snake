using System.Drawing;

class SnakeGame
{
    private Snake snake;
    private bool isGameOver;
    private Apple apple;
    private AppleGenerator appleGenerator;
    private bool isGameSave;

    public SnakeGame()
    {
        snake = new Snake(new Point(38, 15), 5);
        isGameOver = false;
        appleGenerator = new AppleGenerator();
        apple = appleGenerator.GenerateApple();
        isGameSave = false;
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

            MoveSnake();
            DrawGame();
        }

        Console.CursorVisible = true;

        if (isGameOver)
        {
            DrawGameOverScreen();
            SaveGame("D:\\Code\\snake\\snake\\gameState.json");
        }
    }

    private Direction snakeDirection = Direction.Right;

    private void HandleKeyPress(ConsoleKey key)
    {
        snakeDirection = key switch
        {
            ConsoleKey.LeftArrow when snakeDirection != Direction.Right => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            _ => snakeDirection,
        };

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
        else
        {
            if (snake.Body.Last().X < 0 || snake.Body.Last().X >= Console.WindowWidth ||
            snake.Body.Last().Y < 0 || snake.Body.Last().Y >= Console.WindowHeight)
            {
                isGameOver = true;
            }

            for (int i = 0; i < snake.Body.Count - 1; i++)
            {
                if (snake.Body[i] == snake.Body.Last())
                {
                    isGameOver = true;
                    break;
                }
            }
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


    public record GameState
    {
        public int SnakeLength;
        public List<Point> Body;
        public Point AppleLocation;
        public Direction SnakeDirection;
    }

    public void SaveGame(string filepath)
    {
        GameState gameState = new GameState
        {
            SnakeLength = snake.Body.Count,
            Body = snake.Body,
            AppleLocation = apple.Location,
        };

        string convert = Newtonsoft.Json.JsonConvert.SerializeObject(gameState, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), filepath), convert);
    }

    public static SnakeGame LoadGame(string filepath)
    {

        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException(filepath);
        }

        string savedGame = File.ReadAllText(filepath);
        GameState gameState = Newtonsoft.Json.JsonConvert.DeserializeObject<GameState>(savedGame);
        SnakeGame game = new SnakeGame();

        game.snake = new Snake(gameState.Body.Last(), gameState.SnakeLength);
        game.apple = new Apple(gameState.AppleLocation);

        game.snakeDirection = gameState.SnakeDirection;

        return game;
    }


    private void DrawGameOverScreen()
    {
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 1);
        Console.WriteLine("GAME OVER");
    }

}




enum Direction
{
    Left,
    Up,
    Right,
    Down
}