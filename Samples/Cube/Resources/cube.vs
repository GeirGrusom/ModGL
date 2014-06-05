#version 150

in vec3 Position;
in vec3 Normal;

uniform mat4 ModelViewProjection;
uniform mat4 ViewProjection;

out vec3 normal;
out vec3 pos;

void main()
{
	vec4 resultPos = vec4(Position, 1) * ModelViewProjection;
	gl_Position = resultPos;
	pos = resultPos.xyz;
	normal = (vec4(Normal, 1) * ViewProjection).xyz;
}