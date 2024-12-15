using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.Shared.Player;

[Table("TBL_Player")]
public class PlayerModel
{
	[Key]
	[Column("PlayerId")]
	public string? Id { get; set; }
	[Column("PlayerName")]
	public string? Name { get; set; }
	public int CurrentPosition { get; set; }
}
