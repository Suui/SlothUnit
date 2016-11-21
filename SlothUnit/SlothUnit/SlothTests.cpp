#include "SlothTests.h"
#include <string>

namespace SlothUnit
{
	std::vector<std::shared_ptr<TestRunnable>> SlothTests::TestRunnables = std::vector<std::shared_ptr<TestRunnable>>();

	void SlothTests::Register(std::shared_ptr<TestRunnable>& testRunnable)
	{
		TestRunnables.push_back(testRunnable);
	}

	void SlothTests::ExecuteAll()
	{
		auto errorFlag = false;
		for (auto testRunnable : TestRunnables)
		{
			try
			{
				testRunnable->Run();
			}
			catch (AssertionException exception)
			{
				system("color 0C");
				errorFlag = true;
				std::cout << testRunnable->Name() << std::endl << exception.what() << std::endl;
			}
		}

		if (!errorFlag)
		{
			std::cout << "All green!" << std::endl;
			system("color 0A");
		}
	}
}
