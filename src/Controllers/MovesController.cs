using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games/{gameId}/moves")]
public class MovesController : Controller
{
    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
    {
        var game = GamesRepository.Games[gameId];
        if (userInput.KeyPressed == 37 && game.GameCells[^1].Pos.X - 1 >= 0)
            game.GameCells[^1].Pos.X -= 1;
        else if (userInput.KeyPressed == 39&& game.GameCells[^1].Pos.X + 1 < 4)
            game.GameCells[^1].Pos.X += 1;
        else if (userInput.KeyPressed == 38&& game.GameCells[^1].Pos.Y - 1 >= 0)
            game.GameCells[^1].Pos.Y -= 1;
        else if (userInput.KeyPressed == 40&& game.GameCells[^1].Pos.Y + 1 < 4)
            game.GameCells[^1].Pos.Y += 1;
        //else if (userInput.ClickedPos != null)
        //    game.Cells.First(c => c.Type == "color4").Pos = userInput.ClickedPos;
        return Ok(new GameDto(game.GameCells.Concat(GamesRepository.Fields[(game.Width, game.Height)]).ToArray(),
            true, true, game.Width, game.Height, game.Id, game.IsFinished, game.Score));
    }
}