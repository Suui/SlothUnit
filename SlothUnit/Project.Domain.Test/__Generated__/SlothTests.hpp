#pragma once

#include "../../SlothUnit/SlothUnit.h"
#include <vector>
#include <memory>

namespace SlothUnit
{
	class SlothTests
	{
		static std::vector<std::shared_ptr<TestRunnable>> TestRunnables;

	public:

		static void Register(std::shared_ptr<TestRunnable>& testRunnable)
		{
			TestRunnables.push_back(testRunnable);
		}

		static void ExecuteAll()
		{
			for (auto testRunnable : TestRunnables)
			{
				testRunnable->Run();
			}
		}
	};

	std::vector<std::shared_ptr<TestRunnable>> SlothTests::TestRunnables = std::vector<std::shared_ptr<TestRunnable>>();

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
}
