using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Player;

public class PlayerService
{
	private readonly AppDbContext _db;

	public PlayerService()
	{
		_db = new AppDbContext();
	}

	public PlayerResponseModel RegisterPlayer(PlayerModel requestModel)
	{
		PlayerResponseModel responseModel = new();

		if (requestModel.Name is null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Required info not provided.";
			return responseModel;
		}

		var alreadyExistPlayer = _db.Players.FirstOrDefault(x => x.Name == requestModel.Name);
		if (alreadyExistPlayer is not null)
		{
			responseModel.IsSuccess = false;
			responseModel.Message = "Player already exists.";
			return responseModel;
		}

		requestModel.Id = Guid.NewGuid().ToString();
		_db.Players.Add(requestModel);
		int result = _db.SaveChanges();

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Saving Successful." : "Saving failed.";
		responseModel.Data = requestModel;
		return responseModel;
	}


}

