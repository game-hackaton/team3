using System;

namespace thegame.Models;

public class GameData
{
    public GameData(CellDto[] gameCells, int width, int height, Guid id, bool isFinished, int score)
    {
        GameCells = gameCells;
        Width = width;
        Height = height;
        Id = id;
        IsFinished = isFinished;
        Score = score;
    }
    
    public CellDto[] GameCells { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Guid Id { get; set; }
    public bool IsFinished { get; set; }
    public int Score { get; set; }
}