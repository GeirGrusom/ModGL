#version 150
in vec4 Position;
in vec4 Color;

out vec3 color;

void main()
{
    color = Color.rgb;
    gl_Position = Position;
}