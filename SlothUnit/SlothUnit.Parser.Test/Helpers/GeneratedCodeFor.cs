using System.IO;


namespace SlothUnit.Parser.Test.Helpers
{
	public class GeneratedCodeFor : FileSystemTest
	{
		public static string ClassWithASingleTestMethod =
			$@"#pragma once
#include ""{Path.Combine(CodeGenerationTestPath, "ClassWithASingleTestMethod.h")}""

auto registrar = TestRegistrar(TestClass<ClassWithASingleTestMethod>
(
	""{Path.Combine(CodeGenerationTestPath, "ClassWithASingleTestMethod.h")}"",
	""ClassWithASingleTestMethod"",
	ClassWithASingleTestMethod(),
	{{
		{{ ""single_test_method"", &ClassWithASingleTestMethod::single_test_method }}
	}}
));
";
	}
}