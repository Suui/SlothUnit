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
		for (auto testRunnable : TestRunnables)
		{
			testRunnable->Run();
		}

		std::string sloth =
"      `\"\"==,,__\n\
        `\"==..__\"=..__ _    _..-==\"\"_\n\
             .-,`\"=/ /\ \\\"\"/_)==\"\"``\n\
            ( (    | | | \\/ |\n\
             \\ '.  |  \\;  \\ /\n\
              |  \\ |   |   ||\n\
         ,-._.'  |_|   |   ||\n\
        .\\_/\\     -'   ;   Y\n\
       |  `  |        /    |-.\n\
       '. __/_    _.-'     /'\n\
              `'-.._____.-'\n\
All green!\n";

		std::cout << sloth << std::endl;
	}
}
