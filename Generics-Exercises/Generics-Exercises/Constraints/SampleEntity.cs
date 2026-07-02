namespace Generics_Exercises.Constraints;

public class SampleEntity : IInitializable
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;
    }
}