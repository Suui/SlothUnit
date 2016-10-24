using System.Collections.Generic;
using SlothUnitParser.Core.Elements;


namespace SlothUnitParser.Core.Collections
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