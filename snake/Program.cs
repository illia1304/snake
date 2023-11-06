using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        string filePath = "\\gameState.json"; ;
        // if no saved games - create new
        var snakeGame = File.Exists(filePath) ? SnakeGame.LoadGame(filePath) : new SnakeGame();
        
        snakeGame.Run();

        snakeGame.SaveGame(filePath);
    }
}




