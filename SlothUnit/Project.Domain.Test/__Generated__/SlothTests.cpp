#include "SlothTests.h"
#include "TestClass.h"
#include "../CodeGenerationShould/CalculatorShould.h"
#include <unordered_map>
#include <functional>

void voidFunction() {}

void SlothTests::Run()
{
	typedef std::function<void()> TestFunction;
	auto testClassX = TestClass<CalculatorShould>("SuperPath/CalculatorShould.h",
												  "CalculatorShould",
												  CalculatorShould(),
												  {
													  { "return_four_when_adding_two_plus_two", &CalculatorShould::return_four_when_adding_two_plus_two },
													  { "a_method_with_an_annotation", &CalculatorShould::a_method_with_an_annotation }
												  });

//	std::unordered_map<std::string, testFunction> testMethods;
//	CalculatorShould calc;
//	testMethods.emplace("setup", std::bind(&voidFunction));
//	testMethods.emplace("return_four_when_adding_two_plus_two", std::bind(&CalculatorShould::return_four_when_adding_two_plus_two, calc));
//	testMethods.emplace("a_method_with_an_annotation", std::bind(&CalculatorShould::a_method_with_an_annotation, calc));
//
//	std::unordered_map<std::string, std::shared_ptr<CalculatorShould>> testClasses;
//	testClasses.emplace("CalculatorShould", std::make_shared<CalculatorShould>());
}
