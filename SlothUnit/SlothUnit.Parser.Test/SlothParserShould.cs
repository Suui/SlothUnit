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
			var rootDir = Path.Combine(SolutionDir, Path.Combine(TestProjectDir, @"SlothParserShould"));
			var slothParser = new SlothParser();

			var testFiles = slothParser.RetrieveTestFilesFrom(rootDir);

			testFiles.Count.Should().Be(6);
		}
	}
}
