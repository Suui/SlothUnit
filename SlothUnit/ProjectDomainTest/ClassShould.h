#pragma once
#include "../SlothUnit/SlothUnit.h"
#include "IncludedClass.h"

using namespace SlothUnit;

class ClassShould
{
public:

	Test()
	void test_method()
	{
		IncludedClass().DummyFunction();
	}
};

class ClassWithoutTestMethods
{
public:

};
