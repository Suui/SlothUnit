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
			const string filePath = @"E:\Projects\CPP\SlothUnit\SlothUnit\ProjectDomainTest\ClassShould.h";

			var testFile = new SlothParser().TryGetTestFileFrom(filePath);

			testFile.TestClasses.Single().Name.Should().Be("ClassShould");
		}
	}
}
