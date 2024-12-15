using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SnakesAndLadders2.Shared.Board;
using SnakesAndLadders2.Shared.Game;
using SnakesAndLadders2.Shared.Move;
using SnakesAndLadders2.Shared.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.Shared;

public class AppDbContext : DbContext
{
	private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
	{
		DataSource = ".",
		InitialCatalog = "SnakesAndLadderDb2",
		UserID = "sa",
		Password = "Aa145156167!",
		TrustServerCertificate = true
	};

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
	}

	public DbSet<BoardModel> Boards { get; set; }
	public DbSet<GameModel> Games { get; set; }
	public DbSet<PlayerModel> Players { get; set; }
	public DbSet<MoveModel> Moves { get; set; }
}
