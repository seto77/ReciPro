#version 420 core

layout(triangles) in;
layout(triangle_strip, max_vertices = 6) out;

uniform int counter = 0;

in VertexData
{
	vec3 Normal;//Normal direction
	vec3 Light;//Light direction
	vec3 View;//View direction
	vec3 Color;//Color
} gs_in[];

out VertexData
{
	vec3 Normal;//Normal direction
	vec3 Light;//Light direction
	vec3 View;//View direction
	vec3 Color;//Color
} gs_out;

void main(void)
{
	int i;
	for (i = 0; i < gs_in.length(); i++)
	{
		gl_Position = gl_in[i].gl_Position;
		gs_out.Normal = gs_in[i].Normal;
		gs_out.Light = gs_in[i].Light;
		gs_out.View = gs_in[i].View;
		gs_out.Color = gs_in[i].Color;
		EmitVertex();
	}
	EndPrimitive();
}

//Sellers, Graham.OpenGL Superbible : Comprehensive Tutorial and Reference(Kindle の位置No.1939 - 1951).Pearson Education.Kindle 版.