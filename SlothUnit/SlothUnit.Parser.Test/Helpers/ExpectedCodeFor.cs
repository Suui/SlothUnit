using System.IO;


namespace SlothUnit.Parser.Test.Helpers
{
	public class ExpectedCodeFor : FileSystemTest
	{
		public static string TheMainFile =
@"#include <SlothUnit.h>
#include ""__Tests__.generated.h""

using namespace SlothUnit;

int main()
{
	SlothTests::ExecuteAll();
	return 0;
}
";

		public static string AClassWithASingleTestMethod =
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