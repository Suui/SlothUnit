using System.Linq;


namespace SlothUnitParser
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			var clangWrapper = new ClangWrapper();
			var classesInFile = clangWrapper.GetClassCursorsIn(filePath);
			var className = ClangWrapper.GetCursorName(classesInFile.Single());

			var testFile = new TestFile();
			testFile.AddClass(new TestClass(className));

			clangWrapper.Dispose();
			return testFile;
		}
	}
}