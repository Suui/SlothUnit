using System.Collections.Generic;
using System.Linq;


namespace SlothUnitParser
{
	public class TestClasses
	{
		private List<TestClass> Classes { get; } = new List<TestClass>();

		public int Count => Classes.Count;

		public void Add(TestClass testClass) => Classes.Add(testClass);

		public TestClass Single() => Classes.Single();
	}
}