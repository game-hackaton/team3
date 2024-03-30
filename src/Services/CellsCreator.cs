using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using thegame.Models;

namespace thegame.Services;

public static class CellsCreator
{
    private static Random random = new ((int)DateTime.Now.Ticks);
    
    public static CellDto CreateCellInRandomPlace(CellDto[] cells, int gameWidth, int gameHeight)
    {
        var id = int.Parse(cells[^1].Id) + 1;
        var allCells = new HashSet<Point>();
        for (var x = 0; x < gameWidth; x++)
        for (var y = 0; y < gameHeight; y++)
            allCells.Add(new Point(x, y));
        
        foreach (var cell in cells)
        {
            var point = new Point(cell.Pos.X, cell.Pos.Y);
            allCells.Remove(point);
        }

        var newPoint = GetRandomPoint(allCells);
        var value = random.Next(10);
        value = value <= 8 ? 4 : 2;
        return new CellDto($"{id}", new VectorDto { X = newPoint.X, Y = newPoint.Y }, $"tile-{value}", 
            $"{value}", value);
    }

    private static Point GetRandomPoint(HashSet<Point> points)
    {
        return points.ElementAt(random.Next(points.Count));
    }
}