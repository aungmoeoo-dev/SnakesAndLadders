using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Invite;

public class InviteModel
{
	public string? GameId { get; set; }
	public string? InviterId { get; set; }
	public string? InviteeId { get; set; }
}

public class InviteResponseModel
{
	public bool IsSuccess { get; set; }
	public string Message { get; set; }
}