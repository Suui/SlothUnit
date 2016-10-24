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
			var testClass = TestFile.TestClasses.Single();

			testClass.TestMethods.Count.Should().Be(2);
			testClass.TestMethods[0].Name.Should().Be("test_method");
			testClass.TestMethods[0].Path.Should().Be(FilePath);
			testClass.TestMethods[0].Line.Should().Be(11);

			testClass.TestMethods[1].Name.Should().Be("another_test_method");
			testClass.TestMethods[1].Path.Should().Be(FilePath);
			testClass.TestMethods[1].Line.Should().Be(16);
		}
	}
}
