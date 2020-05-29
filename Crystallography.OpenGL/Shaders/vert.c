#version 150
//Sellers, Graham.OpenGL Superbible : Comprehensive Tutorial and Reference(Kindle, No.15531 - 15557).Pearson Education.Kindle version.

// Per-vertex inputs

/*layout(location = 1) */in int ObjType;
/*layout(location = 2) */in int Argb;
/*layout(location = 3) */in vec3 Position;
/*layout(location = 4) */in vec3 Normal;
/*layout(location = 5) */in vec2 Uv;

uniform mat4 ObjectMatrix; //Object matrix
uniform mat4 WorldMatrix; //world matrix
uniform mat4 ViewMatrix; // view matrix
uniform mat4 ProjMatrix; // projection matrix
uniform vec3 LightPosition; // Position of light
uniform vec3 EyePosition;// Position of eye
uniform vec2 ViewportSize;// Viewport Size

// A, B, C, and D for Ax + By + Cz + D = 0
uniform vec4 ClipPlanes[8];
uniform int ClipNum = 8;
float gl_ClipDistance[8];

uniform int FixedArgb;
uniform bool UseFixedArgb = false;

// Inputs from vertex shader
out VertexData
{
	float WithTexture;//-1: without texture, +1: with texture. 	
	vec3 Normal;//Normal direction
	vec3 Light;//Light direction
	vec3 View;//View direction
	vec4 Color;//Color
	float Z;//Depth
	vec2 Uv;//texture coordinates
	
} vs_out;

void main(void)
{
	// Calculate view-space coordinate

	vec3 position = vec3 (ObjectMatrix * vec4(Position, 1));

	vec4 P;
	if (ObjType == 0)
	{
		P = WorldMatrix * vec4(position, 1);
		// Calculate the clip-space position of each vertex
		gl_Position = ProjMatrix * ViewMatrix * P;
	}
	else
	{
		P = WorldMatrix * vec4(Normal, 1) + vec4(0, 0, position.z, 0);
		// Calculate the clip-space position of each vertex
		vec4 P2 = ProjMatrix * ViewMatrix * P;
		gl_Position = P2 + vec4(position.x / ViewportSize.x * 2, position.y / ViewportSize.y * 2, 0, 0) * abs(P2.w);
	}

	// Calculate normal vector in view-space
	vs_out.Normal = mat3(WorldMatrix) * mat3(ObjectMatrix) * Normal;

	// Calculate light vector
	vs_out.Light = LightPosition - P.xyz;

	// Calculate view vector
	vs_out.View = EyePosition - P.xyz;

	//Z depth
	vs_out.Z = P.z;

	//Copy color
	if (UseFixedArgb)
		vs_out.Color = vec4((FixedArgb >> 16 & 0xff) / 255.0, (FixedArgb >> 8 & 0xff) / 255.0, (FixedArgb & 0xff) / 255.0, (FixedArgb >> 24 & 0xff) / 255.0);
	else
		vs_out.Color = vec4((Argb >> 16 & 0xff) / 255.0, (Argb >> 8 & 0xff) / 255.0, (Argb & 0xff) / 255.0, (Argb >> 24 & 0xff) / 255.0);

	// Calculate the clip-space position of each vertex
	for (int i = 0; i < ClipNum; i++)
	{
		gl_ClipDistance[i] = dot(vec4(position, 1), ClipPlanes[i]);
	}

	vs_out.Uv = Uv;

	if (ObjType == 0)
		vs_out.WithTexture = -1;
	else
		vs_out.WithTexture = 1;
}

