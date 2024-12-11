Shader "Custom/WallShader2"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #include "common/sh_sayaka.cginc"

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            float2 uvPosition = sayaka_tileuv(IN.uv_MainTex.xy, float2(6.0,3.0));
            int2 split = int2(sayaka_indexsplit(uvPosition.x, 0.5), sayaka_indexsplit(uvPosition.y, 0.5));
            
            if (split.x == 1) {
                o.Metallic *= 3.5/5.0;
                o.Smoothness *= 6/7.0;
                
            } else {
                if (split.y == 1) {
                    o.Metallic *= 4.0/5.0;
                }
                o.Smoothness = o.Smoothness *= 4/6.0;;//lerp(0, o.Smoothness, min(0.2, uvPosition.x));
            }
            /*if (split.x == 1 ) {
                //o.Metallic *= 4.5/5.0;
                
                //o.Smoothness *= 3/7.0;
            } 
            else */

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
