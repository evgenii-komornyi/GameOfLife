using GameOfLifeEngine;

Console.CursorVisible = false;

GameEngine gameEngine = new GameEngine(20, 20, 2);

while (gameEngine.CountAliveCells() > 0)
{
    var currentGeneration = gameEngine.GetCurrentGeneration();

    for (int y = 0; y < currentGeneration.GetLength(1); y++)
    {
        var str = new char[currentGeneration.GetLength(0)];

        for (int x = 0; x < currentGeneration.GetLength(0); x++)
        {
            if (currentGeneration[x, y])
            {
                str[x] = '#';
            }
            else
            {
                str[x] = ' ';
            }
        }
        Console.WriteLine(str);
    }
    Console.SetCursorPosition(0, 0);
    gameEngine.NextGeneration();
}
Thread.Sleep(1000);
