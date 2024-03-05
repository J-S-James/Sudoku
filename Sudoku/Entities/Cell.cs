namespace Sudoku.Domain.Entities;
public class Cell
{
    private int? _digit;
    private List<int>? _pencilMarks;

    public Cell(int? digit = null)
    {
        _digit = digit;
    }

    public int? Digit
    {
        get => _digit;
        set
        {
            if (value > 0 && value <= 9 || value is null)
            {
                _digit = value;
            }
        }
    }

    public List<int> PencilMarks
    {
        get => _pencilMarks ??= new List<int>();
    }
}
