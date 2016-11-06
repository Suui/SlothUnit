#pragma once
#include "Exports.h"
#include "SlothTests.h"
#include "TestClass.h"
#include <memory>

namespace SlothUnit
{
	class SLOTHUNIT_API TestRegistrar
	{
	public:

		template<class T>
		explicit TestRegistrar(TestClass<T> testClass)
		{
			std::shared_ptr<TestRunnable> testRunnable = std::make_shared<TestClass<T>>(testClass);
			SlothTests::Register(testRunnable);
		}
	};
}
