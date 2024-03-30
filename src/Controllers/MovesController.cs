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

        var field = GetMovedCells(gameId, userInput);

        var prevCells = game.GameCells;
        game.GameCells = field.ToDictionary(x => (x.Pos.X, x.Pos.Y), y => y);
        if (!prevCells.SequenceEqual(game.GameCells)){
            if (!CellsCreator.TryCreateCellInRandomPlace(game.GameCells, game.Width, game
                    .Height, out var
                    newCell))
            {
                var finishedGame = new GameDto(game);
                finishedGame.IsFinished = true;
                return Ok(finishedGame);
            }

            game.GameCells[(newCell.Pos.X, newCell.Pos.Y)] = newCell;
            game.Score = game.GameCells.Max(v => int.Parse(v.Value.Content));
        }
        return Ok(new GameDto(game));
    }

    public static CellDto[] GetMovedCells(Guid gameId, [FromBody] UserInputDto userInput)
    {
        var game = GamesRepository.Games[gameId];

        if (userInput.KeyPressed == 37)
        {
            return CellMover.MoveLeft(gameId);
        }
        else if (userInput.KeyPressed == 39)
        {
            return CellMover.MoveRight(gameId);
        }
        else if (userInput.KeyPressed == 38)
        {
            return CellMover.MoveDown(gameId);
        }
        else if (userInput.KeyPressed == 40)
        {
            return CellMover.MoveUp(gameId);
        }
        return GamesRepository.Games[gameId].GameCells.Values.ToArray();
    }
}