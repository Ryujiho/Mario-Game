Shader "Custom/water_shader"
{
  	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Bumpmap("NormapMap", 2D) = "white" {}
		_Color("S Color", color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		
		CGPROGRAM
		#pragma surface surf WaterSpecular alpha:fade vertex:vert

		sampler2D _Bumpmap;
		float4 _Color;
		
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_Bumpmap;
			float3 worldRefl;
			float3 viewDir;
			INTERNAL_DATA
		};
      sampler2D _MainTex;
		void surf(Input IN, inout SurfaceOutput o)
		{
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb ;
			o.Normal = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap + _Time.x*0.2));
			
			// rim Á¤¹Ý»ç
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = pow(1 - rim, 3);
			o.Emission = _Color * rim * 2;
			o.Alpha = 0.8;
		}	

		float4 LightingWaterSpecular(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			float3 H = normalize(lightDir + viewDir);//phong
			float spec = saturate(dot(H, s.Normal));
			spec = pow(spec, 300);

			float4 finalColor;
			finalColor.rgb = spec * _Color.rgb*300;
			finalColor.a = s.Alpha + spec;
			return finalColor;
		}

		void vert(inout appdata_full v)
		{
			float movement;
			movement = sin(abs((v.texcoord.x)*50 )+_Time.y)*0.3;
			v.vertex.y += movement;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}