Shader "Skybox/SkyBoxShader" {
	Properties {
		//主颜色  
		_MainColor ("Color", Color) = (1,1,1,1)
		//主纹理  
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//光泽度  
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//金属度  
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	//------------------------------------【唯一的子着色器】------------------------------------  
	SubShader {
		//渲染类型设置：不透明  
		Tags { "RenderType"="Fade" }
		//细节层次设为：200  
		LOD 200
		//===========开启CG着色器语言编写模块===========  
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		//编译指令: 指定着色器编译目标为Shader Model 3.0  
		#pragma target 3.0

		//变量的声明  
		sampler2D _MainTex;

		//表面输入结构体  
		struct Input {
			float2 uv_MainTex;//纹理坐标  
			float4 color : Color;
		};

		//变量的声明 
		half _Glossiness;
		half _Metallic;
		fixed4 _MainColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//【1】漫反射颜色为主纹理对应的纹理坐标，并乘以主颜色 
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _MainColor;
			//【2】将准备好的颜色的rgb分量作为漫反射颜色
			//o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//【3】金属度取自属性值
			o.Metallic = _Metallic;
			//【4】光泽度也取自属性值
			o.Smoothness = _Glossiness;
			//【5】将准备好的颜色的alpha分量作为Alpha分量值  
			o.Alpha = c.a;
		}
		//===========结束CG着色器语言编写模块=========== 
		ENDCG
	}
	//备胎为漫反射  
	FallBack "Diffuse"
}
