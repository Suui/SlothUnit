using System.Collections.Generic;
using SlothUnit.CodeGenerator.Core.Elements;


namespace SlothUnit.CodeGenerator.Core.Collections
{
	public class TestProperties
	{
		private List<TestProperty> Properties { get; }

		public TestProperties(List<TestProperty> testProperties)
		{
			Properties = testProperties;
		}

		public List<TestProperty> ToList() => Properties;
	}
}