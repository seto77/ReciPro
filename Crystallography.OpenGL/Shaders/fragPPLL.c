#version 430 core

layout(early_fragment_tests) in;

#define MAX_FRAGMENTS ##

uniform uint MaxNodes;

struct NodeType {
    vec4 color;
    float depth;
    uint next;
};

layout(binding = 0, r32ui) uniform uimage2D headPointers;
layout(binding = 0, offset = 0) uniform atomic_uint nextNodeCounter;
layout(binding = 0, std430) buffer linkedLists {
    NodeType nodes[];
};

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

in float fWithTexture;
in vec3 fNormal;
in vec3 fLight;
in vec3 fView;
in vec4 fColor;
in float fZ;
in vec2 fUv;

out vec4 FragColor;

void sortFragments(inout NodeType frags[MAX_FRAGMENTS], int count)
{
    // Legacy inline sort in passPPLL2 is kept below for reference. (260319Ch)
    int i;
    int j;
    NodeType tmp;
    int inc = count / 2;
    while (inc > 0)
    {
        for (i = inc; i < count; i++)
        {
            tmp = frags[i];
            j = i;
            while (j >= inc && frags[j - inc].depth < tmp.depth)
            {
                frags[j] = frags[j - inc];
                j -= inc;
            }
            frags[j] = tmp;
        }
        inc = int(inc / 2.2 + 0.5);
    }
}

int gatherFragments(out NodeType frags[MAX_FRAGMENTS])
{
    uint n = imageLoad(headPointers, ivec2(gl_FragCoord.xy)).r;
    int count = 0;
    while (n != 0xffffffffu && count < MAX_FRAGMENTS)
    {
        frags[count] = nodes[n];
        n = frags[count].next;
        count++;
    }
    sortFragments(frags, count);
    return count;
}

vec4 setColor()
{
    vec3 c3;
    float a;
    if (fWithTexture < 0.0)
    {
        vec3 normal = normalize(fNormal);
        vec3 light = normalize(fLight);
        vec3 view = normalize(fView);
        vec3 inColor = fColor.rgb;
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
        a = fColor.a;
    }
    else
    {
        // vec4 c = texture2D(Texture, fUv);
        vec4 c = texture(Texture, fUv); // (260319Ch) GLSL 4.30 core uses texture()
        if (c.a <= 0.001)
            discard; // (260319Ch) transparent text texels must not enter the linked list
        c3 = c.rgb;
        a = c.a;
    }

    if (DepthCueing && fZ < Near)
    {
        if (fZ < Far)
            c3 = BgColor;
        else
            c3 = mix(c3, BgColor, (Near - fZ) / (Near - Far));
    }

    return vec4(c3, a);
}

subroutine(RenderPassType)
void passPPLL1()
{
    uint nodeIdx = atomicCounterIncrement(nextNodeCounter);
    if (nodeIdx < MaxNodes)
    {
        uint prevHead = imageAtomicExchange(headPointers, ivec2(gl_FragCoord.xy), nodeIdx);
        nodes[nodeIdx].color = setColor();
        nodes[nodeIdx].depth = gl_FragCoord.z;
        nodes[nodeIdx].next = prevHead;
    }
}

subroutine(RenderPassType)
void passPPLL2()
{
    NodeType frags[MAX_FRAGMENTS];
    int count = gatherFragments(frags); // (260319Ch) pass2 and label overlay share the same sorted linked-list gather

    vec4 color = vec4(BgColor, 1.0);
    int i;
    for (i = 0; i < count; i++)
        color = mix(color, frags[i].color, frags[i].color.a);

    color.a = 1.0;
    FragColor = color;
}

subroutine(RenderPassType)
void passLabelOverlay()
{
    vec4 labelColor = setColor(); // (260319Ch) Text glyph/outline color is taken from the existing texture path
    NodeType frags[MAX_FRAGMENTS];
    int count = gatherFragments(frags);

    vec4 color = vec4(BgColor, 1.0);
    float labelDepth = gl_FragCoord.z;
    bool labelInserted = false;
    const float depthEpsilon = 1.0e-6;

    for (int i = 0; i < count; i++)
    {
        if (!labelInserted && labelDepth > frags[i].depth + depthEpsilon)
        {
            color = mix(color, labelColor, labelColor.a); // (260319Ch) Insert the label into the back-to-front chain at its own depth
            labelInserted = true;
        }
        color = mix(color, frags[i].color, frags[i].color.a);
    }

    if (!labelInserted)
        color = mix(color, labelColor, labelColor.a); // (260319Ch) Label is the nearest fragment, so composite it last

    color.a = 1.0;
    FragColor = color; // (260319Ch) Replace the already-composited framebuffer pixel with the label-aware recomposition
}

subroutine(RenderPassType)
void passNormal()
{
    FragColor = setColor();
}

void main()
{
    RenderPass();
}
