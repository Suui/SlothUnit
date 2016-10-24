namespace SlothUnitParser
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			var clangWrapper = ClangWrapper.For(filePath);

			var testFile = TestFile.BuildFrom(filePath, clangWrapper);

			clangWrapper.Dispose();
			return testFile;
		}
	}
}