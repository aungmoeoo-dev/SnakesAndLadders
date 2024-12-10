using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders.Shared.Game;

namespace SnakesAndLadders.RestApi.Features.Game;

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
	public IActionResult CreateGame([FromBody] GameCreateRequestModel requestModel)
	{
		GameResponseModel responseModel = new();

		try
		{
			responseModel = _gameService.CreateGame(requestModel);

			if (!responseModel.IsSuccess) return BadRequest(responseModel);

			return Created("", responseModel);
		}
		catch (Exception ex)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = ex.Message;
			return StatusCode(500, responseModel);
		}
	}
}
