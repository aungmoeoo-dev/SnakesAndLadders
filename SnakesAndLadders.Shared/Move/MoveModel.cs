using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Move;

[Table("TBL_Move")]
public class MoveModel
{
	[Key]
	[Column("MoveId")]
	public string? Id { get; set; }
	public string? GameId { get; set; }
	public string? PlayerId { get; set; }
	public int FromCell { get; set; }
	public int ToCell { get; set; }
	public DateTime MoveDate { get; set; }
}

public class MoveResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
	public bool IsPlayerWin { get; set; }
	public MoveModel Data { get; set; }
}