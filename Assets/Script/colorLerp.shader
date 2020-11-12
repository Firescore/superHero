Shader "Unlit/colorLerp"
{
	Properties
	{
		_Blend("Blend", Range(0, 1)) = 0.5
		_BlendSpeed("Blend Speed", Range(0, 50)) = 0.5
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_Color2("Flash Color", Color) = (1, 1, 1, 1)
		_MainTex("Texture 1", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 300
		Pass
	{
		SetTexture[_MainTex]

	}

		CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _MainTex;

	fixed4 _Color;
	fixed4 _Color2;
	float _Blend;
	float _BlendSpeed;

	int forward = 1;

	struct Input
	{
		float2 uv_MainTex;
		float2 uv_Texture2;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{

		if (forward == 1) {
			_Blend += unity_DeltaTime.x / _BlendSpeed;
			if (_Blend >= 1)
				forward = 0;
		}
		else {
			_Blend -= unity_DeltaTime.x / _BlendSpeed;
			if (_Blend <= 0)
				forward = 1;
		}

		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = (lerp(_Color, _Color2, _Blend)); // lerp(t1, t2, _Blend);
	}
	ENDCG
	}
		FallBack "Diffuse"
}
