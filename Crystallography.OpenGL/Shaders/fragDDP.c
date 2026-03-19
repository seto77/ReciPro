//#version 130
#version 410 core // (260319Ch) Dual depth peeling path for OpenGL 4.1 core

subroutine void RenderPassType();
subroutine uniform RenderPassType RenderPass;

uniform float Emission = 0.2;
uniform float Ambient = 0.2;
uniform float Diffuse = 0.7;
uniform float Specular = 0.5;
uniform vec3 SpecularColor = vec3(1.0);
uniform float SpecularPower = 128.0;
uniform vec3 BgColor = vec3(1.0, 1.0, 1.0);
uniform bool IgnoreNormalSides = false;

uniform bool DepthCueing = false;
uniform float Far = -2.5;
uniform float Near = 0.5;

uniform sampler2D Texture;
uniform sampler2D DepthBlenderTex;
uniform sampler2D FrontBlenderTex;
uniform sampler2D BackBlenderTex;

in float fWithTexture;
in vec3 fNormal;
in vec3 fLight;
in vec3 fView;
in vec4 fColor;
in float fZ;
in vec2 fUv;

layout(location = 0) out vec4 FragColor0;
layout(location = 1) out vec4 FragColor1;
layout(location = 2) out vec4 FragColor2;

vec3 applyDepthCueing(vec3 c3)
{
	if (DepthCueing)
	{
		float x = clamp((Near - fZ) / (Near - Far), 0.0, 1.0);
		return mix(c3, BgColor, x);
	}
	return c3;
}

vec4 shadeFragment()
{
	vec3 c3;
	float a;
	if (fWithTexture < 0.0)
	{
		vec3 inColor = fColor.rgb;
		// vec3 normal = normalize(fNormal);
		float normalLengthSquared = dot(fNormal, fNormal); // (260320Ch) Zero-normal line primitives should bypass surface lighting
		if (normalLengthSquared <= 1.0e-12)
		{
			c3 = inColor; // (260320Ch) Preserve the requested line color when no stable normal is available
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
		a = fColor.a;
	}
	else
	{
		vec4 c = texture(Texture, fUv); // (260319Ch) Keep compatibility with textured non-text geometry if any exists
		if (c.a <= 0.001)
			discard;
		c3 = c.rgb;
		a = c.a;
	}

	return vec4(applyDepthCueing(c3), a);
}

void clearOutputs()
{
	FragColor0 = vec4(0.0);
	FragColor1 = vec4(0.0);
	FragColor2 = vec4(0.0);
}

subroutine(RenderPassType)
void passDdpInit()
{
	clearOutputs();
	FragColor0 = vec4(-gl_FragCoord.z, gl_FragCoord.z, 0.0, 0.0); // (260319Ch) MAX blend over {-depth, depth} stores the current outer depth interval
}

subroutine(RenderPassType)
void passDdpPeel()
{
	clearOutputs();

	ivec2 texel = ivec2(gl_FragCoord.xy);
	vec2 depthRange = texelFetch(DepthBlenderTex, texel, 0).xy;
	float nearestDepth = -depthRange.x;
	float farthestDepth = depthRange.y;
	float fragDepth = gl_FragCoord.z;
	const float depthEpsilon = 1.0e-6;

	if (nearestDepth < 0.0 || farthestDepth < 0.0)
		discard;
	if (fragDepth < nearestDepth - depthEpsilon || fragDepth > farthestDepth + depthEpsilon)
		discard;

	bool isFrontLayer = abs(fragDepth - nearestDepth) <= depthEpsilon;
	bool isBackLayer = abs(fragDepth - farthestDepth) <= depthEpsilon;

	if (isFrontLayer)
	{
		vec4 frontColor = shadeFragment();
		FragColor1 = vec4(frontColor.rgb * frontColor.a, frontColor.a); // (260319Ch) Front accumulation uses premultiplied RGB for under blending
		return;
	}

	if (isBackLayer)
	{
		FragColor2 = shadeFragment(); // (260319Ch) Back accumulation keeps the regular color and uses over blending
		return;
	}

	FragColor0 = vec4(-fragDepth, fragDepth, 0.0, 0.0); // (260319Ch) Inner fragments define the next min/max interval
}

subroutine(RenderPassType)
void passDdpComposite()
{
	clearOutputs();

	ivec2 texel = ivec2(gl_FragCoord.xy);
	vec4 frontAccum = texelFetch(FrontBlenderTex, texel, 0);
	vec4 backAccum = texelFetch(BackBlenderTex, texel, 0);
	FragColor0 = vec4(frontAccum.rgb + frontAccum.a * backAccum.rgb, 1.0); // (260319Ch) Final color = peeled front + remaining transmittance * peeled back/background
}

void main()
{
	RenderPass();
}
