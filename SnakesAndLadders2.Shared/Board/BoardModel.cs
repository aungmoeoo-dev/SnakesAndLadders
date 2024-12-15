using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.Shared.Board;

[Table("TBL_GameBoard")]
public class BoardModel
{
	[Key]
	public string? BoardId { get; set; }
	public int CellNumber { get; set; }
	public string? CellType { get; set; }
	public int? MoveToCell { get; set; }
}

public class BoardResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
}