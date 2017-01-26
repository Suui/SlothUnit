using System.Linq;
using ClangSharp;
using SlothUnit.CodeGenerator.Core.Collections;


namespace SlothUnit.CodeGenerator.Core.Elements
{
	public class TestMethod
	{
		public string Path { get; }
		public string Name { get; }
		public int Line { get; }
		public TestProperties TestProperties { get; }

		public static TestMethod BuildFrom(ClangWrapper clangWrapper, CXCursor methodCursor)
		{
			var path = clangWrapper.GetCursorFilePath(methodCursor);
			var name = clangWrapper.GetCursorName(methodCursor);
			var line = clangWrapper.GetCursorLine(methodCursor);
			var testProperties = clangWrapper.RetrieveTestPropertiesIn(methodCursor);
			if (testProperties.Any())
				return new TestMethod(path, name, line, new TestProperties(testProperties));

			return new Method();
		}

		private TestMethod(string path, string name, int line, TestProperties testProperties)
		{
			Path = path;
			Name = name;
			Line = line;
			TestProperties = testProperties;
		}

		protected TestMethod() {}
	}

	public class Method : TestMethod
	{
		public static bool IsTestMethod(TestMethod testMethod)
		{
			return testMethod.GetType() == typeof(TestMethod);
		}
	}
}