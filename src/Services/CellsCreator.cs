using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using thegame.Models;

namespace thegame.Services;

public static class CellsCreator
{
    private static Random random = new ((int)DateTime.Now.Ticks);
    
    public static bool TryCreateCellInRandomPlace(Dictionary<(int, int), CellDto> cells, 
        int gameWidth, int gameHeight, out CellDto resultCell)
    {
        var maxId = 100;
        var allCells = new HashSet<Point>();
        for (var x = 0; x < gameWidth; x++)
        for (var y = 0; y < gameHeight; y++)
            allCells.Add(new Point(x, y));
        
        foreach (var cell in cells)
        {
            var point = new Point(cell.Key.Item1, cell.Key.Item2);
            if (!allCells.Contains(point)) continue;
            if (int.TryParse(cell.Value.Id, out var result) && result > maxId)
                maxId = result;
            allCells.Remove(point);
        }

        if (allCells.Count == 0)
        {
            resultCell = null;
            return false;
        }
        var newPoint = allCells.ElementAt(random.Next(allCells.Count));
        var value = random.Next(10);
        value = value >= 8 ? 4 : 2;
        resultCell = new CellDto($"{maxId + 1}", new VectorDto { X = newPoint.X, Y = newPoint.Y }, $"tile-{value}",
            $"{value}", value);
        return true;
    }

    public static CellDto CreateCellWithTile(int tile, int id)
    {
        return new CellDto($"{id}", new VectorDto(), $"tile-{tile}", $"{tile}", tile);
    }
}