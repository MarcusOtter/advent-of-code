using System.Text.RegularExpressions;

namespace PuzzleSolvers.Day03;

public partial class PuzzleSolver03
{
	private static readonly char[] SpecialCharacters = { '=', '*', '+', '/', '&', '#', '-', '%', '$', '@' };
	
	public string SolveFirstStar(PuzzleInput input)
	{
		var grid = input.Grid;
		var sum = 0;
		
		for (var rowIndex = 0; rowIndex < grid.Length; rowIndex++)
		{
			var lineAsString = new string(grid[rowIndex]);
			var numberMatches = NumberRegex().Matches(lineAsString);

			foreach (var match in numberMatches)
			{
				var adjacentToSymbol = false;
				var fullNumber = match.ToString();
				if (string.IsNullOrWhiteSpace(fullNumber)) continue;
				
				var startIndices = lineAsString.AllIndicesOf(fullNumber);
				foreach (var startIndex in startIndices)
				{
					for (var columnIndex = startIndex; columnIndex < startIndex + fullNumber.Length; columnIndex++)
					{
						var canGoUp = rowIndex != 0;
						var canGoDown = rowIndex != grid.Length - 1 && grid[rowIndex + 1].Length > 0;
						var canGoLeft = columnIndex != 0;
						var canGoRight = columnIndex != lineAsString.Length - 1;

						try
						{
							if (canGoUp && canGoLeft &&  SpecialCharacters.Contains(grid[rowIndex - 1][columnIndex - 1])) adjacentToSymbol = true;
							if (canGoUp &&               SpecialCharacters.Contains(grid[rowIndex - 1][columnIndex + 0])) adjacentToSymbol = true;
							if (canGoUp && canGoRight && SpecialCharacters.Contains(grid[rowIndex - 1][columnIndex + 1])) adjacentToSymbol = true;
						
							if (canGoLeft &&  SpecialCharacters.Contains(grid[rowIndex + 0][columnIndex - 1])) adjacentToSymbol = true;
							if (canGoRight && SpecialCharacters.Contains(grid[rowIndex + 0][columnIndex + 1])) adjacentToSymbol = true;
						
							if (canGoDown && canGoLeft &&  SpecialCharacters.Contains(grid[rowIndex + 1][columnIndex - 1])) adjacentToSymbol = true;
							if (canGoDown &&               SpecialCharacters.Contains(grid[rowIndex + 1][columnIndex + 0])) adjacentToSymbol = true;
							if (canGoDown && canGoRight && SpecialCharacters.Contains(grid[rowIndex + 1][columnIndex + 1])) adjacentToSymbol = true;
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
							throw;
						}
					}
				} 
				
				if (adjacentToSymbol)
				{
					sum += int.Parse(fullNumber);
				}
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
