using System.Linq;
using SlothUnit.CodeGenerator.Core.Collections;
using SlothUnit.CodeGenerator.Infrastructure;


namespace SlothUnit.CodeGenerator.Core.Elements
{
	public class TestFile
	{
		public string Path { get; }
		public string Name { get; }
		public TestClasses TestClasses { get; }

		public static TestFile BuildFrom(string filePath, ClangWrapper clangWrapper)
		{
			var path = filePath;
			var name = StringHelper.GetFileNameFrom(filePath);
			var testClasses = clangWrapper.RetrieveTestClasses();
			if (testClasses.Any())
				return new TestFile(path, name, new TestClasses(testClasses));

			return new File();
		}

		private TestFile(string path, string name, TestClasses testClasses)
		{
			Path = path;
			Name = name;
			TestClasses = testClasses;
		}

		protected TestFile() {}
	}

	public class File : TestFile
	{
		public static bool IsTestFile(TestFile testFile)
		{
			return testFile.GetType() == typeof(TestFile);
		}
	}
}