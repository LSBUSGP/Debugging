# Debugging

A tutorial on Unity debugging techniques.

## Project setup

First create a new project. You can create any type, I'm creating a standard 3D project and calling it `Debugging`.

If you are using Visual Studio Code, you need to update the Visual Studio package in the `Package Manager`. If you are using Visual Studio, you might not need to perform this step. First click on the `Visual Studio Editor` package, then click on the `Unlock` button.

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/c2c730f4-d469-4a22-91a7-2c14b5bfa3f0)

Then click on `Version History` and then on the `Update` button.

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/d91c7409-24d8-48a0-a2ce-7e933ecfb09b)

If you are opening Visual Studio or Visual Studio Code for the first time, everything should work. But if you previously opened either editor, before updating the package, you might need to reset the project files. To do this, open `Preferences...` from the `Edit` menu.

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/c7edf5df-8c30-48e0-8002-c6670859ee2a)

Select `External Tools` from the list on the left, and `Visual Studio` or `Visual Studio Code` as the `External Script Editor` and finally click the `Regenerate project files` button.

## Script Errors and Warnings

The most efficient way to debug your software is to catch them before they get into your game. Unity will automatically catch and report errors and warnings in your code before you even run the program. As soon as your program code is compiled within Unity, the `Console` window will show any errors or warnings in the code along with an error code and the line number containing the error. Double clicking on the error line in the `Console` window should open the editor and highlight the line where the error is.

To demonstrate this feature, create a new Script called `DeliberateMistakes.cs` then change the first word of the script from `using` to `usong`. This is an example of a simple typo. What does Unity show when it detects this error?

```cs
usong System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliberateMistakes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/0026e7e1-b618-4430-b463-8aed9e50ab07)

Note: you only see these messages if you have the error filter switched on. The messages don't identify the correct cause of the error. It generates several additional spurious errors that have nothing to do with the mistake. If you have any errors (as opposed to warnings) you will not be able to run or build the project in the Unity editor.

You can usually use the error code to look up what the error means by searching the error code. In this case, the error code is `CS10003`:

https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs1003

