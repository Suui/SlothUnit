namespace SlothUnitParser
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			return TestFile.BuildFrom(filePath);
		}
	}
}