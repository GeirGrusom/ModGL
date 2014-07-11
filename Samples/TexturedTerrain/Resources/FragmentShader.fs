#version 150

out vec4 Color;
in vec2 texCoord;
in vec3 normal;
in vec3 pos;

uniform sampler2D Texture;
uniform vec4 DiffuseColor;

void main()
{
    vec3 light = vec3(4, 4, -4);
    float intensity = min(max(-dot((light - pos), normal), 0.0) /  (length(-pos)*4), 1.0);
    vec4 texSample = texture2D(Texture, texCoord);
    Color = mix(vec4(texSample.xyz * intensity, 1), DiffuseColor, gl_FragCoord.z * 0.7 - 0.1); //vec4(DiffuseColor.x * intensity, DiffuseColor.y * intensity, DiffuseColor.z * intensity, 1);
}