#version 410 core
// //#version 130
// #version 410 core // (260319Ch) ZSORT text path uses a dedicated OpenGL 4.1 vertex shader
// (260320Ch) AMD drivers reject any trailing tokens/comments after #version and require an immediate newline.

layout(location = 0) in int vType;
layout(location = 1) in int vArgb;
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
uniform vec3 EyePosition; // Position of eye // (260319Ch) Text popout must follow the active camera direction
uniform vec2 ViewportSize;// Viewport Size
uniform int FixedArgb;
uniform bool UseFixedArgb = false;

// A, B, C, and D for Ax + By + Cz + D = 0
uniform vec4 ClipPlanes[8];
uniform int ClipNum = 8;

out float fZ;//Depth
out vec2 fUv;//texture coordinates
flat out vec4 fColor; // (260319Ch) PPLL text path can colorize from alpha without relying on texture RGB
flat out vec2 fAnchorPixel; // (260319Ch) PPLL text visibility is decided from one anchor sample instead of per-fragment depth clipping
flat out float fAnchorDepth; // (260319Ch) Anchor depth is compared against the mesh prepass depth texture in fragPPLL_text

vec4 unpackArgb(int argb)
{
	return vec4((argb >> 16 & 0xff) / 255.0, (argb >> 8 & 0xff) / 255.0, (argb & 0xff) / 255.0, (argb >> 24 & 0xff) / 255.0);
}

vec3 gPosition;

void main(void)
{
	// renderTextVertex(); // (260319Ch) Legacy shared text path stays commented in vert.c for reference.
	gPosition = vPosition;

	vec4 worldOrigin = WorldMatrix * ObjectMatrix * vec4(0.0, 0.0, 0.0, 1.0);
	// vec4 worldPosition = worldOrigin + vec4(0.0, 0.0, vPosition.z, 0.0);
	// vec3 eyeDirection = EyePosition - worldOrigin.xyz;
	// float eyeDirectionLength = length(eyeDirection);
	// vec3 popoutDirection = eyeDirectionLength > 1.0e-6 ? eyeDirection / eyeDirectionLength : vec3(0.0, 0.0, 1.0);
	vec3 popoutDirection = normalize(transpose(mat3(ViewMatrix)) * vec3(0.0, 0.0, 1.0));
	vec4 worldPosition = worldOrigin + vec4(popoutDirection * vPosition.z, 0.0); // (260319Ch) In orthographic mode the label must pop out along the active view direction

	vec4 clipPosition = ProjMatrix * ViewMatrix * worldPosition;
	float biasedClipZ = clipPosition.z - 1.0e-4 * abs(clipPosition.w); // (260319Ch) Keep anchor depth and rasterized depth on the same slightly-in-front plane
	float anchorPopout = max(vPosition.z - 0.01, 0.0); // (260319Ch) Visibility should be judged from the atom-facing attachment point, not from the label plane itself
	vec4 anchorWorldPosition = worldOrigin + vec4(popoutDirection * anchorPopout, 0.0);
	vec4 anchorClipPosition = ProjMatrix * ViewMatrix * anchorWorldPosition;
	float anchorClipZ = anchorClipPosition.z - 1.0e-4 * abs(anchorClipPosition.w); // (260319Ch) Keep the anchor test on the same slightly-front-biased surface
	vec2 anchorNdc = anchorClipPosition.xy / anchorClipPosition.w;
	fAnchorPixel = (anchorNdc * 0.5 + 0.5) * ViewportSize;
	fAnchorDepth = (anchorClipZ / anchorClipPosition.w) * 0.5 + 0.5;

	gl_Position = clipPosition + vec4(gPosition.x / ViewportSize.x * 2.0, gPosition.y / ViewportSize.y * 2.0, 0.0, 0.0) * abs(clipPosition.w);
	// gl_Position.z -= 1.0e-4 * abs(gl_Position.w);
	gl_Position.z = biasedClipZ; // (260319Ch) Use the same bias for rasterization and for the anchor-depth visibility test
	for (int i = 0; i < ClipNum; i++)
	{
		gl_ClipDistance[i] = dot(vec4(gPosition, 1.0), ClipPlanes[i]);
	}

	fZ = worldPosition.z;
	fUv = vUv;
	fColor = unpackArgb(UseFixedArgb ? FixedArgb : vArgb); // (260319Ch) Text fragment shader can use the intended label color directly
}
