#pragma once
#include "../SlothUnit/SlothUnit.h"
#include "../Project.Domain/Calculator.h"

using namespace SlothUnit;

class DisplayShould
{
public:

	Test()
	void not_show_a_passing_test()
	{
		auto calculator = Calculator();
		Expect(calculator.Add(2, 2)).ToBe(4);
	}
};
