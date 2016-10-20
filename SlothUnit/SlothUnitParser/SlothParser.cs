using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class SlothParser
	{
		public static List<TestFile> RetrieveTestFilesIn(string rootDirectory)
		{
			return new List<TestFile>();
		}

		public TestFile TryGetTestFileFrom(string filePath)
		{
			var clangWrapper = new ClangWrapper();
			var classesInFile = clangWrapper.GetClassCursorsIn(filePath);

			var testFile = new TestFile("")
			{
				TestClasses = new TestClasses()
			};
			testFile.TestClasses.Add(new TestClass
			{
				Name = clang.getCursorSpelling(classesInFile.Single()).ToString()
			});

			clangWrapper.Dispose();
			return testFile;
		}
	}
}