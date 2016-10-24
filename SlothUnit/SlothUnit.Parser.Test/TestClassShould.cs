using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnitParser;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class TestClassShould : FileSystemTest
	{
		private string FilePath;
		private TestFile TestFile;

		[SetUp]
		public void given_a_test_file()
		{
			FilePath = Path.Combine(TestProjectDir, "TestClassShould.h");
			TestFile = new SlothParser().TryGetTestFileFrom(FilePath);
		}

		[Test]
		public void contain_only_the_test_methods()
		{
			
		}
	}
}
