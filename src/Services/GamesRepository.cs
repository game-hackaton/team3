using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Services;

public class GamesRepository
{
    public static readonly Dictionary<Guid, GameDto> games = new();
}