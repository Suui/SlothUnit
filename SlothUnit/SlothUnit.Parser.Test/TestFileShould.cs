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
		public void be_retrieved_from_a_header_file_when_it_has_test_methods_inside_a_class()
		{
			const string headerFilePath = @"E:\Projects\CPP\SlothUnit\SlothUnit\ProjectDomainTest\TestFileShould.h";
			
			var testFile = new SlothParser().TryGetTestFileFrom(headerFilePath);
			
			testFile.Name.Should().Be("TestFileShould.h");
			testFile.Path.Should().Be(@"E:\Projects\CPP\SlothUnit\SlothUnit\ProjectDomainTest\" + "TestFileShould.h");
		}
	}
}
