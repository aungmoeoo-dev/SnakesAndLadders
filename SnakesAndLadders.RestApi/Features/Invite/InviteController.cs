using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders.Shared.Game;
using SnakesAndLadders.Shared.Invite;

namespace SnakesAndLadders.RestApi.Features.Invite;

[Route("api/[controller]")]
[ApiController]
public class InviteController : ControllerBase
{
	private readonly InviteService _inviteService;

	public InviteController()
	{
		_inviteService = new InviteService();
	}

	[HttpPost]
	public IActionResult InvitePlayer([FromBody] InviteModel requestModel)
	{
		InviteResponseModel responseModel = new();

		try
		{
			responseModel = _inviteService.InvitePlayer(requestModel);

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
