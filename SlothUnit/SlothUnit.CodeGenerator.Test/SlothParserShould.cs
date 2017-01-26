using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SlothUnit.CodeGenerator.Core;
using SlothUnit.CodeGenerator.Test.Helpers;


namespace SlothUnit.CodeGenerator.Test
{
	[TestFixture]
	public class SlothParserShould : FileSystemTest
	{
		[Test]
		public void retrieve_all_the_test_files_in_the_root_directory()
		{
			var rootPath = Path.Combine(SolutionPath, Path.Combine(TestProjectPath, "SlothParserShould"));

			var testFiles = SlothParser.RetrieveTestFilesIn(rootPath);

			testFiles.Count.Should().Be(6);
		}

		[Test]
		public void remove_double_quotes_for_paths_with_spaces()
		{
			var rootPathWithSpaces = "\"" + Path.Combine(SolutionPath, Path.Combine(TestProjectPath, "Path With Spaces")) + "\"";

			var testFiles = SlothParser.RetrieveTestFilesIn(rootPathWithSpaces);

			testFiles.Count.Should().Be(1);
		}
	}
}
