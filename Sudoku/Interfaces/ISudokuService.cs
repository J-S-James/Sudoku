using Sudoku.Domain.Entities;

namespace Sudoku.Domain.Interfaces;
public interface ISudokuService
{
    public Grid GenerateSolution();
    public Grid GenerateStartState(Grid solution);
}
