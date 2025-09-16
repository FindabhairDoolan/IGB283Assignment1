Shader "Custom/Vertex Colour"
{
    Properties
    {
        _MainColour("Colour", Color) = (1, 1, 1, 1)
        _Metallic("Metallic", Range(0, 1)) = 0
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            #pragma vertex vert
            #pragma fragment frag

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                half3 vertexColour : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : NORMAL;
                float3 positionWS : TEXCOORD1;
                half3 vertexColour : COLOR;
            };

            CBUFFER_START(UnityPerMaterial)
            half4 _MainColour;
            float _Metallic;
            float _Smoothness;
            CBUFFER_END

            // Start Unity code
            half3 GammaToLinearSpace (half3 sRGB)
            {
                // Approximate version from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
                return sRGB * (sRGB * (sRGB * 0.305306011h + 0.682171111h) + 0.012522878h);

                // Precise version, useful for debugging.
                //return half3(GammaToLinearSpaceExact(sRGB.r), GammaToLinearSpaceExact(sRGB.g), GammaToLinearSpaceExact(sRGB.b));
            }
            // End Unity code

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.positionWS = TransformObjectToWorld(IN.positionOS);
                OUT.vertexColour = GammaToLinearSpace(IN.vertexColour);
                return OUT;
            }

            half3 AmbientLighting()
            {
                return half3(unity_SHAr.w, unity_SHAg.w, unity_SHAb.w);
            }

            half4 frag(Varyings IN) : SV_TARGET
            {
                // Get the light attributes
                Light mainLight = GetMainLight();
                float3 lightDirection = mainLight.direction;
                half3 lightColour = mainLight.color;

                // Normalize vectors
                float3 normal = normalize(IN.normalWS);
                float3 viewDirectionWS = normalize(_WorldSpaceCameraPos.xyz - IN.positionWS);
                lightDirection = normalize(lightDirection);

                // Calculate lambert lighting
                half3 diffuse = saturate(dot(normal, lightDirection));

                // Calculate shininess
                half smoothness = exp2(10 * _Smoothness + 1);
                half3 halfDirection = SafeNormalize(lightDirection + viewDirectionWS);
                float specular = pow(saturate(dot(normal, halfDirection)), smoothness) * _Smoothness;
                
                // Add a rim light based on the shininess
                float fresnelStrength = 1 - saturate(dot(normal, viewDirectionWS));
                fresnelStrength = pow(fresnelStrength, (_Smoothness + 0.01) * 3) * _Smoothness;

                // Apply the lighting to the colour
                half3 baseColour = _MainColour.rgb * IN.vertexColour;
                half3 colour = baseColour * diffuse;
                colour += AmbientLighting() * baseColour;
                colour *= (1 - _Metallic * 0.85);
                colour += (specular + fresnelStrength * AmbientLighting()) * lerp(lightColour, baseColour, _Metallic);

                return half4(colour, 1);
            }


            ENDHLSL
        }
    }
}
