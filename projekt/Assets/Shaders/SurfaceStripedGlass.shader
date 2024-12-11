Shader "Custom/SurfaceStripedGlass"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _RefractionSteps("Refraction steps", Range(1, 50)) = 2
        _RefractionLength("Refraction length", Range(0.0001, 0.5)) = 0.001

        _EdgeEmission("Edge emission", Range(0,1)) = 0.5

        _TileX("Tiling X", Range(0,10)) = 1.0
        _TileY("Tiling Y", Range(0,10)) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        GrabPass {}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            UNITY_VPOS_TYPE screenPos : VPOS;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        int _RefractionSteps;
        float _RefractionLength;
        float _EdgeEmission;

        sampler2D _GrabTexture;
		float4 _GrabTexture_TexelSize;
        float _TileX, _TileY;

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

            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            float2 p = float2(IN.screenPos.x / IN.screenPos.w, (IN.screenPos.y / IN.screenPos.w) );
            float3 grab = tex2D(_GrabTexture, p.xy);
            float3 sum = grab;
            float refrLength =_RefractionLength;
            if (repeats % 2 == 1) {
                o.Smoothness *= 6/7.0f;
                refrLength *= 2.5;
            }
            for (int i = 1; i < _RefractionSteps+1; i++) {
                sum += tex2D(_GrabTexture, p.xy + float2(-refrLength * i, refrLength*i/2));
                sum += tex2D(_GrabTexture, p.xy + float2(refrLength * i, -refrLength*i/2));
            }
            o.Emission = _EdgeEmission * max(pow(abs((0.5 - IN.uv_MainTex.x) / 0.5), 8), pow(abs((0.5 - IN.uv_MainTex.y) / 0.5), 8));
            o.Albedo = sum / (_RefractionSteps*2 + 1);
            o.Alpha = 0.8;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
