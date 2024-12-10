using Microsoft.EntityFrameworkCore;
using SnakesAndLadders.Shared.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared;

internal class AppDbContext : DbContext
{

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
	}

	public DbSet<PlayerModel> Players { get; set; }
}
