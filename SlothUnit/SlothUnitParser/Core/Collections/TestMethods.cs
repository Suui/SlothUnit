using System.Collections.Generic;
using SlothUnitParser.Core.Elements;


namespace SlothUnitParser.Core.Collections
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