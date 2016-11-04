﻿#pragma once
#include "../../SlothUnit/SlothUnit.h"
#include <unordered_map>
#include <iostream>

using namespace SlothUnit;

template<class T>
class TestClass
{
public:

	std::string Path;
	std::string Name;
	std::unordered_map<std::string, TestFunction> TestMethods;

	typedef void (T::*TestMethod)();
	TestClass(const std::string& path, const std::string& name, T testClass, std::unordered_map<std::string, TestMethod> testFunctions)
	{
		Path = path;
		Name = name;
		for (auto testFunction : testFunctions)
		{
			TestMethods.emplace(testFunction.first, std::bind(testFunction.second, testClass));
		}
	}
};
