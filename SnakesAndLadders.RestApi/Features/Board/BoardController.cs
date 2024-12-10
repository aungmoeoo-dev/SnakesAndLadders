using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders.Shared.Board;
using SnakesAndLadders.Shared.Game;

namespace SnakesAndLadders.RestApi.Features.Board;

[Route("api/[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
	private readonly BoardService _boardService;

	public BoardController()
	{
		_boardService = new BoardService();
	}

	[HttpPost]
	public IActionResult CreateBoard([FromBody] BoardCreateRequestModel requestModel)
	{
		BoardResponseModel responseModel = new();

		try
		{
			responseModel = _boardService.CreateBoard(requestModel);

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
