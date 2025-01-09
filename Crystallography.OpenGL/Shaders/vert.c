#version 130
//Sellers, Graham.OpenGL Superbible : Comprehensive Tutorial and Reference(Kindle, No.15531 - 15557).Pearson Education.Kindle version.
// Per-vertex inputs

/*layout(location = 1) */in int vObjType;
/*layout(location = 2) */in int vArgb;
/*layout(location = 3) */in vec3 vPosition;
/*layout(location = 4) */in vec3 vNormal;
/*layout(location = 5) */in vec2 vUv;

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
out float gl_ClipDistance[8];

uniform int FixedArgb;
uniform bool UseFixedArgb = false;

// Inputs from vertex shader
//out VertexData
//{
out float fWithTexture;//-1: without texture, +1: with texture. 	
out vec3 fNormal;//Normal direction
out vec3 fLight;//Light direction
out vec3 fView;//View direction
out vec4 fColor;//Color
out float fZ;//Depth
out vec2 fUv;//texture coordinates
//} vs_out;

void main(void)
{
	// Calculate view-space coordinate
	vec3 position;
	vec4 P;
	if (vObjType == 0)//Without texture
	{
		position = vec3(ObjectMatrix * vec4(vPosition, 1));
		P = WorldMatrix * vec4(position, 1);
		// Calculate the clip-space position of each vertex
		gl_Position = ProjMatrix * ViewMatrix * P;
	}
	else//case of string
	{
		position = vPosition;
		//In the case of string, ObjectMatrix always means translation operation
		P = WorldMatrix * ObjectMatrix * vec4(0, 0, 0, 1) + vec4(0, 0, vPosition.z, 0); 
		
		// Calculate the clip-space position of each vertex
		vec4 P2 = ProjMatrix * ViewMatrix * P;
		gl_Position = P2 + vec4(position.x / ViewportSize.x * 2, position.y / ViewportSize.y * 2, 0, 0) * abs(P2.w);
	}

	// Calculate normal vector in view-space
	fNormal = mat3(WorldMatrix) * mat3(ObjectMatrix) * vNormal;

	// Calculate light vector
	fLight = LightPosition - P.xyz;

	// Calculate view vector
	fView = EyePosition - P.xyz;

	//Z depth
	fZ = P.z;

	//Copy color
	int argb;
	if (UseFixedArgb || vObjType != 0)
		argb = FixedArgb;
	else
		argb = vArgb;

	fColor = vec4((argb >> 16 & 0xff) / 255.0, (argb >> 8 & 0xff) / 255.0, (argb & 0xff) / 255.0, (argb >> 24 & 0xff) / 255.0);

	// Calculate the clip-space position of each vertex
	for (int i = 0; i < ClipNum; i++)
	{
		gl_ClipDistance[i] = dot(vec4(position, 1), ClipPlanes[i]);
	}

	fUv = vUv;

	if (vObjType == 0)
		fWithTexture = -1;
	else
		fWithTexture = 1;

}

