Shader "Custom/Shader"
{
    Properties
    {
        _ColorTop("Top Color", Color) = (1,1,1,1)
        _ColorBottom("Bottom Color", Color) = (0,0,0,1)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
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
                float height : TEXCOORD0;
            };

            fixed4 _ColorTop;
            fixed4 _ColorBottom;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.height = v.vertex.y;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Ћинейна€ интерпол€ци€ между цветами в зависимости от высоты
                return lerp(_ColorBottom, _ColorTop, i.height);
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}
