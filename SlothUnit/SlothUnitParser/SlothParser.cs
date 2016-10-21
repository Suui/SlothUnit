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

			foreach (var @class in classesInFile)
			{
				var testMethodsInClass = clangWrapper.GetTestMethodsIn(@class);
				if (testMethodsInClass.Any())
				{
					var className = ClangWrapper.GetCursorName(@class);
					testFile.AddClass(new TestClass(className));
				}
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