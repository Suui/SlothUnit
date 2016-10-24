namespace SlothUnitParser.Infrastructure
{
	public class StringHelper
	{
		public static string GetFileNameFrom(string path)
		{
			var lastSlashIndex = path.LastIndexOf('\\');
			return path.Substring(lastSlashIndex + 1);
		}
	}
}