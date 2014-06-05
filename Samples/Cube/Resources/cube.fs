#version 150

out vec4 Color;
in vec3 normal;
in vec3 pos;

void main()
{
    vec3 light = vec3(4, 4, -4);
    float intensity = max(-dot((light - pos), normal), 0.0) /  (length(-pos)*16);
    Color = vec4(intensity, intensity, intensity, 1);
}