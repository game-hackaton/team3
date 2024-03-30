using System;
using System.Collections.Generic;

namespace thegame.Models;

public class GameData
{
    public GameData(CellDto[] gameCells, int width, int height, Guid id, bool isFinished, int score)
    {
        GameCells = new Dictionary<(int x, int y), CellDto>();
        foreach (var cell in gameCells)
        {
            GameCells[(cell.Pos.X, cell.Pos.Y)] = cell;
        }
        Width = width;
        Height = height;
        Id = id;
        IsFinished = isFinished;
        Score = score;
    }
    
    public Dictionary<(int x, int y), CellDto> GameCells { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid Id { get; set; }
    public bool IsFinished { get; set; }
    public int Score { get; set; }
}