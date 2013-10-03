#version 330
uniform sampler2D NormalMap;

in vec3 lightDirection;
in vec3 eyeDirection;
in vec3 normal;
in vec2 texCoord;

out vec4 output;

void main()
{
    vec4 texSample = texture(NormalMap, texCoord);
    vec3 texNormal = (2 * texSample.xyz - 1);
    float lightIntensity = dot(normalize(normal + texNormal), normalize(cross(lightDirection, eyeDirection)));

    output = vec4(lightIntensity, lightIntensity, lightIntensity, 1);
}