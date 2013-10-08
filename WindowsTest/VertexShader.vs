#version 330
in vec3 Position;
in vec3 Normal;
in vec2 TexCoord;
in vec3 Tangent;
in vec3 BiTangent;

uniform mat4 World;
uniform mat4 View;
uniform mat4 WorldViewProjection;
uniform mat4 ViewProjection;
uniform mat4 WorldView;
uniform mat4 Projection;
uniform vec3 Light;

out vec3 lightDirection;
out vec3 eyeDirection;
out vec3 normal;
out vec3 position;
out vec2 texCoord;

void main()
{
	vec4 pos = vec4(Position, 1) * (WorldViewProjection);
	
	vec3 eye = normalize((vec4(0, 0, 1, 1) * (ViewProjection)).xyz);
	
	mat3 tangentMatrix = transpose(mat3(
        (vec4(Tangent, 1) * WorldView).xyz,
        (vec4(BiTangent, 1) * WorldView).xyz,
        (vec4(Normal, 1) * WorldView).xyz
    ));

	vec3 lightPos = (vec4(Light, 1) * WorldView).xyz;

	lightDirection = normalize(lightPos - position) * tangentMatrix;
	eyeDirection = eye * tangentMatrix;
	normal = Normal;

	texCoord = TexCoord;
	
    gl_Position = pos;	
}