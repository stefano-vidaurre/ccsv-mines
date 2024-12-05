using Raylib_cs;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardDownAttribute : GameEventAttribute
{
    private readonly string _name;
    public KeyboardKey Key { get; set; }

    public override string Name => _name;

    public KeyboardDownAttribute(KeyboardKey key)
    {
        Key = key;
        _name = $"KeyboardDown({key})";
    }

    public KeyboardDownAttribute(KeyboardKey key, string name)
    {
        Key = key;
        _name = name;
    }

    public override bool HasHappened()
    {
        return RaylibService.IsKeyDown(Key);
    }
}
