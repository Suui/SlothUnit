#include "SlothTests.h"
#include <unordered_map>
#include <memory>
#include <iostream>
#include "../CodeGenerationShould/CalculatorShould.h"

void SlothTests::Run()
{
	std::unordered_map<std::string, void (CalculatorShould::*)()> testMethods;
	testMethods.emplace("return_four_when_adding_two_plus_two", &CalculatorShould::return_four_when_adding_two_plus_two);
	testMethods.emplace("a_method_with_an_annotation", &CalculatorShould::a_method_with_an_annotation);

	std::unordered_map<std::string, std::shared_ptr<CalculatorShould>> testClasses;
	testClasses.emplace("CalculatorShould", std::make_shared<CalculatorShould>());

	for (auto& testClass : testClasses)
	{
		std::cout << testClass.first << std::endl;
		for (auto& testMethod : testMethods)
		{
			std::cout << testMethod.first << std::endl;
			(testClass.second.get()->*testMethod.second)();
		}
	}
}
