namespace PuzzleSolvers;

public class PuzzleInput(string raw)
{
	public string Raw { get; } = raw;
	public string[] Lines => Raw.Replace("\r", "").Split("\n");
	public string[][] Grid => Lines.Select(l => l.Select(c => c.ToString()).ToArray()).ToArray();
}
