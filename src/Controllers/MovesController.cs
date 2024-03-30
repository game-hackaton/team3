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
    public GameDto game = TestData.AGameDto(vector);

    [HttpPost]
    public IActionResult Moves(Guid gameId, [FromBody]UserInputDto userInput)
    {
        if (userInput.KeyPressed == 37)
            game.Cells.LastOrDefault().Pos.X -= 1;
        else if (userInput.KeyPressed == 39)
            game.Cells.LastOrDefault().Pos.X += 1;
        else if (userInput.KeyPressed == 38)
            game.Cells.LastOrDefault().Pos.Y -= 1;
        else if (userInput.KeyPressed == 40)
            game.Cells.LastOrDefault().Pos.Y += 1;
        //else if (userInput.ClickedPos != null)
        //    game.Cells.First(c => c.Type == "color4").Pos = userInput.ClickedPos;
        return Ok(game);
    }
}