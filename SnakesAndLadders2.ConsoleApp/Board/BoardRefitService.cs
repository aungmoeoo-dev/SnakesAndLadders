using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Board;

public class BoardRefitService : IBoardApi
{
	private readonly IBoardApi _boardApi;

	public BoardRefitService()
	{
		_boardApi = RestService.For<IBoardApi>("https://localhost:7067");
	}

	public async Task<BoardResponseModel> CreateBoard(List<BoardModel> requestModel)
	{
		return await _boardApi.CreateBoard(requestModel);
	}
}
