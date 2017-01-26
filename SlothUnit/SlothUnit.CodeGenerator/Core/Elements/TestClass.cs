using System.Linq;
using ClangSharp;
using SlothUnit.Parser.Core.Collections;


namespace SlothUnit.Parser.Core.Elements
{
	public class TestClass
	{
		public string Path { get; }
		public string Name { get; }
		public int Line { get; }
		public TestMethods TestMethods { get; }

		public static TestClass BuildFrom(ClangWrapper clangWrapper, CXCursor classCursor)
		{
			var path = clangWrapper.GetCursorFilePath(classCursor);
			var name = clangWrapper.GetCursorName(classCursor);
			var line = clangWrapper.GetCursorLine(classCursor);
			var testMethods = clangWrapper.RetrieveTestMethodsIn(classCursor);
			if (testMethods.Any())
				return new TestClass(path, name, line, new TestMethods(testMethods));

			return new Class();
		}

		private TestClass(string path, string name, int line, TestMethods testMethods)
		{
			Path = path;
			Name = name;
			Line = line;
			TestMethods = testMethods;
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