using System.IO;
using System.Linq;
using ClangSharp;
using FluentAssertions;
using NUnit.Framework;
using SlothUnitParser;
using SlothUnitParser.Core;


namespace SlothUnit.Parser.Test
{
	[TestFixture]
	class ClangWrapperShould : FileSystemTest
	{
		private ClangWrapper ClangWrapper;
		private CXCursor ClassCursor;
		private string FilePath;

		[SetUp]
		public void given_a_class_cursor_in_a_file()
		{
			FilePath = Path.Combine(TestProjectDir, "ClangWrapperShould.h");
			ClangWrapper = ClangWrapper.For(FilePath);
			ClassCursor = ClangWrapper.RetrieveClassCursors().Single();
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
