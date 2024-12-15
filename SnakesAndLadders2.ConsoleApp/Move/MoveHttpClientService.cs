using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SnakesAndLadders2.ConsoleApp.Move;

public class MoveHttpClientService : IMoveApi
{
	private readonly HttpClient _httpClient;
	private readonly string _endpoint;

	public MoveHttpClientService()
	{
		_httpClient = new HttpClient();
		_endpoint = "https://localhost:7067/api/move";
	}

	public async Task<MoveResponseModel> MovePlayer(MoveModel requestModel)
	{
		string jsonStr = JsonConvert.SerializeObject(requestModel);
		StringContent strContent = new(jsonStr, Encoding.UTF8, Application.Json);
		HttpResponseMessage responseMessage = await _httpClient.PostAsync(_endpoint, strContent);

		string content = await responseMessage.Content.ReadAsStringAsync();
		return JsonConvert.DeserializeObject<MoveResponseModel>(content)!;
	}
}
