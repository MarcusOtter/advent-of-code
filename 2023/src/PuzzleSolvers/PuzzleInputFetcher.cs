
namespace PuzzleSolvers;

public class PuzzleInputFetcher(string sessionToken)
{
	private static readonly HttpClient HttpClient = new();
	
	// TODO: Would be nice to store the input so we don't need to get a session token every time, can check there first
	public async Task<PuzzleInput> FetchPuzzleInputAsync(DateTime date, CancellationToken ct = default)
	{
		if (date.Month != 12) throw new ArgumentOutOfRangeException(nameof(date), "Must be December");
		
		var year = date.Year;
		var dayOfMonth = date.Day;
		var url = $"https://adventofcode.com/{year}/day/{dayOfMonth}/input";

		var request = new HttpRequestMessage(HttpMethod.Get, url);
		request.Headers.Add("Cookie", $"session={sessionToken}");

		var response = await HttpClient.SendAsync(request, ct);
		response.EnsureSuccessStatusCode();

		var puzzleInputRaw =  await response.Content.ReadAsStringAsync(ct);
		return new PuzzleInput(puzzleInputRaw);
	}
}
