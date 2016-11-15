#pragma once
#include <unordered_map>
#include <functional>

namespace SlothUnit
{
	class TestRunnable
	{
	protected:

		std::string path;
		std::string name;
		typedef std::function<void()> TestFunction;
		std::unordered_map<std::string, TestFunction> testFunctions;

	public:

		virtual ~TestRunnable() {}

		virtual void Run() = 0;

		virtual std::string Path() = 0;

		virtual std::string Name() = 0;
	};
}
