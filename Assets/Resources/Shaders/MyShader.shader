// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MyShader" {
    // Adapted from tutorials on Unity website
    Properties
    {
        _AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
        //_AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0

        _DiffuseDirection("Diffuse Light Direction", Vector) = (0.1,0.1,0.1,1)
        _DiffuseColor("Diffuse Light Color", Color) = (0,0,0,1)
        _DiffuseIntensity("Diffuse Light Intensity", Range(0.0, 1.0)) = 1.0

        _SpecularPosition("Specular Light Position", Vector) = (0.1,0.1,0.1,1)
        _SpecularColor("Specular Light Color", Color) = (1,1,1,1)
        _SpecularIntensity("Specular Light Intensity", Range(0.0, 1.0)) = 1.0
        _SpecularShininess("Shininess", Float) = 10

        _MainTex("Base (RGB)", 2D) = "white" {}
    }

    SubShader
    {

        Pass
        {
            Tags{ "LightMode" = "ForwardBase" "RenderType"="Opaque"  }
			LOD 100

            CGPROGRAM
            #pragma target 2.0
            #pragma vertex vertexShader
            #pragma fragment fragmentShader
            #pragma multi_compile_fwdbase
			#pragma multi_compile_fog
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            float4 _AmbientLightColor;
            float _AmbientLighIntensity;
            float3 _DiffuseDirection;
            float4 _DiffuseColor;
            float _DiffuseIntensity;
            float4 _SpecularPosition;
            float4 _SpecularColor;
            float _SpecularIntensity;
            float _SpecularShininess;
            float4 _MainTex_ST;

            struct vsIn {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            struct vsOut {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD2;
                float2 uv : TEXCOORD3;
                SHADOW_COORDS(1) // put shadows data into TEXCOORD1
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
				UNITY_FOG_COORDS(4)
            };

            vsOut vertexShader(appdata_base v)
            {
                vsOut o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                //o.uv = v.texcoord;

                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                float4x4 modelMatrix = unity_ObjectToWorld;
                float4x4 modelMatrixInverse = unity_WorldToObject;
                o.posWorld = mul(modelMatrix, v.vertex);
                o.normalDir = normalize(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal, 1));
                TRANSFER_SHADOW(o);
				UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }

            sampler2D _MainTex;

            float4 fragmentShader(vsOut psIn) : SV_Target
            {
                float4 diffuse = saturate(dot(_DiffuseDirection, psIn.normal));

                float3 specularReflection;

                //float4x4 modelMatrixInverse = unity_WorldToObject;
                //float3 normalDir = normalize(mul(float4(psIn.normal, 0.0), modelMatrixInverse).xyz);
                float3 normalDirection = normalize(psIn.normalDir);

                float3 viewDirection = normalize(_WorldSpaceCameraPos - psIn.posWorld.xyz);
                float3 lightDirection;
                float attenuation;

        

                if (0.0 == _SpecularPosition.w) // directional light?
                {
                    attenuation = 1.0; // no attenuation
                    lightDirection = normalize(_SpecularPosition.xyz);
                }
                else // point or spot light
                {
                    float3 vertexToLightSource =
                        _SpecularPosition.xyz - psIn.posWorld.xyz;
                    float distance = length(vertexToLightSource);
                    attenuation = 1.0 / distance; // linear attenuation 
                    lightDirection = normalize(vertexToLightSource);
                }

                if (dot(normalDirection, lightDirection) < 0.0)
                    // light source on the wrong side?
                {
                    specularReflection = float3(0.0, 0.0, 0.0);
                    // no specular reflection
                }
                else // light source on the right side
                {
                    specularReflection = attenuation * _SpecularColor.rgb * pow(max(0.0, dot(
                        reflect(-lightDirection, normalDirection),
                        viewDirection)), _SpecularShininess);
                }

                fixed shadow = SHADOW_ATTENUATION(psIn);

                fixed3 lighting = psIn.diff * shadow + psIn.ambient;

                float4 phongCol = saturate((_AmbientLightColor * _AmbientLighIntensity)
                    + (diffuse * _DiffuseColor * _DiffuseIntensity) + (_SpecularIntensity * float4(specularReflection, 1)));

                float4 col = tex2D(_MainTex, psIn.uv);

                col.rgb *= phongCol;
                col.rgb *= lighting;

				UNITY_APPLY_FOG(psIn.fogCoord, col);
				UNITY_OPAQUE_ALPHA(col.a);

                return col;
            }

            ENDCG
        }

        // shadow caster rendering pass, implemented manually
        // using macros from UnityCG.cginc
        Pass
        {
            Tags{ "LightMode" = "ShadowCaster" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f {
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}