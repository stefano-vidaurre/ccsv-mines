namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Class)]
public class GameViewAttribute : Attribute
{
    public Type ViewType { get; private set; }

    public GameViewAttribute(Type viewType)
    {
        ViewType = viewType;
    }
}
