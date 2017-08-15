Shader "Heatmap/HeatMap_Linked"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color1("Color1", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
	}
	SubShader
	{

		Tags 
		{
			"Queue" = "Transparent"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			//Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _Color1;
			float4 _Color2;

			float4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//col = 1 - col;
				//return col;

				//float4 col1 = tex2D(_MainTex, i.uv*5) * _Color1;
				//float4 col2 = tex2D(_MainTex, i.uv*3) * _Color2;
				//float4 col= lerp(col1, col2, _Slider);

				float4 lerpCol = lerp(_Color1, _Color2, i.uv.y);
				float4 col = tex2D(_MainTex, i.uv * 5) * lerpCol;
				return col*col.a;

				//return col*col.a; //for blend one one option

				//return float4(i.uv.r, i.uv.g, 1, 1.0);
			}
			ENDCG
		}
	}
}
