#pragma once
#include "TestRunnable.h"
#include <unordered_map>

namespace SlothUnit
{
	template<class T>
	class TestClass : public TestRunnable
	{
	public:

		typedef void (T::*TestMethod)();
		TestClass(const std::string& path, const std::string& name, T testClass, std::unordered_map<std::string, TestMethod> testMethods)
		{
			this->path = path;
			this->name = name;
			for (auto& testMethod : testMethods)
			{
				testFunctions.emplace(testMethod.first, std::bind(testMethod.second, testClass));
			}
		}

		void Run() override
		{
			for (auto& testFunction : testFunctions)
			{
				testFunction.second();
			}
		}

		std::string Path() override { return path; }

		std::string Name() override { return name; }
	};
}
