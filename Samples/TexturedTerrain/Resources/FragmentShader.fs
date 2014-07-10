#version 150

out vec4 Color;
in vec3 normal;
in vec3 pos;

uniform vec4 DiffuseColor;

void main()
{
    vec3 light = vec3(4, 4, -4);
    float intensity = min(max(-dot((light - pos), normal), 0.0) /  (length(-pos)*4), 1.0);
    Color = vec4(DiffuseColor.x * intensity, DiffuseColor.y * intensity, DiffuseColor.z * intensity, 1);
}