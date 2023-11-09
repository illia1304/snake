using System;
using System.Drawing;
using Xunit;

public class SnakeGameTests
{
    [Fact]
    public void SnakeMovesCorrectly()
    {
        Snake snake = new Snake(new Point(10, 10), 3);
        snake.Move(Direction.Right);
        snake.Move(Direction.Down);
        Assert.Equal(new Point(12, 11), snake.Body.Last());
    }

    [Fact]
    public void SnakeLengthIncreasesOnAppleEaten()
    {
        Snake snake = new Snake(new Point(5, 5), 2);
        Apple apple = new Apple(new Point(6, 5));

        snake.Move(Direction.Right);
        Assert.False(snake.IsAppleEaten(apple));

        snake.Move(Direction.Right);
        Assert.True(snake.IsAppleEaten(apple));
        Assert.Equal(3, snake.Body.Count);
    }

    [Fact]
    public void AppleGeneratorGeneratesValidLocation()
    {
        AppleGenerator appleGenerator = new AppleGenerator();

        for (int i = 0; i < 1000; i++)
        {
            Apple apple = appleGenerator.GenerateApple();
            Assert.InRange(apple.Location.X, 0, Console.WindowWidth - 1);
            Assert.InRange(apple.Location.Y, 0, Console.WindowHeight - 1);
        }
    }


    [Theory]
    [InlineData(Direction.Left, 10, 10, 9, 10)]
    [InlineData(Direction.Up, 10, 10, 10, 9)]
    [InlineData(Direction.Right, 10, 10, 11, 10)]
    [InlineData(Direction.Down, 10, 10, 10, 11)]
    public void SnakeMovesCorrectly(Direction direction, int startX, int startY, int expectedX, int expectedY)
    {
        Snake snake = new Snake(new Point(startX, startY), 3);
        snake.Move(direction);
        Assert.Equal(new Point(expectedX, expectedY), snake.Body.Last());
    }

    [Theory]
    [InlineData(5, 5, 6, 5, true)] 
    [InlineData(5, 5, 7, 5, false)]
    public void SnakeEatsAppleCorrectly(int snakeX, int snakeY, int appleX, int appleY, bool expected)
    {
        Snake snake = new Snake(new Point(snakeX, snakeY), 3);
        Apple apple = new Apple(new Point(appleX, appleY));

        bool result = snake.IsAppleEaten(apple);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(15, 20)]
    [InlineData(5, 5)]
    public void AppleGeneratorGeneratesValidLocation(int expectedX, int expectedY)
    {
        AppleGenerator appleGenerator = new AppleGenerator();

        Apple apple = appleGenerator.GenerateApple();

        Assert.Equal(new Point(expectedX, expectedY), apple.Location);
    }
}
