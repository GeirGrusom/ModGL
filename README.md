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