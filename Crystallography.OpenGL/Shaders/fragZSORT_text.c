//#version 130
#version 410 core // (260319Ch) Dedicated text fragment shader for the ZSORT path

// layout(early_fragment_tests) in; // (260319Ch) OpenGL 4.1 / GLSL 410 safe path keeps this disabled

//Depth Cueing
uniform bool DepthCueing = false;
uniform float Far = -2.5;
uniform float Near = 0.5;
uniform vec3 BgColor = vec3(1, 1, 1);

uniform sampler2D Texture;

in float fZ;//Depth
in vec2 fUv;//texture coordinates
flat in vec4 fColor;

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

void main()
{
	// shadeTextFragment(); // (260319Ch) Legacy shared text path stays commented in fragZSORT.c for reference.
	vec4 c = texture(Texture, fUv);
	if (c.a <= 0.001)
		discard; // (260319Ch) avoid tinting the full text quad when alpha is effectively zero
	vec3 c3 = applyDepthCueing(vec3(c));
	FragColor = vec4(c3, c.a);
}
