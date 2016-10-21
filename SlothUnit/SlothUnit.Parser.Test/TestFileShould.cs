using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnitParser;


/* TODO
	- Get test method, its name, line, class and fileName
*/

namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class TestFileShould : FileSystemTest
	{
		[Test]
		public void be_retrieved_from_a_header_file()
		{
			const string fileName = "TestFileShould.h";
			var headerFilePath = Path.Combine(TestProjectDir, fileName);
			
			var testFile = new SlothParser().TryGetTestFileFrom(headerFilePath);
			
			testFile.Name.Should().Be(fileName);
			testFile.Path.Should().Be(headerFilePath);
		}
	}
}
