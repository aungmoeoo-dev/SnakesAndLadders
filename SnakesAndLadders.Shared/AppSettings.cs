using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared;

internal static class AppSettings
{
	private static readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
	{
		DataSource = ".",
		InitialCatalog = "SnakesAndLaddersDB",
		UserID = "sa",
		Password = "Aa145156167!",
		TrustServerCertificate = true
	};

	public static readonly string ConnectionString = _sqlConnectionStringBuilder.ConnectionString;
}
