using System;
using System.Collections.Generic;
using System.Linq;
using SlothUnit.Parser.Core.Elements;


namespace SlothUnit.Parser.Core.Collections
{
	public class TestFiles
	{
		private List<TestFile> Files { get; }
		public int Count => Files.Count;

		public TestFiles(List<TestFile> testFiles)
		{
			Files = testFiles;
		}

		public TestFile First() => Files.First();

		public TestFile Single(Func<TestFile, bool> function) => Files.Single(function);
	}
}