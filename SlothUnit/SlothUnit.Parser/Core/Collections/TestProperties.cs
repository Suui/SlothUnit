using System.Collections.Generic;
using SlothUnit.Parser.Core.Elements;


namespace SlothUnit.Parser.Core.Collections
{
	public class TestProperties
	{
		private List<TestProperty> Properties { get; }

		public TestProperties(List<TestProperty> testProperties)
		{
			Properties = testProperties;
		}
	}
}