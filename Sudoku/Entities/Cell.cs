namespace Sudoku.Domain.Entities;
public class Cell
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
            _digit = value;
            /**
            if (value > 0 && value <= 9)
            {
                _digit = value;
            }
            **/
        }
    }

    public void RemoveDigit()
    {
        _digit = null;
    }
}
