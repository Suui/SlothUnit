#include "SlothTests.h"

namespace SlothUnit
{
	std::vector<std::shared_ptr<TestRunnable>> SlothTests::TestRunnables = std::vector<std::shared_ptr<TestRunnable>>();

	void SlothTests::Register(std::shared_ptr<TestRunnable>& testRunnable)
	{
		TestRunnables.push_back(testRunnable);
	}

	void SlothTests::ExecuteAll()
	{
		for (auto testRunnable : TestRunnables)
		{
			testRunnable->Run();
		}
	}
}
