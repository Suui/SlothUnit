using System.Linq;


namespace SlothUnitParser
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			var testFile = new TestFile(filePath);

			var clangWrapper = new ClangWrapper();
			var classesInFile = clangWrapper.GetClassCursorsIn(filePath);

			if (classesInFile.Any())
			{
				var className = ClangWrapper.GetCursorName(classesInFile.Single());
				testFile.AddClass(new TestClass(className));
			}

			clangWrapper.Dispose();
			return testFile;
		}
	}
}