Shader "Custom/TilemapSharedTextureShader"
{
    Properties
    {
        [MainTexture] _MainTex("Mask Sprite (Diamond Alpha)", 2D) = "white" {}
        _TexArray("World Textures", 2DArray) = "" {}
        _WorldToUV("World->UV Scale", Float) = 0.125
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            TEXTURE2D_ARRAY(_TexArray);
            SAMPLER(sampler_TexArray);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _WorldToUV;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uvMask : TEXCOORD0;
                float4 color : COLOR;
                float3 worldPos : TEXCOORD1;
            };

            uint colorToIndex(float4 c)
            {
                uint r = (uint)floor(c.r * 255.0 + 0.5);
                uint g = (uint)floor(c.g * 255.0 + 0.5);
                uint b = (uint)floor(c.b * 255.0 + 0.5);
                uint a = (uint)floor(c.a * 255.0 + 0.5);
                return (r << 24) | (g << 16) | (b << 8) | a;
            }

            // uint colorToIndex(float4 c)
            // {
            //     uint r = (uint)floor(saturate(c.r) * 255.0 + 0.5);
            //     uint g = (uint)floor(saturate(c.g) * 255.0 + 0.5);
            //     uint b = (uint)floor(saturate(c.b) * 255.0 + 0.5);
            //     return (r << 16) | (g << 8) | b;
            // }

            // uint colorToIndex(float4 c)
            // {
            //     return (uint)floor(saturate(c.a) * 255.0 + 0.5);
            // }

            Varyings vert(Attributes v)
            {
                Varyings o;
                VertexPositionInputs p = GetVertexPositionInputs(v.positionOS.xyz);
                o.positionHCS = p.positionCS;
                o.worldPos = p.positionWS;
                o.uvMask = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                half maskA = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uvMask).a;
                clip(maskA - 0.001h);

                float layer = (float)colorToIndex(i.color);

                float2 uv = frac(i.worldPos.xy * _WorldToUV);

                half4 col = SAMPLE_TEXTURE2D_ARRAY(_TexArray, sampler_TexArray, uv, layer);
                // col.a = maskA;
                return col;
            }
            ENDHLSL
        }
    }
}