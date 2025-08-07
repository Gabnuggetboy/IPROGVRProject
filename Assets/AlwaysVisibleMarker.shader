Shader "Unlit/AlwaysVisibleMarker"
{
    Properties
    {
        _Color ("Main Color", Color) = (0, 1, 0, 0.6)        // Green with alpha
        _EmissionColor ("Emission Color", Color) = (0, 1, 0, 1)
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        LOD 100

        ZTest Always
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            fixed4 _EmissionColor;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Apply both color and emission
                return _Color + _EmissionColor;
            }
            ENDCG
        }
    }
}
