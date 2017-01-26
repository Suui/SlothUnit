using System.Collections.Generic;
using System.Linq;
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

		public TestMethod Single() => Methods.Single();

		public string GeneratedCode(string className)
		{
			var generatedCode = "";
			Methods.ForEach(method => generatedCode += $@"{{ ""{method.Name}"", &{className}::{method.Name} }},
		");
			return generatedCode.Substring(0, generatedCode.LastIndexOf(','));
		}
	}
}