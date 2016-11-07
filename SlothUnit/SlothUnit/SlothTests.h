#pragma once
#include "Exports.h"
#include "TestClass.h"
#include <memory>

namespace SlothUnit
{
	class SlothTests
	{
		static std::vector<std::shared_ptr<TestRunnable>> TestRunnables;

	public:

		SLOTHUNIT_API static void Register(std::shared_ptr<TestRunnable>& testRunnable);

		SLOTHUNIT_API static void ExecuteAll();
	};
}
