Shader "Custom/AlleyDither" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _PosterizeLevels ("Levels", Range(2, 32)) = 4
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}
 
        CGPROGRAM
        #pragma surface surf Standard
 
        sampler2D _MainTex;
        float _PosterizeLevels;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutputStandard o) {
            float4 c = tex2D(_MainTex, IN.uv_MainTex);
            c.rgb = round(c.rgb * (_PosterizeLevels-1.0)) / (_PosterizeLevels-1.0);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
 
    FallBack "Diffuse"
}
