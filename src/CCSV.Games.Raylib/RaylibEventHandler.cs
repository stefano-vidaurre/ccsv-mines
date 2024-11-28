using Raylib_cs;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;
public class RaylibEventHandler : GameEventHandler
{
    public RaylibEventHandler(IGameWindow window, IGameControllerProvider controllers) : base(window, controllers)
    {
    }

    protected override bool HasToBeInvoked(GameEventAttribute attribute)
    {
        if (attribute is KeyboardPressedAttribute keyboardPressedAttribute)
        {
            return RaylibService.IsKeyPressed((KeyboardKey)keyboardPressedAttribute.Key);
        }

        if (attribute is KeyboardDownAttribute keyboardDownAttribute)
        {
            return RaylibService.IsKeyDown((KeyboardKey)keyboardDownAttribute.Key);
        }

        return false;
    }
}
