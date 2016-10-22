namespace SlothUnitParser
{
	public class TestFile
	{
		public string Name { get; }
		public string Path { get; }
		public TestClasses TestClasses { get; }

		public static TestFile BuildFrom(string filePath)
		{
			var path = filePath;
			var name = StringHelper.GetFileNameFrom(filePath);
			var testClasses = new TestClasses(new ClangWrapper().GetTestClassesIn(filePath));
			if (testClasses.Any())
				return new TestFile(path, name, testClasses);

			return new NoTestFile();
		}

		private TestFile(string path, string name, TestClasses testClasses)
		{
			Path = path;
			Name = name;
			TestClasses = testClasses;
		}

		protected TestFile() {}
	}

	public class NoTestFile : TestFile
	{
	}
}