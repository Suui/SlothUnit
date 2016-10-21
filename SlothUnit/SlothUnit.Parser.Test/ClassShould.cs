using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnitParser;


/* TODO
	
*/

namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class ClassShould : FileSystemTest
	{
		[Test]
		public void be_found_in_file()
		{
			var filePath = Path.Combine(TestProjectDir, "ClassShould.h");

			var testFile = new SlothParser().TryGetTestFileFrom(filePath);

			testFile.TestClasses.Single().Name.Should().Be("ClassShould");
		}

		[Test]
		public void only_be_retrieved_if_it_contains_test_methods()
		{
			var filePath = Path.Combine(TestProjectDir, "ClassShould.h");

			var testFile = new SlothParser().TryGetTestFileFrom(filePath);

			testFile.TestClasses.Single().Name.Should().NotBe("ClassWithoutTestMethods");
		}

		[Test]
		public void be_aware_of_its_()
		{
			
		}
	}
}
