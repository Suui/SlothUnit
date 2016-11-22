#pragma once
#include "IncludedClass.h"

using namespace SlothUnit;

class TestClassShould
{
public:

	Test()
	void test_method() {}

	void normal_method() {}

	Test()
	void another_test_method()
	{
		IncludedClass().DummyFunction();
	}
};
