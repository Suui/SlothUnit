using System.IO;
using System.Linq;
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
		[Test]
		public void retrieve_the_name_of_a_cursor()
		{
			var filePath = Path.Combine(TestProjectDir, "ClangWrapperShould.h");
			var clangWrapper = new ClangWrapper();
			var classCursor = clangWrapper.GetClassCursorsIn(filePath).Single();

			ClangWrapper.GetCursorName(classCursor).Should().Be("ClangWrapperShould");
		}

		[Test]
		public void retrieve_the_line_of_a_cursor()
		{
			var filePath = Path.Combine(TestProjectDir, "ClangWrapperShould.h");
			var clangWrapper = new ClangWrapper();
			var classCursor = clangWrapper.GetClassCursorsIn(filePath).Single();

			ClangWrapper.GetCursorLine(classCursor).Should().Be(6);
		}

		[Test]
		public void retrieve_the_filepath_for_the_file_a_cursor_is_in()
		{
			var filePath = Path.Combine(TestProjectDir, "ClangWrapperShould.h");
			var clangWrapper = new ClangWrapper();
			var classCursor = clangWrapper.GetClassCursorsIn(filePath).Single();

			ClangWrapper.GetCursorFilePath(classCursor).Should().Be(filePath);
		}
	}
}
