using Newtonsoft.Json;
using RestSharp;
using SnakesAndLadders2.ConsoleApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Game;

public class GameRestSharpService : IGameApi
{
	private readonly RestClient _restClient;
	private readonly string _endpoint;

	public GameRestSharpService()
	{
		_restClient = new RestClient();
		_endpoint = "https://localhost:7067/api/board";
	}

	public async Task<GameResponseModel> CreateGame(List<PlayerModel> requestModel)
	{
		RestRequest request = new(_endpoint);
		request.AddJsonBody(requestModel);
		RestResponse response = await _restClient.ExecuteAsync(request);

		string content = response.Content!;
		return JsonConvert.DeserializeObject<GameResponseModel>(content)!;
	}
}
