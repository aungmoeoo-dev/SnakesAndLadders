using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SnakesAndLadders2.ConsoleApp.Board;

public class BoardHttpClientService : IBoardApi
{
	private readonly string _endpoint;
	private readonly HttpClient _httpClient;

	public BoardHttpClientService()
	{
		_endpoint = "https://localhost:7067/api/board";
		_httpClient = new HttpClient();
	}

	public async Task<BoardResponseModel> CreateBoard(List<BoardModel> requestModel)
	{
		string jsonStr = JsonConvert.SerializeObject(requestModel);
		StringContent strContent = new(jsonStr, Encoding.UTF8, Application.Json);
		HttpResponseMessage responseMessage = await _httpClient.PostAsync(_endpoint, strContent);
		
		string content = await responseMessage.Content.ReadAsStringAsync();
		return JsonConvert.DeserializeObject<BoardResponseModel>(content)!;
	}
}
