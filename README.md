# SlothUnit

SlothUnit is a C++ unit testing framework relying on reflection, implemented using code generation. It aims to be really simple to use, expressive and easy to setup. More than anything, trying to be really familiar to your usual C++ code, so you are not required to study it in-depth to be able to start using it fluently (no learning curve whatsoever!).


## Current status

As of today, SlothUnit is still starting out. Only available in VS, there are just a few simple assertions for bools, strings, integers and floats. But it may be used for fun on some katas or in pretty small projects where you don't need more than that :)


## Setup

### Usage

1. Download the files within [the latest release](https://github.com/Suui/SlothUnit/releases).
2. Install the VS SlothUnit Test Project template.
    - Simply move the `SlothUnit Test Project.zip` into `C:\Users\{your user}\Documents\{your VS version}\Templates\ProjectTemplates`.
3. Extract the contents of `SlothUnit.zip` inside the solution folder.
    - For the project template to work, you must have a folder named `SlothUnit` inside your solution folder.
    - The configuration that depends on this will be in the Pre-Build Events for the project.
4. Create a new SlothUnit Test Project and start writing tests :)

### Development

1. Simply fork and clone the repo, the whole project is inside a VS solution.


## Writing and executing tests

An example of a test would be:

```cpp
#include "../SlothUnit/SlothUnit.h"

using namespace SlothUnit;

class HappinessShould
{
public:

    Test()
    void be_over_nine_thousand()
    {
        Expect(Happiness().Level()).ToBe(9999);
    }
};
```
**The include path has to be absolute instead of using the VS additional includes** since the compilation with Clang wouldn't find this VS shortcuts. At least for now it's that way.

To execute, simply remember to have your SlothUnit Test Project as the StartUp project, build and run/debug :)


