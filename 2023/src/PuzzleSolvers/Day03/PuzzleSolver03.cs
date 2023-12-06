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

		foreach (var numberMatch in numberMatches)
		{
			var flattenedIndexStart = numberMatch.Index;
			var flattenedIndexEnd = numberMatch.Index + numberMatch.Length;
			var value = input.Raw[flattenedIndexStart..flattenedIndexEnd];
			var rowIndex = flattenedIndexStart / gridWidth;
			var columnIndexStart = flattenedIndexStart % gridWidth;
			var columnIndexEnd = flattenedIndexEnd % gridWidth;
			
			var isAdjacentToSymbol = false;
			for (var columnIndex = columnIndexStart; columnIndex < columnIndexEnd; columnIndex++)
			{
				var adjacentSymbols = CheckAdjacentCells(grid, rowIndex, columnIndex, c => SpecialCharacters.Contains(c));
				if (adjacentSymbols.Any(c => c.isTrue))
				{
					isAdjacentToSymbol = true;
					break;
				}
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
		var sum = 0;
		
		// Populate numbers
		var numberMatches = NumberRegex().EnumerateMatches(input.Raw);
		var numbers = new Dictionary<int, int>();
		foreach (var numberMatch in numberMatches)
		{
			var flattenedIndexStart = numberMatch.Index;
			var flattenedIndexEnd = numberMatch.Index + numberMatch.Length;
			var value = int.Parse(input.Raw[flattenedIndexStart..flattenedIndexEnd]);
			var occupiedIndices = Enumerable.Range(flattenedIndexStart, flattenedIndexEnd - flattenedIndexStart);
			foreach(var occupiedIndex in occupiedIndices)
			{
				numbers[occupiedIndex] = value;
			}
		}

		var grid = input.GridRaw;
		var gridWidth = grid[0].Length;
		
		var asterisks = AsteriskRegex().EnumerateMatches(input.Raw);
		foreach (var asteriskMatch in asterisks)
		{
			var flattenedIndex = asteriskMatch.Index;
			var rowIndex = flattenedIndex / gridWidth;
			var columnIndex = flattenedIndex % gridWidth;
			
			var digitMatches = CheckAdjacentCells(grid, rowIndex, columnIndex, c => c.IsDigit());
			var distinctNumbers = digitMatches.Where(m => m.isTrue).Select(m => numbers[m.columnIndex + m.rowIndex * gridWidth]).Distinct().ToArray();
			if (distinctNumbers.Length == 2)
			{
				sum += distinctNumbers[0] * distinctNumbers[1];
			}
		}
		
		return sum.ToString();
	}

	// Also diagonal
	private IEnumerable<(int rowIndex, int columnIndex, bool isTrue)> CheckAdjacentCells(char[][] grid, int rowIndex, int columnIndex, Func<char, bool> action)
	{
		var results = new List<(int rowIndex, int columnIndex, bool isTrue)>();
		var gridWidth = grid[0].Length;
		var canGoUp = rowIndex > 0;
		var canGoDown = rowIndex < grid.Length - 1 && grid[rowIndex + 1].Length > 0;
		var canGoLeft = columnIndex > 0;
		var canGoRight = columnIndex < gridWidth - 1;
		
		if (canGoUp && canGoLeft)  results.Add(new (rowIndex - 1, columnIndex - 1, action(grid[rowIndex - 1][columnIndex - 1])));
		if (canGoUp)               results.Add(new (rowIndex - 1, columnIndex + 0, action(grid[rowIndex - 1][columnIndex + 0])));
		if (canGoUp && canGoRight) results.Add(new (rowIndex - 1, columnIndex + 1, action(grid[rowIndex - 1][columnIndex + 1])));
						
		if (canGoLeft)  results.Add(new (rowIndex + 0, columnIndex - 1, action(grid[rowIndex + 0][columnIndex - 1])));
		if (canGoRight) results.Add(new (rowIndex + 0, columnIndex + 1, action(grid[rowIndex + 0][columnIndex + 1])));
						
		if (canGoDown && canGoLeft)  results.Add(new (rowIndex + 1, columnIndex - 1, action(grid[rowIndex + 1][columnIndex - 1])));
		if (canGoDown)               results.Add(new (rowIndex + 1, columnIndex + 0, action(grid[rowIndex + 1][columnIndex + 0])));
		if (canGoDown && canGoRight) results.Add(new (rowIndex + 1, columnIndex + 1, action(grid[rowIndex + 1][columnIndex + 1])));

		return results;
	}
	
    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
    
    [GeneratedRegex(@"\*")]
    private static partial Regex AsteriskRegex();
}
