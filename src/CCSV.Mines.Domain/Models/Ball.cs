using CCSV.Domain.Entities;

namespace CCSV.Mines.Domain.Models;

public class Ball : Entity
{
    public static Ball Default => new Ball();

    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public int Radius { get; private set; }

    private Ball() : base(Guid.Empty)
    {
        PosX = 0;
        PosY = 0;
        Radius = 0;
    }

    private Ball(Guid id, int posX, int posY) : base(id)
    {
        PosX = posX;
        PosY = posY;
        Radius = 10;
    }

    public static Ball Create(Guid id, int posX, int posY)
    {
        return new Ball(id, posX, posY);
    }

    public void MoveX(int x)
    {
        PosX += x;
    }

    public void MoveY(int y)
    {
        PosY += y;
    }
}
