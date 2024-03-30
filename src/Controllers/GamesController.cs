using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games")]
public class GamesController : Controller
{
    [HttpPost]
    public IActionResult Index()
    {
        var guid = Guid.NewGuid();
        var game = TestData.AGameDto(guid);
        
        GamesRepository.Games[guid] = new GameData(new []{game.Cells[^1]}, game.Width, game.Height,
            game.Id, game.IsFinished, game.Score);
        return Ok(game);
    }
}