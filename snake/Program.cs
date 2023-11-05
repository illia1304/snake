using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        string filePath = "D:\\Code\\snake\\snake\\gameState.json";
        // if no saved games - create new
        var snakeGame = File.Exists(filePath) ? SnakeGame.LoadGame("D:\\Code\\snake\\snake\\gameState.json") : new SnakeGame();
        
        snakeGame.Run();

        snakeGame.SaveGame("D:\\Code\\snake\\snake\\gameState.json");
    }
}




