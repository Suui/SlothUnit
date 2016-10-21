using System;
using System.Collections.Generic;
using System.Linq;
using ClangSharp;


namespace SlothUnitParser
{
	public class ClangWrapper
	{
		private string[] Arguments { get; } = { "-x", "c++", "-std=c++11", "-D__SLOTH_UNIT_PARSER__" };
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

		public static string GetCursorName(CXCursor cursor)
		{
			return clang.getCursorSpelling(cursor).ToString();
		}

		public List<CXCursor> GetTestMethodsIn(CXCursor cxCursor)
		{
			var testMethodCursors = new List<CXCursor>();

			CXCursorVisitor visitor = (cursor, parent, data) =>
			{
				if (cursor.kind == CXCursorKind.CXCursor_CXXMethod)
				{
					CXCursorVisitor methodVisitor = (cursorX, parentX, dataX) =>
					{
						if (cursorX.kind == CXCursorKind.CXCursor_AnnotateAttr)
						{
							var properties = GetCursorName(cursorX).Split(',').ToList();

							if (properties.Contains("Test"))
								testMethodCursors.Add(cursorX);
						}

						return CXChildVisitResult.CXChildVisit_Continue;
					};

					clang.visitChildren(cursor, methodVisitor, new CXClientData());
				}

				return CXChildVisitResult.CXChildVisit_Continue;
			};

			clang.visitChildren(cxCursor, visitor, new CXClientData());
			return testMethodCursors;
		}

		public static int GetCursorLine(CXCursor cursor)
		{
			CXFile file;
			uint line, column, offset;
			clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
			return Convert.ToInt32(line);
		}
	}
}