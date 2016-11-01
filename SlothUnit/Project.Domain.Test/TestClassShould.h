#pragma once
#include "../SlothUnit/SlothUnit.h"
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
