using System;
using System.Linq;
using thegame.Services;

namespace thegame.Models;

public class GameDto
{
    public GameDto(GameData data)
    {
        Cells = GamesRepository.Fields[(data.Width, data.Height)].Concat(data.GameCells.Values).ToArray();
        MonitorKeyboard = true;
        MonitorMouseClicks = true;
        Width = data.Width;
        Height = data.Height;
        Id = data.Id;
        IsFinished = data.IsFinished;
        Score = data.Score;
    }
    
    public GameDto(CellDto[] cells, bool monitorKeyboard, bool monitorMouseClicks, int width, int height, Guid id, bool isFinished, int score)
    {
        Cells = cells;
        MonitorKeyboard = monitorKeyboard;
        MonitorMouseClicks = monitorMouseClicks;
        Width = width;
        Height = height;
        Id = id;
        IsFinished = isFinished;
        Score = score;
    }

    public CellDto[] Cells { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool MonitorKeyboard { get; set; }
    public bool MonitorMouseClicks { get; set; }
    public Guid Id { get; set; }
    public bool IsFinished { get; set; }
    public int Score { get; set; }
}