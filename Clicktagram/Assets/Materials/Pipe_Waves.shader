Shader "Muhammed/MC_Waves_Vertical"
{
	Properties
	{
		_MainColor("Main Color",Color) = (1,1,1,1)
		_MainTex("Main Texture",2D) = "white" {}
		_DisTex("Displacement Tex",2D) ="white" {}
		_Freq("Frequency",Range(0,10)) = 0
		_Speed("Speed",Range(0,100)) = 0
		_Amp("Amplitude",Range(0,2)) = 0
	}

		SubShader
	{
		CGPROGRAM
		#pragma surface surf NoLighting vertex:vert
		#pragma target 3.0

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
		  fixed4 c;
		 c.rgb = s.Albedo;
		 c.a = s.Alpha;
		 return c;
	 }

		float4 _MainColor;
		sampler2D _MainTex;
		sampler2D _DisTex;
		half _Freq;
		half _Speed;
		half _Amp;
		half _WhfVa;

		struct Input {
			float2 uv_MainTex;
			float3 vertColor;
		};

		struct appdata {
			float4 vertex: POSITION;
			float3 normal: NORMAL;
			float4 texcoord: TEXCOORD0;
			float4 texcoord1: TEXCOORD1;
			float4 texcoord2: TEXCOORD2;
		};

		void vert(inout appdata v) {
			
			//Time multiply by the Speed variable.
			float t = _Time * _Speed;

			//Displacement Texture is turned into float4 variable and the UV variable is multiplied by Frequency variable, then added with Time variable.
			float4 mainv = tex2Dlod(_DisTex,float4((v.texcoord * _Freq) + float4(t,0,0,0)));
	
			//Displacement Texture, Normal values of vertices and Amplitude variables are multiplied and new variable are created.
			float3 mr = mainv.r * v.normal.xyz * _Amp;
			
			//Displacement of vertices
			v.vertex.xyz += mr;

			//Arrangement of New Normals
			v.normal = normalize(float3(v.normal.x , (v.normal.y), v.normal.z + mr.z));
		}

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 main = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = _MainColor.rgb * main.rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
}