namespace Sudoku.Domain.Entities;
public struct Cell
{
    private int? _digit;

    public Cell(int? digit = null)
    {
        _digit = digit;
    }

    public int? Digit
    {
        get => _digit;
        set
        {
            if (value > 0 && value < 9)
            {
                _digit = value;
            }
        }
    }

    public void RemoveDigit()
    {
        _digit = null;
    }
}
