using CCSV.Mines.GameApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Mines.RaylibApplications;

public class RaylibWindowBuilder : IGameWindowBuilder
{
    private string _title;
    private int _width;
    private int _height;

    public RaylibWindowBuilder()
    {
        _title = "Window title";
        _width = 800;
        _height = 480;
    }

    public IGameWindow Build()
    {
        return RaylibWindow.Create(_title, _width, _height);
    }

    public IGameWindowBuilder SetSize(int width, int height)
    {
        _width = width;
        _height = height;

        return this;
    }

    public IGameWindowBuilder SetTitle(string title)
    {
        _title = title;

        return this;
    }
}
