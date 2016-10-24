using System.Collections.Generic;


namespace SlothUnitParser
{
	public class TestMethods
	{
		private List<TestMethod> Methods { get; }
		public int Count => Methods.Count;

		public TestMethods(List<TestMethod> testMethods)
		{
			Methods = testMethods;
		}

		public TestMethod this[int index] => Methods[index];
	}
}