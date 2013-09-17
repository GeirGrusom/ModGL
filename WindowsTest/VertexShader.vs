#version 150
in vec4 Position;
//in vec4 Color;

out vec3 color;

void main()
{
    color = vec3(0, 1, 0);
    gl_Position = Position;
}