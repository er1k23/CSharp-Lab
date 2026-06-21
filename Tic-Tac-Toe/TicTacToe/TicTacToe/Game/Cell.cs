namespace TicTacToe.Game;

public class Cell
{
    public int Row { get;  }
    public int Column { get;  }
    public Mark Mark { get; private set;  }

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
        Mark = Mark.Empty;
    }

    public bool IsEmpty()
    {
        return Mark == Mark.Empty;
    }

    public void SetMark(Mark mark)
    {
        Mark = mark;
    }
}