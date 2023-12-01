using System.Text.RegularExpressions;

namespace PuzzleSolvers.Day01;

public partial class PuzzleSolver01
{
	[GeneratedRegex(@"\D")]
	private static partial Regex NonDigitRegex();

	[GeneratedRegex("one|two|three|four|five|six|seven|eight|nine")]
	private static partial Regex NumberWordsRegex();
	
	private static readonly Dictionary<string, string> NumberMapping = new()
	{
		{ "one", "1" },
		{ "two", "2" },
		{ "three", "3" },
		{ "four", "4" },
		{ "five", "5" },
		{ "six", "6" },
		{ "seven", "7" },
		{ "eight", "8" },
		{ "nine", "9" },
	};
	
	public string SolveFirstStar(PuzzleInput input)
	{
		var sum = 0;
		foreach (var line in input.Lines)
		{
			var numbers = NonDigitRegex().Replace(line, "");
			if (numbers.Length == 0) continue;
			
			var calibrationValue = string.Concat(numbers[0], numbers[^1]);
			sum += int.Parse(calibrationValue);
		}

		return sum.ToString();
	}

	public string SolveSecondStar(PuzzleInput input)
	{
        var replacedInput = NumberWordsRegex().Replace(input.Raw, match => NumberMapping[match.Value]);
		var newInput = new PuzzleInput(replacedInput);
		
		return SolveFirstStar(newInput);
	}
}
