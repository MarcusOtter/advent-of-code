using System.Text.RegularExpressions;

namespace PuzzleSolvers.Day02;

public partial class PuzzleSolver02
{
	public string SolveFirstStar(PuzzleInput input)
	{
		const int blueLimit = 14;
		const int redLimit = 12;
		const int greenLimit = 13;

		var sumOfIds = 0;
		
		foreach(var game in input.Lines)
		{
			if (string.IsNullOrWhiteSpace(game)) continue;

			var highestBlueInGame = 0;
			var highestRedInGame = 0;
			var highestGreenInGame = 0;
			
			var bagPulls = game.Split(":")[1].Split(";");
			foreach (var bagPull in bagPulls)
			{
				var bluesMatch = BluesRegex().Match(bagPull).Value;
				var redsMatch = RedsRegex().Match(bagPull).Value;
				var greensMatch = GreensRegex().Match(bagPull).Value;

				var blues = string.IsNullOrWhiteSpace(bluesMatch) ? 0 : int.Parse(bluesMatch);
				var reds = string.IsNullOrWhiteSpace(redsMatch) ? 0 : int.Parse(redsMatch);
				var greens = string.IsNullOrWhiteSpace(greensMatch) ? 0 : int.Parse(greensMatch);
				
				if (blues > highestBlueInGame) highestBlueInGame = blues;
				if (reds > highestRedInGame) highestRedInGame = reds;
				if (greens > highestGreenInGame) highestGreenInGame = greens;
			}
			
			if (highestBlueInGame <= blueLimit && highestRedInGame <= redLimit && highestGreenInGame <= greenLimit)
			{
				var gameId = GameIdRegex().Match(game).Value;
				sumOfIds += int.Parse(gameId);
			}
		}
		
		return sumOfIds.ToString();
	}

	public string SolveSecondStar(PuzzleInput input)
	{
		var sumOfPower = 0;
		
		foreach(var game in input.Lines)
		{
			if (string.IsNullOrWhiteSpace(game)) continue;

			var highestBlueInGame = 0;
			var highestRedInGame = 0;
			var highestGreenInGame = 0;
			
			var bagPulls = game.Split(":")[1].Split(";");
			foreach (var bagPull in bagPulls)
			{
				var bluesMatch = BluesRegex().Match(bagPull).Value;
				var redsMatch = RedsRegex().Match(bagPull).Value;
				var greensMatch = GreensRegex().Match(bagPull).Value;
				
				var blues = string.IsNullOrWhiteSpace(bluesMatch) ? 0 : int.Parse(bluesMatch);
				var reds = string.IsNullOrWhiteSpace(redsMatch) ? 0 : int.Parse(redsMatch);
				var greens = string.IsNullOrWhiteSpace(greensMatch) ? 0 : int.Parse(greensMatch);
				
				if (blues > highestBlueInGame) highestBlueInGame = blues;
				if (reds > highestRedInGame) highestRedInGame = reds;
				if (greens > highestGreenInGame) highestGreenInGame = greens;
			}
			
			var power = highestBlueInGame * highestRedInGame * highestGreenInGame;
			sumOfPower += power;
		}
		
		return sumOfPower.ToString();
	}

	[GeneratedRegex(@"\d*(?= blue)")]
    private static partial Regex BluesRegex();
    
    [GeneratedRegex(@"\d*(?= red)")]
    private static partial Regex RedsRegex();
    
    [GeneratedRegex(@"\d*(?= green)")]
    private static partial Regex GreensRegex();

    [GeneratedRegex(@"(?<=Game )\d*")]
    private static partial Regex GameIdRegex();
}
