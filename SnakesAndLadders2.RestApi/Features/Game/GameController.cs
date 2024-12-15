using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders2.Shared.Board;
using SnakesAndLadders2.Shared.Game;
using SnakesAndLadders2.Shared.Player;

namespace SnakesAndLadders2.RestApi.Features.Game;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
	private readonly GameService _gameService;

	public GameController()
	{
		_gameService = new GameService();
	}

	[HttpPost]
	public IActionResult CreateBoard([FromBody] List<PlayerModel> requestModel)
	{
		GameResponseModel responseModel = new();
		try
		{
			responseModel = _gameService.CreateGame(requestModel);

			if (!responseModel.IsSuccess) return BadRequest(responseModel);

			return Ok(responseModel);
		}
		catch (Exception ex)
		{
			responseModel.Message = ex.Message;
			return StatusCode(500, responseModel);
		}
	}
}
