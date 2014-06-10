ModGL
=====

Modern OpenGL wrapper for .NET

Initialization (Windows)
========================

```csharp
var context = ContextFactory.Instance.Create(new ContextCreationParameters { Window = hwnd, Device = hdc });
var gl = context.CreateInterface<IOpenGL33>();
```

Usage
=====

ModGL mirrors OpenGL through the different interfaces, but it also contains an object orientation layer for handling
vertex buffers, vertex arrays, index buffers, arrays and shaders.

```csharp
while(true)
{
    gl.Clear(0, 0, 0, 0);
    // Draw here
    context.SwapBuffers();
}
```

What ModGL is not
=================
* Game engine
* DevIL wrapper
* OpenCL wrapper

Basically ModGL is strictly a OpenGL wrapper and utility library. Any other handy libraries should be separate NuGet packages.

What's missing
==============
* Currently a lot of math functionality is missing or poorly implemented.
* OpenGL enumerations are lacking in a lot of functions.
* Linux and OS X support.
 
Linux and OS X is supposed to be supported in the future. Platform.Invoke should already support both so it's a matter of writing the Context for GLX and whatever Cocoa uses.
