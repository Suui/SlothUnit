using System;
using ClangSharp;


namespace SlothUnitParser
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Parsing the code...");

			const string filePath = @"E:\Projects\CPP\SlothUnit\SlothUnit\ProjectDomainTest\CalculatorShould.h";
			CXUnsavedFile unsavedFile;

			var index = clang.createIndex(0, 0);
			var arguments = new[] { "-x", "c++", "-std=c++11", "-D__SLOTH_UNIT_PARSER__" };

			var translationUnit = clang.createTranslationUnitFromSourceFile(index, filePath, arguments.Length, arguments, 0, out unsavedFile);
			var cursor = clang.getTranslationUnitCursor(translationUnit);

			var macroExpansionVisitor = new MacroExpansionVisitor();
			clang.visitChildren(cursor, PrettyPrint, new CXClientData(IntPtr.Zero));

			clang.disposeTranslationUnit(translationUnit);
			clang.disposeIndex(index);
		}

		private static CXChildVisitResult PrettyPrint(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_ClassDecl)
			{
				Console.WriteLine(clang.getCursorSpelling(cursor).ToString());
				clang.visitChildren(cursor, PrintClassChildren, new CXClientData(IntPtr.Zero));
			}

			return CXChildVisitResult.CXChildVisit_Continue;
		}

		private static CXChildVisitResult PrintClassChildren(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_CXXMethod)
			{
				Console.WriteLine(clang.getCursorSpelling(cursor).ToString());
				clang.visitChildren(cursor, PrintFunctionChildren, new CXClientData(IntPtr.Zero));
			}

			return CXChildVisitResult.CXChildVisit_Continue;
		}

		private static CXChildVisitResult PrintFunctionChildren(CXCursor cursor, CXCursor parent, IntPtr client_data)
		{
			if (cursor.kind == CXCursorKind.CXCursor_AnnotateAttr)
			{
				var propertyString = clang.getCursorSpelling(cursor).ToString();
				Console.WriteLine(propertyString);
			}

			return CXChildVisitResult.CXChildVisit_Continue;
		}
	}

	internal class MacroExpansionVisitor : Visitor
	{
		public CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr data)
		{
			CXFile file;
			uint line, column, offset;

			if (IsInSystemHeader(cursor))
			{
				return CXChildVisitResult.CXChildVisit_Continue;
			}

			var cursorKind = clang.getCursorKind(cursor);
//			clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
//			Console.WriteLine(cursorKind + ": " + " @ " + clang.getFileName(file) + ": " + line);

			if (cursorKind == CXCursorKind.CXCursor_MacroExpansion)
			{
				var macroName = clang.getCursorSpelling(cursor).ToString();
				if (macroName == "Test")
				{
					clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
					Console.WriteLine(cursorKind + ": " + macroName + " @ " + clang.getFileName(file) + ": " + line);
				}
				return CXChildVisitResult.CXChildVisit_Recurse;
			}
			if (cursorKind == CXCursorKind.CXCursor_CXXMethod)
			{
				var functionName = clang.getCursorSpelling(cursor).ToString();
				clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
				Console.WriteLine(cursorKind + ": " + functionName + " @ " + clang.getFileName(file) + ": " + line);

				var semanticParent = clang.getCursorSemanticParent(cursor);
				Console.WriteLine("method semantic parent: " + clang.getCursorSpelling(semanticParent));
				return CXChildVisitResult.CXChildVisit_Recurse;
			}

			if (cursorKind == CXCursorKind.CXCursor_AnnotateAttr)
			{
				var annotationAttributes = clang.getCursorSpelling(cursor).ToString();
				clang.getExpansionLocation(clang.getCursorLocation(cursor), out file, out line, out column, out offset);
				Console.WriteLine(cursorKind + ": " + annotationAttributes + " @ " + clang.getFileName(file) + ": " + line);

				return CXChildVisitResult.CXChildVisit_Recurse;
			}

			return CXChildVisitResult.CXChildVisit_Recurse;
		}

		private static bool IsInSystemHeader(CXCursor cursor)
		{
			return clang.Location_isInSystemHeader(clang.getCursorLocation(cursor)) != 0;
		}
	}

	internal interface Visitor
	{
		CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr data);
	}
}
