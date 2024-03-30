using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public class GamesRepository
{
    public static readonly Dictionary<Guid, GameData> Games = new();
    public static readonly Dictionary<(int, int), CellDto[]> Fields= new ();
}