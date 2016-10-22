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
	class ClangWrapperShould : FileSystemTest
	{
		private CXCursor ClassCursor { get; set; }
		private string FilePath { get; set; }

		[SetUp]
		public void given_a_class_cursor_in_a_file()
		{
			FilePath = Path.Combine(TestProjectDir, "ClangWrapperShould.h");
			ClassCursor = ClangWrapper.For(FilePath).RetrieveClassCursors().Single();
		}

		[Test]
		public void retrieve_the_filepath_for_the_file_a_cursor_is_in()
		{
			ClangWrapper.GetCursorFilePath(ClassCursor).Should().Be(FilePath);
		}

		[Test]
		public void retrieve_the_name_of_a_cursor()
		{
			ClangWrapper.GetCursorName(ClassCursor).Should().Be("ClangWrapperShould");
		}

		[Test]
		public void retrieve_the_line_of_a_cursor()
		{
			ClangWrapper.GetCursorLine(ClassCursor).Should().Be(6);
		}
	}
}
