#version 150

in vec3 color;
out vec4 output;

void main()
{
    output = vec4(color, 1);
}