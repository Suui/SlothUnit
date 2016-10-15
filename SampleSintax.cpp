#include <iostream>

#define Setup()
#define Test()


class BooleanShould
{
public:

	Setup()
	void initialize()
	{
		std::cout << "The initialize function has been called" << std::endl;
	}

	Test()
	void test_function()
	{
		std::cout << "The test function has been called" << std::endl;
	}
};