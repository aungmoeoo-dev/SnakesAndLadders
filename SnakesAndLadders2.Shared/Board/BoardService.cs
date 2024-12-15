using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders2.Shared.Board;

public class BoardService
{

	private readonly AppDbContext _db;

	public BoardService()
	{
		_db = new AppDbContext();
	}

	private bool ValidateBoard(BoardModel boardModel)
	{
		if (boardModel.CellNumber <= 1
			|| boardModel.CellNumber >= 100
			|| boardModel.CellType == "Normal"
			|| boardModel.CellType == "SnakeTail"
			|| boardModel.CellType == "LadderTop")
		{
			return false;
		}
		return true;
	}

	private List<BoardModel> CategorizedBoard(BoardModel boardModel)
	{
		List<BoardModel> boards = new();

		if (boardModel.CellType == "SnakeHead")
		{
			BoardModel board2 = new()
			{
				CellNumber = (int)boardModel.MoveToCell,
				CellType = "SnakeTail",
				MoveToCell = null
			};

			boards.Add(board2);
		}

		boards.Add(boardModel);

		if (boardModel.CellType == "LadderBottom")
		{
			BoardModel board2 = new()
			{
				CellNumber = (int)boardModel.MoveToCell,
				CellType = "LadderTop",
				MoveToCell = null
			};

			boards.Add(board2);
		}

		return boards;
	}

	public BoardResponseModel CreateBoard(List<BoardModel> requestModel)
	{
		BoardResponseModel responseModel = new();
		List<BoardModel> specialCells = new();

		foreach (BoardModel boardModel in requestModel)
		{
			bool isValid = ValidateBoard(boardModel);
			if (!isValid)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = "Invalid board cell";
				return responseModel;
			}
		}

		foreach (BoardModel boardModel in requestModel)
		{
			var specialCellPair = CategorizedBoard(boardModel);
			specialCells.AddRange(specialCellPair);
		}

		var orderdSpecialCells = specialCells.OrderBy(x => x.CellNumber).ToList();
		for (var i = 1; i < 101; i++)
		{
			BoardModel boardModel = new();

			var specialCell = orderdSpecialCells.FirstOrDefault(x => x.CellNumber == i);
			if (specialCell is not null)
			{
				boardModel.BoardId = Guid.NewGuid().ToString();
				boardModel.CellNumber = i;
				boardModel.CellType = specialCell.CellType;
				boardModel.MoveToCell = specialCell.MoveToCell;

				_db.Boards.Add(boardModel);
				continue;
			}

			boardModel.BoardId = Guid.NewGuid().ToString();
			boardModel.CellNumber = i;
			boardModel.CellType = "Normal";
			boardModel.MoveToCell = null;

			_db.Boards.Add(boardModel);
		};

		int result = _db.SaveChanges();

		responseModel.IsSuccess = result > 0;
		responseModel.Message = result > 0 ? "Board created successfully." : "Board creating failed.";
		return responseModel;
	}

}
