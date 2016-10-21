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
	}
}
