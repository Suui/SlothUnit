using System.Collections.Generic;
using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class TestFile
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public TestClasses TestClasses { get; }

		public TestFile(string filePath)
		{
			Name = GetFileNameFrom(filePath);
			Path = filePath;
			TestClasses = new TestClasses();
		}

		public void AddClass(TestClass testClass) => TestClasses.Add(testClass);

		private string GetFileNameFrom(string filePath)
		{
			var lastSlashIndex = filePath.LastIndexOf('\\');
			return filePath.Substring(lastSlashIndex + 1);
		}
	}

	public class TestClass
	{
		public CXCursor Cursor { get; }
		public string Path { get; }
		public string Name { get; }
		public int Line { get; }

		public TestClass() {}

		public TestClass(string name)
		{
			Name = name;
		}

		private TestClass(CXCursor cursor, string path, string name, int line)
		{
			Cursor = cursor;
			Path = path;
			Name = name;
			Line = line;
		}

		public static TestClass BuildFrom(CXCursor classCursor)
		{
			var path = ClangWrapper.GetCursorFilePath(classCursor);
			var name = ClangWrapper.GetCursorName(classCursor);
			var line = ClangWrapper.GetCursorLine(classCursor);
			return new TestClass(classCursor, path, name, line);
		}
	}


	public class TestClasses
	{
		private List<TestClass> Classes { get; } = new List<TestClass>();

		public int Count => Classes.Count;

		public void Add(TestClass testClass) => Classes.Add(testClass);

		public TestClass Single() => Classes.Single();
	}
}