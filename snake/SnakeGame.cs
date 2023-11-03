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

            if (!isGameOver)
            {
                MoveSnake();
                DrawGame();
            }
            else
            {
                SaveGame();
            }
            LoadGame();

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
        else
        {
            for (int i = 0; i < snake.Body.Count - 1; i++)
            {
                if (snake.Body[i] == snake.Body.Last())
                {
                    isGameOver = true;
                    isGameSave = true;
                    SaveGame();
                }
                if (snake.Body[i].X < 0 || snake.Body[i].X >= Console.WindowWidth ||
                    snake.Body[i].Y < 0 || snake.Body[i].Y >= Console.WindowHeight)
                {
                    isGameOver = true;
                    SaveGame();
                    isGameSave = true;
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
    public void SaveGame()
    {
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "isGameSave.txt"), isGameSave.ToString());

        GameState gameState = new GameState
        {
            SnakeLength = snake.Body.Count,
            Body = snake.Body,
            AppleLocation = apple.Location,
        };

        string convert = Newtonsoft.Json.JsonConvert.SerializeObject(gameState, Newtonsoft.Json.Formatting.Indented);
        isGameSave = true;

        if (!File.Exists("isGameSave.txt"))
        {
            isGameSave = false;
        }


        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "game.json"), convert);
    }
    public void LoadGame()
    {
        if (File.Exists("game.json"))
        {
            string savedGame = File.ReadAllText("game.json");
            GameState gameState = Newtonsoft.Json.JsonConvert.DeserializeObject<GameState>(savedGame);
            snake = new Snake(gameState.Body.Last(), gameState.SnakeLength);
            apple = new Apple(gameState.AppleLocation);

            snakeDirection = gameState.SnakeDirection;
        }
    }


}




enum Direction
{
    Left,
    Up,
    Right,
    Down
}