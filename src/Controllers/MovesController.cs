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
        while (true)
        {
            if (userInput.KeyPressed == 37 && game.GameCells.FirstOrDefault().Value.Pos.X - 1 >= 0)
            {
                game.GameCells.FirstOrDefault().Value.Pos.X -= 1;
            }
            else if (userInput.KeyPressed == 39 && game.GameCells.FirstOrDefault().Value.Pos.X + 1 < 4)
            {
                game.GameCells.FirstOrDefault().Value.Pos.X += 1;
            }
            else if (userInput.KeyPressed == 38 && game.GameCells.FirstOrDefault().Value.Pos.Y - 1 >= 0)
            {
                game.GameCells.FirstOrDefault().Value.Pos.Y -= 1;
            }
            else if (userInput.KeyPressed == 40 && game.GameCells.FirstOrDefault().Value.Pos.Y + 1 < 4)
            {
                game.GameCells.FirstOrDefault().Value.Pos.Y += 1;
            }
            else
            {
                break;
            }
        }

        if (!CellsCreator.TryCreateCellInRandomPlace(game.GameCells, game.Width, game.Height, out var newCell))
        {
            var finishedGame = new GameDto(game);
            finishedGame.IsFinished = true;
            return Ok(finishedGame);
        }
        game.GameCells[(newCell.Pos.X, newCell.Pos.Y)] = newCell;
        game.Score = game.GameCells.Max(v => int.Parse(v.Value.Content));
        return Ok(new GameDto(game));
    }
}