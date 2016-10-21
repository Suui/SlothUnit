using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			var testFile = new TestFile(filePath);

			var clangWrapper = new ClangWrapper();
			var classesInFile = clangWrapper.GetClassCursorsIn(filePath);

			foreach (var classCursor in classesInFile)
			{
				var testMethodsInClass = clangWrapper.GetTestMethodsIn(classCursor);
				if (testMethodsInClass.Any())
					testFile.AddClass(TestClass.BuildFrom(classCursor));
			}

			clangWrapper.Dispose();
			return testFile;
		}

		private bool ContainsTestMethods(CXCursor @class)
		{
			throw new System.NotImplementedException();
		}
	}
}