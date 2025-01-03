﻿using Microsoft.EntityFrameworkCore;
using SnakesAndLadders.Shared.Cell;
using SnakesAndLadders.Shared.Game;
using SnakesAndLadders.Shared.Move;
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
	public DbSet<GameModel> Games { get; set; }
	public DbSet<CellModel> Cells { get; set; }
	public DbSet<MoveModel> Moves { get; set; }
}
