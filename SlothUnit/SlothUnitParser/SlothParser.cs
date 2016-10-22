namespace SlothUnitParser
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			var clangWrapper = ClangWrapper.For(filePath);

			var testClasses = clangWrapper.RetrieveTestClasses();

			return TestFile.BuildFrom(filePath, testClasses);
		}
	}
}