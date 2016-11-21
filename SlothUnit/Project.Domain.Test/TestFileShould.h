#pragma once
#include "../SlothUnit/SlothUnit.h"
#include "IncludedClass.h"

using namespace SlothUnit;

class TestFileShould
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
