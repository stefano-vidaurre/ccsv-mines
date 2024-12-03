using Raylib_cs;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;

[AttributeUsage(AttributeTargets.Method)]
public class KeyboardDownAttribute : GameEventAttribute
{
    public KeyboardKey Key { get; set; }

    public KeyboardDownAttribute(KeyboardKey key)
    {
        Key = key;
    }

    public override bool HasHappened()
    {
        return RaylibService.IsKeyDown(Key);
    }
}
