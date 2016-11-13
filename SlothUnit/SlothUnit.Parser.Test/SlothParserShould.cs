using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.Parser.Core;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	public class SlothParserShould : FileSystemTest
	{
		[Test]
		public void retrieve_all_the_test_files_in_the_root_directory()
		{
			var rootPath = Path.Combine(SolutionPath, Path.Combine(TestProjectPath, @"SlothParserShould"));

			var testFiles = SlothParser.RetrieveTestFilesIn(rootPath);

			testFiles.Count.Should().Be(6);
		}
	}
}
