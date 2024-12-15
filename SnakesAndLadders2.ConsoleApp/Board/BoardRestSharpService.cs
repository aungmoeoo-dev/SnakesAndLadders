using Newtonsoft.Json;
using RestSharp;
using SnakesAndLadders2.ConsoleApp.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Board;

public class BoardRestSharpService : IBoardApi
{
	private readonly string _endpoint;
	private readonly RestClient _restClient;

	public BoardRestSharpService()
	{
		_endpoint = "https://localhost:7067/api/board";
		_restClient = new RestClient();
	}

	public async Task<BoardResponseModel> CreateBoard(List<BoardModel> requestModel)
	{
		RestRequest request = new(_endpoint, Method.Post);
		request.AddJsonBody(requestModel);
		RestResponse response = await _restClient.ExecuteAsync(request);

		string content = response.Content!;
		return JsonConvert.DeserializeObject<BoardResponseModel>(content)!;
	}
}
