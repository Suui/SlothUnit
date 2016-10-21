using System;
using System.Collections.Generic;
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

			var testClasses = classesInFile.Select(TestClass.BuildFrom)
										   .Where(Class.IsTestClass)
										   .ToList();
			testClasses.ForEach(testClass => testFile.AddClass(testClass));

			clangWrapper.Dispose();
			return testFile;
		}
	}
}