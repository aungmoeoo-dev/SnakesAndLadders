using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakesAndLadders2.Shared.Board;

namespace SnakesAndLadders2.RestApi.Features.GameBoard;

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
	public IActionResult CreateBoard([FromBody] List<BoardModel> requestModel)
	{
		BoardResponseModel responseModel = new();
		try
		{
			responseModel = _boardService.CreateBoard(requestModel);

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
