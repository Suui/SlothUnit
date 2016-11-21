#pragma once
#include "../SlothUnit/SlothUnit.h"
#include "../Project.Domain/Calculator.h"

using namespace SlothUnit;

class CalculatorShould
{
public:
	
	Test()
	void return_four_when_adding_two_plus_two()
	{
		auto calculator = Calculator();
		auto sum = calculator.Add(2, 2);

		Expect(sum).ToBe(4);
	}

	Test(ThreeAttributes)
	Test(TwoAttributes)
	Test(SampleProperty, Ignore, Explicit, Category("Integration"))
	void a_method_with_an_annotation()
	{
		
	}
};
