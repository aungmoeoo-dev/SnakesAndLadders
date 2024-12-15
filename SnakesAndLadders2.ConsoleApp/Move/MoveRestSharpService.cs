using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.ConsoleApp.Move;

public class MoveRestSharpService : IMoveApi
{
	private readonly RestClient _restClient;
	private readonly string _endpoint;

	public MoveRestSharpService()
	{
		_restClient = new RestClient();
		_endpoint = "https://localhost:7067/api/move";
	}

	public async Task<MoveResponseModel> MovePlayer(MoveModel requestModel)
	{
		RestRequest request = new(_endpoint, Method.Post);
		request.AddJsonBody(requestModel);

		RestResponse response = await _restClient.ExecuteAsync(request);
		string content = response.Content!;

		return JsonConvert.DeserializeObject<MoveResponseModel>(content)!;

	}
}
