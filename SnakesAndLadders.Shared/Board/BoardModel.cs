using SnakesAndLadders.Shared.Cell;
using SnakesAndLadders.Shared.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Board;

public class BoardResponseModel
{
	public bool IsSuccess { get; set; }
	public string? Message { get; set; }
	public string? BoardId { get; set; }
}

public class BoardCreateRequestModel
{
	public List<CellModel> Cells { get; set; }
}