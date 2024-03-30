using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers;

[Route("api/games/{gameId}/moves")]
public class MovesController : Controller
{
    public static VectorDto vector = new VectorDto { X = 1, Y = 1 };
    public static GameDto game = TestData.AGameDto(vector);

    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
    {
        
        var game = GamesRepository.games[gameId];
        while (true)
        {
            if (userInput.KeyPressed == 37 && game.Cells[^1].Pos.X - 1 >= 0)
            {
                game.Cells[^1].Pos.X -= 1;
            }
            else if (userInput.KeyPressed == 39 && game.Cells[^1].Pos.X + 1 < 4)
            {
                game.Cells[^1].Pos.X += 1;
            }
            else if (userInput.KeyPressed == 38 && game.Cells[^1].Pos.Y - 1 >= 0)
            {
                game.Cells[^1].Pos.Y -= 1;
            }
            else if (userInput.KeyPressed == 40 && game.Cells[^1].Pos.Y + 1 < 4)
            {
                game.Cells[^1].Pos.Y += 1;
            }
            else
            {
                break;
            }
        }

        //else if (userInput.ClickedPos != null)
        //    game.Cells.First(c => c.Type == "color4").Pos = userInput.ClickedPos;
        return Ok(game);
    }
}