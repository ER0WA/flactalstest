Shader "Unlit/FractalTest1"
{
	SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend OneMinusDstColor One //OneMinusDstColor

		Pass{
		CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma target 3.0

#include "UnityCG.cginc"


	float complexity = 29.0;
	float test = 1.0;
	float blueMode = -1.0;

	


	float4 frag(v2f_img i) : COLOR{
	float2 mcoord;
	float2 coord = float2(0.0,0.0);
	mcoord.x = ((1.0 - i.uv.x)*3.5) - 2.5;
	mcoord.y = (i.uv.y*2.0) - 1.0;
	float iteration = 0.0;
	float _MaxIter = 29.0;
	const float PI = 3.14159;
	float xtemp;
	float4 color;

	

	for (iteration = 0.0; iteration < _MaxIter; iteration += 1.0) {
		if (coord.x*coord.x + coord.y*coord.y > 2.0*(cos(fmod(complexity, 2.0*PI)) + 1.0))
			break;
		xtemp = coord.x*coord.x - coord.y*coord.y + mcoord.x;
		coord.y = 2.0*coord.x*coord.y + mcoord.y;
		coord.x = xtemp;
	}
	//float val = fmod((iteration / _MaxIter) + _Time.x,1.0);
	float val = fmod((iteration / _MaxIter) + complexity, 1.0);
	
	color.a = 1.0;

	if (blueMode < 0.0)
	{

		color.b = clamp(complexity / PI + 0.2, 0.1, 1.0);
		color.r = clamp((abs(fmod(val, 1.0) - 0.5)), 0.2, 0.6);
		color.g = clamp((abs(fmod(val, 1.0) - 0.5)), 0.1, 0.2);

		if (abs(fmod(iteration, 2.0)) < 1.0)
		{
			color.b = 0.0;
			color.r = 0.0;
			color.g = 0.0;
		}
	}
	else
	{


		color.r = clamp((3.0*abs(fmod(2.0*val, 1.0) - 0.5)), 0.0, 1.0);
		color.g = clamp((3.0*abs(fmod(2.0*val + (1.0 / 3.0), 1.0) - 0.5)), 0.0, 1.0);
		color.b = clamp((3.0*abs(fmod(2.0*val - (1.0 / 3.0), 1.0) - 0.5)), 0.0, 1.0);
	}
	
	
	if (iteration < (_MaxIter / 7.0))
	{
		color.r = 0.0;
		color.g = 0.0;
		color.b = 0.0;
		color.a = 0.0;
	}

	return color;
	}
		ENDCG
	}
	}
}
