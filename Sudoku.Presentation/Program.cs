using Sudoku.Application.Services;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Interfaces;

namespace Sudoku.Presentation;

internal class Program
{
    static void Main(string[] args)
    {
        ISudokuService service = new SudokuService();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var grid = service.GenerateSolution();
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
        PrintBoard(grid);
    }

    private static void PrintBoard(Grid grid)
    {
        PrintGridLine();
        var CellRowArray = new Square[9];
        for (int i = 0; i < 9; i++)
        {
            for (int column = 0; column <9; column++)
            {
                CellRowArray[column] = grid.GetSquare(i, column);
            }

            PrintNumbersLine(CellRowArray);
            if ((i + 1) % 3 == 0)
            {
                PrintGridLine();
            }
        }
    }

    private static void PrintGridLine()
    {
        Console.Write("+");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("---------");
            Console.Write("+");
        }

        Console.WriteLine();
    }

    private static void PrintNumbersLine(Square[] CellRowArray)
    {
        Console.Write("|");
        for (int y = 0; y < 9; y++)
        {
            Console.Write(" ");
            var digit = CellRowArray[y].Digit;
            if (digit != null)
            {
                Console.Write(digit);
            }
            else
            {
                Console.Write(" ");
            }
            Console.Write(" ");
            if ((y + 1) % 3 == 0)
            {
                Console.Write("|");
            }
        }
        Console.WriteLine();
    }
}
