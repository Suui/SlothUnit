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
			const string className = "ClassShould";
			var filePath = Path.Combine(TestProjectDir, className + ".h");

			var testFile = new SlothParser().TryGetTestFileFrom(filePath);

			testFile.TestClasses.Single().Name.Should().Be("ClassShould");
		}
	}
}
