Shader "JovDK/Custom/Alpha Stencil Mask"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _AlphaTex("External Alpha", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            sampler2D _AlphaTex;
            float4 _AlphaTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD0;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv, _AlphaTex);
                o.color = v.color;
                return o;
            }

            float4 frag (v2f i) : COLOR
            {
                fixed4 top = tex2D(_MainTex, i.uv) * i.color;
                float4 alphaTexColor = tex2D(_AlphaTex, i.uv);

                return fixed4(top.rgb, top.a * alphaTexColor.a);
            }

            ENDCG
        }
    }
}
