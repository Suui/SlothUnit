using ClangSharp;


namespace SlothUnitParser
{
	public class TestClass
	{
		public CXCursor Cursor { get; }
		public string Path { get; }
		public string Name { get; }
		public int Line { get; }

		public static TestClass BuildFrom(CXCursor classCursor)
		{
			var path = ClangWrapper.GetCursorFilePath(classCursor);
			var name = ClangWrapper.GetCursorName(classCursor);
			var line = ClangWrapper.GetCursorLine(classCursor);
			return new TestClass(classCursor, path, name, line);
		}

		private TestClass(CXCursor cursor, string path, string name, int line)
		{
			Cursor = cursor;
			Path = path;
			Name = name;
			Line = line;
		}
	}
}