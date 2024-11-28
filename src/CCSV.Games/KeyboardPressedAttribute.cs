namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardPressedAttribute : GameEventAttribute
{
    public int Key { get; set; }

    public KeyboardPressedAttribute(int key)
    {
        Key = key;
    }
}
