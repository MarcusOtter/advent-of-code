using Microsoft.Extensions.Configuration;
using PuzzleSolvers;
using PuzzleSolvers.Day01;

namespace PuzzleRunnerConsole;

public class Program
{
	public static async Task Main()
	{
		var date = DateTime.Now;
		var sessionToken = GetSessionToken();
		var inputFetcher = new PuzzleInputFetcher(sessionToken);
		var input = await inputFetcher.FetchPuzzleInputAsync(date);
		
		// TODO: Get dynamically like last year :)
		var solver = new PuzzleSolver01();
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
