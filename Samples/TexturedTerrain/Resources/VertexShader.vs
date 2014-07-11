#version 150

in vec3 Position;
in vec3 Normal;
in vec2 TexCoord;

uniform mat4 ModelViewProjection;
uniform mat4 ViewProjection;


out vec3 normal;
out vec3 pos;
out vec2 texCoord;

void main()
{
	vec4 resultPos = vec4(Position, 1) * ModelViewProjection;
	gl_Position = resultPos;
	pos = resultPos.xyz;
	normal = normalize((vec4(Normal, 1) * ViewProjection).xyz);
	texCoord = TexCoord;
}