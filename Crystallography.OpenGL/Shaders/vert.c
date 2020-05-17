#version 150 core

//#pragma optionNV(ifcvt none)
//#pragma optionNV(inline all)
//#pragma optionNV(strict on)
//#pragma optionNV(unroll all)

// Per-vertex inputs
/*layout(location = 2) */in vec3 Position;
/*layout(location = 3) */in vec3 Normal;
/*layout(location = 4) */in int Argb;

uniform mat4 WorldMatrix; //world matrix
uniform mat4 ViewMatrix; // view matrix
uniform mat4 ProjMatrix; // projection matrix
uniform vec3 LightPosition; // Position of light
uniform vec3 EyePosition;// Position of eye

// A, B, C, and D for Ax + By + Cz + D = 0
uniform vec4 ClipPlanes[8];
uniform int ClipNum = 8;
float gl_ClipDistance[8];

uniform int FixedArgb;
uniform bool UseFixedArgb = false;

// Inputs from vertex shader
out VertexData
{
	vec3 Normal;//Normal direction
	vec3 Light;//Light direction
	vec3 View;//View direction
	vec4 Color;//Color
	float Z;//Depth
} vs_out;

void main(void)
{
	// Calculate view-space coordinate
	vec4 P = WorldMatrix * vec4(Position, 1);

	// Calculate the clip-space position of each vertex
	gl_Position = ProjMatrix * ViewMatrix * P;

	// Calculate normal in view-space
	vs_out.Normal = mat3(WorldMatrix) * Normal;

	// Calculate light vector
	vs_out.Light = LightPosition - P.xyz;

	// Calculate view vector
	vs_out.View = EyePosition - P.xyz;

	//Z depth
	vs_out.Z = P.z;

	//Copy color
	if (UseFixedArgb)
		vs_out.Color = vec4( (FixedArgb >> 16 & 0xff)/255.0, (FixedArgb >> 8 & 0xff) / 255.0, (FixedArgb  & 0xff) / 255.0, (FixedArgb >> 24 & 0xff) / 255.0) ;
	else
		vs_out.Color = vec4((Argb >> 16 & 0xff) / 255.0, (Argb >> 8 & 0xff) / 255.0, (Argb  & 0xff) / 255.0, (Argb >> 24 & 0xff) / 255.0);

	// Calculate the clip-space position of each vertex
	for (int i = 0; i < ClipNum; i++)
	{
		gl_ClipDistance[i] = dot(vec4(Position, 1), ClipPlanes[i]);
	}

	
}
//Sellers, Graham.OpenGL Superbible : Comprehensive Tutorial and Reference(Kindle の位置No.15531 - 15557).Pearson Education.Kindle 版.