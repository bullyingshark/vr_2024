Shader "Custom/FractalShader"
{
    Properties
    {
        _IterationCount("Iteration Count", Range(1, 100)) = 10
        _Scale("Scale", Range(0.1, 10.0)) = 1.0
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _HighlightColor("Highlight Color", Color) = (0,0,1,1)
    }
        SubShader
    {
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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float _Scale;
            float _IterationCount;
            fixed4 _BaseColor;
            fixed4 _HighlightColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy * _Scale; // Масштабирование
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 p = float3(i.uv, 0.5);
                int count = 0;

                for (int iter = 0; iter < _IterationCount; iter++)
                {
                    p = float3(p.x * p.x - p.y * p.y, 2 * p.x * p.y, 0) + float3(-0.7, 0.27015, 0);
                    if (length(p) > 2.0) break;
                    count++;
                }

                float t = (float)count / _IterationCount;
                return lerp(_BaseColor, _HighlightColor, t);
            }
            ENDCG
        }
    }
}
