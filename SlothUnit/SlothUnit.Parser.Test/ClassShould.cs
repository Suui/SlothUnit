using System;
using System.IO;
using System.Linq;
using ClangSharp;
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
		public void be_built_from_a_class_cursor()
		{
			var filePath = Path.Combine(TestProjectDir, "ClassShould.h");
			var classCursor = new ClangWrapper().GetClassCursorsIn(filePath).First();

			var testClass = TestClass.BuildFrom(classCursor);

			testClass.Cursor.Should().Be(classCursor);
			testClass.Path.Should().Be(filePath);
			testClass.Name.Should().Be("ClassShould");
			testClass.Line.Should().Be(6);
		}
	}
}
