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
        var emptyCells = new List<int[]>();

        // Initialize empty cells with coords of every empty cell
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                emptyCells.Add([x, y]);
            }
        }

        // Initial seeding
        // Seed nine cells with 1 to 9 randomly
        for (int i = 1; i < 10; i++)
        {
            // Get random cell coords from emptyCells
            var ran = _random.Next(emptyCells.Count-1);
            var coords = emptyCells[ran];

            // Get the cell from the grid and initialize digit
            var cell = grid.GetCell(coords[0], coords[1]);
            cell.Digit = i;

            // Remove populated cell from emptyCells
            emptyCells.RemoveAt(ran);
        }

        // Fill the rest of the board
        for (int cellsLeft = emptyCells.Count-1; cellsLeft >= 0; cellsLeft--)
        {
            // Get random cell coords from emptyCells
            var ran = _random.Next(cellsLeft);
            var coords = emptyCells[ran];

            // Attempt to fill the cell
            for (int digit = 0; digit < 9; digit++)
            {
                if (IsUnusedInBox(coords[0], coords[1], digit, grid)
                    && IsUnusedInColumn(coords[1], digit, grid)
                    && IsUnusedInRow(coords[0], digit, grid))
                {
                    var cell = grid.GetCell(coords[0], coords[1]);
                    cell.Digit = digit;
                    emptyCells.RemoveAt(ran);
                }
            }
        }

        return grid;
    }

    public Grid GenerateStartState(Grid solution)
    {
        throw new NotImplementedException();
    }

    private bool Solve(Grid grid)
    {
        for (int x = 0; x < 9;  x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (grid.GetCell(x, y).Digit is null)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (IsValid(x, y, i, grid))
                        {
                            var cell = grid.GetCell(x, y);
                            cell.Digit = i;

                        }
                    }
                }
            }
        }
    }

    private bool IsUnusedInRow(int x, int digit, Grid grid)
    {
        for (int y = 0; y < 9; y++)
        {
            var cell = grid.GetCell(x, y);
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
            var cell = grid.GetCell(x, y);
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
                var cell = grid.GetCell(boxXStart + i, boxYStart + j);
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
