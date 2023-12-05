namespace PuzzleSolvers;

public static class StringHelpers
{
	public static IEnumerable<int> AllIndicesOf(this string str, string value)
	{
		var indices = new List<int>();
		if (string.IsNullOrEmpty(value))
		{
			return indices;
		}

		var startIndex = 0;
		// ReSharper disable once StringIndexOfIsCultureSpecific.2
		while ((startIndex = str.IndexOf(value, startIndex)) != -1)
		{
			indices.Add(startIndex);
			startIndex += value.Length;
		}

		return indices;
	}
}
