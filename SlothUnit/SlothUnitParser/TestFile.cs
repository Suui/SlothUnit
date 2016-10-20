using System.Collections.Generic;
using System.Linq;


namespace SlothUnitParser
{
	public class TestFile
	{
		public TestFile(string filePath)
		{
			
		}

		public string Name { get; set; }
		public string Path { get; set; }
		public TestClasses TestClasses { get; set; }
	}

	public class TestClass
	{
		public string Name { get; set; }
	}

	public class TestClasses
	{
		private List<TestClass> Classes { get; } = new List<TestClass>();

		public void Add(TestClass testClass) => Classes.Add(testClass);

		public TestClass Single() => Classes.Single();
	}
}