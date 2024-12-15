using SnakesAndLadders2.Shared.Move;
using SnakesAndLadders2.Shared.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.Shared.Game;

public class GameService
{
	private readonly AppDbContext _db;

	public GameService()
	{
		_db = new AppDbContext();
	}

	public GameResponseModel CreateGame(List<PlayerModel> requestModel)
	{
		GameResponseModel responseModel = new();

		string gameId = Guid.NewGuid().ToString();

		foreach (var playerModel in requestModel)
		{
			playerModel.Id = Guid.NewGuid().ToString();
			playerModel.CurrentPosition = 1;
			_db.Players.Add(playerModel);

			MoveModel moveModel = new()
			{
				Id = Guid.NewGuid().ToString(),
				GameId = gameId,
				PlayerId = playerModel.Id,
				FromCell = 1,
				ToCell = 1,
				MoveDate = DateTime.UtcNow,
			};
			_db.Moves.Add(moveModel);
		}

		GameModel gameModel = new()
		{
			Id = gameId,
			Status = "InProgress",
			CurrentPlayerId = requestModel[0].Id,
		};
		_db.Games.Add(gameModel);
		int result = _db.SaveChanges();

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Game creating successful." : "Game creating failed.";
		responseModel.Data = gameModel;

		return responseModel;
	}
}
