// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorShader"
{
    Properties
    {

    }
    SubShader
    {
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			struct VertexIn {
				float4 pos : POSITION;
			};

			struct VertexOut {
				float4 pos : SV_POSITION;
				half3 color : COLOR;
			};

			VertexOut vert(VertexIn i) {
				VertexOut o;
				o.pos = UnityObjectToClipPos(i.pos);
				o.color = i.pos.xyz * 109.0f;

				return o;
			}

			half4 frag(VertexOut i) : COLOR{
				return half4(i.color, 0.0f);
			}
			ENDCG

		}
    }
    FallBack "Diffuse"
}
