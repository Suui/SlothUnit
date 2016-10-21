using System;
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
	public class TestFileShould
	{
		[Test]
		public void be_retrieved_from_a_header_file()
		{
			var solutionDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
			var testProjectDir = Path.Combine(solutionDir, "ProjectDomainTest");
			var headerFilePath = Path.Combine(testProjectDir, "TestFileShould.h");
			
			var testFile = new SlothParser().TryGetTestFileFrom(headerFilePath);
			
			testFile.Name.Should().Be("TestFileShould.h");
			testFile.Path.Should().Be(headerFilePath);
		}
	}
}
