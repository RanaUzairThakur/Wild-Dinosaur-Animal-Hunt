Shader "Tatakai/explosion_burn" {
Properties {
	_flameValue ("Flame Scale", Float) = 1.0
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_FireColor1 ("Fire Color1", Color) = (0.5,0.5,0.5,0.5)
	_FireColor2 ("Fire Color2", Color) = (0.5,0.5,0.5,0.5)
	_SmokeColor ("Smoke Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "" {}
	_SmokeTex ("Smoke Texture", 2D) = "" {}
	_FogColor ("Fog Color", Color) = (0.5, 0.5, 0.5, 1)

}

Category {
	Tags { "IgnoreProjector"="True" "Queue"= "Transparent+11"}
	Blend SrcAlpha OneMinusSrcAlpha
	//Blend One One 
	AlphaTest Greater .01
	Fog {Mode Off}
	//ColorMask RGB
	Cull Back Lighting Off ZWrite On


	
	
	SubShader {
	
		Tags {"IgnoreProjector" = "True" "Queue" = "Transparent+11"}
		Cull Off
		
		CGPROGRAM
		//#pragma target 3.0
		#include "UnityCG.cginc"
		#pragma surface surf BlinnPhong alpha vertex:vert

			
		sampler2D _MainTex;
		sampler2D _SmokeTex;
		float4 _TintColor;
		float4 _FireColor1;
		float4 _FireColor2;
		float4 _SmokeColor;
      	float4 color : COLOR;
      	float _flameValue;

			
		struct Input {
			float2 uv_MainTex;
			float4 vertexColor;
		};
        
        
		void vert (inout appdata_full v, out Input o) {
			o.uv_MainTex = v.texcoord;
			o.vertexColor.rgb = v.color.rgb;
			o.vertexColor.a = v.color.a;
			v.normal = normalize (v.vertex);
			v.tangent = float4 (0, 0, 0, 0);
		}
		


		void surf (Input IN, inout SurfaceOutput o) {
    
	        half4 tex = tex2D(_MainTex, IN.uv_MainTex);
	        half4 smoke = tex2D(_SmokeTex, float2(IN.uv_MainTex.x*0.33,IN.uv_MainTex.y*0.33));

			half4 fireCol = (lerp(_FireColor1*1.5,_FireColor2*5.0,(tex.r * IN.vertexColor.r))*(tex.r * IN.vertexColor.r));//(_FireColor * (tex.r * _TintColor.r));	
			
			o.Albedo = smoke.r * _SmokeColor.rgb * (IN.vertexColor.a);
			o.Alpha = tex.a * IN.vertexColor.a;

			if (o.Alpha > 1.0){
				o.Alpha = 1.0;
			}

			o.Emission = fireCol;
			o.Emission = 0.0;
			if (_flameValue > fireCol.r){ 
				o.Emission = fireCol*_flameValue;
			}

			
			}
		
		ENDCG 

} 	
	
	
}
}
