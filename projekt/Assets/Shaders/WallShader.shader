Shader "Custom/WallShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _AltMetallic ("Alt Metallic", Range(0,1)) = 0.6
        _AltSmoothness ("Alt Smoothness", Range(0,1)) = 0.6

        _Split("Split at", Range(0,1)) = 0.5
        _TileX("Tiling X", Range(0,10)) = 1.0
        _TileY("Tiling Y", Range(0,10)) = 1.0
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

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        half _AltMetallic;
        half _AltSmoothness;
        fixed4 _Color;
        float _Split;
        float _TileX;
        float _TileY;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            int rx = abs(floor(IN.uv_MainTex.x / _TileX));
            int ry = abs(floor(IN.uv_MainTex.y / _TileY));
            int repeats = rx + ry;
            float2 uvPos = float2(
                    (IN.uv_MainTex.x - floor(IN.uv_MainTex.x / _TileX) * _TileX) / _TileX,
                    (IN.uv_MainTex.y - floor(IN.uv_MainTex.y / _TileY) * _TileY) / _TileY
                );
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //checkerboard
            //if ((repeats % 2 == 1) ^ (uvPos.y > _Split)) {
            if ((rx % 4) == (ry % 4)) {
                o.Albedo = c.rgb;
                o.Metallic = _AltMetallic;
                o.Smoothness = _AltSmoothness;
                o.Alpha = c.a;
            } else {

                // Albedo comes from a texture tinted by color
                
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
