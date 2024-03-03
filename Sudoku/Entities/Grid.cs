namespace Sudoku.Domain.Entities;
public class Grid
{
    private Cell[,] _cells = new Cell[9, 9];

    public Grid(Cell[,] cells)
    {
        if (cells.GetLength(0) != 8 || cells.GetLength(1) != 8)
        {
            throw new ArgumentException("Invalid array dimensions. The array must be 9x9");
        }

        _cells = cells;
    }

    public Grid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _cells[i, j] = new Cell();
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x > 8 || y < 0 || y > 8)
        {
            throw new ArgumentException("Invalid cell coordinates. Coords must be between 0,0 and 8,8");
        }
        else
        {
            return _cells[x, y];
        }
    }
}
