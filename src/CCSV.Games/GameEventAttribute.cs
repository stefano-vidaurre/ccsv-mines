namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Method)]
public abstract class GameEventAttribute : Attribute
{
    public abstract string Name { get; }
    public abstract bool HasHappened();
}
