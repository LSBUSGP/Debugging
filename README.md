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

Can you fix this error?

### Warnings

Open the scene `RuntimeWarning` in the folder `RuntimeErrorsAndWarnings`. This scene contains a single cube with a `BoxCollider`. If you run this scene, you will see a runtime warning in the `Console` window:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/b3b15036-463b-4efe-9d2d-89addbf04304)

Again, click on the warning message highlights the object that generated the warning. In this case there is no code to open as the problem is in the `Size` parameters on the `Box Collider` in the Unity Editor.

### Custom messages

As well as built in errors and warnings, you can add your own with `Debug.LogWarning` and `Debug.LogError`. Open the scene `CustomErrorsAndWarnings` and run it. It has two empty game objects `CustomError` and `CustomWarning`. Each has a script, `CustomError.cs` and `CustomWarning.cs` respectively.

```cs
using UnityEngine;

public class CustomError : MonoBehaviour
{
    void Start()
    {
        Debug.LogError("This is a custom error message.", gameObject);
    }
}
```

```cs
using UnityEngine;

public class CustomWarning : MonoBehaviour
{
    void Start()
    {
        Debug.LogWarning("This is a custom warning message.", gameObject);
    }
}
```

As with other warning and error messages the object highlights in the editor when the message is clicked and the script opens when it is double clicked. Note that the `gameObject` passed as the second parameter to these messages is what indicates the object so you can (if you want to) have other objects highlight in response to the message. Note also that custom error messages do not stop execution like the built in errors.

## Unity inspector debug mode

Open the scene `InspectorDebug` in the folder `EditorDebugging`. This project has a single empty `GameObject` object with the `HiddenData.cs` script on it.

```cs
using UnityEngine;

public class HiddenData : MonoBehaviour
{
    public int visibleValue = 1;
    int hiddenValue = 0;

    void Update()
    {
        hiddenValue++;
        visibleValue = hiddenValue;
    }
}
```

Running this project, you will see that the `Visible Value` variable in the script is increasing in the `Inspector` window, but the `hiddenValue` is not shown. You can view this as well by switching the `Inspector` view into `Debug` mode. If you right click the `Inspector` tab, you can choose `Debug` mode from the context menu:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/92c1c223-8f88-405b-93b6-f4b37b042fce)

Now you will see normally hidden additional information about your script:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/5308ae60-688f-4165-b480-cf909e26cb03)

This feature can also be useful in debugging plugins and built in components as well as you own scripts. Note, if you customized the editor view for your object, this option will ignore that and just show the raw data.

## Custom profiling

Sometimes the bugs you have don't produce any errors, but they still don't work how you want. And sometimes it can be difficult to identify exactly where the code is going wrong. Unity contains a profiler window which allows you to trace out the amount of time spent dealing with each function in your project. But you can customize it to trace out any data so long as it's positive number. First, let's open the `Profiler` window and see how it works. You can open the profiler window by clicking on the `Window` menu, then `Analysis`, then `Profiler`. Here is a typical trace:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/3e094495-ce7d-49f4-8b59-d5543661fda2)

To customise this window, we first need to install the package `Unity Profiling Core` from the `Unity Registry`:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/1135e930-24b4-4ef5-9a00-d5eae23e9b05)

Click on the `Packages` pull down box, and choose `Unity Registry`. Then find `Unity Profiling Core` and click the `Install` button.

Now open the `CustomProfiler` scene from the `CustomProfiler` folder. This scene has one sphere object with the `ProfileMovement` script on it:

```cs
using Unity.Profiling;
using UnityEngine;

public class ProfileMovement : MonoBehaviour
{
    public float speed = 5f;
    public float smoothTime = 1f;
    float velocity = 0f;

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        Vector3 position = transform.position;
        float input = Input.GetAxis("Horizontal");
        float target = position.x + input * speed;
        position.x = Mathf.SmoothDamp(position.x, target, ref velocity, smoothTime);
        transform.position = position;
    }
}
```

If you run this scene, you will see that you can move the sphere smoothly with the left and right arrow keys or the `A` and `D` keys. The bug here is subtle and occurs when you let go of the input key.

To get a better idea of what is going on here, we can add a custom profile trace to the profiler window. First add this line to the top of the class:

```cs
    public static readonly ProfilerCounter<float> SpeedCounter = new ProfilerCounter<float>(ProfilerCategory.Scripts, "Speed", ProfilerMarkerDataUnit.Count);
```

Then add this line to the bottom of the `Update` function:

```cs
        SpeedCounter.Sample(Mathf.Abs(velocity));
```

