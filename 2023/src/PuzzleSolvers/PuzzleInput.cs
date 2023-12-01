namespace PuzzleSolvers;

public class PuzzleInput(string raw)
{
	public string Raw { get; } = raw;
	public string[] Lines => Raw.Split("\n");
}
