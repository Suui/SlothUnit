using System.IO;


namespace SlothUnit.Parser.Test.Helpers
{
	public class ExpectedCodeFor : FileSystemTest
	{
		public static string TheMainFile =
@"#include ""../../SlothUnit/SlothUnit.h""
#include ""__Tests__.generated.h""

using namespace SlothUnit;

int main()
{
	SlothTests::ExecuteAll();
	return 0;
}
";

		public static string TheStartOfTheFile =
$@"#pragma once
#include ""{Path.Combine(CodeGenerationTestPath, "StartOfTheFile.h")}""

auto registrar_";

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

		public static string AClassWithMultipleTestMethods =
$@"#pragma once
#include ""{Path.Combine(CodeGenerationTestPath, "ClassWithMultipleTestMethods.h")}""

auto registrar = TestRegistrar(TestClass<ClassWithMultipleTestMethods>
(
	""{Path.Combine(CodeGenerationTestPath, "ClassWithMultipleTestMethods.h")}"",
	""ClassWithMultipleTestMethods"",
	ClassWithMultipleTestMethods(),
	{{
		{{ ""first_test_method"", &ClassWithMultipleTestMethods::first_test_method }},
		{{ ""second_test_method"", &ClassWithMultipleTestMethods::second_test_method }},
		{{ ""third_test_method"", &ClassWithMultipleTestMethods::third_test_method }}
	}}
));
";

		public static string AFileWithMultipleTestClasses =
$@"#pragma once
#include ""{Path.Combine(CodeGenerationTestPath, "FileWithMultipleTestClasses.h")}""

auto registrar = TestRegistrar(TestClass<FirstTestClass>
(
	""{Path.Combine(CodeGenerationTestPath, "FileWithMultipleTestClasses.h")}"",
	""FirstTestClass"",
	FirstTestClass(),
	{{
		{{ ""test_method"", &FirstTestClass::test_method }}
	}}
));

auto registrar = TestRegistrar(TestClass<SecondTestClass>
(
	""{Path.Combine(CodeGenerationTestPath, "FileWithMultipleTestClasses.h")}"",
	""SecondTestClass"",
	SecondTestClass(),
	{{
		{{ ""test_method"", &SecondTestClass::test_method }}
	}}
));
";
	}
}