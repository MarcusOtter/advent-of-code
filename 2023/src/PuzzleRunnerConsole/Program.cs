using Microsoft.Extensions.Configuration;
using PuzzleSolvers;
using PuzzleSolvers.Day02;

namespace PuzzleRunnerConsole;

public class Program
{
	public static async Task Main()
	{
		var date = new DateTime(2023, 12, 02);
		var sessionToken = GetSessionToken();
		var inputFetcher = new PuzzleInputFetcher(sessionToken);
		var input = await inputFetcher.FetchPuzzleInputAsync(date);


		var demoInput = new PuzzleInput(
			"""
			467..114..
			...*......
			..35..633.
			......#...
			617*......
			.....+.58.
			..592.....
			......755.
			...$.*....
			.664.598..         
			""");

		var grid = demoInput.Grid;

		// TODO: Get dynamically like last year :)
		var solver = new PuzzleSolver02();
		var firstStar = solver.SolveFirstStar(input);
		Console.WriteLine(firstStar);

		var secondStar = solver.SolveSecondStar(input);
		Console.WriteLine(secondStar);
	}

	private static string GetSessionToken()
	{
		var config = new ConfigurationBuilder()
			.AddUserSecrets<Program>()
			.Build();

		var sessionToken = config["sessionToken"];
		if (string.IsNullOrWhiteSpace(sessionToken))
		{
			throw new InvalidOperationException("Missing sessionToken in secrets.json");
		}

		return sessionToken;
	}
}
