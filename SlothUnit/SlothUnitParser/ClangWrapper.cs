using System.Collections.Generic;
using ClangSharp;


namespace SlothUnitParser
{
	public class ClangWrapper
	{
		private string[] Arguments { get; }= { "-x", "c++", "-std=c++11", "-D__SLOTH_UNIT_PARSER__" };
		private CXIndex Index { get; set; }
		private CXTranslationUnit TranslationUnit { get; set; }

		public CXCursor GetCursorForFile(string filePath)
		{
			CXUnsavedFile unsavedFile;
			Index = clang.createIndex(0, 0);
			TranslationUnit = clang.createTranslationUnitFromSourceFile(Index, filePath, Arguments.Length, Arguments, 0, out unsavedFile);
			return clang.getTranslationUnitCursor(TranslationUnit);
		}

		public void Dispose()
		{
			clang.disposeTranslationUnit(TranslationUnit);
			clang.disposeIndex(Index);
		}

		public List<CXCursor> GetClassCursorsIn(string filePath)
		{
			var classCursors = new List<CXCursor>();

			CXCursorVisitor visitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
					classCursors.Add(cursor);

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(GetCursorForFile(filePath), visitor, new CXClientData());
			return classCursors;
		}
	}
}