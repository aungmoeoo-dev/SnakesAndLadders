using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders.Shared.Player;

namespace SnakesAndLadders.RestApi.Features.Player;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
	private PlayerService _playerService;

	public PlayerController()
	{
		_playerService = new PlayerService();
	}

	[HttpPost]
	public IActionResult RegisterPlayer([FromBody] PlayerModel requestModel)
	{
		PlayerResponseModel responseModel = new();
		try
		{
			responseModel = _playerService.RegisterPlayer(requestModel);

			if (!responseModel.IsSuccess) return BadRequest(responseModel);

			return Ok(responseModel);
		}
		catch (Exception ex)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = ex.Message;
			return StatusCode(500, responseModel);
		}
	}
}
