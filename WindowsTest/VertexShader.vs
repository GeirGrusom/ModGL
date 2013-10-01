#version 330
in vec3 Position;
in vec3 Normal;
in vec2 TexCoord;

uniform mat4 World;
uniform mat4 View;
uniform mat4 Projection;
uniform vec3 Light;

out vec3 lightPos;
out vec3 normal;
out vec3 position;
out vec2 texCoord;

void main()
{
	
	vec4 pos = vec4(Position, 1) * (World * View * Projection);
	position = pos.xyz;
	lightPos = Light;

	normal = Normal;
	texCoord = TexCoord;
	
    gl_Position = pos;	
}