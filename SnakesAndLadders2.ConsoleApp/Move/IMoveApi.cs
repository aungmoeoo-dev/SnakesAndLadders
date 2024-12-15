using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Move;

public interface IMoveApi
{
	[Post("/api/move")]
	public Task<MoveResponseModel> MovePlayer(MoveModel requestModel);
}
