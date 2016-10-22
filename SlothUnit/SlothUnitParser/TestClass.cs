using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class TestClass
	{
		public CXCursor Cursor { get; }
		public string Path { get; }
		public string Name { get; }
		public int Line { get; }

		public static TestClass BuildFrom(CXCursor classCursor, ClangWrapper clangWrapper)
		{
			var path = ClangWrapper.GetCursorFilePath(classCursor);
			var name = ClangWrapper.GetCursorName(classCursor);
			var line = ClangWrapper.GetCursorLine(classCursor);
			var testMethods = clangWrapper.GetTestMethodsIn(classCursor);
			if (testMethods.Any())
				return new TestClass(classCursor, path, name, line);

			return new Class();
		}

		private TestClass(CXCursor cursor, string path, string name, int line)
		{
			Cursor = cursor;
			Path = path;
			Name = name;
			Line = line;
		}

		protected TestClass() {}
	}

	public class Class : TestClass
	{
		public static bool IsTestClass(TestClass testClass)
		{
			return testClass.GetType() == typeof(TestClass);
		}
	}
}