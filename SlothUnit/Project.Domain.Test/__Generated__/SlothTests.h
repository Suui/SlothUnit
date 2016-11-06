#pragma once
#include "TestRunnable.h"
#include <vector>
#include <memory>

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
