using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.Shared.Move;

public class MoveService
{
	private readonly AppDbContext _db;

	public MoveService()
	{
		_db = new AppDbContext();
	}

	private int RollDice()
	{
		Random random = new();
		return random.Next(1, 7);
	}

	public MoveResponseModel MovePlayer(MoveModel requestModel)
	{
		MoveResponseModel responseModel = new();

		var currentGame = _db.Games.AsNoTracking().FirstOrDefault(x => x.Id == requestModel.GameId);
		if (currentGame is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Game does'nt exist.";
			return responseModel;
		}

		if (currentGame.CurrentPlayerId != requestModel.PlayerId)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Not your turn.";
			return responseModel;
		}

		var currentPlayer = _db.Players.AsNoTracking().FirstOrDefault(x => x.Id == requestModel.PlayerId);
		if (currentPlayer is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Player does'nt exist.";
			return responseModel;
		}

		int diceResult = RollDice();
		int oldPosition = currentPlayer.CurrentPosition;
		int newPosition = oldPosition + diceResult;

		var newPositionCell = _db.Boards.FirstOrDefault(x => x.CellNumber == newPosition);
		if (newPositionCell.CellType == "SnakeHead" || newPositionCell.CellType == "LadderBottom")
		{
			newPosition = (int)newPositionCell.MoveToCell;
		}

		if (newPosition >= 100)
		{
			currentGame.Status = "Completed";
		}

		currentPlayer.CurrentPosition = newPosition;
		_db.Entry(currentPlayer).State = EntityState.Modified;

		MoveModel moveModel = new()
		{
			Id = Guid.NewGuid().ToString(),
			GameId = requestModel.GameId,
			PlayerId = requestModel.PlayerId,
			FromCell = oldPosition,
			ToCell = newPosition,
			MoveDate = DateTime.UtcNow
		};
		_db.Moves.Add(moveModel);

		var boardingMoves = _db.Moves
			.AsNoTracking()
			.Where(x => x.GameId == requestModel.GameId && x.FromCell == 1 && x.ToCell == 1)
			.OrderBy(x => x.MoveDate)
			.ToList();

		var currentPlayerIndex = boardingMoves.FindIndex(x => x.PlayerId == currentPlayer.Id);

		var nextPlayerId = currentPlayerIndex != (boardingMoves.Count - 1) ? boardingMoves[currentPlayerIndex + 1].PlayerId : boardingMoves[0].PlayerId;
		currentGame.CurrentPlayerId = nextPlayerId;
		_db.Entry(currentGame).State = EntityState.Modified;
		int result = _db.SaveChanges();

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Game creating successful." : "Game creating failed.";
		responseModel.IsPlayerWin = newPosition > 100;
		responseModel.Data = moveModel;

		return responseModel;

	}
}
