#version 150 core

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
uniform vec4 BgColor = vec4(1, 1, 1, 1);
uniform bool IgnoreNormalSides = false;

//Depth Cueing
uniform bool DepthCueing = false;
uniform float Far = -2.5;
uniform float Near = 0.5;

// Input from vertex shader
in VertexData
{
	vec3 Normal;//Normal direction
	vec3 Light;//Light direction
	vec3 View;//View direction
	vec4 Color;//Color
	float Z;//Depth
} fs_in;

/*layout(location = 0) */out vec4 FragColor;

void main()
{
	// Normalize the incoming N, L, and V vectors
	vec3 normal = normalize(fs_in.Normal);
	vec3 light = normalize(fs_in.Light);
	vec3 view = normalize(fs_in.View);
	vec3 c = vec3(fs_in.Color);
	// Calculate R locally
	vec3 ref = reflect(-light, normal);

	// Compute the diffuse and specular components for each fragment
	vec3 ambient = Ambient * c;
	vec3 specular = pow(max(dot(ref, view), 0.0), SpecularPower) * Specular * SpecularColor;
	vec3 emission;
	vec3 diffuse;
	
	
	if (IgnoreNormalSides)
	{
		emission = max(abs(dot(normal, view)), 0.0) * Emission * c;
		diffuse = max(abs(dot(normal, light)), 0.0) * Diffuse * c;
	}
	else
	{
		emission = max(dot(normal, view), 0.0) * Emission * c;
		diffuse = max(dot(normal, light), 0.0) * Diffuse * c;
	}

	vec4 color = vec4(diffuse + specular + ambient + emission, fs_in.Color.a);

	if(DepthCueing)
		if (fs_in.Z < Near)
		{
			if (fs_in.Z < Far)
				color = BgColor;
			else
				color = mix(color, BgColor, (Near - fs_in.Z) / (Near - Far));
		}

	FragColor = color;// Write final color to the framebuffer
}




