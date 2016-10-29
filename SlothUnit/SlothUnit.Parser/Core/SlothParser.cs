using SlothUnit.Parser.Core.Elements;


namespace SlothUnit.Parser.Core
{
	public class SlothParser
	{
		public TestFile TryGetTestFileFrom(string filePath)
		{
			using (var clangWrapper = ClangWrapper.For(filePath))
			{
				return TestFile.BuildFrom(filePath, clangWrapper);
			}
		}
	}
}