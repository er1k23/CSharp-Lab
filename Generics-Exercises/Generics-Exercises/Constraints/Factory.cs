namespace Generics_Exercises.Constraints;

public static class Factory
{
    public static T Create<T>() where T : IInitializable, new()
    {
        T instance = new T();
        instance.Initialize();
        return instance;
    }
}