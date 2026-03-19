#version 410 core // (260319Ch) Post-process FXAA for OIT paths on OpenGL 4.1

uniform sampler2D SceneTexture;
uniform vec2 ViewportSize;

layout(location = 0) out vec4 FragColor;

float luma(vec3 rgb)
{
    return dot(rgb, vec3(0.299, 0.587, 0.114));
}

void main()
{
    if (ViewportSize.x <= 1.0 || ViewportSize.y <= 1.0)
    {
        FragColor = texture(SceneTexture, vec2(0.5, 0.5)); // (260319Ch) Degenerate viewport fallback
        return;
    }

    vec2 inverseViewport = 1.0 / ViewportSize;
    vec2 uv = gl_FragCoord.xy * inverseViewport;

    vec3 rgbNW = texture(SceneTexture, uv + vec2(-1.0, -1.0) * inverseViewport).rgb;
    vec3 rgbNE = texture(SceneTexture, uv + vec2(1.0, -1.0) * inverseViewport).rgb;
    vec3 rgbSW = texture(SceneTexture, uv + vec2(-1.0, 1.0) * inverseViewport).rgb;
    vec3 rgbSE = texture(SceneTexture, uv + vec2(1.0, 1.0) * inverseViewport).rgb;
    vec3 rgbM = texture(SceneTexture, uv).rgb;

    float lumaNW = luma(rgbNW);
    float lumaNE = luma(rgbNE);
    float lumaSW = luma(rgbSW);
    float lumaSE = luma(rgbSE);
    float lumaM = luma(rgbM);

    float lumaMin = min(lumaM, min(min(lumaNW, lumaNE), min(lumaSW, lumaSE)));
    float lumaMax = max(lumaM, max(max(lumaNW, lumaNE), max(lumaSW, lumaSE)));

    vec2 direction;
    direction.x = -((lumaNW + lumaNE) - (lumaSW + lumaSE));
    direction.y = (lumaNW + lumaSW) - (lumaNE + lumaSE);

    float directionReduce = max(
        (lumaNW + lumaNE + lumaSW + lumaSE) * (0.25 * 0.125),
        1.0 / 128.0);
    float inverseDirectionAdjustment = 1.0 / (min(abs(direction.x), abs(direction.y)) + directionReduce);
    direction = clamp(direction * inverseDirectionAdjustment, vec2(-8.0), vec2(8.0)) * inverseViewport;

    vec3 rgbA = 0.5 * (
        texture(SceneTexture, uv + direction * (1.0 / 3.0 - 0.5)).rgb +
        texture(SceneTexture, uv + direction * (2.0 / 3.0 - 0.5)).rgb);

    vec3 rgbB = rgbA * 0.5 + 0.25 * (
        texture(SceneTexture, uv + direction * -0.5).rgb +
        texture(SceneTexture, uv + direction * 0.5).rgb);

    float lumaB = luma(rgbB);
    vec3 finalRgb = (lumaB < lumaMin || lumaB > lumaMax) ? rgbA : rgbB;
    FragColor = vec4(finalRgb, 1.0);
}
