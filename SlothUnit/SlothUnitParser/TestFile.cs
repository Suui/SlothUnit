namespace SlothUnitParser
{
	public class TestFile
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public TestClasses TestClasses { get; }

		public TestFile(string filePath)
		{
			Name = GetFileNameFrom(filePath);
			Path = filePath;
			TestClasses = new TestClasses();
		}

		public void AddClass(TestClass testClass) => TestClasses.Add(testClass);

		private string GetFileNameFrom(string filePath)
		{
			var lastSlashIndex = filePath.LastIndexOf('\\');
			return filePath.Substring(lastSlashIndex + 1);
		}
	}
}