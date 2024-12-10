using SnakesAndLadders.Shared.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Shared.Board;

public class BoardService
{
	private readonly AppDbContext _db;

	public BoardService()
	{
		_db = new AppDbContext();
	}

	private bool ValidateCell(CellModel cell)
	{
		if (cell.Number <= 1
			|| cell.Number >= 100
			|| cell.MoveToCell <= 1
			|| cell.MoveToCell >= 100
			|| cell.Type == "SnakeTail"
			|| cell.Type == "LadderTop"
			) return false;

		return true;
	}

	private List<CellModel> CategorizeCell(CellModel cellModel, string boardId)
	{
		List<CellModel> returnModel = new();

		if (cellModel.Type == "SnakeHead")
		{
			CellModel firstCell = new()
			{
				Id = Guid.NewGuid().ToString(),
				BoardId = boardId,
				Number = (int)cellModel.MoveToCell,
				Type = "SnakeTail",
				MoveToCell = null
			};

			returnModel.Add(firstCell);

			CellModel secondCell = new()
			{
				Id = Guid.NewGuid().ToString(),
				BoardId = boardId,
				Number = cellModel.Number,
				Type = cellModel.Type,
				MoveToCell = cellModel.MoveToCell
			};

			returnModel.Add(secondCell);
		}

		if (cellModel.Type == "LadderBottom")
		{
			CellModel firstCell = new()
			{
				Id = Guid.NewGuid().ToString(),
				BoardId = boardId,
				Number = cellModel.Number,
				Type = cellModel.Type,
				MoveToCell = cellModel.MoveToCell
			};


			returnModel.Add(firstCell);

			CellModel secondCell = new()
			{
				Id = Guid.NewGuid().ToString(),
				BoardId = boardId,
				Number = (int)cellModel.MoveToCell,
				Type = "LadderTop",
				MoveToCell = null
			};

			returnModel.Add(secondCell);
		}

		return returnModel;
	}

	public BoardResponseModel CreateBoard(BoardCreateRequestModel requestModel)
	{
		BoardResponseModel responseModel = new();
		List<CellModel> specialCells = new();
		string boardId = Guid.NewGuid().ToString();

		foreach (var cell in requestModel.Cells)
		{
			bool isValid = ValidateCell(cell);
			if (!isValid)
			{
				responseModel.IsSuccess = false;
				responseModel.Message = "Invalid cells";
				return responseModel;
			}

			var cells = CategorizeCell(cell, boardId);
			specialCells.AddRange(cells);
		}

		for (var i = 1; i < 101; i++)
		{
			var currentCell = specialCells.FirstOrDefault(x => x.Number == i);
			if (currentCell is not null)
			{
				_db.Cells.Add(currentCell);
				_db.SaveChanges();
				continue;
			}

			CellModel cellModel = new()
			{
				Id = Guid.NewGuid().ToString(),
				BoardId = boardId,
				Number = i,
				Type = "Normal",
				MoveToCell = null
			};

			_db.Cells.Add(cellModel);
			_db.SaveChanges();
		}

		responseModel.IsSuccess = true;
		responseModel.Message = "Board creating successful.";
		responseModel.BoardId = boardId;
		return responseModel;
	}
}
