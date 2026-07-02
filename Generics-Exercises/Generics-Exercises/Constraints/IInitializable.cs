namespace Generics_Exercises.Constraints;

public interface IInitializable
{
    bool IsInitialized { get; }
    void Initialize();
}