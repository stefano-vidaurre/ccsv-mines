namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Method)]
public abstract class GameEventAttribute : Attribute
{
    public abstract bool HasHappened();
}
