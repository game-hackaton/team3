using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace thegame.Services;

public class TestData
{
    private static Dictionary<(int, int), CellDto> CreateField(int height, int width)
    {
        var cellsDict = new Dictionary<(int, int), CellDto>();
        var counter = 0;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                cellsDict[(x, y)] = new CellDto($"{counter}", new VectorDto { X = x, Y = y }, "tile-2", "", -1);
                counter++;
            }
        }

        GamesRepository.Fields[(width, height)] = cellsDict.Values.ToArray();
        
        return cellsDict;
    }
    public static GameDto AGameDto(Guid gameId)
    {
        var width = 4;
        var height = 4;
        var cellsDict = CreateField(height, width);
        CellsCreator.TryCreateCellInRandomPlace(new Dictionary<(int, int), CellDto>(), width, height, out var firstCell);
        var testCells = cellsDict.Values.Concat(new [] {firstCell}).ToArray();
        return new GameDto(testCells, true, true, width, height, gameId, false, 0);
    }
}