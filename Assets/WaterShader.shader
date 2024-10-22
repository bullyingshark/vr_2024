Shader "Custom/WaterShader"
{
    Properties
    {
        _Color("Water Color", Color) = (0, 0.5, 1, 0.5)
        _Transparency("Transparency", Range(0, 1)) = 0.5
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float4 _Color;
            float _Transparency;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(_Color.rgb, _Transparency);
            }
            ENDCG
        }
    }
        FallBack "Transparent/Diffuse"
}
