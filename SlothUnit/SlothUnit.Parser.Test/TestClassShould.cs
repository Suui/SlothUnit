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
		public void be_found_in_file()
		{
			TestFile.TestClasses.Single().Name.Should().Be("TestClassShould");
		}

		[Test]
		public void be_built_from_a_class_cursor()
		{
			var testClass = TestFile.TestClasses.Single();

			testClass.Path.Should().Be(FilePath);
			testClass.Name.Should().Be("TestClassShould");
			testClass.Line.Should().Be(7);
		}

		[Test]
		public void only_be_retrieved_if_it_contains_test_methods()
		{
			TestFile.TestClasses.Single().Name.Should().NotBe("ClassWithoutTestMethods");
		}

		[Test]
		public void be_avoided_if_it_is_included_from_another_file()
		{
			TestFile.TestClasses.Count.Should().Be(1);
		}

		[Test]
		public void contain_only_the_test_methods()
		{
			
		}
	}
}
