using Microsoft.EntityFrameworkCore;
using SnakesAndLadders.Shared.Move;
using SnakesAndLadders.Shared.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Game;

public class GameService
{
	private readonly AppDbContext _db;

	public GameService()
	{
		_db = new AppDbContext();
	}

	public GameResponseModel CreateGame(GameCreateRequestModel requestModel)
	{
		GameResponseModel responseModel = new();

		List<PlayerModel> playerList = new();

		foreach (var player in requestModel.Players)
		{
			var foundPlayer = _db.Players.AsNoTracking().FirstOrDefault(x => x.Id == player.Id);

			if (foundPlayer is null) continue;

			playerList.Add(foundPlayer);
		}

		string gameId = Guid.NewGuid().ToString();

		foreach (var player in playerList)
		{
			MoveModel moveModel = new()
			{
				Id = Guid.NewGuid().ToString(),
				GameId = gameId,
				PlayerId = player.Id,
				FromCell = 1,
				ToCell = 1,
				MoveDate = DateTime.Now,
			};
			_db.Moves.Add(moveModel);

			player.CurrentPosition = 1;
			_db.Entry(player).State = EntityState.Modified;

			_db.SaveChanges();
		}

		GameModel gameModel = new()
		{
			Id = gameId,
			Status = "InProgress",
			CurrentPlayerId = playerList[0].Id,
		};

		_db.Games.Add(gameModel);
		int result = _db.SaveChanges();

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Game creating successful." : "Game creating failed.";
		responseModel.Data = result > 0 ? gameModel : null;
		return responseModel;

	}
}
