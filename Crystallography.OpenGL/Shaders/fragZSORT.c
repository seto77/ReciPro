#version 410 core
// //#version 130
// #version 410 core // (260319Ch) OpenGL 4.1 core baseline for ZSORT
// (260320Ch) AMD drivers reject any trailing tokens/comments after #version and require an immediate newline.

//layout(early_fragment_tests) in;
// layout(early_fragment_tests) in; // (260319Ch) OpenGL 4.1 / GLSL 410 safe path keeps this disabled

// Material properties
uniform float Emission = 0.2;
uniform float Ambient = 0.2;
uniform float Diffuse = 0.7;
uniform float Specular = 0.5;
uniform vec3 SpecularColor = vec3(1.0);
uniform float SpecularPower = 128.0;
uniform vec3 BgColor = vec3(1, 1, 1);
uniform bool IgnoreNormalSides = false;

//Depth Cueing
uniform bool DepthCueing = false;
uniform float Far = -2.5;
uniform float Near = 0.5;

uniform sampler2D Texture;

// Input from vertex shader
//in VertexData
//{
in vec3 fNormal;//Normal direction
in vec3 fLight;//Light direction
in vec3 fView;//View direction
in vec4 fColor;//Color
in float fZ;//Depth
in vec2 fUv;//texture coordinates
//} fs_in;

layout(location = 0) out vec4 FragColor; // (260319Ch) Explicit output location

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
	// FragColor = FragmentPath(); // (260319Ch) Legacy shared text/mesh path stays commented for reference.
	vec3 inColor = vec3(fColor);
	// vec3 normal = normalize(fNormal);
	float normalLengthSquared = dot(fNormal, fNormal); // (260320Ch) Lines/points can carry a zero normal, so avoid normalize(0)
	vec3 c3;
	if (normalLengthSquared <= 1.0e-12)
	{
		c3 = inColor; // (260320Ch) If no reliable normal exists, keep the requested line color without undefined lighting math
	}
	else
	{
		vec3 normal = fNormal * inversesqrt(normalLengthSquared); // (260320Ch) Safe normalization for valid surface normals
		vec3 light = normalize(fLight);
		vec3 view = normalize(fView);
		vec3 ref = reflect(-light, normal);

		vec3 ambient = Ambient * inColor;
		vec3 specular = pow(max(dot(ref, view), 0.0), SpecularPower) * Specular * SpecularColor;
		vec3 emission;
		vec3 diffuse;

		if (IgnoreNormalSides)
		{
			emission = max(abs(dot(normal, view)), 0.0) * Emission * inColor;
			diffuse = max(abs(dot(normal, light)), 0.0) * Diffuse * inColor;
		}
		else
		{
			emission = max(dot(normal, view), 0.0) * Emission * inColor;
			diffuse = max(dot(normal, light), 0.0) * Diffuse * inColor;
		}

		c3 = diffuse + specular + ambient + emission;
	}

	c3 = applyDepthCueing(c3); // (260320Ch) Zero-normal fallback should still respect depth cueing
	FragColor = vec4(c3, fColor.a); // (260319Ch) ZSORT mesh path now uses a dedicated fragment shader.
}
