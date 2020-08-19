#version 130
//#pragma optionNV(ifcvt none)
//#pragma optionNV(inline all)
//#pragma optionNV(strict on)
//#pragma optionNV(unroll all)

//layout(early_fragment_tests) in;

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
in float fWithTexture;//-1: without texture, +1: with texture
in vec3 fNormal;//Normal direction
in vec3 fLight;//Light direction
in vec3 fView;//View direction
in vec4 fColor;//Color
in float fZ;//Depth
in vec2 fUv;//texture coordinates
//} fs_in;

/*layout(location = 0) */out vec4 FragColor;

void main()
{
	vec3 c3;
	float a;
	//Without texture
	if (fWithTexture < 0.0)
	{
		// Normalize the incoming N, L, and V vectors
		vec3 normal = normalize(fNormal);
		vec3 light = normalize(fLight);
		vec3 view = normalize(fView);
		vec3 inColor = vec3(fColor);
		// Calculate R locally
		vec3 ref = reflect(-light, normal);

		// Compute the diffuse and specular components for each fragment
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

		c3 = vec3(diffuse + specular + ambient + emission);
		a = fColor.a;
	}
	//With texture
	else
	{
		vec4 c = texture2D(Texture, fUv);
		c3 = vec3(c);
		a = c.a;
	}

	if (DepthCueing)
	{ 
		float x = clamp((Near - fZ) / (Near - Far), 0, 1);
		c3 = mix(c3, BgColor, x);
	}

	FragColor = vec4(c3, a);// Write final color to the framebuffer
}




