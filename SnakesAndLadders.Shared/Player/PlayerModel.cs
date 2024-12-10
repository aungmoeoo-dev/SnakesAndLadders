using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Player;

[Table("TBL_Player")]
public class PlayerModel
{
	[Key]
	[Column("PlayerId")]
	public string? Id { get; set; }
	[Column("PlayerName")]
	public string? Name { get; set; }
	[Column("CurrentPosition")]
	public int CurrentPosition { get; set; }
}

public class PlayerResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public PlayerModel Data { get; set; }
}