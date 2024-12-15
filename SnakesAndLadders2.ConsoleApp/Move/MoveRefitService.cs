using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Move;

public class MoveRefitService : IMoveApi
{
	private readonly IMoveApi _moveApi;

	public MoveRefitService()
	{
		_moveApi = RestService.For<IMoveApi>("https://localhost:7067");
	}

	public async Task<MoveResponseModel> MovePlayer(MoveModel requestModel)
	{
		return await _moveApi.MovePlayer(requestModel);
	}
}
