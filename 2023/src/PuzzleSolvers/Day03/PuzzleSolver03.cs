using System.Text.RegularExpressions;

namespace PuzzleSolvers.Day03;

public partial class PuzzleSolver03
{
	private const string SpecialCharacters = "=*+/&#-%$@";
	
	public string SolveFirstStar(PuzzleInput input)
	{
		var grid = input.GridRaw;
		var gridWidth = grid[0].Length;
		var numberMatches = NumberRegex().EnumerateMatches(input.Raw);
		var sum = 0;

		foreach (var match in numberMatches)
		{
			var isAdjacentToSymbol = false;
			var flattenedIndexStart = match.Index;
			var flattenedIndexEnd = match.Index + match.Length;
			var value = input.Raw[flattenedIndexStart..flattenedIndexEnd];
			var rowIndex = flattenedIndexStart / gridWidth;
			var columnIndexStart = flattenedIndexStart % gridWidth;
			var columnIndexEnd = flattenedIndexEnd % gridWidth;
			
			for (var columnIndex = columnIndexStart; columnIndex < columnIndexEnd; columnIndex++)
			{
				var canGoUp = rowIndex > 0;
				var canGoDown = rowIndex < grid.Length - 1 && grid[rowIndex + 1].Length > 0;
				var canGoLeft = columnIndex > 0;
				var canGoRight = columnIndex < gridWidth - 1;

				if (canGoUp && canGoLeft &&  SpecialCharacters.Contains(grid[rowIndex - 1][columnIndex - 1])) isAdjacentToSymbol = true;
				if (canGoUp &&               SpecialCharacters.Contains(grid[rowIndex - 1][columnIndex + 0])) isAdjacentToSymbol = true;
				if (canGoUp && canGoRight && SpecialCharacters.Contains(grid[rowIndex - 1][columnIndex + 1])) isAdjacentToSymbol = true;
						
				if (canGoLeft &&  SpecialCharacters.Contains(grid[rowIndex + 0][columnIndex - 1])) isAdjacentToSymbol = true;
				if (canGoRight && SpecialCharacters.Contains(grid[rowIndex + 0][columnIndex + 1])) isAdjacentToSymbol = true;
						
				if (canGoDown && canGoLeft &&  SpecialCharacters.Contains(grid[rowIndex + 1][columnIndex - 1])) isAdjacentToSymbol = true;
				if (canGoDown &&               SpecialCharacters.Contains(grid[rowIndex + 1][columnIndex + 0])) isAdjacentToSymbol = true;
				if (canGoDown && canGoRight && SpecialCharacters.Contains(grid[rowIndex + 1][columnIndex + 1])) isAdjacentToSymbol = true;
				
			}
			
			if (isAdjacentToSymbol)
			{
				sum += int.Parse(value);
			}
		}
		
		return sum.ToString();
	}

	public string SolveSecondStar(PuzzleInput input)
	{
		return "Not implemented";
	}
	
    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
