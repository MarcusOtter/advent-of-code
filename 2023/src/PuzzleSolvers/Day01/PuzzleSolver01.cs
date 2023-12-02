using System.Text.RegularExpressions;

namespace PuzzleSolvers.Day01;

public partial class PuzzleSolver01
{
	[GeneratedRegex(@"\D")]
	private static partial Regex NonDigitRegex();
	
	private static readonly Dictionary<string, int> NumberMapping = new()
	{
		{ "one", 1 },
		{ "two", 2 },
		{ "three", 3 },
		{ "four", 4 },
		{ "five", 5 },
		{ "six", 6 },
		{ "seven", 7 },
		{ "eight", 8 },
		{ "nine", 9 },
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
		var sum = 0;
		
		foreach (var line in input.Lines)
		{
			var left = 0;
			for (var i = 0; i < line.Length; i++)
			{
				if (int.TryParse(line[i].ToString(), out var num))
				{
					left = num;
					break;
				}

				foreach (var word in NumberMapping.Keys)
				{
					if (i + word.Length > line.Length) continue;
					
					var maybeWord = line[i..(i + word.Length)];
					if (maybeWord != word) continue;

					left = NumberMapping[word];
					break;
				}

				if (left != 0)
				{
					break;
				}
			}
			
			
			var right = 0;
			for (var i = line.Length - 1; i >= 0; i--)
			{
				if (int.TryParse(line[i].ToString(), out var num))
				{
					right = num;
					break;
				}

				foreach (var word in NumberMapping.Keys)
				{
					if (i - word.Length < 0) continue;
					
					var maybeWord = line[(i - word.Length + 1)..(i + 1)];
					if (maybeWord != word) continue;

					right = NumberMapping[word];
					break;
				}

				if (right != 0)
				{
					break;
				}
			}

			sum += int.Parse(left.ToString() + right.ToString());
		}

		return sum.ToString();
	}
}
