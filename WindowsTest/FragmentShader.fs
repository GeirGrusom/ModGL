#version 330
uniform sampler2D NormalMap;

in vec3 position;
in vec3 normal;
in vec3 lightPos;
in vec2 texCoord;
out vec4 output;

void main()
{
    vec4 texSample = texture(NormalMap, texCoord);
    float lightIntensity = dot(normal, normalize(normalize(position - lightPos) + texSample.xyz));

    output = texSample + vec4(lightIntensity, lightIntensity, lightIntensity, 1);
}