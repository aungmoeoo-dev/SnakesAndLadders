using Newtonsoft.Json;
using SnakesAndLadders2.ConsoleApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SnakesAndLadders2.ConsoleApp.Game;

public class GameHttpClientService : IGameApi
{
	private readonly string _endpoint;
	private readonly HttpClient _httpClient;

	public GameHttpClientService()
	{
		_endpoint = "https://localhost:7067/api/board";
		_httpClient = new HttpClient();
	}

	public async Task<GameResponseModel> CreateGame(List<PlayerModel> requestModel)
	{
		string jsonStr = JsonConvert.SerializeObject(requestModel);
		StringContent strContent = new(jsonStr, Encoding.UTF8, Application.Json);
		HttpResponseMessage responseMessage = await _httpClient.PostAsync(_endpoint, strContent)!;

		string content = await responseMessage.Content.ReadAsStringAsync();
		return JsonConvert.DeserializeObject<GameResponseModel>(content)!;
	}
}
