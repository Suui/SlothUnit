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
	public class ClassShould
	{
		[Test]
		public void be_found_in_file()
		{
			var solutionDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
			var testProjectDir = Path.Combine(solutionDir, "ProjectDomainTest");
			var filePath = Path.Combine(testProjectDir, "ClassShould.h");

			var testFile = new SlothParser().TryGetTestFileFrom(filePath);

			testFile.TestClasses.Single().Name.Should().Be("ClassShould");
		}
	}
}