Now go to the `Profiler` window and select the `Profiler Modules` menu. Untick all of the unwanted categories. Then at the bottom you should see a cog. Click that to add a new section:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/787df04f-38b9-4a44-a950-f6264a7a6508)

Click the `Add` button, then enter the name of your module (I've used `Movement`) then select the `User` and `Scripts` sections. Any new counters you define should appear here. Double click your counter to add it to the counters to be displayed in the module. Finally click on the `Save Changes` button to apply your changes.

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/1a4cec64-25f2-489f-afd8-d5c221dec73b)

Now run the project and watch the output trace:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/b59398fa-5497-4d73-b31d-35b6bc1e000f)

Here the trace on the left is when I move from left to right and stop, then second is when I move from right to left and stop.

Now we know exactly what the output looks like we can try digging into the cause.

Let's add another trace, this time for the input. Add another counter:

```cs
    public static readonly ProfilerCounter<float> InputCounter = new ProfilerCounter<float>(ProfilerCategory.Scripts, "Input", ProfilerMarkerDataUnit.Count);
```

And another sampple:

```cs
        InputCounter.Sample(Mathf.Abs(input));
```

And configure this in the `Profiler` window.

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/3949dc56-2337-4cca-959b-105f85dd5c93)

Now we can see that the green speed line get clamped suddenly to zero when the blue input line hits zero. This seems to always happen when going from right left, but occassionally also happens when going from left to right.

## Visual Studio debugger

Open the scene `VisualStudioDebugging` in the folder `VisualStudioDebugging`. This scene contains the same object and script as before, but now I have found the source code to the `SmoothDamp` function and included it in the project. If you run the project you'll see that is exhibits the same sudden stop issue.

```cs
using UnityEngine;

public class VisualStudioDebugCode : MonoBehaviour
{
    // from https://issuetracker.unity3d.com/issues/smoothdamp-behaves-differently-between-positive-and-negative-velocity
    static float SmoothDampUnity(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        // Based on Game Programming Gems 4 Chapter 1.10
        smoothTime = Mathf.Max(0.0001F, smoothTime);
        float omega = 2F / smoothTime;

        float x = omega * deltaTime;
        float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
        float change = current - target;
        float originalTo = target;

        // Clamp maximum speed
        float maxChange = maxSpeed * smoothTime;
        change = Mathf.Clamp(change, -maxChange, maxChange);
        target = current - change;

        float temp = (currentVelocity + omega * change) * deltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        float output = target + (change + temp) * exp;

        // Prevent overshooting
        if (originalTo - current > 0.0F == output > originalTo)
        {
            output = originalTo;
            currentVelocity = (output - originalTo) / deltaTime;
        }

        return output;
    }

    public float speed = 5f;
    public float smoothTime = 1f;
    float velocity = 0f;

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        Vector3 position = transform.position;
        float input = Input.GetAxis("Horizontal");
        float target = position.x + input * speed;
        position.x = SmoothDampUnity(position.x, target, ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);
        transform.position = position;
    }
}
```

We can debug into this using the Visual Studio debugger. In `Visual Studio Code` select the debug section and choose `Attach to Unity`:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/18805a55-1f03-4bd5-aabb-f0334422cbc9)

If you are using `Visual Studio` the options will be similar but under different menus. When you have successfully attached to Unity, it will open this box:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/38dae78b-b1e3-4b2c-9829-36cbea38a524)

Select the `Enable` button.

Now you can stop the execution of your Unity project at any line of your program and examine what is going on. To do this you need to set a breakpoint. To set a breakpoint, select the line you want to examine and press `F9`. Then run your project in Unity and the execution should stop and the line you selected.

At this point, Unity will stop responding and the current instruction should be highlighted in your program:

![image](https://github.com/LSBUSGP/Debugging/assets/3679392/9ca2cef3-ff92-44e9-beab-27ec0c7c4c1d)

You can view the contents of any variable at this point by hovering over it. You can step to the next instruction with `F10`, into a function with `F11` and so on. You can find the debugger control mapping here: https://code.visualstudio.com/Docs/editor/debugging#_debug-actions

We can even introduce code to help us track down the point at which the failure occurs. If we add this condition just before calling `SmoothDampUnity`:

```cs
        if (velocity < 0f && input == 0f)
        {
            Debug.Log("Break");
        }
```

And put a breakpoint on the break log message, the debugger will stop exactly on the frame where the error occurs. We can then step through line by line to see what is going on. Once we know what the problem is we can start to think of solutions.
