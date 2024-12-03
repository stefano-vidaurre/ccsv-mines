using Raylib_cs;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardPressedAttribute : GameEventAttribute
{
    public KeyboardKey Key { get; set; }

    public KeyboardPressedAttribute(KeyboardKey key)
    {
        Key = key;
    }

    public override bool HasHappened()
    {
        return RaylibService.IsKeyPressed(Key);
    }
}
