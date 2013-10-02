#version 330

in vec3 position;
in vec3 normal;
in vec3 lightPos;
out vec4 output;

void main()
{
    float lightIntensity = dot(normal, normalize(normalize(position - lightPos)));

    output = vec4(lightIntensity, lightIntensity, lightIntensity, 1);
}