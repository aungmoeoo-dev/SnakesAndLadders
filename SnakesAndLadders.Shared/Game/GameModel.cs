using SnakesAndLadders.Shared.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Game;

[Table("TBL_Game")]
public class GameModel
{
	[Key]
	[Column("GameId")]
	public string? Id { get; set; }
	[Column("GameStatus")]
	public string? Status { get; set; }
	[Column("CurrentPlayerId")]
	public string? CurrentPlayerId { get; set; }
}

public class GameResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public GameModel Data { get; set; }
}

public class GameCreateRequestModel
{
	public List<PlayerModel> Players { get; set; }
}