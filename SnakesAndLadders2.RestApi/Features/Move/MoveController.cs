using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders2.Shared.Move;

namespace SnakesAndLadders2.RestApi.Features.Move;

[Route("api/[controller]")]
[ApiController]
public class MoveController : ControllerBase
{
	private readonly MoveService _moveService;

	public MoveController()
	{
		_moveService = new MoveService();
	}

	[HttpPost]
	public IActionResult CreateBoard([FromBody] MoveModel requestModel)
	{
		MoveResponseModel responseModel = new();
		try
		{
			responseModel = _moveService.MovePlayer(requestModel);

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
