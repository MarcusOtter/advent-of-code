namespace PuzzleSolvers;

public static class StringHelpers
{
	private const string Digits = "0123456789";
	
	public static bool IsDigit(this char c)
	{
		return Digits.Contains(c);
	}
	
}
