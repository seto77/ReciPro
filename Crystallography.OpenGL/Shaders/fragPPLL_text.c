#version 410 core
// //#version 130
// #version 410 core // (260319Ch) Dedicated text fragment shader for PPLL overlay
// (260320Ch) AMD drivers reject any trailing tokens/comments after #version and require an immediate newline.

// Depth cueing
uniform bool DepthCueing = false;
uniform float Far = -2.5;
uniform float Near = 0.5;
uniform vec3 BgColor = vec3(1, 1, 1);
uniform vec2 ViewportSize;

uniform sampler2D Texture;
uniform sampler2D DepthTexture;
uniform bool UseAnchorVisibility = false;

in float fZ;//Depth
in vec2 fUv;//texture coordinates
flat in vec4 fColor;
flat in vec2 fAnchorPixel;
flat in float fAnchorDepth;

layout(location = 0) out vec4 FragColor;

vec3 applyDepthCueing(vec3 c3)
{
	if (DepthCueing)
	{
		float x = clamp((Near - fZ) / (Near - Far), 0, 1);
		return mix(c3, BgColor, x);
	}
	return c3;
}

bool isAnchorVisible()
{
	if (!UseAnchorVisibility)
		return true;

	// (260319Ch) Per-fragment depth test: sample textDepthTex at this fragment's own screen pixel
	// instead of the pre-computed anchor pixel. gl_FragCoord.z is the actual rasterized depth,
	// which is uniform across all fragments of one label quad (gl_Position.z = biasedClipZ for all 4 vertices).
	// fAnchorPixel / fAnchorDepth left in place but no longer used here.
	ivec2 texel = ivec2(floor(gl_FragCoord.xy));
	float sceneDepth = texelFetch(DepthTexture, texel, 0).r;
	return gl_FragCoord.z <= sceneDepth + 2.5e-5; // (260319Ch) Discard fragments behind opaque geometry
}

void main()
{
	if (!isAnchorVisible())
		discard; // (260319Ch) Whole-label visibility is decided once at the anchor point instead of clipping per fragment

	vec4 c = texture(Texture, fUv); // (260319Ch) Keep bitmap RGB so the white outline stays visible in PPLL mode
	if (c.a <= 0.001)
		discard; // (260319Ch) Transparent texels in the text quad must not tint the PPLL result

	vec3 rgb = applyDepthCueing(c.rgb); // (260319Ch) Use the bitmap RGBA directly instead of recoloring in the shader
	FragColor = vec4(rgb, c.a);
}
