namespace TicTacToe.Game;

public class Board
{
    public const int Size = 3;

    private readonly Cell[] _cells;


    public Board()
    {
        _cells = new Cell[Size * Size];

        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                _cells[row * Size + col] = new(row, col);
            }
        }
    }

    public Cell GetCell(int row, int col)
    {
        return _cells[row * Size + col];
    }

    public bool TryPlaceMark(int row, int col, Mark mark)
    {
        var cell = GetCell(row, col);

        if (!cell.IsEmpty())
        {
            return false;
        }
        
        cell.SetMark(mark);
        return true;
    }

    public bool IsFull()
    {
        foreach (var cell in _cells)
        {
            if (cell.IsEmpty())
            {
                return false;
            }
        }

        return true;
    }
    
}