using Refit;
using SnakesAndLadders2.ConsoleApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Game;

public class GameRefitService : IGameApi
{
	private readonly IGameApi _gameApi;

	public GameRefitService()
	{
		_gameApi = RestService.For<IGameApi>("https://localhost:7067");
	}

	public async Task<GameResponseModel> CreateGame(List<PlayerModel> requestModel)
	{
		return await _gameApi.CreateGame(requestModel);
	}
}
