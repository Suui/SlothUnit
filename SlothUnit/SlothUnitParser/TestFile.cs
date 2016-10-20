using System.Collections.Generic;
using System.Linq;


namespace SlothUnitParser
{
	public class TestFile
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public TestClasses TestClasses { get; }

		public TestFile()
		{
			TestClasses = new TestClasses();
		}

		public void AddClass(TestClass testClass) => TestClasses.Add(testClass);
	}

	public class TestClass
	{
		public string Name { get; }

		public TestClass(string name)
		{
			Name = name;
		}
	}

	public class TestClasses
	{
		private List<TestClass> Classes { get; } = new List<TestClass>();

		public void Add(TestClass testClass) => Classes.Add(testClass);

		public TestClass Single() => Classes.Single();
	}
}