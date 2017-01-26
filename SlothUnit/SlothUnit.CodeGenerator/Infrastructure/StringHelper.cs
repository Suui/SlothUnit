using System.Linq;


namespace SlothUnit.CodeGenerator.Infrastructure
{
	public class StringHelper
	{
		public static string GetFileNameFrom(string path)
		{
			var lastSlashIndex = path.LastIndexOf('\\');
			return path.Substring(lastSlashIndex + 1);
		}

		public static string CppPathFor(string csharpPath)
		{
			return csharpPath.Replace(@"\", @"\\");
		}

		public static string RemoveTrailingSlashIn(string path)
		{
			return path.Last() == '\\' ? path.Substring(0, path.Length - 1) : path;
		}

		public static string RemoveSurroundingQuotesIn(string path)
		{
			return path.Trim('"');
		}
	}
}