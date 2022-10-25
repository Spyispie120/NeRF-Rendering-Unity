Shader "Custom/VolumeRenderingColored"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_Volume("Volume", 3D) = "" {}
		_Intensity("Intensity", Range(1.0, 5.0)) = 1.2
		_Threshold("Threshold", Range(0.0, 1.0)) = 0.95
		_SliceMin("Slice min", Vector) = (0.0, 0.0, 0.0, -1.0)
		_SliceMax("Slice max", Vector) = (1.0, 1.0, 1.0, -1.0)
		_PointerPosition("Hand Pointer Pos", Vector) = (0.2, 0.2, 0.0, -1.0)
		_PointerIntensity("Pointer Light Intensity", Range(1.0, 5.0)) = 5.0
		_PlaneScanPara("Scanning Normal", Vector) = (0.2, 0.2, 0.2, 0.2) // a, b, c, d to represent a 3d plane
		_ThicknessPlane("Plane Thickness", Range(0.0, 0.2)) = 0.05
	}
		CGINCLUDE

			ENDCG

			SubShader{
				Cull Back 
				ZWrite Off 
				ZTest Always
				Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM

		  #define ITERATIONS 200
				#include "./VRcolored.cginc"
				#pragma vertex vert
				#pragma fragment frag

				ENDCG
			}
		}
    FallBack "Diffuse"
}

