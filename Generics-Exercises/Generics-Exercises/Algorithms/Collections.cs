namespace Generics_Exercises.Algorithms;

public static class Collections
{
    public static T[] Filter<T>(T[] source, Predicate<T> condition)
    {
        int count = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (condition(source[i]))
                count++;
        }

        T[] result = new T[count];
        int index = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (condition(source[i]))
            {
                result[index] = source[i];
                index++;
            }
        }

        return result;
    }

    public static TOut[] Project<TIn, TOut>(TIn[] source, Func<TIn, TOut> transform)
    {
        TOut[] result = new TOut[source.Length];

        for (int i = 0; i < source.Length; i++)
        {
            result[i] = transform(source[i]);
        }

        return result;
    }
}