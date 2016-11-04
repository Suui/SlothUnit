#pragma once
#include "Runnable.h"
#include <vector>
#include <iostream>

class TestRepository
{
	static std::vector<Runnable> TestClasses;

public:

	template<class T>
	static void Register(Runnable testClass)
	{
		TestClasses.push_back(testClass);
		std::cout << TestClasses[TestClasses.size() - 1].Name << std::endl;
	}
};
