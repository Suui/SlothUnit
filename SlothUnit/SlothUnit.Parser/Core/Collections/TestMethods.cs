using System.Collections.Generic;
using SlothUnit.Parser.Core.Elements;


namespace SlothUnit.Parser.Core.Collections
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