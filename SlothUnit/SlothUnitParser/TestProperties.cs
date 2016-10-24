using System.Collections.Generic;


namespace SlothUnitParser
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