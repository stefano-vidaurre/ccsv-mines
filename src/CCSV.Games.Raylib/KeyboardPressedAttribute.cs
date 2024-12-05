using Raylib_cs;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardPressedAttribute : GameEventAttribute
{
    private readonly string _name;
    public KeyboardKey Key { get; set; }

    public override string Name => _name;

    public KeyboardPressedAttribute(KeyboardKey key)
    {
        Key = key;
        _name = $"KeyboardPressed({key})";
    }

    public KeyboardPressedAttribute(KeyboardKey key, string name)
    {
        Key = key;
        _name = name;
    }

    public override bool HasHappened()
    {
        return RaylibService.IsKeyPressed(Key);
    }
}
