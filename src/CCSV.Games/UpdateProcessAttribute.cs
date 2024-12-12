namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Method)]
public class UpdateProcessAttribute : GameEventAttribute
{
    private readonly string _name;

    public override string Name => _name;

    public UpdateProcessAttribute()
    {
        _name = "TickProcess";
    }

    public UpdateProcessAttribute(string name)
    {
        _name = name;
    }

    public override bool HasHappened()
    {
        return true;
    }
}
