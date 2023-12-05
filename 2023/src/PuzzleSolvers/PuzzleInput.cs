namespace PuzzleSolvers;

public class PuzzleInput(string raw)
{
	public string Raw { get; } = raw;
	public string[] Lines => Raw.Replace("\r", "").Split("\n");
	public char[][] Grid => Lines.Select(l => l.Select(c => c).ToArray()).ToArray();
}
