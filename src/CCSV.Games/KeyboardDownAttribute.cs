namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardDownAttribute : GameEventAttribute
{
    public int Key { get; set; }

    public KeyboardDownAttribute(int key)
    {
        Key = key;
    }
}
