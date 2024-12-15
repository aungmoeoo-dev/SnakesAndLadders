using Refit;
using SnakesAndLadders2.ConsoleApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Game;

public interface IGameApi
{
	[Post("/api/game")]
	public Task<GameResponseModel> CreateGame(List<PlayerModel> requestModel);
}
