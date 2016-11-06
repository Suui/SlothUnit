#pragma once

#include "../CalculatorShould.h"

auto registrar = TestRegistrar(TestClass<CalculatorShould>
(
	"SuperPath/CalculatorShould.h",
	"CalculatorShould",
	CalculatorShould(),
	{
		{ "return_four_when_adding_two_plus_two", &CalculatorShould::return_four_when_adding_two_plus_two },
		{ "a_method_with_an_annotation", &CalculatorShould::a_method_with_an_annotation }
	}
));
