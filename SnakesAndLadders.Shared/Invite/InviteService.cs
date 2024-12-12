using Azure.Core.GeoJson;
using Microsoft.EntityFrameworkCore;
using SnakesAndLadders.Shared.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Invite;

public class InviteService
{
	private readonly AppDbContext _db;

	public InviteService()
	{
		_db = new AppDbContext();
	}

	public InviteResponseModel InvitePlayer(InviteModel requestModel)
	{
		InviteResponseModel responseModel = new();

		var gameModel = _db.Games.FirstOrDefault(x => x.Id == requestModel.GameId);
		if (gameModel is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Game does not exist.";
			return responseModel;
		}

		if (gameModel.Status == "Completed")
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Game is completed.";
			return responseModel;
		}

		var inviteeModel = _db.Players.FirstOrDefault(x => x.Id == requestModel.InviteeId);
		if (inviteeModel is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Player does not exist.";
			return responseModel;
		}

		if (inviteeModel.CurrentPosition != 0)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Player is already in a game.";
			return responseModel;
		}

		inviteeModel.CurrentPosition = 1;
		_db.Entry(inviteeModel).State = EntityState.Modified;

		MoveModel moveModel = new()
		{
			Id = Guid.NewGuid().ToString(),
			GameId = requestModel.GameId,
			PlayerId = requestModel.InviteeId,
			FromCell = 1,
			ToCell = 1,
			MoveDate = DateTime.UtcNow
		};

		_db.Moves.Add(moveModel);
		int result = _db.SaveChanges();

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Invite successful." : "Invite failed.";
		return responseModel;
	}
}
