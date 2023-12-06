namespace PuzzleSolvers;

public class PuzzleInput(string raw)
{
	public string Raw { get; } = raw;
	public string[] Lines => Raw.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
	public char[][] Grid => Lines.Select(l => l.Select(c => c).ToArray()).ToArray();
	public char[][] GridRaw => Raw.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(l => l.Select(c => c).Append('\n').ToArray()).ToArray();
}
