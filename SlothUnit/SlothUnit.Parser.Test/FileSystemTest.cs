using System;
using System.IO;


namespace SlothUnit.Parser.Test
{
	public class FileSystemTest
	{
		protected static string TestProjectName { get; } = "Project.Domain.Test";
		protected static string SolutionPath { get; } = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
		protected static string TestProjectPath { get; } = Path.Combine(SolutionPath, TestProjectName);
	}
}