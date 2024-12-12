using Microsoft.EntityFrameworkCore;
using SnakesAndLadders.Shared.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Move;

public class MoveService
{
	private readonly AppDbContext _db;

	public MoveService()
	{
		_db = new AppDbContext();
	}

	private int RollDice()
	{
		Random random = new Random();
		return random.Next(1, 7);
	}

	public MoveResponseModel Move(MoveModel requestModel)
	{
		MoveResponseModel responseModel = new();

		#region Check if the game exists
		var currentGame = _db.Games.AsNoTracking().FirstOrDefault(x => x.Id == requestModel.GameId);
		if (currentGame is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Game does not exit.";
			return responseModel;
		}
		#endregion

		if (currentGame.Status == "Completed")
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Game is completed";
			return responseModel;
		}

		#region Check if the player exists
		var player = _db.Players.AsNoTracking().FirstOrDefault(x => x.Id == requestModel.PlayerId);
		if (player is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Player does not exit.";
			return responseModel;
		}
		#endregion

		#region Check the current player of the game
		if (player.Id != currentGame.CurrentPlayerId)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Not your turn.";
			return responseModel;
		}
		#endregion

		var diceResult = RollDice();
		int oldPosition = player.CurrentPosition; ;
		int newPosition = player.CurrentPosition + diceResult;

		//This else statement is for testing Move.
		//if (requestModel.ToCell is 0)
		//{
		//	var diceResult = RollDice();
		//	newPosition = player.CurrentPosition + diceResult;
		//}
		//else
		//{
		//	newPosition = requestModel.ToCell;
		//}

		#region Player go backward if the new positon is greater then 100
		if (newPosition > 100)
		{
			newPosition = 100 - (newPosition - 100);
		}
		#endregion

		if (newPosition == 100)
		{
			currentGame.Status = "Completed";
			_db.Entry(currentGame).State = EntityState.Modified;
		}

		#region Check if the dice result is on special cells
		var newCell = _db.Cells.FirstOrDefault(x => x.Number == newPosition);
		if (newCell.Type == "SnakeHead" || newCell.Type == "LadderBottom")
		{
			newPosition = (int)newCell.MoveToCell;
		}
		#endregion

		player.CurrentPosition = newPosition;
		_db.Entry(player).State = EntityState.Modified;

		#region Get all the player by querying moves & update their current position
		var moves = _db.Moves.AsNoTracking().Where(x => x.GameId == currentGame.Id && x.FromCell == 1 && x.ToCell == 1).OrderBy(x => x.MoveDate).ToList();
		for (var i = 0; i < moves.Count; i++)
		{
			var currentPlayerId = moves[i].PlayerId;

			#region If newPosition is 100, reset ingame player's current postion to 0
			if (newPosition == 100)
			{
				var resetPlayer = _db.Players.FirstOrDefault(x => x.Id == currentPlayerId);
				resetPlayer.CurrentPosition = 0;
				_db.Entry(resetPlayer).State = EntityState.Modified;
			}
			#endregion


			if (newPosition != 100 && currentGame.CurrentPlayerId == currentPlayerId)
			{
				var nextPlayerId = i != moves.Count - 1 ? moves[i + 1].PlayerId : moves[0].PlayerId;

				currentGame.CurrentPlayerId = nextPlayerId;
				_db.Entry(currentGame).State = EntityState.Modified;
				break;
			}

		}
		#endregion

		#region Save the move
		MoveModel moveModel = new()
		{
			Id = Guid.NewGuid().ToString(),
			GameId = currentGame.Id,
			PlayerId = player.Id,
			FromCell = oldPosition,
			ToCell = newPosition,
			MoveDate = DateTime.UtcNow
		};

		_db.Moves.Add(moveModel);
		int result = _db.SaveChanges();
		#endregion

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Success" : "Failed.";
		responseModel.IsPlayerWin = newPosition == 100;
		responseModel.Data = result > 0 ? moveModel : null;
		return responseModel;
	}
}
