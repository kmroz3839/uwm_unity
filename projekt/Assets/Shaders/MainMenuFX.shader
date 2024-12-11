Shader "Unlit/MainMenuFX" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorShade ("Shade", float) = 0.5
    }
    
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
    
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
    
        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0
                #pragma multi_compile_fog
    
                #include "UnityCG.cginc"
                #include "common/sh_sayaka.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };
    
                struct v2f {
                    float4 vertex : SV_POSITION;
                    float2 texcoord : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    UNITY_VERTEX_OUTPUT_STEREO
                };
    
                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _ColorShade;
    
                v2f vert (appdata_t v)
                {
                    v2f o;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }
    
                fixed4 frag (v2f i) : SV_Target
                {
                    //fixed4 col = tex2D(_MainTex, i.texcoord);

                    float2 tile = float2(sayaka_indexsplit(i.texcoord.x, 0.2), sayaka_indexsplit(i.texcoord.y, 0.2));
                    int index = sayaka_indexsplit(_Time, 0.15) % 4;
                    return sayaka_gradient_radial(float4(0.0,0.0,0.0,0.0), float4(_ColorShade, _ColorShade, _ColorShade, 1.0), tile, 
                    index == 0 ? float2(0,0)
                    : index == 1 ? float2(5,0)
                    : index == 2 ? float2(0,5)
                    : float2(5,5)
                    , (_Time % 0.15) * 100);
                    //col.a = 0.2;
                    //return col;
                }
            ENDCG
        }
    }
    
    }
    