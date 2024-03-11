using Sudoku.Domain.Entities;
using Sudoku.Domain.Interfaces;

namespace Sudoku.Application.Services;
public class SudokuService : ISudokuService
{
    private Random _random;

    public SudokuService()
    {
        _random = new Random();
    }

    public Grid GenerateSolution()
    {
        var grid = new Grid();

        // Initial seeding
        // Seed nine cells with 1 to 9 randomly
        for (int i = 1; i <=9; i++)
        {
            Square cell;
            do
            {
                var x = _random.Next(8);
                var y = _random.Next(8);
                cell = grid.GetSquare(x, y);
            }
            while (cell.Digit is not null);

            cell.Digit = i;
        }

        Solve(grid);

        return grid;
    }

    public Grid GenerateStartState(Grid solution)
    {
        throw new NotImplementedException();
    }

    private bool HumanlikeSolve(Grid grid)
    {
        var possibleArray = new List<int>[9, 9];

        for (int x = 0; x < 9; x++) 
        { 
            for (int y = 0; y < 9; y++) 
            { 
                if (grid.GetSquare(x, y).Digit is null)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (IsValid(x, y, i, grid))
                        {
                            possibleArray[x,y].Add(i);
                        }
                    }
                }
            }
        }

        return false;
    }


    // Backtracking algorithm to solve a Sudoku grid
    private bool Solve(Grid grid)
    {
        for (int x = 0; x < 9;  x++)
        {
            for (int y = 0; y < 9; y++)
            {
                var cell = grid.GetSquare(x, y);
                if (cell.Digit is null)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (IsValid(x, y, i, grid))
                        {
                            cell.Digit = i;

                            if (Solve(grid))
                            {
                                return true;
                            }
                            else
                            {
                                cell.Digit = null;
                            }
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsUnusedInRow(int x, int digit, Grid grid)
    {
        for (int y = 0; y < 9; y++)
        {
            var cell = grid.GetSquare(x, y);
            if (cell.Digit == digit)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsUnusedInColumn(int y, int digit, Grid grid)
    {
        for (int x = 0; x < 9; x++)
        {
            var cell = grid.GetSquare(x, y);
            if (cell.Digit == digit)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsUnusedInBox(int x, int y, int digit, Grid grid)
    {
        // Calculate the starting x and y point of the box
        var boxXStart = x - (x % 3);
        var boxYStart = y - (y % 3);


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var cell = grid.GetSquare(boxXStart + i, boxYStart + j);
                if (cell.Digit == digit)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool IsValid(int x, int y, int digit, Grid grid)
    {
        if (IsUnusedInBox(x, y, digit, grid)
                    && IsUnusedInColumn(y, digit, grid)
                    && IsUnusedInRow(x, digit, grid))
        {
            return true;
        }

        return false;
    }
}
