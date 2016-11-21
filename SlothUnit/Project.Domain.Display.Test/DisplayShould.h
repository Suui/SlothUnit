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

	Test()
	void show_a_failing_test()
	{
		auto calculator = Calculator();
		Expect(calculator.Add(2, 2)).ToBe(1);
	}

	Test()
	void not_show_a_passing_boolean_test()
	{
		Expect(true).ToBeTrue();
		Expect(false).ToBeFalse();
	}

	Test()
	void show_a_failing_boolean_test()
	{
		Expect(true).ToBeFalse();
	}

	Test()
	void show_another_failing_boolean_test()
	{
		Expect(false).ToBeTrue();
	}
};
