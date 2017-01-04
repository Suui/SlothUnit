using System.Collections.Generic;
using System.IO;
using System.Linq;
using SlothUnit.Parser.Core.Collections;
using SlothUnit.Parser.Core.Elements;
using SlothUnit.Parser.Infrastructure;
using File = SlothUnit.Parser.Core.Elements.File;


namespace SlothUnit.Parser.Core
{
	public class SlothParser
	{
		public static TestFiles RetrieveTestFilesIn(string rootPath)
		{
			rootPath = StringHelper.RemoveSurroundingQuotesIn(rootPath);
			rootPath = StringHelper.RemoveTrailingSlashIn(rootPath);
			return new TestFiles(new SlothParser().RetrieveTestFilesFrom(rootPath));
		}

		public List<TestFile> RetrieveTestFilesFrom(string path)
		{
			var testFiles = Directory.GetFiles(path)
									 .Select(filePath => TryGetTestFileFrom(filePath))
									 .Where(File.IsTestFile)
									 .ToList();

			var directoryPaths = Directory.GetDirectories(path);
			foreach (var directoryPath in directoryPaths)
			{
				testFiles.AddRange(RetrieveTestFilesFrom(directoryPath));
			}

			return testFiles;
		}

		public TestFile TryGetTestFileFrom(string filePath)
		{
			using (var clangWrapper = ClangWrapper.For(filePath))
			{
				return TestFile.BuildFrom(filePath, clangWrapper);
			}
		}
	}
}