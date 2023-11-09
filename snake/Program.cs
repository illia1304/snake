using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

class Program
{
    static void Main()
    {
        // Викликаймо тести перед створенням гри
        RunTests();
        Console.ReadKey();
        string filePath = "\\gameState.json"; ;
        // if no saved games - create new
        var snakeGame = File.Exists(filePath) ? SnakeGame.LoadGame(filePath) : new SnakeGame();

        snakeGame.Run();

        snakeGame.SaveGame(filePath);
    }

    static void RunTests()
    {
        Console.WriteLine("Running Tests...");

        var testClass = typeof(SnakeGameTests);
        var testMethods = testClass.GetMethods().Where(m => m.GetCustomAttributes(typeof(FactAttribute), true).Any());

        foreach (var method in testMethods)
        {
            try
            {
                method.Invoke(null, null);
                Console.WriteLine($"Test {method.Name} passed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test {method.Name} failed: {ex.Message}");
            }
        }

        Console.WriteLine("Tests completed.");
    }
}
