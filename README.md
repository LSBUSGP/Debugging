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

To demonstrate this feature, open the script called `DeliberateMistakes.cs` in the folder `ScriptErrorsAndWarnings` then change the first word of the script from `using` to `usong`. This is an example of a simple typo. What does Unity show when it detects this error?

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

### Experiment

How many different error codes can you create? What code error do you need to create that error?

### Warnings

As well as errors, Unity will also warn you about code which is legal but probably not what you want. For example, if you add this code to your `Start` function:

```cs
        if (false)
        {
            Debug.Log("This is a deliberate mistake");
        }
```

This generates warning code CS0162:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/e7955e39-ee58-4226-990a-9248cefa4aef)

https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs0162

Unlike errors, you may still run your projects even with warnings in the scripts. However, it is good practice to investigate the warnings and what they mean as they usually mean you are trying to do something you shouldn't.

## Runtime errors and warnings

Open the scene `RuntimeError` in the folder `RuntimeErrorsAndWarnings`. This has a `Sphere` object and an empty `Target` object. The `Sphere` object has the script `RuntimeError` attached:
```cs
using UnityEngine;

public class RuntimeError : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
```

Although there are no errors in this script, if you press the run button, you'll see a stream of errors appear in the `Console` window.

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/f9e371ef-4d41-43f4-87ff-911d60a00623)

This is one of the most common errors that you will see, and what it indicates is that a variable (in this case `target`) is supposed to be pointing to a component, but it hasn't been set to a component in the Unity editor.

A single click on one of these error messages, highlights the object that caused the message (in this case, the `Sphere` object), and double clicking on the message, opens the editor at the line in the script where the error was detected.

One thing to note about Runtime errors is that lines of code after the error do not get executed. You can test this by adding a line `Debug.Log("Update sphere");` at the end of the `Update` function. You'll notice that the message does not appear no matter how many times the function is called.
