#pragma once

#include "Exports.h"
#include "TestClass.h"
#include <memory>

namespace SlothUnit
{
	class SLOTHUNIT_API SlothTests
	{
		static std::vector<std::shared_ptr<TestRunnable>> TestRunnables;

	public:

		static void Register(std::shared_ptr<TestRunnable>& testRunnable);

		static void ExecuteAll();
	};
}
