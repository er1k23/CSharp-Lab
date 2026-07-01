namespace Generics_Exercises.Models;

public class Pair<T1, T2>
{
    public T1 First { get; }
    public T2 Second { get; }

    public Pair(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }

    public Pair<T2, T1> SwapSides()
    {
        return new Pair<T2, T1>(Second, First);
    }

    public override string ToString()
    {
        return $"({First}, {Second})";
    }
}