Shader"Universal Render Pipeline/ForceShield"
{
    Properties
    {
        // Base Color
        _Color("Color", Color) = (0,0,0,0)
        _Opacity("Opacity", Range(0, 1)) = 0.5

        // Shield Pattern
        _ShieldPatternColor("Additional Color", Color) = (0.4941176470588235,0.8274509803921569,0.6980392156862745,1)
        _ShieldPatternPower("Additional Color Power", Range(0, 100)) = 5
        _ShieldRimPower("Shield Rim Power", Range(0, 10)) = 7

        // Hit Effect
        _HitPosition("Hit Position", Vector) = (0,0,0,0)
        _HitTime("Hit Time", Float) = 0
        _HitColor("Hit Color", Color) = (1,1,1,1)
        _HitSize("Hit Size", Float) = 0.2

        // Edge Length
        _EdgeLength("Edge length", Range(2, 100)) = 15.0

        // Displacement
        _Displacement("Hit Wave", Range(0, 1)) = 0.03
    }

    SubShader
    {
        Tags{ "RenderType"="Transparent" "Queue"="Transparent" }
Blend SrcAlpha
OneMinusSrcAlpha
        CullOff
        Pass
        {
            Tags{ "LightMode" = "ForwardBase" }
            
            HLSLINCLUDE
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Core.hlsl"

struct Attributes
{
    float2 uv_texcoord;
    float3 worldPos;
    float3 worldNormal;
};

struct Varyings
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 worldPos : TEXCOORD1;
    float3 worldNormal : TEXCOORD2;
};

Varyings vert(Attributes input)
{
    Varyings output;
    output.pos = UnityObjectToClipPos(input.worldPos);
    output.uv = input.uv_texcoord;
    output.worldPos = input.worldPos;
    output.worldNormal = UnityObjectToWorldNormal(input.worldNormal);
    return output;
}

half4 frag(Varyings input, UNITY_VPOS_TYPE vpos)
{
    half4 color = _Color;
                // Perform any additional calculations here
                
    return color;
}
            ENDHLSL
        }
    }
}