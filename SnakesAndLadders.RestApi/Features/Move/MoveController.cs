using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders.Shared.Move;

namespace SnakesAndLadders.RestApi.Features.Move;

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
	public IActionResult Move([FromBody] MoveModel requestModel)
	{
		MoveResponseModel responseModel = new();

		try
		{
			responseModel = _moveService.Move(requestModel);
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
