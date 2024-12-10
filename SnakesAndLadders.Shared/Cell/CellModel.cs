using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Cell;

[Table("TBL_Cell")]
public class CellModel
{
	[Key]
	[Column("CellId")]
	public string? Id { get; set; }
	public string? BoardId { get; set; }
	[Column("CellNumber")]
	public int Number { get; set; }
	[Column("CellType")]
	public string Type { get; set; }
	public int? MoveToCell { get; set; }
}
