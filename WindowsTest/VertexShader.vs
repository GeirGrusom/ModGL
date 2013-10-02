#version 330
in vec3 Position;
in vec3 Normal;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;
uniform vec3 Light;

out vec3 lightPos;
out vec3 normal;
out vec3 position;

void main()
{
	
	vec4 pos = vec4(Position, 1) * (World * View * Projection);
	position = pos.xyz;
	lightPos = Light;

	normal = Normal;
	
    gl_Position = pos;	
}