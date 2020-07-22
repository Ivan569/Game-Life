using System;
using System.Threading;

class GameLife
{
    static void Main()
    {
        int size = 50;
        string[,] currentGeneration = CreateMap(size);

        string[,] newGeneration = new string[size, size];              

        while(true)
        {
            #region Расчитывание следующего поколения.
            for (int i = 0; i < currentGeneration.GetLength(0); i++)
            {
                for (int j = 0; j < currentGeneration.GetLength(1); j++)
                {
                    int k = 0;
                    //  +  |  +  |     |  - | -  | - 
                    //  -  |   - |  +- | +  | +  |  +  ...
                    if (i - 1 != -1 && currentGeneration[i - 1, j] == "*")
                        k += 1;
                    
                    if (i - 1 != -1 && j - 1 != -1 && currentGeneration[i - 1, j - 1] == "*")
                        k += 1;
                    
                    if (j - 1 != -1 && currentGeneration[i, j - 1] == "*")
                        k += 1;

                    if (j - 1 != -1 && i + 1 != size && currentGeneration[i + 1, j - 1] == "*")
                        k += 1;
                    
                    if (i + 1 != size && currentGeneration[i + 1, j] == "*")
                        k += 1;

                    if (i + 1 != size && j + 1 != size && currentGeneration[i + 1, j + 1] == "*")
                        k += 1;
                    if (j + 1 != size && currentGeneration[i, j + 1] == "*")
                        k += 1;
                    
                    if (j + 1 != size && i - 1 != -1 && currentGeneration[i - 1, j + 1] == "*")
                        k += 1;

                    newGeneration[i, j] = (currentGeneration[i, j]) switch
                    {
                        "*" when k != 2 && k != 3 => " ",
                        " " when k == 3 => "*",
                        _ => currentGeneration[i, j],
                    };
                }
            }

            Thread.Sleep(100);
            Console.Clear();

            ShowMapAndStatistics(newGeneration);

            currentGeneration = newGeneration;
            newGeneration = new string[size, size];
            #endregion
        }
    }

    private static int NumberLive = 0, NumberDad = 0;

    private static void ShowMapAndStatistics(string[,] newGeneration)
    {
        #region Отображение статистики.
        Console.Write("Количество живых: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(NumberLive + " ");
        Console.ResetColor();
        Console.Write("Количество мертвых: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(NumberDad + " ");
        NumberLive = 0;
        NumberDad = 0;
        Console.ResetColor();
        #endregion
        for (int i = 0; i < newGeneration.GetLength(0); i++)
        {            
            for (int j = 0; j < newGeneration.GetLength(1); j++)
            {
                if (newGeneration[i, j] == "*")
                    NumberLive += 1;
                else
                    NumberDad += 1;
                Console.Write(newGeneration[i, j]);               
            }
          
            Console.WriteLine();
        }
    }

    private static string[,] CreateMap(int size)
    {
        string[,] currentGeneration = new string[size, size];

        for (int i = 0; i < currentGeneration.GetLength(0); i++)
            for (int j = 0; j < currentGeneration.GetLength(1); j++)
                currentGeneration[i, j] = " ";

        (int, int)[] lifes =
        {
            (14, 6), (14, 7), (15, 6), (15, 7),
            (14, 15), (14, 16), (15, 14), (15, 16),
            (16, 14), (16, 15), (16, 22), (16, 23),
            (17, 22), (17, 24), (18, 22), (14, 28),
            (14, 29), (13, 28), (12, 29), (12, 30),
            (13, 30), (13, 40), (13, 41), (12, 41),
            (12, 40), (19, 41), (19, 42), (20, 41),
            (20, 43), (21, 41), (24, 30), (24, 31),
            (24, 32), (25, 30), (26, 31)
        };

        foreach (var (x, y) in lifes)
            currentGeneration[x, y] = "*";      

        return currentGeneration;
    }
}
