using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Board;

public interface IBoardApi
{
	[Post("/api/board")]
	public Task<BoardResponseModel> CreateBoard(List<BoardModel> requestModel);
}
