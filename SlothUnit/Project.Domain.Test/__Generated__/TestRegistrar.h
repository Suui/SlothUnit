#pragma once
#include "TestRunnable.h"
#include "SlothTests.h"

class TestRegistrar
{
public:

	template<class T>
	explicit TestRegistrar(TestClass<T> testClass)
	{
		std::shared_ptr<TestRunnable> testRunnable = std::make_shared<TestClass<T>>(testClass);
		SlothTests::Register(testRunnable);
	}
};
