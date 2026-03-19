//#version 130
#version 410 core // (260319Ch) OpenGL 4.1 core baseline
//Sellers, Graham.OpenGL Superbible : Comprehensive Tutorial and Reference(Kindle, No.15531 - 15557).Pearson Education.Kindle version.
// Per-vertex inputs

layout(location = 0) in int vType; // (260319Ch) Explicit integer attribute
layout(location = 1) in int vArgb; // (260319Ch) Explicit integer attribute
layout(location = 2) in vec3 vPosition;
layout(location = 3) in vec3 vNormal;
layout(location = 4) in vec2 vUv;

out gl_PerVertex
{
	vec4 gl_Position;
	float gl_ClipDistance[8];
};

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

vec3 gPosition;
vec4 gWorldPosition;

vec4 unpackArgb(int argb)
{
	return vec4((argb >> 16 & 0xff) / 255.0, (argb >> 8 & 0xff) / 255.0, (argb & 0xff) / 255.0, (argb >> 24 & 0xff) / 255.0);
}

subroutine void VertexPathType();
subroutine uniform VertexPathType VertexPath;

subroutine(VertexPathType)
void renderMeshVertex()
{
	gPosition = vec3(ObjectMatrix * vec4(vPosition, 1.0));
	gWorldPosition = WorldMatrix * vec4(gPosition, 1.0);
	gl_Position = ProjMatrix * ViewMatrix * gWorldPosition;
	fNormal = mat3(WorldMatrix) * mat3(ObjectMatrix) * vNormal;
	fLight = LightPosition - gWorldPosition.xyz;
	fView = EyePosition - gWorldPosition.xyz;
	fColor = unpackArgb(UseFixedArgb ? FixedArgb : vArgb); // (260319Ch) Lighting/color work stays on the mesh path
	fZ = gWorldPosition.z;
	fWithTexture = -1.0;
}

subroutine(VertexPathType)
void renderTextVertex()
{
	gPosition = vPosition;
	// gWorldPosition = WorldMatrix * ObjectMatrix * vec4(0.0, 0.0, 0.0, 1.0) + vec4(0.0, 0.0, vPosition.z, 0.0);
	vec4 worldOrigin = WorldMatrix * ObjectMatrix * vec4(0.0, 0.0, 0.0, 1.0);
	// vec3 eyeDirection = EyePosition - worldOrigin.xyz;
	// float eyeDirectionLength = length(eyeDirection);
	// vec3 popoutDirection = eyeDirectionLength > 1.0e-6 ? eyeDirection / eyeDirectionLength : vec3(0.0, 0.0, 1.0);
	vec3 popoutDirection = normalize(transpose(mat3(ViewMatrix)) * vec3(0.0, 0.0, 1.0));
	gWorldPosition = worldOrigin + vec4(popoutDirection * vPosition.z, 0.0); // (260319Ch) Keep text popout aligned with the active view direction in both orthographic and perspective modes

	vec4 clipPosition = ProjMatrix * ViewMatrix * gWorldPosition;
	gl_Position = clipPosition + vec4(gPosition.x / ViewportSize.x * 2.0, gPosition.y / ViewportSize.y * 2.0, 0.0, 0.0) * abs(clipPosition.w);

	fNormal = vec3(0.0);
	fLight = vec3(0.0);
	fView = vec3(0.0);
	fColor = vec4(0.0);
	fZ = gWorldPosition.z;
	fWithTexture = 1.0; // (260319Ch) Text path avoids mesh-lighting setup
}

void main(void)
{
	VertexPath();

	for (int i = 0; i < ClipNum; i++)
	{
		gl_ClipDistance[i] = dot(vec4(gPosition, 1.0), ClipPlanes[i]);
	}

	fUv = vUv;
}
