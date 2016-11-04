#include "SlothTests.h"
#include "TestClass.h"
#include "../CodeGenerationShould/CalculatorShould.h"
#include <unordered_map>

TestClass<CalculatorShould> calculatorShould = TestClass<CalculatorShould>("SuperPath/CalculatorShould.h",
											   "CalculatorShould",
											   CalculatorShould(),
											   {
											       { "return_four_when_adding_two_plus_two", &CalculatorShould::return_four_when_adding_two_plus_two },
											       { "a_method_with_an_annotation", &CalculatorShould::a_method_with_an_annotation }
											   });

void SlothTests::Run()
{
	
}
