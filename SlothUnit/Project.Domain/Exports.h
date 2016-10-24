#pragma once

#ifdef PROJECTDOMAIN_EXPORTS
#define PROJECTDOMAIN_API __declspec(dllexport)
#else
#define PROJECTDOMAIN_API __declspec(dllimport)
#endif
