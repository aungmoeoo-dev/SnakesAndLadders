// See https://aka.ms/new-console-template for more information

using SnakesAndLadders2.ConsoleApp.Board;
using SnakesAndLadders2.ConsoleApp.Game;
using SnakesAndLadders2.ConsoleApp.Move;
using SnakesAndLadders2.ConsoleApp.Player;


List<BoardModel> boards = new() {
  new BoardModel(){
	CellNumber = 9,
	CellType = "LadderBottom",
	MoveToCell = 23
  },
	new BoardModel(){
	CellNumber = 58,
	CellType = "LadderBottom",
	MoveToCell = 72
  },
	new BoardModel(){
	CellNumber = 36,
	CellType = "SnakeHead",
	MoveToCell = 18
  },
	new BoardModel(){
	CellNumber = 78,
	CellType = "SnakeHead",
	MoveToCell = 52
  }
};

List<PlayerModel> players = new()
{
	new PlayerModel(){
	Name = "Red"
  },
	new PlayerModel(){
	Name = "Green"
  },
	new PlayerModel(){
	Name = "Blue"
  }
};

MoveModel move1 = new()
{
	PlayerId = "c25c3a94-da18-4848-8ac6-93b3af403693",
	GameId = "4847f423-df9d-4df5-a124-5491abd057b7"
};

MoveModel move2 = new()
{
	PlayerId = "c25c3a94-da18-4848-8ac6-93b3af403693",
	GameId = "4847f423-df9d-4df5-a124-5491abd057b7"
};

IBoardApi boardService = new BoardRefitService();
BoardResponseModel boardResponseModel = await boardService.CreateBoard(boards);
Console.WriteLine("Board Service");
Console.WriteLine("Message: " + boardResponseModel.Message + "\n");

IGameApi gameService = new GameRefitService();
GameResponseModel gameResponseModel = await gameService.CreateGame(players);
Console.WriteLine("Game Service");
Console.WriteLine("Message: " + gameResponseModel.Message + "\n");

IMoveApi moveService = new MoveRefitService();
MoveResponseModel moveResponseModel1 = await moveService.MovePlayer(move1);
Console.WriteLine("Move Service");
Console.WriteLine("IsSuccess: " + moveResponseModel1.IsSuccess);
Console.WriteLine("Message: " + moveResponseModel1.Message);

MoveResponseModel moveResponseModel2 = await moveService.MovePlayer(move2);
Console.WriteLine("IsSuccess: " + moveResponseModel2.IsSuccess);
Console.WriteLine("Message: " + moveResponseModel2.Message);

Console.ReadLine();