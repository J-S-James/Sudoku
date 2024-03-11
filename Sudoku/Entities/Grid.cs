namespace Sudoku.Domain.Entities;
public class Grid
{
    private Square[,] _squares = new Square[9, 9];

    public Grid(Square[,] squares)
    {
        if (squares.GetLength(0) != 8 || squares.GetLength(1) != 8)
        {
            throw new ArgumentException("Invalid array dimensions. The array must be 9x9");
        }

        _squares = squares;
    }

    public Grid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                _squares[i, j] = new Square();
            }
        }
    }

    public Square GetSquare(int x, int y)
    {
        if (x < 0 || x > 8 || y < 0 || y > 8)
        {
            throw new ArgumentException("Invalid cell coordinates. Coords must be between 0,0 and 8,8");
        }
        else
        {
            return _squares[x, y];
        }
    }
}
